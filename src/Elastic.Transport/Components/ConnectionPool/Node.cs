// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Transport.Extensions;

namespace Elastic.Transport
{
	public class Node : IEquatable<Node>
	{
		private IReadOnlyCollection<string> _features;

		public Node(Uri uri, IEnumerable<string> features = null)
		{
			// This make sures that a node can be rooted at a path to. Without the trailing slash Uri's will remove `instance` from
			// http://my-saas-provider.com/instance
			// Where this might be the user specific path
			if (!uri.OriginalString.EndsWith("/", StringComparison.Ordinal))
				uri = new Uri(uri.OriginalString + "/");
			Uri = uri;
			IsAlive = true;
			if (features is IReadOnlyCollection<string> s)
				Features = s;
			else
				Features = features?.ToList().AsReadOnly() ?? EmptyReadOnly<string>.Collection;
			IsResurrected = true;
		}

		private HashSet<string> _featureSet;
		public IReadOnlyCollection<string> Features
		{
			get => _features;
			set
			{
				_features = value;
				_featureSet = new HashSet<string>(_features);
			}
		}

		public IReadOnlyDictionary<string, object> Settings { get; set; } = EmptyReadOnly<string, object>.Dictionary;

		/// <summary>The id of the node, defaults to null when unknown/unspecified</summary>
		public string Id { get; internal set; }

		/// <summary>The name of the node, defaults to null when unknown/unspecified</summary>
		public string Name { get; set; }

		public Uri Uri { get; }



		public bool IsAlive { get; private set; }

		/// <summary> When marked dead this reflects the date that the node has to be taken out of rotation till</summary>
		public DateTime DeadUntil { get; private set; }

		/// <summary> The number of failed attempts trying to use this node, resets when a node is marked alive</summary>
		public int FailedAttempts { get; private set; }

		/// <summary> When set this signals the transport that a ping before first usage would be wise</summary>
		public bool IsResurrected { get; set; }

		public bool HasFeature(string feature) => _features.Count == 0 || _featureSet.Contains(feature);


		//
		//
		// public bool ClientNode => !MasterEligible && !HoldsData;
		//
		// /// <summary>Indicates whether this node holds data, defaults to true when unknown/unspecified</summary>
		// public bool HoldsData { get; set; }
		//
		// /// <summary>Whether HTTP is enabled on the node or not</summary>
		// public bool HttpEnabled { get; set; } = true;
		//
		// /// <summary>Indicates whether this node is allowed to run ingest pipelines, defaults to true when unknown/unspecified</summary>
		// public bool IngestEnabled { get; set; }
		//
		// /// <summary>Indicates whether this node is master eligible, defaults to true when unknown/unspecified</summary>
		// public bool MasterEligible { get; set; }
		//
		// public bool MasterOnlyNode => MasterEligible && !HoldsData;

		//a Node is only unique by its Uri
		public bool Equals(Node other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return Uri == other.Uri;
		}

		public void MarkDead(DateTime untill)
		{
			FailedAttempts++;
			IsAlive = false;
			IsResurrected = false;
			DeadUntil = untill;
		}

		public void MarkAlive()
		{
			FailedAttempts = 0;
			IsAlive = true;
			IsResurrected = false;
			DeadUntil = default(DateTime);
		}

		public Uri CreatePath(string path) => new Uri(Uri, path);

		public Node Clone() =>
			new Node(Uri, Features)
			{
				IsResurrected = IsResurrected,
				Id = Id,
				Name = Name,
				FailedAttempts = FailedAttempts,
				DeadUntil = DeadUntil,
				IsAlive = IsAlive,
				Settings = Settings,
			};


		public static bool operator ==(Node left, Node right) =>
			// ReSharper disable once MergeConditionalExpression
			ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);

		public static bool operator !=(Node left, Node right) => !(left == right);

		public static implicit operator Node(Uri uri) => new Node(uri);

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;

			return Equals((Node)obj);
		}

		public override int GetHashCode() => Uri.GetHashCode();
	}
}

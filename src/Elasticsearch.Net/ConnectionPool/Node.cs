using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Elasticsearch.Net
{
	public class Node : IEquatable<Node>
	{
		private static readonly IReadOnlyDictionary<string, object> EmptySettings =
			new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());

		public Node(Uri uri)
		{
			//this makes sure that elasticsearch paths stay relative to the path passed in
			//http://my-saas-provider.com/instance
			if (!uri.OriginalString.EndsWith("/", StringComparison.Ordinal))
				uri = new Uri(uri.OriginalString + "/");
			Uri = uri;
			IsAlive = true;
			HoldsData = true;
			MasterEligible = true;
			IsResurrected = true;
		}

		public bool ClientNode => !MasterEligible && !HoldsData;

		/// <summary> When marked dead this reflects the date that the node has to be taken out of rotation till</summary>
		public DateTime DeadUntil { get; private set; }

		/// <summary> The number of failed attempts trying to use this node, resets when a node is marked alive</summary>
		public int FailedAttempts { get; private set; }

		/// <summary>Indicates whether this node holds data, defaults to true when unknown/unspecified</summary>
		public bool HoldsData { get; set; }

		/// <summary>Whether HTTP is enabled on the node or not</summary>
		public bool HttpEnabled { get; set; } = true;

		/// <summary>The id of the node, defaults to null when unknown/unspecified</summary>
		public string Id { get; set; }

		/// <summary>Indicates whether this node is allowed to run ingest pipelines, defaults to true when unknown/unspecified</summary>
		public bool IngestEnabled { get; set; }

		public virtual bool IsAlive { get; private set; }

		/// <summary> When set this signals the transport that a ping before first usage would be wise</summary>
		public bool IsResurrected { get; set; }

		/// <summary>Indicates whether this node is master eligible, defaults to true when unknown/unspecified</summary>
		public bool MasterEligible { get; set; }

		public bool MasterOnlyNode => MasterEligible && !HoldsData;

		/// <summary>The name of the node, defaults to null when unknown/unspecified</summary>
		public string Name { get; set; }

		public IReadOnlyDictionary<string, object> Settings { get; set; } = EmptySettings;

		public Uri Uri { get; }

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
			new Node(Uri)
			{
				IsResurrected = IsResurrected,
				Id = Id,
				Name = Name,
				HoldsData = HoldsData,
				MasterEligible = MasterEligible,
				FailedAttempts = FailedAttempts,
				DeadUntil = DeadUntil,
				IsAlive = IsAlive,
				Settings = Settings,
				IngestEnabled = IngestEnabled,
				HttpEnabled = HttpEnabled
			};


		public static bool operator ==(Node left, Node right) =>
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Elasticsearch.Net
{
	public class Node : IEquatable<Node>
	{
		public Node(Uri uri)
		{
			//this makes sure that elasticsearch paths stay relative to the path passed in
			//http://my-saas-provider.com/instance
			if (!uri.OriginalString.EndsWith("/", StringComparison.Ordinal))
				uri = new Uri(uri.OriginalString + "/");
			this.Uri = uri;
			this.IsAlive = true;
			this.HoldsData = true;
			this.MasterEligible = true;
			this.IsResurrected = true;
		}

		public Uri Uri { get; }

		/// <summary> When set this signals the transport that a ping before first usage would be wise</summary>
		public bool IsResurrected { get; set; }

		/// <summary>Whether HTTP is enabled on the node or not</summary>
		public bool HttpEnabled { get; set; } = true;

		/// <summary>Indicates whether this node holds data, defaults to true when unknown/unspecified</summary>
		public bool HoldsData { get; set; }

		/// <summary>Indicates whether this node is master eligible, defaults to true when unknown/unspecified</summary>
		public bool MasterEligible { get; set; }

		/// <summary>Indicates whether this node is allowed to run ingest pipelines, defaults to true when unknown/unspecified</summary>
		public bool IngestEnabled { get; set; }

		public bool MasterOnlyNode => this.MasterEligible && !this.HoldsData;

		public bool ClientNode => !this.MasterEligible && !this.HoldsData;

		/// <summary>The id of the node, defaults to null when unknown/unspecified</summary>
		public string Id { get; set; }

		/// <summary>The name of the node, defaults to null when unknown/unspecified</summary>
		public string Name { get; set; }

		private static readonly IReadOnlyDictionary<string, object> EmptySettings = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());
		public IReadOnlyDictionary<string, object> Settings { get; set; } = EmptySettings;

		/// <summary> The number of failed attempts trying to use this node, resets when a node is marked alive</summary>
		public int FailedAttempts { get; private set; }

		/// <summary> When marked dead this reflects the date that the node has to be taken out of rotation till</summary>
		public DateTime DeadUntil { get; private set; }

		public virtual bool IsAlive { get; private set; }

		public void MarkDead(DateTime untill)
		{
			this.FailedAttempts++;
			this.IsAlive = false;
			this.IsResurrected = false;
			this.DeadUntil = untill;
		}

		public void MarkAlive()
		{
			this.FailedAttempts = 0;
			this.IsAlive = true;
			this.IsResurrected = false;
			this.DeadUntil = default(DateTime);
		}

		public Uri CreatePath(string path) => new Uri(this.Uri, path);

		public Node Clone() =>
			new Node(this.Uri)
			{
				IsResurrected = this.IsResurrected,
				Id = this.Id,
				Name = this.Name,
				HoldsData = this.HoldsData,
				MasterEligible = this.MasterEligible,
				FailedAttempts = this.FailedAttempts,
				DeadUntil = this.DeadUntil,
				IsAlive = this.IsAlive,
				Settings = this.Settings,
				IngestEnabled = this.IngestEnabled,
				HttpEnabled = this.HttpEnabled
			};


		public static bool operator ==(Node left, Node right) =>
			ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);

		public static bool operator !=(Node left, Node right) => !(left == right);

		public static implicit operator Node(Uri uri) => new Node(uri);

		//a Node is only unique by its Uri
		public bool Equals(Node other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return this.Uri == other.Uri;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Node)obj);
		}

		public override int GetHashCode() => this.Uri.GetHashCode();
	}
}

using System;
using System.Diagnostics;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(IndexNameJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class IndexName : IEquatable<IndexName>, IUrlParameter
	{
		private static readonly char[] ClusterSeparator = {':'};
		internal string DebugDisplay => Type == null ? Name : $"{nameof(IndexName)} for typeof: {Type?.Name}";

		//TODO 6.0 make setters private and use constructor
		public string Cluster { get; set; }
		public string Name { get; set; }
		public Type Type { get; set; }

		public static IndexName From<T>() => typeof(T);
		public static IndexName From<T>(string clusterName) => From(typeof(T), clusterName);
		private static IndexName From(Type t, string clusterName) => new IndexName { Type = t, Cluster = clusterName};

		public Indices And<T>() => new Indices(new[] { this, typeof(T) });
		public Indices And<T>(string clusterName) => new Indices(new[] { this, From(typeof(T), clusterName) });
		public Indices And(IndexName index) => new Indices(new[] { this, index });

		private static IndexName Parse(string indexName)
		{
			if (string.IsNullOrWhiteSpace(indexName)) return null;
			var tokens = indexName.Split(ClusterSeparator, 2, StringSplitOptions.RemoveEmptyEntries);
			return tokens.Length == 1
				? new IndexName { Name = tokens[0].Trim() }
				: new IndexName { Name = tokens[1].Trim(), Cluster = tokens[0].Trim() };
		}

		public static implicit operator IndexName(string indexName) => Parse(indexName);
		public static implicit operator IndexName(Type type) => type == null ? null : new IndexName { Type = type };

		bool IEquatable<IndexName>.Equals(IndexName other) => EqualsMarker(other);

		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return this.EqualsString(s);
			var pp = obj as IndexName;
			return EqualsMarker(pp);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = this.Name?.GetHashCode() ?? this.Type?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (this.Cluster?.GetHashCode() ?? 0);
				return hashCode;
			}
		}

		public override string ToString()
		{
			if (!this.Name.IsNullOrEmpty())
				return PrefixClusterName(this.Name);
			return this.Type != null ? PrefixClusterName(this.Type.Name) : string.Empty;
		}
		private string PrefixClusterName(string name) => PrefixClusterName(this, name);
		private static string PrefixClusterName(IndexName i, string name) => i.Cluster.IsNullOrEmpty() ? name : $"{i.Cluster}:{name}";

		public bool EqualsString(string other)
		{
			return !other.IsNullOrEmpty() && other == PrefixClusterName(this.Name);
		}

		public bool EqualsMarker(IndexName other)
		{
			if (!this.Name.IsNullOrEmpty() && other != null && !other.Name.IsNullOrEmpty())
				return EqualsString(PrefixClusterName(other,other.Name));

			if (this.Type != null && other != null && other.Type != null)
				return this.GetHashCode() == other.GetHashCode();
			return false;
		}

		public string GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			if (nestSettings == null)
				throw new Exception("Tried to pass index name on querysting but it could not be resolved because no nest settings are available");

			return nestSettings.Inferrer.IndexName(this);
		}

	}
}

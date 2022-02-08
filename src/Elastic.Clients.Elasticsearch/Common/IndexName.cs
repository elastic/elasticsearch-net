using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	//public readonly struct SearchQuery
	//{
	//	private readonly int? _intValue;
	//	private readonly string _stringValue;
	//	//private readonly bool? _boolValue;

	//	private SearchQuery(int value)
	//	{
	//		_intValue = value;
	//		_stringValue = null;
	//		//_boolValue = null;
	//	}

	//	private SearchQuery(string value)
	//	{
	//		_intValue = null;
	//		_stringValue = value;
	//		//_boolValue = null;
	//	}

	//	public static SearchQuery String(string value) => new SearchQuery(value);
	//	public static SearchQuery Integer(int value) => new SearchQuery(value);
	//}

	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class IndexName : IEquatable<IndexName>, IUrlParameter
	{
		private const char ClusterSeparator = ':';

		private IndexName(string index, string cluster = null)
		{
			Name = index;
			Cluster = cluster;
		}

		private IndexName(Type type, string cluster = null)
		{
			Type = type;
			Cluster = cluster;
		}

		private IndexName(string index, Type type, string cluster = null)
		{
			Name = index;
			Type = type;
			Cluster = cluster;
		}

		public string Cluster { get; }
		public string Name { get; }
		public Type Type { get; }

		internal string DebugDisplay => Type == null ? Name : $"{nameof(IndexName)} for typeof: {Type?.Name}";

		private static int TypeHashCode { get; } = typeof(IndexName).GetHashCode();

		bool IEquatable<IndexName>.Equals(IndexName other) => EqualsMarker(other);

		public string GetString(ITransportConfiguration settings)
		{
			if (settings is not IElasticsearchClientSettings elasticsearchClientSettings)
				throw new Exception("Tried to pass index name on querystring but it could not be resolved because no Elastic.Clients.Elasticsearch settings are available");

			return elasticsearchClientSettings.Inferrer.IndexName(this);
		}

		public static IndexName From<T>() => typeof(T);

		public static IndexName From<T>(string clusterName) => From(typeof(T), clusterName);

		private static IndexName From(Type type, string clusterName) => new IndexName(type, clusterName);

		internal static IndexName Rebuild(string index, Type type, string clusterName = null) => new IndexName(index, type, clusterName);

		public Indices And<T>() => new Indices(new[] { this, typeof(T) });

		public Indices And<T>(string clusterName) => new Indices(new[] { this, From(typeof(T), clusterName) });

		public Indices And(IndexName index) => new Indices(new[] { this, index });

		internal static IndexName Parse(string indexName)
		{
			if (string.IsNullOrWhiteSpace(indexName))
				return null;

			var separatorIndex = indexName.IndexOf(ClusterSeparator);

			if (separatorIndex > -1)
			{
				var cluster = indexName.Substring(0, separatorIndex);
				var index = indexName.Substring(separatorIndex + 1);
				return new IndexName(index, cluster);
			}

			return new IndexName(indexName);
		}

		public static implicit operator IndexName(string indexName) => Parse(indexName);

		public static implicit operator IndexName(Type type) => type == null ? null : new IndexName(type);

		public override bool Equals(object obj) => obj is string s ? EqualsString(s) : obj is IndexName i && EqualsMarker(i);

		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (Name?.GetHashCode() ?? Type?.GetHashCode() ?? 0);
				result = (result * 397) ^ (Cluster?.GetHashCode() ?? 0);
				return result;
			}
		}

		public static bool operator ==(IndexName left, IndexName right) => Equals(left, right);

		public static bool operator !=(IndexName left, IndexName right) => !Equals(left, right);

		public override string ToString()
		{
			if (!Name.IsNullOrEmpty())
				return PrefixClusterName(Name);

			return Type != null ? PrefixClusterName(Type.Name) : string.Empty;
		}

		private string PrefixClusterName(string name) => PrefixClusterName(this, name);

		private static string PrefixClusterName(IndexName index, string name) => index.Cluster.IsNullOrEmpty() ? name : $"{index.Cluster}:{name}";

		private bool EqualsString(string other) => !other.IsNullOrEmpty() && other == PrefixClusterName(Name);

		private bool EqualsMarker(IndexName other)
		{
			if (other == null)
				return false;
			if (!Name.IsNullOrEmpty() && !other.Name.IsNullOrEmpty())
				return EqualsString(PrefixClusterName(other, other.Name));

			if ((!Cluster.IsNullOrEmpty() || !other.Cluster.IsNullOrEmpty()) && Cluster != other.Cluster)
				return false;

			return Type != null && other?.Type != null && Type == other.Type;
		}
	}
}

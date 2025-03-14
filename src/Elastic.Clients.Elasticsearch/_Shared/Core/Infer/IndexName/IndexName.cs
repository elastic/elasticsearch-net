// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Represents the name of an index, which may be inferred from a <see cref="Type"/>.
/// </summary>
[JsonConverter(typeof(IndexNameConverter))]
[DebuggerDisplay("{DebugDisplay,nq}")]
public class IndexName :
	IEquatable<IndexName>,
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<IndexName>
#endif
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

	internal string Cluster { get; }
	internal string Name { get; }
	internal Type Type { get; }

	internal string DebugDisplay => Type == null ? Name : $"{nameof(IndexName)} for typeof: {Type?.Name}";

	private static int TypeHashCode { get; } = typeof(IndexName).GetHashCode();

	bool IEquatable<IndexName>.Equals(IndexName other) => EqualsMarker(other);

	string IUrlParameter.GetString(ITransportConfiguration settings)
	{
		if (settings is not IElasticsearchClientSettings elasticsearchClientSettings)
			throw new Exception("Tried to pass index name on query string but it could not be resolved because no Elastic.Clients.Elasticsearch settings are available.");

		return elasticsearchClientSettings.Inferrer.IndexName(this);
	}

	public static IndexName From<T>() => typeof(T);

	public static IndexName From<T>(string clusterName) => From(typeof(T), clusterName);

	private static IndexName From(Type type, string clusterName) => new(type, clusterName);

	internal static IndexName Rebuild(string index, Type type, string clusterName = null) => new(index, type, clusterName);

	public Indices And<T>() => new(new[] { this, typeof(T) });

	public Indices And<T>(string clusterName) => new(new[] { this, From(typeof(T), clusterName) });

	public Indices And(IndexName index) => new(new[] { this, index });

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

		return Type is not null && other?.Type is not null && Type == other.Type;
	}

	#region IParsable

	public static IndexName Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out IndexName? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		result = Parse(s);
		return true;
	}

	#endregion IParsable
}

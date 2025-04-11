// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class ShardQueryCacheConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ShardQueryCache>
{
	private static readonly System.Text.Json.JsonEncodedText PropCacheCount = System.Text.Json.JsonEncodedText.Encode("cache_count");
	private static readonly System.Text.Json.JsonEncodedText PropCacheSize = System.Text.Json.JsonEncodedText.Encode("cache_size");
	private static readonly System.Text.Json.JsonEncodedText PropEvictions = System.Text.Json.JsonEncodedText.Encode("evictions");
	private static readonly System.Text.Json.JsonEncodedText PropHitCount = System.Text.Json.JsonEncodedText.Encode("hit_count");
	private static readonly System.Text.Json.JsonEncodedText PropMemorySizeInBytes = System.Text.Json.JsonEncodedText.Encode("memory_size_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropMissCount = System.Text.Json.JsonEncodedText.Encode("miss_count");
	private static readonly System.Text.Json.JsonEncodedText PropTotalCount = System.Text.Json.JsonEncodedText.Encode("total_count");

	public override Elastic.Clients.Elasticsearch.IndexManagement.ShardQueryCache Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propCacheCount = default;
		LocalJsonValue<long> propCacheSize = default;
		LocalJsonValue<long> propEvictions = default;
		LocalJsonValue<long> propHitCount = default;
		LocalJsonValue<long> propMemorySizeInBytes = default;
		LocalJsonValue<long> propMissCount = default;
		LocalJsonValue<long> propTotalCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCacheCount.TryReadProperty(ref reader, options, PropCacheCount, null))
			{
				continue;
			}

			if (propCacheSize.TryReadProperty(ref reader, options, PropCacheSize, null))
			{
				continue;
			}

			if (propEvictions.TryReadProperty(ref reader, options, PropEvictions, null))
			{
				continue;
			}

			if (propHitCount.TryReadProperty(ref reader, options, PropHitCount, null))
			{
				continue;
			}

			if (propMemorySizeInBytes.TryReadProperty(ref reader, options, PropMemorySizeInBytes, null))
			{
				continue;
			}

			if (propMissCount.TryReadProperty(ref reader, options, PropMissCount, null))
			{
				continue;
			}

			if (propTotalCount.TryReadProperty(ref reader, options, PropTotalCount, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.IndexManagement.ShardQueryCache(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CacheCount = propCacheCount.Value,
			CacheSize = propCacheSize.Value,
			Evictions = propEvictions.Value,
			HitCount = propHitCount.Value,
			MemorySizeInBytes = propMemorySizeInBytes.Value,
			MissCount = propMissCount.Value,
			TotalCount = propTotalCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ShardQueryCache value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCacheCount, value.CacheCount, null, null);
		writer.WriteProperty(options, PropCacheSize, value.CacheSize, null, null);
		writer.WriteProperty(options, PropEvictions, value.Evictions, null, null);
		writer.WriteProperty(options, PropHitCount, value.HitCount, null, null);
		writer.WriteProperty(options, PropMemorySizeInBytes, value.MemorySizeInBytes, null, null);
		writer.WriteProperty(options, PropMissCount, value.MissCount, null, null);
		writer.WriteProperty(options, PropTotalCount, value.TotalCount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ShardQueryCacheConverter))]
public sealed partial class ShardQueryCache
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShardQueryCache(long cacheCount, long cacheSize, long evictions, long hitCount, long memorySizeInBytes, long missCount, long totalCount)
	{
		CacheCount = cacheCount;
		CacheSize = cacheSize;
		Evictions = evictions;
		HitCount = hitCount;
		MemorySizeInBytes = memorySizeInBytes;
		MissCount = missCount;
		TotalCount = totalCount;
	}
#if NET7_0_OR_GREATER
	public ShardQueryCache()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ShardQueryCache()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ShardQueryCache(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	long CacheCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long CacheSize { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Evictions { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long HitCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long MemorySizeInBytes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long MissCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long TotalCount { get; set; }
}
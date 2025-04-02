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

namespace Elastic.Clients.Elasticsearch.Core.MSearch;

internal sealed partial class MultisearchHeaderConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.MSearch.MultisearchHeader>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllowNoIndices = System.Text.Json.JsonEncodedText.Encode("allow_no_indices");
	private static readonly System.Text.Json.JsonEncodedText PropAllowPartialSearchResults = System.Text.Json.JsonEncodedText.Encode("allow_partial_search_results");
	private static readonly System.Text.Json.JsonEncodedText PropCcsMinimizeRoundtrips = System.Text.Json.JsonEncodedText.Encode("ccs_minimize_roundtrips");
	private static readonly System.Text.Json.JsonEncodedText PropExpandWildcards = System.Text.Json.JsonEncodedText.Encode("expand_wildcards");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreThrottled = System.Text.Json.JsonEncodedText.Encode("ignore_throttled");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreUnavailable = System.Text.Json.JsonEncodedText.Encode("ignore_unavailable");
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropPreference = System.Text.Json.JsonEncodedText.Encode("preference");
	private static readonly System.Text.Json.JsonEncodedText PropRequestCache = System.Text.Json.JsonEncodedText.Encode("request_cache");
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("routing");
	private static readonly System.Text.Json.JsonEncodedText PropSearchType = System.Text.Json.JsonEncodedText.Encode("search_type");

	public override Elastic.Clients.Elasticsearch.Core.MSearch.MultisearchHeader Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propAllowNoIndices = default;
		LocalJsonValue<bool?> propAllowPartialSearchResults = default;
		LocalJsonValue<bool?> propCcsMinimizeRoundtrips = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?> propExpandWildcards = default;
		LocalJsonValue<bool?> propIgnoreThrottled = default;
		LocalJsonValue<bool?> propIgnoreUnavailable = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Indices?> propIndices = default;
		LocalJsonValue<string?> propPreference = default;
		LocalJsonValue<bool?> propRequestCache = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Routing?> propRouting = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.SearchType?> propSearchType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllowNoIndices.TryReadProperty(ref reader, options, PropAllowNoIndices, null))
			{
				continue;
			}

			if (propAllowPartialSearchResults.TryReadProperty(ref reader, options, PropAllowPartialSearchResults, null))
			{
				continue;
			}

			if (propCcsMinimizeRoundtrips.TryReadProperty(ref reader, options, PropCcsMinimizeRoundtrips, null))
			{
				continue;
			}

			if (propExpandWildcards.TryReadProperty(ref reader, options, PropExpandWildcards, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.ExpandWildcard>(o, null)))
			{
				continue;
			}

			if (propIgnoreThrottled.TryReadProperty(ref reader, options, PropIgnoreThrottled, null))
			{
				continue;
			}

			if (propIgnoreUnavailable.TryReadProperty(ref reader, options, PropIgnoreUnavailable, null))
			{
				continue;
			}

			if (propIndices.TryReadProperty(ref reader, options, PropIndices, null))
			{
				continue;
			}

			if (propPreference.TryReadProperty(ref reader, options, PropPreference, null))
			{
				continue;
			}

			if (propRequestCache.TryReadProperty(ref reader, options, PropRequestCache, null))
			{
				continue;
			}

			if (propRouting.TryReadProperty(ref reader, options, PropRouting, null))
			{
				continue;
			}

			if (propSearchType.TryReadProperty(ref reader, options, PropSearchType, null))
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
		return new Elastic.Clients.Elasticsearch.Core.MSearch.MultisearchHeader(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AllowNoIndices = propAllowNoIndices.Value,
			AllowPartialSearchResults = propAllowPartialSearchResults.Value,
			CcsMinimizeRoundtrips = propCcsMinimizeRoundtrips.Value,
			ExpandWildcards = propExpandWildcards.Value,
			IgnoreThrottled = propIgnoreThrottled.Value,
			IgnoreUnavailable = propIgnoreUnavailable.Value,
			Indices = propIndices.Value,
			Preference = propPreference.Value,
			RequestCache = propRequestCache.Value,
			Routing = propRouting.Value,
			SearchType = propSearchType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.MSearch.MultisearchHeader value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowNoIndices, value.AllowNoIndices, null, null);
		writer.WriteProperty(options, PropAllowPartialSearchResults, value.AllowPartialSearchResults, null, null);
		writer.WriteProperty(options, PropCcsMinimizeRoundtrips, value.CcsMinimizeRoundtrips, null, null);
		writer.WriteProperty(options, PropExpandWildcards, value.ExpandWildcards, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.ExpandWildcard>(o, v, null));
		writer.WriteProperty(options, PropIgnoreThrottled, value.IgnoreThrottled, null, null);
		writer.WriteProperty(options, PropIgnoreUnavailable, value.IgnoreUnavailable, null, null);
		writer.WriteProperty(options, PropIndices, value.Indices, null, null);
		writer.WriteProperty(options, PropPreference, value.Preference, null, null);
		writer.WriteProperty(options, PropRequestCache, value.RequestCache, null, null);
		writer.WriteProperty(options, PropRouting, value.Routing, null, null);
		writer.WriteProperty(options, PropSearchType, value.SearchType, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Contains parameters used to limit or change the subsequent search body request.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.MSearch.MultisearchHeaderConverter))]
public sealed partial class MultisearchHeader
{
#if NET7_0_OR_GREATER
	public MultisearchHeader()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public MultisearchHeader()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MultisearchHeader(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public bool? AllowNoIndices { get; set; }
	public bool? AllowPartialSearchResults { get; set; }
	public bool? CcsMinimizeRoundtrips { get; set; }
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get; set; }
	public bool? IgnoreThrottled { get; set; }
	public bool? IgnoreUnavailable { get; set; }
	public Elastic.Clients.Elasticsearch.Indices? Indices { get; set; }
	public string? Preference { get; set; }
	public bool? RequestCache { get; set; }
	public Elastic.Clients.Elasticsearch.Routing? Routing { get; set; }
	public Elastic.Clients.Elasticsearch.SearchType? SearchType { get; set; }
}
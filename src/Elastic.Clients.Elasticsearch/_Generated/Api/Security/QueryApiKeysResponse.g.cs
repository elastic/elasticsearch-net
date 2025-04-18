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

namespace Elastic.Clients.Elasticsearch.Security;

internal sealed partial class QueryApiKeysResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.QueryApiKeysResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAggregations = System.Text.Json.JsonEncodedText.Encode("aggregations");
	private static readonly System.Text.Json.JsonEncodedText PropApiKeys = System.Text.Json.JsonEncodedText.Encode("api_keys");
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropTotal = System.Text.Json.JsonEncodedText.Encode("total");

	public override Elastic.Clients.Elasticsearch.Security.QueryApiKeysResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionary?> propAggregations = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ApiKey>> propApiKeys = default;
		LocalJsonValue<int> propCount = default;
		LocalJsonValue<int> propTotal = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAggregations.TryReadProperty(ref reader, options, PropAggregations, null))
			{
				continue;
			}

			if (propApiKeys.TryReadProperty(ref reader, options, PropApiKeys, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ApiKey> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.ApiKey>(o, null)!))
			{
				continue;
			}

			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propTotal.TryReadProperty(ref reader, options, PropTotal, null))
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
		return new Elastic.Clients.Elasticsearch.Security.QueryApiKeysResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Aggregations = propAggregations.Value,
			ApiKeys = propApiKeys.Value,
			Count = propCount.Value,
			Total = propTotal.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.QueryApiKeysResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAggregations, value.Aggregations, null, null);
		writer.WriteProperty(options, PropApiKeys, value.ApiKeys, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ApiKey> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.ApiKey>(o, v, null));
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropTotal, value.Total, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.QueryApiKeysResponseConverter))]
public sealed partial class QueryApiKeysResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public QueryApiKeysResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal QueryApiKeysResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The aggregations result, if requested.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionary? Aggregations { get; set; }

	/// <summary>
	/// <para>
	/// A list of API key information.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ApiKey> ApiKeys { get; set; }

	/// <summary>
	/// <para>
	/// The number of API keys returned in the response.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Count { get; set; }

	/// <summary>
	/// <para>
	/// The total number of API keys found.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Total { get; set; }
}
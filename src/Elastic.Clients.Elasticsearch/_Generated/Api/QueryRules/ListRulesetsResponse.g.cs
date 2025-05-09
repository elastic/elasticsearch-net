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

namespace Elastic.Clients.Elasticsearch.QueryRules;

internal sealed partial class ListRulesetsResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryRules.ListRulesetsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropResults = System.Text.Json.JsonEncodedText.Encode("results");

	public override Elastic.Clients.Elasticsearch.QueryRules.ListRulesetsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propCount = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRulesetListItem>> propResults = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propResults.TryReadProperty(ref reader, options, PropResults, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRulesetListItem> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.QueryRules.QueryRulesetListItem>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.QueryRules.ListRulesetsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Count = propCount.Value,
			Results = propResults.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryRules.ListRulesetsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropResults, value.Results, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRulesetListItem> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.QueryRules.QueryRulesetListItem>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryRules.ListRulesetsResponseConverter))]
public sealed partial class ListRulesetsResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ListRulesetsResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ListRulesetsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		long Count { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRulesetListItem> Results { get; set; }
}
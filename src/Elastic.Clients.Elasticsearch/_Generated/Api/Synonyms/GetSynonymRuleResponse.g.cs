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

namespace Elastic.Clients.Elasticsearch.Synonyms;

internal sealed partial class GetSynonymRuleResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRuleResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropSynonyms = System.Text.Json.JsonEncodedText.Encode("synonyms");

	public override Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRuleResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propId = default;
		LocalJsonValue<string> propSynonyms = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propSynonyms.TryReadProperty(ref reader, options, PropSynonyms, null))
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
		return new Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRuleResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Id = propId.Value,
			Synonyms = propSynonyms.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRuleResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropSynonyms, value.Synonyms, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Synonyms.GetSynonymRuleResponseConverter))]
public sealed partial class GetSynonymRuleResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetSynonymRuleResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetSynonymRuleResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Synonym Rule identifier
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Id { get; set; }

	/// <summary>
	/// <para>
	/// Synonyms, in Solr format, that conform the synonym rule.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Synonyms { get; set; }
}
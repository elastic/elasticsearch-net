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

internal sealed partial class GetSynonymResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Synonyms.GetSynonymResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropSynonymsSet = System.Text.Json.JsonEncodedText.Encode("synonyms_set");

	public override Elastic.Clients.Elasticsearch.Synonyms.GetSynonymResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propCount = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Synonyms.SynonymRuleRead>> propSynonymsSet = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propSynonymsSet.TryReadProperty(ref reader, options, PropSynonymsSet, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Synonyms.SynonymRuleRead> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Synonyms.SynonymRuleRead>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.Synonyms.GetSynonymResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Count = propCount.Value,
			SynonymsSet = propSynonymsSet.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Synonyms.GetSynonymResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropSynonymsSet, value.SynonymsSet, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Synonyms.SynonymRuleRead> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Synonyms.SynonymRuleRead>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Synonyms.GetSynonymResponseConverter))]
public sealed partial class GetSynonymResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetSynonymResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetSynonymResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The total number of synonyms rules that the synonyms set contains.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Count { get; set; }

	/// <summary>
	/// <para>
	/// Synonym rule details.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Synonyms.SynonymRuleRead> SynonymsSet { get; set; }
}
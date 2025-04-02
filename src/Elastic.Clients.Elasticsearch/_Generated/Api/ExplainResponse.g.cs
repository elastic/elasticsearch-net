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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class ExplainResponseConverter<TDocument> : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.ExplainResponse<TDocument>>
{
	private static readonly System.Text.Json.JsonEncodedText PropExplanation = System.Text.Json.JsonEncodedText.Encode("explanation");
	private static readonly System.Text.Json.JsonEncodedText PropGet = System.Text.Json.JsonEncodedText.Encode("get");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("_id");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("_index");
	private static readonly System.Text.Json.JsonEncodedText PropMatched = System.Text.Json.JsonEncodedText.Encode("matched");

	public override Elastic.Clients.Elasticsearch.ExplainResponse<TDocument> Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Explain.ExplanationDetail?> propExplanation = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.InlineGet<TDocument>?> propGet = default;
		LocalJsonValue<string> propId = default;
		LocalJsonValue<string> propIndex = default;
		LocalJsonValue<bool> propMatched = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propExplanation.TryReadProperty(ref reader, options, PropExplanation, null))
			{
				continue;
			}

			if (propGet.TryReadProperty(ref reader, options, PropGet, null))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propMatched.TryReadProperty(ref reader, options, PropMatched, null))
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
		return new Elastic.Clients.Elasticsearch.ExplainResponse<TDocument>(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Explanation = propExplanation.Value,
			Get = propGet.Value,
			Id = propId.Value,
			Index = propIndex.Value,
			Matched = propMatched.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.ExplainResponse<TDocument> value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropExplanation, value.Explanation, null, null);
		writer.WriteProperty(options, PropGet, value.Get, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropMatched, value.Matched, null, null);
		writer.WriteEndObject();
	}
}

internal sealed partial class ExplainResponseConverterFactory : System.Text.Json.Serialization.JsonConverterFactory
{
	public override bool CanConvert(System.Type typeToConvert)
	{
		return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(ExplainResponse<>);
	}

	public override System.Text.Json.Serialization.JsonConverter CreateConverter(System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();
#pragma warning disable IL3050
		var converter = (System.Text.Json.Serialization.JsonConverter)System.Activator.CreateInstance(typeof(ExplainResponseConverter<>).MakeGenericType(args[0]), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, binder: null, args: null, culture: null)!;
#pragma warning restore IL3050
		return converter;
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.ExplainResponseConverterFactory))]
public sealed partial class ExplainResponse<TDocument> : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExplainResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ExplainResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Core.Explain.ExplanationDetail? Explanation { get; set; }
	public Elastic.Clients.Elasticsearch.InlineGet<TDocument>? Get { get; set; }
	public
#if NET7_0_OR_GREATER
required
#endif
string Id { get; set; }
	public
#if NET7_0_OR_GREATER
required
#endif
string Index { get; set; }
	public
#if NET7_0_OR_GREATER
required
#endif
bool Matched { get; set; }
}
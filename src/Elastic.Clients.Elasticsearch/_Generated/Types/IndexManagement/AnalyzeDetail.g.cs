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

internal sealed partial class AnalyzeDetailConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.AnalyzeDetail>
{
	private static readonly System.Text.Json.JsonEncodedText PropAnalyzer = System.Text.Json.JsonEncodedText.Encode("analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropCharfilters = System.Text.Json.JsonEncodedText.Encode("charfilters");
	private static readonly System.Text.Json.JsonEncodedText PropCustomAnalyzer = System.Text.Json.JsonEncodedText.Encode("custom_analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropTokenfilters = System.Text.Json.JsonEncodedText.Encode("tokenfilters");
	private static readonly System.Text.Json.JsonEncodedText PropTokenizer = System.Text.Json.JsonEncodedText.Encode("tokenizer");

	public override Elastic.Clients.Elasticsearch.IndexManagement.AnalyzeDetail Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.AnalyzerDetail?> propAnalyzer = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.CharFilterDetail>?> propCharfilters = default;
		LocalJsonValue<bool> propCustomAnalyzer = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.TokenDetail>?> propTokenfilters = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.TokenDetail?> propTokenizer = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAnalyzer.TryReadProperty(ref reader, options, PropAnalyzer, null))
			{
				continue;
			}

			if (propCharfilters.TryReadProperty(ref reader, options, PropCharfilters, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.CharFilterDetail>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.CharFilterDetail>(o, null)))
			{
				continue;
			}

			if (propCustomAnalyzer.TryReadProperty(ref reader, options, PropCustomAnalyzer, null))
			{
				continue;
			}

			if (propTokenfilters.TryReadProperty(ref reader, options, PropTokenfilters, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.TokenDetail>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.TokenDetail>(o, null)))
			{
				continue;
			}

			if (propTokenizer.TryReadProperty(ref reader, options, PropTokenizer, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.AnalyzeDetail(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Analyzer = propAnalyzer.Value,
			Charfilters = propCharfilters.Value,
			CustomAnalyzer = propCustomAnalyzer.Value,
			Tokenfilters = propTokenfilters.Value,
			Tokenizer = propTokenizer.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.AnalyzeDetail value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAnalyzer, value.Analyzer, null, null);
		writer.WriteProperty(options, PropCharfilters, value.Charfilters, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.CharFilterDetail>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.CharFilterDetail>(o, v, null));
		writer.WriteProperty(options, PropCustomAnalyzer, value.CustomAnalyzer, null, null);
		writer.WriteProperty(options, PropTokenfilters, value.Tokenfilters, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.TokenDetail>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.TokenDetail>(o, v, null));
		writer.WriteProperty(options, PropTokenizer, value.Tokenizer, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.AnalyzeDetailConverter))]
public sealed partial class AnalyzeDetail
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AnalyzeDetail(bool customAnalyzer)
	{
		CustomAnalyzer = customAnalyzer;
	}
#if NET7_0_OR_GREATER
	public AnalyzeDetail()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public AnalyzeDetail()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AnalyzeDetail(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.AnalyzerDetail? Analyzer { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.CharFilterDetail>? Charfilters { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool CustomAnalyzer { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.TokenDetail>? Tokenfilters { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.TokenDetail? Tokenizer { get; set; }
}
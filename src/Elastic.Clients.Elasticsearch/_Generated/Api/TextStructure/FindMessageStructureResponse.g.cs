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

namespace Elastic.Clients.Elasticsearch.TextStructure;

internal sealed partial class FindMessageStructureResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCharset = System.Text.Json.JsonEncodedText.Encode("charset");
	private static readonly System.Text.Json.JsonEncodedText PropEcsCompatibility = System.Text.Json.JsonEncodedText.Encode("ecs_compatibility");
	private static readonly System.Text.Json.JsonEncodedText PropFieldStats = System.Text.Json.JsonEncodedText.Encode("field_stats");
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");
	private static readonly System.Text.Json.JsonEncodedText PropGrokPattern = System.Text.Json.JsonEncodedText.Encode("grok_pattern");
	private static readonly System.Text.Json.JsonEncodedText PropIngestPipeline = System.Text.Json.JsonEncodedText.Encode("ingest_pipeline");
	private static readonly System.Text.Json.JsonEncodedText PropJavaTimestampFormats = System.Text.Json.JsonEncodedText.Encode("java_timestamp_formats");
	private static readonly System.Text.Json.JsonEncodedText PropJodaTimestampFormats = System.Text.Json.JsonEncodedText.Encode("joda_timestamp_formats");
	private static readonly System.Text.Json.JsonEncodedText PropMappings = System.Text.Json.JsonEncodedText.Encode("mappings");
	private static readonly System.Text.Json.JsonEncodedText PropMultilineStartPattern = System.Text.Json.JsonEncodedText.Encode("multiline_start_pattern");
	private static readonly System.Text.Json.JsonEncodedText PropNeedClientTimezone = System.Text.Json.JsonEncodedText.Encode("need_client_timezone");
	private static readonly System.Text.Json.JsonEncodedText PropNumLinesAnalyzed = System.Text.Json.JsonEncodedText.Encode("num_lines_analyzed");
	private static readonly System.Text.Json.JsonEncodedText PropNumMessagesAnalyzed = System.Text.Json.JsonEncodedText.Encode("num_messages_analyzed");
	private static readonly System.Text.Json.JsonEncodedText PropSampleStart = System.Text.Json.JsonEncodedText.Encode("sample_start");
	private static readonly System.Text.Json.JsonEncodedText PropTimestampField = System.Text.Json.JsonEncodedText.Encode("timestamp_field");

	public override Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propCharset = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TextStructure.EcsCompatibilityType?> propEcsCompatibility = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.TextStructure.FieldStat>> propFieldStats = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TextStructure.FormatType> propFormat = default;
		LocalJsonValue<string?> propGrokPattern = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Ingest.PipelineConfig> propIngestPipeline = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>?> propJavaTimestampFormats = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>?> propJodaTimestampFormats = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.TypeMapping> propMappings = default;
		LocalJsonValue<string?> propMultilineStartPattern = default;
		LocalJsonValue<bool> propNeedClientTimezone = default;
		LocalJsonValue<int> propNumLinesAnalyzed = default;
		LocalJsonValue<int> propNumMessagesAnalyzed = default;
		LocalJsonValue<string> propSampleStart = default;
		LocalJsonValue<string?> propTimestampField = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCharset.TryReadProperty(ref reader, options, PropCharset, null))
			{
				continue;
			}

			if (propEcsCompatibility.TryReadProperty(ref reader, options, PropEcsCompatibility, null))
			{
				continue;
			}

			if (propFieldStats.TryReadProperty(ref reader, options, PropFieldStats, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.TextStructure.FieldStat> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.TextStructure.FieldStat>(o, null, null)!))
			{
				continue;
			}

			if (propFormat.TryReadProperty(ref reader, options, PropFormat, null))
			{
				continue;
			}

			if (propGrokPattern.TryReadProperty(ref reader, options, PropGrokPattern, null))
			{
				continue;
			}

			if (propIngestPipeline.TryReadProperty(ref reader, options, PropIngestPipeline, null))
			{
				continue;
			}

			if (propJavaTimestampFormats.TryReadProperty(ref reader, options, PropJavaTimestampFormats, static System.Collections.Generic.IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propJodaTimestampFormats.TryReadProperty(ref reader, options, PropJodaTimestampFormats, static System.Collections.Generic.IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propMappings.TryReadProperty(ref reader, options, PropMappings, null))
			{
				continue;
			}

			if (propMultilineStartPattern.TryReadProperty(ref reader, options, PropMultilineStartPattern, null))
			{
				continue;
			}

			if (propNeedClientTimezone.TryReadProperty(ref reader, options, PropNeedClientTimezone, null))
			{
				continue;
			}

			if (propNumLinesAnalyzed.TryReadProperty(ref reader, options, PropNumLinesAnalyzed, null))
			{
				continue;
			}

			if (propNumMessagesAnalyzed.TryReadProperty(ref reader, options, PropNumMessagesAnalyzed, null))
			{
				continue;
			}

			if (propSampleStart.TryReadProperty(ref reader, options, PropSampleStart, null))
			{
				continue;
			}

			if (propTimestampField.TryReadProperty(ref reader, options, PropTimestampField, null))
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
		return new Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Charset = propCharset.Value,
			EcsCompatibility = propEcsCompatibility.Value,
			FieldStats = propFieldStats.Value,
			Format = propFormat.Value,
			GrokPattern = propGrokPattern.Value,
			IngestPipeline = propIngestPipeline.Value,
			JavaTimestampFormats = propJavaTimestampFormats.Value,
			JodaTimestampFormats = propJodaTimestampFormats.Value,
			Mappings = propMappings.Value,
			MultilineStartPattern = propMultilineStartPattern.Value,
			NeedClientTimezone = propNeedClientTimezone.Value,
			NumLinesAnalyzed = propNumLinesAnalyzed.Value,
			NumMessagesAnalyzed = propNumMessagesAnalyzed.Value,
			SampleStart = propSampleStart.Value,
			TimestampField = propTimestampField.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCharset, value.Charset, null, null);
		writer.WriteProperty(options, PropEcsCompatibility, value.EcsCompatibility, null, null);
		writer.WriteProperty(options, PropFieldStats, value.FieldStats, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.TextStructure.FieldStat> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.TextStructure.FieldStat>(o, v, null, null));
		writer.WriteProperty(options, PropFormat, value.Format, null, null);
		writer.WriteProperty(options, PropGrokPattern, value.GrokPattern, null, null);
		writer.WriteProperty(options, PropIngestPipeline, value.IngestPipeline, null, null);
		writer.WriteProperty(options, PropJavaTimestampFormats, value.JavaTimestampFormats, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropJodaTimestampFormats, value.JodaTimestampFormats, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropMappings, value.Mappings, null, null);
		writer.WriteProperty(options, PropMultilineStartPattern, value.MultilineStartPattern, null, null);
		writer.WriteProperty(options, PropNeedClientTimezone, value.NeedClientTimezone, null, null);
		writer.WriteProperty(options, PropNumLinesAnalyzed, value.NumLinesAnalyzed, null, null);
		writer.WriteProperty(options, PropNumMessagesAnalyzed, value.NumMessagesAnalyzed, null, null);
		writer.WriteProperty(options, PropSampleStart, value.SampleStart, null, null);
		writer.WriteProperty(options, PropTimestampField, value.TimestampField, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponseConverter))]
public sealed partial class FindMessageStructureResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FindMessageStructureResponse(string charset, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.TextStructure.FieldStat> fieldStats, Elastic.Clients.Elasticsearch.TextStructure.FormatType format, Elastic.Clients.Elasticsearch.Ingest.PipelineConfig ingestPipeline, Elastic.Clients.Elasticsearch.Mapping.TypeMapping mappings, bool needClientTimezone, int numLinesAnalyzed, int numMessagesAnalyzed, string sampleStart)
	{
		Charset = charset;
		FieldStats = fieldStats;
		Format = format;
		IngestPipeline = ingestPipeline;
		Mappings = mappings;
		NeedClientTimezone = needClientTimezone;
		NumLinesAnalyzed = numLinesAnalyzed;
		NumMessagesAnalyzed = numMessagesAnalyzed;
		SampleStart = sampleStart;
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FindMessageStructureResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FindMessageStructureResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		string Charset { get; set; }
	public Elastic.Clients.Elasticsearch.TextStructure.EcsCompatibilityType? EcsCompatibility { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.TextStructure.FieldStat> FieldStats { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.TextStructure.FormatType Format { get; set; }
	public string? GrokPattern { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.Ingest.PipelineConfig IngestPipeline { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<string>? JavaTimestampFormats { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<string>? JodaTimestampFormats { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.Mapping.TypeMapping Mappings { get; set; }
	public string? MultilineStartPattern { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		bool NeedClientTimezone { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		int NumLinesAnalyzed { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		int NumMessagesAnalyzed { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		string SampleStart { get; set; }
	public string? TimestampField { get; set; }
}
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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.TextStructure;

internal sealed partial class FindFieldStructureResponseConverter : System.Text.Json.Serialization.JsonConverter<FindFieldStructureResponse>
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

	public override FindFieldStructureResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propCharset = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TextStructure.EcsCompatibilityType?> propEcsCompatibility = default;
		LocalJsonValue<IReadOnlyDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.TextStructure.FieldStat>> propFieldStats = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TextStructure.FormatType> propFormat = default;
		LocalJsonValue<string?> propGrokPattern = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Ingest.PipelineConfig> propIngestPipeline = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propJavaTimestampFormats = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propJodaTimestampFormats = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.TypeMapping> propMappings = default;
		LocalJsonValue<string?> propMultilineStartPattern = default;
		LocalJsonValue<bool> propNeedClientTimezone = default;
		LocalJsonValue<int> propNumLinesAnalyzed = default;
		LocalJsonValue<int> propNumMessagesAnalyzed = default;
		LocalJsonValue<string> propSampleStart = default;
		LocalJsonValue<string?> propTimestampField = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCharset.TryRead(ref reader, options, PropCharset))
			{
				continue;
			}

			if (propEcsCompatibility.TryRead(ref reader, options, PropEcsCompatibility))
			{
				continue;
			}

			if (propFieldStats.TryRead(ref reader, options, PropFieldStats))
			{
				continue;
			}

			if (propFormat.TryRead(ref reader, options, PropFormat))
			{
				continue;
			}

			if (propGrokPattern.TryRead(ref reader, options, PropGrokPattern))
			{
				continue;
			}

			if (propIngestPipeline.TryRead(ref reader, options, PropIngestPipeline))
			{
				continue;
			}

			if (propJavaTimestampFormats.TryRead(ref reader, options, PropJavaTimestampFormats))
			{
				continue;
			}

			if (propJodaTimestampFormats.TryRead(ref reader, options, PropJodaTimestampFormats))
			{
				continue;
			}

			if (propMappings.TryRead(ref reader, options, PropMappings))
			{
				continue;
			}

			if (propMultilineStartPattern.TryRead(ref reader, options, PropMultilineStartPattern))
			{
				continue;
			}

			if (propNeedClientTimezone.TryRead(ref reader, options, PropNeedClientTimezone))
			{
				continue;
			}

			if (propNumLinesAnalyzed.TryRead(ref reader, options, PropNumLinesAnalyzed))
			{
				continue;
			}

			if (propNumMessagesAnalyzed.TryRead(ref reader, options, PropNumMessagesAnalyzed))
			{
				continue;
			}

			if (propSampleStart.TryRead(ref reader, options, PropSampleStart))
			{
				continue;
			}

			if (propTimestampField.TryRead(ref reader, options, PropTimestampField))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new FindFieldStructureResponse
		{
			Charset = propCharset.Value
,
			EcsCompatibility = propEcsCompatibility.Value
,
			FieldStats = propFieldStats.Value
,
			Format = propFormat.Value
,
			GrokPattern = propGrokPattern.Value
,
			IngestPipeline = propIngestPipeline.Value
,
			JavaTimestampFormats = propJavaTimestampFormats.Value
,
			JodaTimestampFormats = propJodaTimestampFormats.Value
,
			Mappings = propMappings.Value
,
			MultilineStartPattern = propMultilineStartPattern.Value
,
			NeedClientTimezone = propNeedClientTimezone.Value
,
			NumLinesAnalyzed = propNumLinesAnalyzed.Value
,
			NumMessagesAnalyzed = propNumMessagesAnalyzed.Value
,
			SampleStart = propSampleStart.Value
,
			TimestampField = propTimestampField.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, FindFieldStructureResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCharset, value.Charset);
		writer.WriteProperty(options, PropEcsCompatibility, value.EcsCompatibility);
		writer.WriteProperty(options, PropFieldStats, value.FieldStats);
		writer.WriteProperty(options, PropFormat, value.Format);
		writer.WriteProperty(options, PropGrokPattern, value.GrokPattern);
		writer.WriteProperty(options, PropIngestPipeline, value.IngestPipeline);
		writer.WriteProperty(options, PropJavaTimestampFormats, value.JavaTimestampFormats);
		writer.WriteProperty(options, PropJodaTimestampFormats, value.JodaTimestampFormats);
		writer.WriteProperty(options, PropMappings, value.Mappings);
		writer.WriteProperty(options, PropMultilineStartPattern, value.MultilineStartPattern);
		writer.WriteProperty(options, PropNeedClientTimezone, value.NeedClientTimezone);
		writer.WriteProperty(options, PropNumLinesAnalyzed, value.NumLinesAnalyzed);
		writer.WriteProperty(options, PropNumMessagesAnalyzed, value.NumMessagesAnalyzed);
		writer.WriteProperty(options, PropSampleStart, value.SampleStart);
		writer.WriteProperty(options, PropTimestampField, value.TimestampField);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(FindFieldStructureResponseConverter))]
public sealed partial class FindFieldStructureResponse : ElasticsearchResponse
{
	public string Charset { get; init; }
	public Elastic.Clients.Elasticsearch.TextStructure.EcsCompatibilityType? EcsCompatibility { get; init; }
	public IReadOnlyDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.TextStructure.FieldStat> FieldStats { get; init; }
	public Elastic.Clients.Elasticsearch.TextStructure.FormatType Format { get; init; }
	public string? GrokPattern { get; init; }
	public Elastic.Clients.Elasticsearch.Ingest.PipelineConfig IngestPipeline { get; init; }
	public IReadOnlyCollection<string>? JavaTimestampFormats { get; init; }
	public IReadOnlyCollection<string>? JodaTimestampFormats { get; init; }
	public Elastic.Clients.Elasticsearch.Mapping.TypeMapping Mappings { get; init; }
	public string? MultilineStartPattern { get; init; }
	public bool NeedClientTimezone { get; init; }
	public int NumLinesAnalyzed { get; init; }
	public int NumMessagesAnalyzed { get; init; }
	public string SampleStart { get; init; }
	public string? TimestampField { get; init; }
}
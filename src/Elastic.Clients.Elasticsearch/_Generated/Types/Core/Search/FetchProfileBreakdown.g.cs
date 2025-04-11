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

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class FetchProfileBreakdownConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.FetchProfileBreakdown>
{
	private static readonly System.Text.Json.JsonEncodedText PropLoadSource = System.Text.Json.JsonEncodedText.Encode("load_source");
	private static readonly System.Text.Json.JsonEncodedText PropLoadSourceCount = System.Text.Json.JsonEncodedText.Encode("load_source_count");
	private static readonly System.Text.Json.JsonEncodedText PropLoadStoredFields = System.Text.Json.JsonEncodedText.Encode("load_stored_fields");
	private static readonly System.Text.Json.JsonEncodedText PropLoadStoredFieldsCount = System.Text.Json.JsonEncodedText.Encode("load_stored_fields_count");
	private static readonly System.Text.Json.JsonEncodedText PropNextReader = System.Text.Json.JsonEncodedText.Encode("next_reader");
	private static readonly System.Text.Json.JsonEncodedText PropNextReaderCount = System.Text.Json.JsonEncodedText.Encode("next_reader_count");
	private static readonly System.Text.Json.JsonEncodedText PropProcess = System.Text.Json.JsonEncodedText.Encode("process");
	private static readonly System.Text.Json.JsonEncodedText PropProcessCount = System.Text.Json.JsonEncodedText.Encode("process_count");

	public override Elastic.Clients.Elasticsearch.Core.Search.FetchProfileBreakdown Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int?> propLoadSource = default;
		LocalJsonValue<int?> propLoadSourceCount = default;
		LocalJsonValue<int?> propLoadStoredFields = default;
		LocalJsonValue<int?> propLoadStoredFieldsCount = default;
		LocalJsonValue<int?> propNextReader = default;
		LocalJsonValue<int?> propNextReaderCount = default;
		LocalJsonValue<int?> propProcess = default;
		LocalJsonValue<int?> propProcessCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propLoadSource.TryReadProperty(ref reader, options, PropLoadSource, null))
			{
				continue;
			}

			if (propLoadSourceCount.TryReadProperty(ref reader, options, PropLoadSourceCount, null))
			{
				continue;
			}

			if (propLoadStoredFields.TryReadProperty(ref reader, options, PropLoadStoredFields, null))
			{
				continue;
			}

			if (propLoadStoredFieldsCount.TryReadProperty(ref reader, options, PropLoadStoredFieldsCount, null))
			{
				continue;
			}

			if (propNextReader.TryReadProperty(ref reader, options, PropNextReader, null))
			{
				continue;
			}

			if (propNextReaderCount.TryReadProperty(ref reader, options, PropNextReaderCount, null))
			{
				continue;
			}

			if (propProcess.TryReadProperty(ref reader, options, PropProcess, null))
			{
				continue;
			}

			if (propProcessCount.TryReadProperty(ref reader, options, PropProcessCount, null))
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
		return new Elastic.Clients.Elasticsearch.Core.Search.FetchProfileBreakdown(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			LoadSource = propLoadSource.Value,
			LoadSourceCount = propLoadSourceCount.Value,
			LoadStoredFields = propLoadStoredFields.Value,
			LoadStoredFieldsCount = propLoadStoredFieldsCount.Value,
			NextReader = propNextReader.Value,
			NextReaderCount = propNextReaderCount.Value,
			Process = propProcess.Value,
			ProcessCount = propProcessCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.FetchProfileBreakdown value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropLoadSource, value.LoadSource, null, null);
		writer.WriteProperty(options, PropLoadSourceCount, value.LoadSourceCount, null, null);
		writer.WriteProperty(options, PropLoadStoredFields, value.LoadStoredFields, null, null);
		writer.WriteProperty(options, PropLoadStoredFieldsCount, value.LoadStoredFieldsCount, null, null);
		writer.WriteProperty(options, PropNextReader, value.NextReader, null, null);
		writer.WriteProperty(options, PropNextReaderCount, value.NextReaderCount, null, null);
		writer.WriteProperty(options, PropProcess, value.Process, null, null);
		writer.WriteProperty(options, PropProcessCount, value.ProcessCount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.FetchProfileBreakdownConverter))]
public sealed partial class FetchProfileBreakdown
{
#if NET7_0_OR_GREATER
	public FetchProfileBreakdown()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public FetchProfileBreakdown()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FetchProfileBreakdown(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public int? LoadSource { get; set; }
	public int? LoadSourceCount { get; set; }
	public int? LoadStoredFields { get; set; }
	public int? LoadStoredFieldsCount { get; set; }
	public int? NextReader { get; set; }
	public int? NextReaderCount { get; set; }
	public int? Process { get; set; }
	public int? ProcessCount { get; set; }
}
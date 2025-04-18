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

namespace Elastic.Clients.Elasticsearch.Core.HealthReport;

internal sealed partial class StagnatingBackingIndicesConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.HealthReport.StagnatingBackingIndices>
{
	private static readonly System.Text.Json.JsonEncodedText PropFirstOccurrenceTimestamp = System.Text.Json.JsonEncodedText.Encode("first_occurrence_timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropIndexName = System.Text.Json.JsonEncodedText.Encode("index_name");
	private static readonly System.Text.Json.JsonEncodedText PropRetryCount = System.Text.Json.JsonEncodedText.Encode("retry_count");

	public override Elastic.Clients.Elasticsearch.Core.HealthReport.StagnatingBackingIndices Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propFirstOccurrenceTimestamp = default;
		LocalJsonValue<string> propIndexName = default;
		LocalJsonValue<int> propRetryCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFirstOccurrenceTimestamp.TryReadProperty(ref reader, options, PropFirstOccurrenceTimestamp, null))
			{
				continue;
			}

			if (propIndexName.TryReadProperty(ref reader, options, PropIndexName, null))
			{
				continue;
			}

			if (propRetryCount.TryReadProperty(ref reader, options, PropRetryCount, null))
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
		return new Elastic.Clients.Elasticsearch.Core.HealthReport.StagnatingBackingIndices(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			FirstOccurrenceTimestamp = propFirstOccurrenceTimestamp.Value,
			IndexName = propIndexName.Value,
			RetryCount = propRetryCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.HealthReport.StagnatingBackingIndices value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFirstOccurrenceTimestamp, value.FirstOccurrenceTimestamp, null, null);
		writer.WriteProperty(options, PropIndexName, value.IndexName, null, null);
		writer.WriteProperty(options, PropRetryCount, value.RetryCount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.HealthReport.StagnatingBackingIndicesConverter))]
public sealed partial class StagnatingBackingIndices
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public StagnatingBackingIndices(long firstOccurrenceTimestamp, string indexName, int retryCount)
	{
		FirstOccurrenceTimestamp = firstOccurrenceTimestamp;
		IndexName = indexName;
		RetryCount = retryCount;
	}
#if NET7_0_OR_GREATER
	public StagnatingBackingIndices()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public StagnatingBackingIndices()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal StagnatingBackingIndices(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	long FirstOccurrenceTimestamp { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string IndexName { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int RetryCount { get; set; }
}
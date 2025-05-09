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

namespace Elastic.Clients.Elasticsearch.Rollup;

internal sealed partial class RollupCapabilitySummaryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Rollup.RollupCapabilitySummary>
{
	private static readonly System.Text.Json.JsonEncodedText PropFields = System.Text.Json.JsonEncodedText.Encode("fields");
	private static readonly System.Text.Json.JsonEncodedText PropIndexPattern = System.Text.Json.JsonEncodedText.Encode("index_pattern");
	private static readonly System.Text.Json.JsonEncodedText PropJobId = System.Text.Json.JsonEncodedText.Encode("job_id");
	private static readonly System.Text.Json.JsonEncodedText PropRollupIndex = System.Text.Json.JsonEncodedText.Encode("rollup_index");

	public override Elastic.Clients.Elasticsearch.Rollup.RollupCapabilitySummary Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Rollup.RollupFieldSummary>>> propFields = default;
		LocalJsonValue<string> propIndexPattern = default;
		LocalJsonValue<string> propJobId = default;
		LocalJsonValue<string> propRollupIndex = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFields.TryReadProperty(ref reader, options, PropFields, static System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Rollup.RollupFieldSummary>> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Rollup.RollupFieldSummary>>(o, null, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Rollup.RollupFieldSummary> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Rollup.RollupFieldSummary>(o, null)!)!))
			{
				continue;
			}

			if (propIndexPattern.TryReadProperty(ref reader, options, PropIndexPattern, null))
			{
				continue;
			}

			if (propJobId.TryReadProperty(ref reader, options, PropJobId, null))
			{
				continue;
			}

			if (propRollupIndex.TryReadProperty(ref reader, options, PropRollupIndex, null))
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
		return new Elastic.Clients.Elasticsearch.Rollup.RollupCapabilitySummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Fields = propFields.Value,
			IndexPattern = propIndexPattern.Value,
			JobId = propJobId.Value,
			RollupIndex = propRollupIndex.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Rollup.RollupCapabilitySummary value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFields, value.Fields, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Rollup.RollupFieldSummary>> v) => w.WriteDictionaryValue<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Rollup.RollupFieldSummary>>(o, v, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Rollup.RollupFieldSummary> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Rollup.RollupFieldSummary>(o, v, null)));
		writer.WriteProperty(options, PropIndexPattern, value.IndexPattern, null, null);
		writer.WriteProperty(options, PropJobId, value.JobId, null, null);
		writer.WriteProperty(options, PropRollupIndex, value.RollupIndex, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Rollup.RollupCapabilitySummaryConverter))]
public sealed partial class RollupCapabilitySummary
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RollupCapabilitySummary(System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Rollup.RollupFieldSummary>> fields, string indexPattern, string jobId, string rollupIndex)
	{
		Fields = fields;
		IndexPattern = indexPattern;
		JobId = jobId;
		RollupIndex = rollupIndex;
	}
#if NET7_0_OR_GREATER
	public RollupCapabilitySummary()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RollupCapabilitySummary()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RollupCapabilitySummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Rollup.RollupFieldSummary>> Fields { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string IndexPattern { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string JobId { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string RollupIndex { get; set; }
}
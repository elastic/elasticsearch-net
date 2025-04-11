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

internal sealed partial class RankEvalResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.RankEvalResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropDetails = System.Text.Json.JsonEncodedText.Encode("details");
	private static readonly System.Text.Json.JsonEncodedText PropFailures = System.Text.Json.JsonEncodedText.Encode("failures");
	private static readonly System.Text.Json.JsonEncodedText PropMetricScore = System.Text.Json.JsonEncodedText.Encode("metric_score");

	public override Elastic.Clients.Elasticsearch.RankEvalResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricDetail>> propDetails = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, object>> propFailures = default;
		LocalJsonValue<double> propMetricScore = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDetails.TryReadProperty(ref reader, options, PropDetails, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricDetail> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricDetail>(o, null, null)!))
			{
				continue;
			}

			if (propFailures.TryReadProperty(ref reader, options, PropFailures, static System.Collections.Generic.IReadOnlyDictionary<string, object> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)!))
			{
				continue;
			}

			if (propMetricScore.TryReadProperty(ref reader, options, PropMetricScore, null))
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
		return new Elastic.Clients.Elasticsearch.RankEvalResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Details = propDetails.Value,
			Failures = propFailures.Value,
			MetricScore = propMetricScore.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.RankEvalResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDetails, value.Details, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricDetail> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricDetail>(o, v, null, null));
		writer.WriteProperty(options, PropFailures, value.Failures, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object> v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropMetricScore, value.MetricScore, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.RankEvalResponseConverter))]
public sealed partial class RankEvalResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RankEvalResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RankEvalResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The details section contains one entry for every query in the original requests section, keyed by the search request id
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricDetail> Details { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, object> Failures { get; set; }

	/// <summary>
	/// <para>
	/// The overall evaluation quality calculated by the defined metric
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double MetricScore { get; set; }
}
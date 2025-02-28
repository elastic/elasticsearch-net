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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class ExplainDataFrameAnalyticsResponseConverter : System.Text.Json.Serialization.JsonConverter<ExplainDataFrameAnalyticsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropFieldSelection = System.Text.Json.JsonEncodedText.Encode("field_selection");
	private static readonly System.Text.Json.JsonEncodedText PropMemoryEstimation = System.Text.Json.JsonEncodedText.Encode("memory_estimation");

	public override ExplainDataFrameAnalyticsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsFieldSelection>> propFieldSelection = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsMemoryEstimation> propMemoryEstimation = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFieldSelection.TryReadProperty(ref reader, options, PropFieldSelection, static IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsFieldSelection> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsFieldSelection>(o, null)!))
			{
				continue;
			}

			if (propMemoryEstimation.TryReadProperty(ref reader, options, PropMemoryEstimation, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new ExplainDataFrameAnalyticsResponse
		{
			FieldSelection = propFieldSelection.Value
,
			MemoryEstimation = propMemoryEstimation.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, ExplainDataFrameAnalyticsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFieldSelection, value.FieldSelection, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsFieldSelection> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsFieldSelection>(o, v, null));
		writer.WriteProperty(options, PropMemoryEstimation, value.MemoryEstimation, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(ExplainDataFrameAnalyticsResponseConverter))]
public sealed partial class ExplainDataFrameAnalyticsResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>
	/// An array of objects that explain selection for each field, sorted by the field names.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsFieldSelection> FieldSelection { get; init; }

	/// <summary>
	/// <para>
	/// An array of objects that explain selection for each field, sorted by the field names.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsMemoryEstimation MemoryEstimation { get; init; }
}
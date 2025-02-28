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

internal sealed partial class GetDataFrameAnalyticsStatsResponseConverter : System.Text.Json.Serialization.JsonConverter<GetDataFrameAnalyticsStatsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropDataFrameAnalytics = System.Text.Json.JsonEncodedText.Encode("data_frame_analytics");

	public override GetDataFrameAnalyticsStatsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propCount = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalytics>> propDataFrameAnalytics = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propDataFrameAnalytics.TryReadProperty(ref reader, options, PropDataFrameAnalytics, static IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalytics> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalytics>(o, null)!))
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
		return new GetDataFrameAnalyticsStatsResponse
		{
			Count = propCount.Value
,
			DataFrameAnalytics = propDataFrameAnalytics.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GetDataFrameAnalyticsStatsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropDataFrameAnalytics, value.DataFrameAnalytics, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalytics> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalytics>(o, v, null));
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(GetDataFrameAnalyticsStatsResponseConverter))]
public sealed partial class GetDataFrameAnalyticsStatsResponse : ElasticsearchResponse
{
	public long Count { get; init; }

	/// <summary>
	/// <para>
	/// An array of objects that contain usage information for data frame analytics jobs, which are sorted by the id value in ascending order.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalytics> DataFrameAnalytics { get; init; }
}
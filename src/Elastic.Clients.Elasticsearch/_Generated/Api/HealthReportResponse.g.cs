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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class HealthReportResponseConverter : System.Text.Json.Serialization.JsonConverter<HealthReportResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropClusterName = System.Text.Json.JsonEncodedText.Encode("cluster_name");
	private static readonly System.Text.Json.JsonEncodedText PropIndicators = System.Text.Json.JsonEncodedText.Encode("indicators");
	private static readonly System.Text.Json.JsonEncodedText PropStatus = System.Text.Json.JsonEncodedText.Encode("status");

	public override HealthReportResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propClusterName = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.HealthReport.Indicators> propIndicators = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorHealthStatus?> propStatus = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propClusterName.TryReadProperty(ref reader, options, PropClusterName, null))
			{
				continue;
			}

			if (propIndicators.TryReadProperty(ref reader, options, PropIndicators, null))
			{
				continue;
			}

			if (propStatus.TryReadProperty(ref reader, options, PropStatus, null))
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
		return new HealthReportResponse
		{
			ClusterName = propClusterName.Value
,
			Indicators = propIndicators.Value
,
			Status = propStatus.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, HealthReportResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropClusterName, value.ClusterName, null, null);
		writer.WriteProperty(options, PropIndicators, value.Indicators, null, null);
		writer.WriteProperty(options, PropStatus, value.Status, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(HealthReportResponseConverter))]
public sealed partial class HealthReportResponse : ElasticsearchResponse
{
	public string ClusterName { get; init; }
	public Elastic.Clients.Elasticsearch.Core.HealthReport.Indicators Indicators { get; init; }
	public Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorHealthStatus? Status { get; init; }
}
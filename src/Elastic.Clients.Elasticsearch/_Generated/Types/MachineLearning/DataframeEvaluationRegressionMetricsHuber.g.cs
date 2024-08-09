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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class DataframeEvaluationRegressionMetricsHuber
{
	/// <summary>
	/// <para>
	/// Approximates 1/2 (prediction - actual)2 for values much less than delta and approximates a straight line with slope delta for values much larger than delta. Defaults to 1. Delta needs to be greater than 0.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("delta")]
	public double? Delta { get; set; }
}

public sealed partial class DataframeEvaluationRegressionMetricsHuberDescriptor : SerializableDescriptor<DataframeEvaluationRegressionMetricsHuberDescriptor>
{
	internal DataframeEvaluationRegressionMetricsHuberDescriptor(Action<DataframeEvaluationRegressionMetricsHuberDescriptor> configure) => configure.Invoke(this);

	public DataframeEvaluationRegressionMetricsHuberDescriptor() : base()
	{
	}

	private double? DeltaValue { get; set; }

	/// <summary>
	/// <para>
	/// Approximates 1/2 (prediction - actual)2 for values much less than delta and approximates a straight line with slope delta for values much larger than delta. Defaults to 1. Delta needs to be greater than 0.
	/// </para>
	/// </summary>
	public DataframeEvaluationRegressionMetricsHuberDescriptor Delta(double? delta)
	{
		DeltaValue = delta;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DeltaValue.HasValue)
		{
			writer.WritePropertyName("delta");
			writer.WriteNumberValue(DeltaValue.Value);
		}

		writer.WriteEndObject();
	}
}
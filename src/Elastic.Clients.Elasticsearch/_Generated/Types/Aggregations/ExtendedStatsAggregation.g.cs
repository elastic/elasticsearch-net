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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	public partial class ExtendedStatsAggregation : Aggregations.FormatMetricAggregationBase, IAggregationContainerVariant
	{
		public ExtendedStatsAggregation(string name) : base(name)
		{
		}

		[JsonIgnore]
		string Aggregations.IAggregationContainerVariant.AggregationContainerVariantName => "extended_stats";
		[JsonInclude]
		[JsonPropertyName("sigma")]
		public double? Sigma { get; set; }
	}

	public sealed partial class ExtendedStatsAggregationDescriptor<T> : DescriptorBase<ExtendedStatsAggregationDescriptor<T>>
	{
		public ExtendedStatsAggregationDescriptor()
		{
		}

		internal ExtendedStatsAggregationDescriptor(Action<ExtendedStatsAggregationDescriptor<T>> configure) => configure.Invoke(this);
		internal double? SigmaValue { get; private set; }

		internal string? FormatValue { get; private set; }

		public ExtendedStatsAggregationDescriptor<T> Sigma(double? sigma) => Assign(sigma, (a, v) => a.SigmaValue = v);
		public ExtendedStatsAggregationDescriptor<T> Format(string? format) => Assign(format, (a, v) => a.FormatValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("extended_stats");
			writer.WriteStartObject();
			if (SigmaValue.HasValue)
			{
				writer.WritePropertyName("sigma");
				writer.WriteNumberValue(SigmaValue.Value);
			}

			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}
}
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
	public partial class ExtendedStatsBucketAggregation : Aggregations.PipelineAggregationBase, IAggregationContainerVariant
	{
		public ExtendedStatsBucketAggregation(string name) : base(name)
		{
		}

		[JsonIgnore]
		string Aggregations.IAggregationContainerVariant.AggregationContainerVariantName => "extended_stats_bucket";
		[JsonInclude]
		[JsonPropertyName("sigma")]
		public double? Sigma { get; set; }
	}

	public sealed partial class ExtendedStatsBucketAggregationDescriptor : DescriptorBase<ExtendedStatsBucketAggregationDescriptor>
	{
		public ExtendedStatsBucketAggregationDescriptor()
		{
		}

		internal ExtendedStatsBucketAggregationDescriptor(Action<ExtendedStatsBucketAggregationDescriptor> configure) => configure.Invoke(this);
		internal double? SigmaValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.BucketsPath? BucketsPathValue { get; private set; }

		internal string? FormatValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? GapPolicyValue { get; private set; }

		public ExtendedStatsBucketAggregationDescriptor Sigma(double? sigma) => Assign(sigma, (a, v) => a.SigmaValue = v);
		public ExtendedStatsBucketAggregationDescriptor BucketsPath(Elastic.Clients.Elasticsearch.Aggregations.BucketsPath? bucketsPath) => Assign(bucketsPath, (a, v) => a.BucketsPathValue = v);
		public ExtendedStatsBucketAggregationDescriptor Format(string? format) => Assign(format, (a, v) => a.FormatValue = v);
		public ExtendedStatsBucketAggregationDescriptor GapPolicy(Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? gapPolicy) => Assign(gapPolicy, (a, v) => a.GapPolicyValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("extended_stats_bucket");
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
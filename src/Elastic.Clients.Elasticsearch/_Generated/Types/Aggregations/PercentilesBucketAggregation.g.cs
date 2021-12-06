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
	internal sealed class PercentilesBucketAggregationConverter : JsonConverter<PercentilesBucketAggregation>
	{
		public override PercentilesBucketAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			return new PercentilesBucketAggregation("");
		}

		public override void Write(Utf8JsonWriter writer, PercentilesBucketAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("percentiles_bucket");
			writer.WriteStartObject();
			writer.WriteEndObject();
			if (value.Meta is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, value.Meta, options);
			}

			if (value.Percents is not null)
			{
				writer.WritePropertyName("percents");
				JsonSerializer.Serialize(writer, value.Percents, options);
			}

			if (value.BucketsPath is not null)
			{
				writer.WritePropertyName("buckets_path");
				JsonSerializer.Serialize(writer, value.BucketsPath, options);
			}

			if (!string.IsNullOrEmpty(value.Format))
			{
				writer.WritePropertyName("format");
				writer.WriteStringValue(value.Format);
			}

			if (value.GapPolicy is not null)
			{
				writer.WritePropertyName("gap_policy");
				JsonSerializer.Serialize(writer, value.GapPolicy, options);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(PercentilesBucketAggregationConverter))]
	public partial class PercentilesBucketAggregation : Aggregations.PipelineAggregationBase, IAggregationContainerVariant
	{
		[JsonConstructor]
		public PercentilesBucketAggregation(string name) : base(name)
		{
		}

		[JsonIgnore]
		string Aggregations.IAggregationContainerVariant.AggregationContainerVariantName => "percentiles_bucket";
		[JsonInclude]
		[JsonPropertyName("percents")]
		public IEnumerable<double>? Percents { get; set; }
	}

	public sealed partial class PercentilesBucketAggregationDescriptor : DescriptorBase<PercentilesBucketAggregationDescriptor>
	{
		public PercentilesBucketAggregationDescriptor()
		{
		}

		internal PercentilesBucketAggregationDescriptor(Action<PercentilesBucketAggregationDescriptor> configure) => configure.Invoke(this);
		internal IEnumerable<double>? PercentsValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.BucketsPath? BucketsPathValue { get; private set; }

		internal string? FormatValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? GapPolicyValue { get; private set; }

		internal Dictionary<string, object>? MetaValue { get; private set; }

		public PercentilesBucketAggregationDescriptor Percents(IEnumerable<double>? percents) => Assign(percents, (a, v) => a.PercentsValue = v);
		public PercentilesBucketAggregationDescriptor BucketsPath(Elastic.Clients.Elasticsearch.Aggregations.BucketsPath? bucketsPath) => Assign(bucketsPath, (a, v) => a.BucketsPathValue = v);
		public PercentilesBucketAggregationDescriptor Format(string? format) => Assign(format, (a, v) => a.FormatValue = v);
		public PercentilesBucketAggregationDescriptor GapPolicy(Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? gapPolicy) => Assign(gapPolicy, (a, v) => a.GapPolicyValue = v);
		public PercentilesBucketAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) => Assign(selector, (a, v) => a.MetaValue = v?.Invoke(new FluentDictionary<string, object>()));
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("percentiles_bucket");
			writer.WriteStartObject();
			if (PercentsValue is not null)
			{
				writer.WritePropertyName("percents");
				JsonSerializer.Serialize(writer, PercentsValue, options);
			}

			if (BucketsPathValue is not null)
			{
				writer.WritePropertyName("buckets_path");
				JsonSerializer.Serialize(writer, BucketsPathValue, options);
			}

			if (!string.IsNullOrEmpty(FormatValue))
			{
				writer.WritePropertyName("format");
				writer.WriteStringValue(FormatValue);
			}

			if (GapPolicyValue is not null)
			{
				writer.WritePropertyName("gap_policy");
				JsonSerializer.Serialize(writer, GapPolicyValue, options);
			}

			writer.WriteEndObject();
			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			writer.WriteEndObject();
		}
	}
}
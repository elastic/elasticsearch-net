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
	internal sealed class InferenceAggregationConverter : JsonConverter<InferenceAggregation>
	{
		public override InferenceAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var agg = new InferenceAggregation("");
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("model_id"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Name>(ref reader, options);
						if (value is not null)
						{
							agg.ModelId = value;
						}
					}

					if (reader.ValueTextEquals("inference_config"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.InferenceConfigContainer?>(ref reader, options);
						if (value is not null)
						{
							agg.InferenceConfig = value;
						}
					}

					if (reader.ValueTextEquals("buckets_path"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.BucketsPath?>(ref reader, options);
						if (value is not null)
						{
							agg.BucketsPath = value;
						}
					}

					if (reader.ValueTextEquals("format"))
					{
						var value = JsonSerializer.Deserialize<string?>(ref reader, options);
						if (value is not null)
						{
							agg.Format = value;
						}
					}

					if (reader.ValueTextEquals("gap_policy"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GapPolicy?>(ref reader, options);
						if (value is not null)
						{
							agg.GapPolicy = value;
						}
					}
				}
			}

			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("meta"))
					{
						var value = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
						if (value is not null)
						{
							agg.Meta = value;
						}
					}
				}
			}

			reader.Read();
			return agg;
		}

		public override void Write(Utf8JsonWriter writer, InferenceAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("inference");
			writer.WriteStartObject();
			writer.WritePropertyName("model_id");
			JsonSerializer.Serialize(writer, value.ModelId, options);
			if (value.InferenceConfig is not null)
			{
				writer.WritePropertyName("inference_config");
				JsonSerializer.Serialize(writer, value.InferenceConfig, options);
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
			if (value.Meta is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, value.Meta, options);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(InferenceAggregationConverter))]
	public partial class InferenceAggregation : Aggregations.PipelineAggregationBase
	{
		public InferenceAggregation(string name) : base(name)
		{
		}

		[JsonInclude]
		[JsonPropertyName("model_id")]
		public Elastic.Clients.Elasticsearch.Name ModelId { get; set; }

		[JsonInclude]
		[JsonPropertyName("inference_config")]
		public Elastic.Clients.Elasticsearch.Aggregations.InferenceConfigContainer? InferenceConfig { get; set; }
	}

	public sealed partial class InferenceAggregationDescriptor<T> : DescriptorBase<InferenceAggregationDescriptor<T>>
	{
		public InferenceAggregationDescriptor()
		{
		}

		internal InferenceAggregationDescriptor(Action<InferenceAggregationDescriptor<T>> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.Name ModelIdValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.InferenceConfigContainer? InferenceConfigValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.BucketsPath? BucketsPathValue { get; private set; }

		internal string? FormatValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? GapPolicyValue { get; private set; }

		internal Dictionary<string, object>? MetaValue { get; private set; }

		internal InferenceConfigContainerDescriptor<T> InferenceConfigDescriptor { get; private set; }

		internal Action<InferenceConfigContainerDescriptor<T>> InferenceConfigDescriptorAction { get; private set; }

		public InferenceAggregationDescriptor<T> ModelId(Elastic.Clients.Elasticsearch.Name modelId) => Assign(modelId, (a, v) => a.ModelIdValue = v);
		public InferenceAggregationDescriptor<T> InferenceConfig(Elastic.Clients.Elasticsearch.Aggregations.InferenceConfigContainer? inferenceConfig)
		{
			InferenceConfigDescriptor = null;
			InferenceConfigDescriptorAction = null;
			return Assign(inferenceConfig, (a, v) => a.InferenceConfigValue = v);
		}

		public InferenceAggregationDescriptor<T> InferenceConfig(Aggregations.InferenceConfigContainerDescriptor<T> descriptor)
		{
			InferenceConfigValue = null;
			InferenceConfigDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.InferenceConfigDescriptor = v);
		}

		public InferenceAggregationDescriptor<T> InferenceConfig(Action<Aggregations.InferenceConfigContainerDescriptor<T>> configure)
		{
			InferenceConfigValue = null;
			InferenceConfigDescriptorAction = null;
			return Assign(configure, (a, v) => a.InferenceConfigDescriptorAction = v);
		}

		public InferenceAggregationDescriptor<T> BucketsPath(Elastic.Clients.Elasticsearch.Aggregations.BucketsPath? bucketsPath) => Assign(bucketsPath, (a, v) => a.BucketsPathValue = v);
		public InferenceAggregationDescriptor<T> Format(string? format) => Assign(format, (a, v) => a.FormatValue = v);
		public InferenceAggregationDescriptor<T> GapPolicy(Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? gapPolicy) => Assign(gapPolicy, (a, v) => a.GapPolicyValue = v);
		public InferenceAggregationDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) => Assign(selector, (a, v) => a.MetaValue = v?.Invoke(new FluentDictionary<string, object>()));
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("inference");
			writer.WriteStartObject();
			writer.WritePropertyName("model_id");
			JsonSerializer.Serialize(writer, ModelIdValue, options);
			if (InferenceConfigDescriptor is not null)
			{
				writer.WritePropertyName("inference_config");
				JsonSerializer.Serialize(writer, InferenceConfigDescriptor, options);
			}
			else if (InferenceConfigDescriptorAction is not null)
			{
				writer.WritePropertyName("inference_config");
				JsonSerializer.Serialize(writer, new Aggregations.InferenceConfigContainerDescriptor<T>(InferenceConfigDescriptorAction), options);
			}
			else if (InferenceConfigValue is not null)
			{
				writer.WritePropertyName("inference_config");
				JsonSerializer.Serialize(writer, InferenceConfigValue, options);
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
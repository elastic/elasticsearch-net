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
	internal sealed class CumulativeCardinalityAggregationConverter : JsonConverter<CumulativeCardinalityAggregation>
	{
		public override CumulativeCardinalityAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var variant = new CumulativeCardinalityAggregation();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var property = reader.GetString();
					if (property == "buckets_path")
					{
						variant.BucketsPath = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.BucketsPath?>(ref reader, options);
						continue;
					}

					if (property == "format")
					{
						variant.Format = JsonSerializer.Deserialize<string?>(ref reader, options);
						continue;
					}

					if (property == "gap_policy")
					{
						variant.GapPolicy = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GapPolicy?>(ref reader, options);
						continue;
					}

					if (property == "meta")
					{
						variant.Meta = JsonSerializer.Deserialize<Dictionary<string, object>?>(ref reader, options);
						continue;
					}

					if (property == "name")
					{
						variant.Name = JsonSerializer.Deserialize<string?>(ref reader, options);
						continue;
					}
				}
			}

			return variant;
		}

		public override void Write(Utf8JsonWriter writer, CumulativeCardinalityAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
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

			if (value.Meta is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, value.Meta, options);
			}

			if (!string.IsNullOrEmpty(value.Name))
			{
				writer.WritePropertyName("name");
				writer.WriteStringValue(value.Name);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(CumulativeCardinalityAggregationConverter))]
	public sealed partial class CumulativeCardinalityAggregation : Aggregation
	{
		public CumulativeCardinalityAggregation(string name) => Name = name;
		internal CumulativeCardinalityAggregation()
		{
		}

		public Elastic.Clients.Elasticsearch.Aggregations.BucketsPath? BucketsPath { get; set; }

		public string? Format { get; set; }

		public Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? GapPolicy { get; set; }

		public Dictionary<string, object>? Meta { get; set; }

		public override string? Name { get; internal set; }
	}

	public sealed partial class CumulativeCardinalityAggregationDescriptor : SerializableDescriptorBase<CumulativeCardinalityAggregationDescriptor>
	{
		internal CumulativeCardinalityAggregationDescriptor(Action<CumulativeCardinalityAggregationDescriptor> configure) => configure.Invoke(this);
		public CumulativeCardinalityAggregationDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Aggregations.BucketsPath? BucketsPathValue { get; set; }

		private string? FormatValue { get; set; }

		private Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? GapPolicyValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		public CumulativeCardinalityAggregationDescriptor BucketsPath(Elastic.Clients.Elasticsearch.Aggregations.BucketsPath? bucketsPath)
		{
			BucketsPathValue = bucketsPath;
			return Self;
		}

		public CumulativeCardinalityAggregationDescriptor Format(string? format)
		{
			FormatValue = format;
			return Self;
		}

		public CumulativeCardinalityAggregationDescriptor GapPolicy(Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? gapPolicy)
		{
			GapPolicyValue = gapPolicy;
			return Self;
		}

		public CumulativeCardinalityAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("cumulative_cardinality");
			writer.WriteStartObject();
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
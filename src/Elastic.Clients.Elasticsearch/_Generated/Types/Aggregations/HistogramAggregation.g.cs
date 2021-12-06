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
	internal sealed class HistogramAggregationConverter : JsonConverter<HistogramAggregation>
	{
		public override HistogramAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var agg = new HistogramAggregation("");
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("field"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Field?>(ref reader, options);
						if (value is not null)
						{
							agg.Field = value;
						}
					}

					if (reader.ValueTextEquals("interval"))
					{
						var value = JsonSerializer.Deserialize<double?>(ref reader, options);
						if (value is not null)
						{
							agg.Interval = value;
						}
					}

					if (reader.ValueTextEquals("min_doc_count"))
					{
						var value = JsonSerializer.Deserialize<int?>(ref reader, options);
						if (value is not null)
						{
							agg.MinDocCount = value;
						}
					}

					if (reader.ValueTextEquals("missing"))
					{
						var value = JsonSerializer.Deserialize<double?>(ref reader, options);
						if (value is not null)
						{
							agg.Missing = value;
						}
					}

					if (reader.ValueTextEquals("offset"))
					{
						var value = JsonSerializer.Deserialize<double?>(ref reader, options);
						if (value is not null)
						{
							agg.Offset = value;
						}
					}

					if (reader.ValueTextEquals("order"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.HistogramOrder?>(ref reader, options);
						if (value is not null)
						{
							agg.Order = value;
						}
					}

					if (reader.ValueTextEquals("script"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Script?>(ref reader, options);
						if (value is not null)
						{
							agg.Script = value;
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

					if (reader.ValueTextEquals("keyed"))
					{
						var value = JsonSerializer.Deserialize<bool?>(ref reader, options);
						if (value is not null)
						{
							agg.Keyed = value;
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

					if (reader.ValueTextEquals("aggs") || reader.ValueTextEquals("aggregations"))
					{
						var value = JsonSerializer.Deserialize<AggregationDictionary>(ref reader, options);
						if (value is not null)
						{
							agg.Aggregations = value;
						}
					}
				}
			}

			reader.Read();
			return agg;
		}

		public override void Write(Utf8JsonWriter writer, HistogramAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("histogram");
			writer.WriteStartObject();
			if (value.Field is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, value.Field, options);
			}

			if (value.Interval.HasValue)
			{
				writer.WritePropertyName("interval");
				writer.WriteNumberValue(value.Interval.Value);
			}

			if (value.MinDocCount.HasValue)
			{
				writer.WritePropertyName("min_doc_count");
				writer.WriteNumberValue(value.MinDocCount.Value);
			}

			if (value.Missing.HasValue)
			{
				writer.WritePropertyName("missing");
				writer.WriteNumberValue(value.Missing.Value);
			}

			if (value.Offset.HasValue)
			{
				writer.WritePropertyName("offset");
				writer.WriteNumberValue(value.Offset.Value);
			}

			if (value.Order is not null)
			{
				writer.WritePropertyName("order");
				JsonSerializer.Serialize(writer, value.Order, options);
			}

			if (value.Script is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, value.Script, options);
			}

			if (!string.IsNullOrEmpty(value.Format))
			{
				writer.WritePropertyName("format");
				writer.WriteStringValue(value.Format);
			}

			if (value.Keyed.HasValue)
			{
				writer.WritePropertyName("keyed");
				writer.WriteBooleanValue(value.Keyed.Value);
			}

			writer.WriteEndObject();
			if (value.Meta is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, value.Meta, options);
			}

			if (value.Aggregations is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, value.Aggregations, options);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(HistogramAggregationConverter))]
	public partial class HistogramAggregation : Aggregations.BucketAggregationBase, TransformManagement.IPivotGroupByContainerVariant
	{
		public HistogramAggregation(string name) : base(name)
		{
		}

		[JsonIgnore]
		string TransformManagement.IPivotGroupByContainerVariant.PivotGroupByContainerVariantName => "histogram";
		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("interval")]
		public double? Interval { get; set; }

		[JsonInclude]
		[JsonPropertyName("min_doc_count")]
		public int? MinDocCount { get; set; }

		[JsonInclude]
		[JsonPropertyName("missing")]
		public double? Missing { get; set; }

		[JsonInclude]
		[JsonPropertyName("offset")]
		public double? Offset { get; set; }

		[JsonInclude]
		[JsonPropertyName("order")]
		public Elastic.Clients.Elasticsearch.Aggregations.HistogramOrder? Order { get; set; }

		[JsonInclude]
		[JsonPropertyName("script")]
		public Elastic.Clients.Elasticsearch.Script? Script { get; set; }

		[JsonInclude]
		[JsonPropertyName("format")]
		public string? Format { get; set; }

		[JsonInclude]
		[JsonPropertyName("keyed")]
		public bool? Keyed { get; set; }
	}

	public sealed partial class HistogramAggregationDescriptor<T> : DescriptorBase<HistogramAggregationDescriptor<T>>
	{
		public HistogramAggregationDescriptor()
		{
		}

		internal HistogramAggregationDescriptor(Action<HistogramAggregationDescriptor<T>> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.Field? FieldValue { get; private set; }

		internal double? IntervalValue { get; private set; }

		internal int? MinDocCountValue { get; private set; }

		internal double? MissingValue { get; private set; }

		internal double? OffsetValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.HistogramOrder? OrderValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Script? ScriptValue { get; private set; }

		internal string? FormatValue { get; private set; }

		internal bool? KeyedValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? AggregationsValue { get; private set; }

		internal Dictionary<string, object>? MetaValue { get; private set; }

		internal HistogramOrderDescriptor OrderDescriptor { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor<T> AggregationsDescriptor { get; private set; }

		internal Action<HistogramOrderDescriptor> OrderDescriptorAction { get; private set; }

		internal Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor<T>> AggregationsDescriptorAction { get; private set; }

		public HistogramAggregationDescriptor<T> Field(Elastic.Clients.Elasticsearch.Field? field) => Assign(field, (a, v) => a.FieldValue = v);
		public HistogramAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.FieldValue = v);
		public HistogramAggregationDescriptor<T> Interval(double? interval) => Assign(interval, (a, v) => a.IntervalValue = v);
		public HistogramAggregationDescriptor<T> MinDocCount(int? minDocCount) => Assign(minDocCount, (a, v) => a.MinDocCountValue = v);
		public HistogramAggregationDescriptor<T> Missing(double? missing) => Assign(missing, (a, v) => a.MissingValue = v);
		public HistogramAggregationDescriptor<T> Offset(double? offset) => Assign(offset, (a, v) => a.OffsetValue = v);
		public HistogramAggregationDescriptor<T> Order(Elastic.Clients.Elasticsearch.Aggregations.HistogramOrder? order)
		{
			OrderDescriptor = null;
			OrderDescriptorAction = null;
			return Assign(order, (a, v) => a.OrderValue = v);
		}

		public HistogramAggregationDescriptor<T> Order(Aggregations.HistogramOrderDescriptor descriptor)
		{
			OrderValue = null;
			OrderDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.OrderDescriptor = v);
		}

		public HistogramAggregationDescriptor<T> Order(Action<Aggregations.HistogramOrderDescriptor> configure)
		{
			OrderValue = null;
			OrderDescriptorAction = null;
			return Assign(configure, (a, v) => a.OrderDescriptorAction = v);
		}

		public HistogramAggregationDescriptor<T> Script(Elastic.Clients.Elasticsearch.Script? script) => Assign(script, (a, v) => a.ScriptValue = v);
		public HistogramAggregationDescriptor<T> Format(string? format) => Assign(format, (a, v) => a.FormatValue = v);
		public HistogramAggregationDescriptor<T> Keyed(bool? keyed = true) => Assign(keyed, (a, v) => a.KeyedValue = v);
		public HistogramAggregationDescriptor<T> Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? aggregations)
		{
			AggregationsDescriptor = null;
			AggregationsDescriptorAction = null;
			return Assign(aggregations, (a, v) => a.AggregationsValue = v);
		}

		public HistogramAggregationDescriptor<T> Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor<T> descriptor)
		{
			AggregationsValue = null;
			AggregationsDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.AggregationsDescriptor = v);
		}

		public HistogramAggregationDescriptor<T> Aggregations(Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor<T>> configure)
		{
			AggregationsValue = null;
			AggregationsDescriptorAction = null;
			return Assign(configure, (a, v) => a.AggregationsDescriptorAction = v);
		}

		public HistogramAggregationDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) => Assign(selector, (a, v) => a.MetaValue = v?.Invoke(new FluentDictionary<string, object>()));
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("histogram");
			writer.WriteStartObject();
			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (IntervalValue.HasValue)
			{
				writer.WritePropertyName("interval");
				writer.WriteNumberValue(IntervalValue.Value);
			}

			if (MinDocCountValue.HasValue)
			{
				writer.WritePropertyName("min_doc_count");
				writer.WriteNumberValue(MinDocCountValue.Value);
			}

			if (MissingValue.HasValue)
			{
				writer.WritePropertyName("missing");
				writer.WriteNumberValue(MissingValue.Value);
			}

			if (OffsetValue.HasValue)
			{
				writer.WritePropertyName("offset");
				writer.WriteNumberValue(OffsetValue.Value);
			}

			if (OrderDescriptor is not null)
			{
				writer.WritePropertyName("order");
				JsonSerializer.Serialize(writer, OrderDescriptor, options);
			}
			else if (OrderDescriptorAction is not null)
			{
				writer.WritePropertyName("order");
				JsonSerializer.Serialize(writer, new Aggregations.HistogramOrderDescriptor(OrderDescriptorAction), options);
			}
			else if (OrderValue is not null)
			{
				writer.WritePropertyName("order");
				JsonSerializer.Serialize(writer, OrderValue, options);
			}

			if (ScriptValue is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
			}

			if (!string.IsNullOrEmpty(FormatValue))
			{
				writer.WritePropertyName("format");
				writer.WriteStringValue(FormatValue);
			}

			if (KeyedValue.HasValue)
			{
				writer.WritePropertyName("keyed");
				writer.WriteBooleanValue(KeyedValue.Value);
			}

			writer.WriteEndObject();
			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			if (AggregationsDescriptor is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, AggregationsDescriptor, options);
			}
			else if (AggregationsDescriptorAction is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, new AggregationContainerDescriptor<T>(AggregationsDescriptorAction), options);
			}
			else if (AggregationsValue is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, AggregationsValue, options);
			}

			writer.WriteEndObject();
		}
	}
}
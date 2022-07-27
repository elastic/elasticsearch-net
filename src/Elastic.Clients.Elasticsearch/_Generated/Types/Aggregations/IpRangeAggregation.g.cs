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
	internal sealed class IpRangeAggregationConverter : JsonConverter<IpRangeAggregation>
	{
		public override IpRangeAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			reader.Read();
			var aggName = reader.GetString();
			if (aggName != "ip_range")
				throw new JsonException("Unexpected JSON detected.");
			var agg = new IpRangeAggregation(aggName);
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("field"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Field?>(ref reader, options);
						if (value is not null)
						{
							agg.Field = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("ranges"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.IpRangeAggregationRange>?>(ref reader, options);
						if (value is not null)
						{
							agg.Ranges = value;
						}

						continue;
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

						continue;
					}

					if (reader.ValueTextEquals("aggs") || reader.ValueTextEquals("aggregations"))
					{
						var value = JsonSerializer.Deserialize<AggregationDictionary>(ref reader, options);
						if (value is not null)
						{
							agg.Aggregations = value;
						}

						continue;
					}
				}
			}

			return agg;
		}

		public override void Write(Utf8JsonWriter writer, IpRangeAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("ip_range");
			writer.WriteStartObject();
			if (value.Field is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, value.Field, options);
			}

			if (value.Ranges is not null)
			{
				writer.WritePropertyName("ranges");
				JsonSerializer.Serialize(writer, value.Ranges, options);
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

	[JsonConverter(typeof(IpRangeAggregationConverter))]
	public sealed partial class IpRangeAggregation : Aggregation
	{
		public IpRangeAggregation(string name) => Name = name;
		internal IpRangeAggregation()
		{
		}

		public Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? Aggregations { get; set; }

		public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

		public Dictionary<string, object>? Meta { get; set; }

		public override string? Name { get; internal set; }

		public IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.IpRangeAggregationRange>? Ranges { get; set; }
	}

	public sealed partial class IpRangeAggregationDescriptor<TDocument> : SerializableDescriptorBase<IpRangeAggregationDescriptor<TDocument>>
	{
		internal IpRangeAggregationDescriptor(Action<IpRangeAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);
		public IpRangeAggregationDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? AggregationsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor<TDocument> AggregationsDescriptor { get; set; }

		private Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor<TDocument>> AggregationsDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.IpRangeAggregationRange>? RangesValue { get; set; }

		private IpRangeAggregationRangeDescriptor RangesDescriptor { get; set; }

		private Action<IpRangeAggregationRangeDescriptor> RangesDescriptorAction { get; set; }

		private Action<IpRangeAggregationRangeDescriptor>[] RangesDescriptorActions { get; set; }

		public IpRangeAggregationDescriptor<TDocument> Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? aggregations)
		{
			AggregationsDescriptor = null;
			AggregationsDescriptorAction = null;
			AggregationsValue = aggregations;
			return Self;
		}

		public IpRangeAggregationDescriptor<TDocument> Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor<TDocument> descriptor)
		{
			AggregationsValue = null;
			AggregationsDescriptorAction = null;
			AggregationsDescriptor = descriptor;
			return Self;
		}

		public IpRangeAggregationDescriptor<TDocument> Aggregations(Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor<TDocument>> configure)
		{
			AggregationsValue = null;
			AggregationsDescriptor = null;
			AggregationsDescriptorAction = configure;
			return Self;
		}

		public IpRangeAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public IpRangeAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public IpRangeAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public IpRangeAggregationDescriptor<TDocument> Ranges(IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.IpRangeAggregationRange>? ranges)
		{
			RangesDescriptor = null;
			RangesDescriptorAction = null;
			RangesDescriptorActions = null;
			RangesValue = ranges;
			return Self;
		}

		public IpRangeAggregationDescriptor<TDocument> Ranges(IpRangeAggregationRangeDescriptor descriptor)
		{
			RangesValue = null;
			RangesDescriptorAction = null;
			RangesDescriptorActions = null;
			RangesDescriptor = descriptor;
			return Self;
		}

		public IpRangeAggregationDescriptor<TDocument> Ranges(Action<IpRangeAggregationRangeDescriptor> configure)
		{
			RangesValue = null;
			RangesDescriptor = null;
			RangesDescriptorActions = null;
			RangesDescriptorAction = configure;
			return Self;
		}

		public IpRangeAggregationDescriptor<TDocument> Ranges(params Action<IpRangeAggregationRangeDescriptor>[] configure)
		{
			RangesValue = null;
			RangesDescriptor = null;
			RangesDescriptorAction = null;
			RangesDescriptorActions = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("ip_range");
			writer.WriteStartObject();
			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (RangesDescriptor is not null)
			{
				writer.WritePropertyName("ranges");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, RangesDescriptor, options);
				writer.WriteEndArray();
			}
			else if (RangesDescriptorAction is not null)
			{
				writer.WritePropertyName("ranges");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, new IpRangeAggregationRangeDescriptor(RangesDescriptorAction), options);
				writer.WriteEndArray();
			}
			else if (RangesDescriptorActions is not null)
			{
				writer.WritePropertyName("ranges");
				writer.WriteStartArray();
				foreach (var action in RangesDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new IpRangeAggregationRangeDescriptor(action), options);
				}

				writer.WriteEndArray();
			}
			else if (RangesValue is not null)
			{
				writer.WritePropertyName("ranges");
				JsonSerializer.Serialize(writer, RangesValue, options);
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
				JsonSerializer.Serialize(writer, new AggregationContainerDescriptor<TDocument>(AggregationsDescriptorAction), options);
			}
			else if (AggregationsValue is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, AggregationsValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class IpRangeAggregationDescriptor : SerializableDescriptorBase<IpRangeAggregationDescriptor>
	{
		internal IpRangeAggregationDescriptor(Action<IpRangeAggregationDescriptor> configure) => configure.Invoke(this);
		public IpRangeAggregationDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? AggregationsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor AggregationsDescriptor { get; set; }

		private Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor> AggregationsDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.IpRangeAggregationRange>? RangesValue { get; set; }

		private IpRangeAggregationRangeDescriptor RangesDescriptor { get; set; }

		private Action<IpRangeAggregationRangeDescriptor> RangesDescriptorAction { get; set; }

		private Action<IpRangeAggregationRangeDescriptor>[] RangesDescriptorActions { get; set; }

		public IpRangeAggregationDescriptor Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? aggregations)
		{
			AggregationsDescriptor = null;
			AggregationsDescriptorAction = null;
			AggregationsValue = aggregations;
			return Self;
		}

		public IpRangeAggregationDescriptor Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor descriptor)
		{
			AggregationsValue = null;
			AggregationsDescriptorAction = null;
			AggregationsDescriptor = descriptor;
			return Self;
		}

		public IpRangeAggregationDescriptor Aggregations(Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor> configure)
		{
			AggregationsValue = null;
			AggregationsDescriptor = null;
			AggregationsDescriptorAction = configure;
			return Self;
		}

		public IpRangeAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public IpRangeAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public IpRangeAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public IpRangeAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public IpRangeAggregationDescriptor Ranges(IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.IpRangeAggregationRange>? ranges)
		{
			RangesDescriptor = null;
			RangesDescriptorAction = null;
			RangesDescriptorActions = null;
			RangesValue = ranges;
			return Self;
		}

		public IpRangeAggregationDescriptor Ranges(IpRangeAggregationRangeDescriptor descriptor)
		{
			RangesValue = null;
			RangesDescriptorAction = null;
			RangesDescriptorActions = null;
			RangesDescriptor = descriptor;
			return Self;
		}

		public IpRangeAggregationDescriptor Ranges(Action<IpRangeAggregationRangeDescriptor> configure)
		{
			RangesValue = null;
			RangesDescriptor = null;
			RangesDescriptorActions = null;
			RangesDescriptorAction = configure;
			return Self;
		}

		public IpRangeAggregationDescriptor Ranges(params Action<IpRangeAggregationRangeDescriptor>[] configure)
		{
			RangesValue = null;
			RangesDescriptor = null;
			RangesDescriptorAction = null;
			RangesDescriptorActions = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("ip_range");
			writer.WriteStartObject();
			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (RangesDescriptor is not null)
			{
				writer.WritePropertyName("ranges");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, RangesDescriptor, options);
				writer.WriteEndArray();
			}
			else if (RangesDescriptorAction is not null)
			{
				writer.WritePropertyName("ranges");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, new IpRangeAggregationRangeDescriptor(RangesDescriptorAction), options);
				writer.WriteEndArray();
			}
			else if (RangesDescriptorActions is not null)
			{
				writer.WritePropertyName("ranges");
				writer.WriteStartArray();
				foreach (var action in RangesDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new IpRangeAggregationRangeDescriptor(action), options);
				}

				writer.WriteEndArray();
			}
			else if (RangesValue is not null)
			{
				writer.WritePropertyName("ranges");
				JsonSerializer.Serialize(writer, RangesValue, options);
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
				JsonSerializer.Serialize(writer, new AggregationContainerDescriptor(AggregationsDescriptorAction), options);
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
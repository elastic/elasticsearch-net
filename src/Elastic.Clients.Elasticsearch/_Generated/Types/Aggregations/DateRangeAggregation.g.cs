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
	internal sealed class DateRangeAggregationConverter : JsonConverter<DateRangeAggregation>
	{
		public override DateRangeAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var variant = new DateRangeAggregation();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var property = reader.GetString();
					if (property == "field")
					{
						variant.Field = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Field?>(ref reader, options);
						continue;
					}

					if (property == "format")
					{
						variant.Format = JsonSerializer.Deserialize<string?>(ref reader, options);
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

					if (property == "ranges")
					{
						variant.Ranges = JsonSerializer.Deserialize<IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.DateRangeExpression>?>(ref reader, options);
						continue;
					}

					if (property == "time_zone")
					{
						variant.TimeZone = JsonSerializer.Deserialize<string?>(ref reader, options);
						continue;
					}
				}
			}

			return variant;
		}

		public override void Write(Utf8JsonWriter writer, DateRangeAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value.Field is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, value.Field, options);
			}

			if (!string.IsNullOrEmpty(value.Format))
			{
				writer.WritePropertyName("format");
				writer.WriteStringValue(value.Format);
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

			if (value.Ranges is not null)
			{
				writer.WritePropertyName("ranges");
				JsonSerializer.Serialize(writer, value.Ranges, options);
			}

			if (value.TimeZone is not null)
			{
				writer.WritePropertyName("time_zone");
				JsonSerializer.Serialize(writer, value.TimeZone, options);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(DateRangeAggregationConverter))]
	public sealed partial class DateRangeAggregation : Aggregation
	{
		public DateRangeAggregation(string name) => Name = name;
		internal DateRangeAggregation()
		{
		}

		public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

		public string? Format { get; set; }

		public Dictionary<string, object>? Meta { get; set; }

		public override string? Name { get; internal set; }

		public IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.DateRangeExpression>? Ranges { get; set; }

		public string? TimeZone { get; set; }
	}

	public sealed partial class DateRangeAggregationDescriptor<TDocument> : SerializableDescriptorBase<DateRangeAggregationDescriptor<TDocument>>
	{
		internal DateRangeAggregationDescriptor(Action<DateRangeAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);
		public DateRangeAggregationDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private string? FormatValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.DateRangeExpression>? RangesValue { get; set; }

		private DateRangeExpressionDescriptor RangesDescriptor { get; set; }

		private Action<DateRangeExpressionDescriptor> RangesDescriptorAction { get; set; }

		private Action<DateRangeExpressionDescriptor>[] RangesDescriptorActions { get; set; }

		private string? TimeZoneValue { get; set; }

		public DateRangeAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public DateRangeAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public DateRangeAggregationDescriptor<TDocument> Format(string? format)
		{
			FormatValue = format;
			return Self;
		}

		public DateRangeAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public DateRangeAggregationDescriptor<TDocument> Ranges(IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.DateRangeExpression>? ranges)
		{
			RangesDescriptor = null;
			RangesDescriptorAction = null;
			RangesDescriptorActions = null;
			RangesValue = ranges;
			return Self;
		}

		public DateRangeAggregationDescriptor<TDocument> Ranges(DateRangeExpressionDescriptor descriptor)
		{
			RangesValue = null;
			RangesDescriptorAction = null;
			RangesDescriptorActions = null;
			RangesDescriptor = descriptor;
			return Self;
		}

		public DateRangeAggregationDescriptor<TDocument> Ranges(Action<DateRangeExpressionDescriptor> configure)
		{
			RangesValue = null;
			RangesDescriptor = null;
			RangesDescriptorActions = null;
			RangesDescriptorAction = configure;
			return Self;
		}

		public DateRangeAggregationDescriptor<TDocument> Ranges(params Action<DateRangeExpressionDescriptor>[] configure)
		{
			RangesValue = null;
			RangesDescriptor = null;
			RangesDescriptorAction = null;
			RangesDescriptorActions = configure;
			return Self;
		}

		public DateRangeAggregationDescriptor<TDocument> TimeZone(string? timeZone)
		{
			TimeZoneValue = timeZone;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("date_range");
			writer.WriteStartObject();
			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (!string.IsNullOrEmpty(FormatValue))
			{
				writer.WritePropertyName("format");
				writer.WriteStringValue(FormatValue);
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
				JsonSerializer.Serialize(writer, new DateRangeExpressionDescriptor(RangesDescriptorAction), options);
				writer.WriteEndArray();
			}
			else if (RangesDescriptorActions is not null)
			{
				writer.WritePropertyName("ranges");
				writer.WriteStartArray();
				foreach (var action in RangesDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new DateRangeExpressionDescriptor(action), options);
				}

				writer.WriteEndArray();
			}
			else if (RangesValue is not null)
			{
				writer.WritePropertyName("ranges");
				JsonSerializer.Serialize(writer, RangesValue, options);
			}

			if (TimeZoneValue is not null)
			{
				writer.WritePropertyName("time_zone");
				JsonSerializer.Serialize(writer, TimeZoneValue, options);
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

	public sealed partial class DateRangeAggregationDescriptor : SerializableDescriptorBase<DateRangeAggregationDescriptor>
	{
		internal DateRangeAggregationDescriptor(Action<DateRangeAggregationDescriptor> configure) => configure.Invoke(this);
		public DateRangeAggregationDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private string? FormatValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.DateRangeExpression>? RangesValue { get; set; }

		private DateRangeExpressionDescriptor RangesDescriptor { get; set; }

		private Action<DateRangeExpressionDescriptor> RangesDescriptorAction { get; set; }

		private Action<DateRangeExpressionDescriptor>[] RangesDescriptorActions { get; set; }

		private string? TimeZoneValue { get; set; }

		public DateRangeAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public DateRangeAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public DateRangeAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public DateRangeAggregationDescriptor Format(string? format)
		{
			FormatValue = format;
			return Self;
		}

		public DateRangeAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public DateRangeAggregationDescriptor Ranges(IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.DateRangeExpression>? ranges)
		{
			RangesDescriptor = null;
			RangesDescriptorAction = null;
			RangesDescriptorActions = null;
			RangesValue = ranges;
			return Self;
		}

		public DateRangeAggregationDescriptor Ranges(DateRangeExpressionDescriptor descriptor)
		{
			RangesValue = null;
			RangesDescriptorAction = null;
			RangesDescriptorActions = null;
			RangesDescriptor = descriptor;
			return Self;
		}

		public DateRangeAggregationDescriptor Ranges(Action<DateRangeExpressionDescriptor> configure)
		{
			RangesValue = null;
			RangesDescriptor = null;
			RangesDescriptorActions = null;
			RangesDescriptorAction = configure;
			return Self;
		}

		public DateRangeAggregationDescriptor Ranges(params Action<DateRangeExpressionDescriptor>[] configure)
		{
			RangesValue = null;
			RangesDescriptor = null;
			RangesDescriptorAction = null;
			RangesDescriptorActions = configure;
			return Self;
		}

		public DateRangeAggregationDescriptor TimeZone(string? timeZone)
		{
			TimeZoneValue = timeZone;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("date_range");
			writer.WriteStartObject();
			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (!string.IsNullOrEmpty(FormatValue))
			{
				writer.WritePropertyName("format");
				writer.WriteStringValue(FormatValue);
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
				JsonSerializer.Serialize(writer, new DateRangeExpressionDescriptor(RangesDescriptorAction), options);
				writer.WriteEndArray();
			}
			else if (RangesDescriptorActions is not null)
			{
				writer.WritePropertyName("ranges");
				writer.WriteStartArray();
				foreach (var action in RangesDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new DateRangeExpressionDescriptor(action), options);
				}

				writer.WriteEndArray();
			}
			else if (RangesValue is not null)
			{
				writer.WritePropertyName("ranges");
				JsonSerializer.Serialize(writer, RangesValue, options);
			}

			if (TimeZoneValue is not null)
			{
				writer.WritePropertyName("time_zone");
				JsonSerializer.Serialize(writer, TimeZoneValue, options);
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
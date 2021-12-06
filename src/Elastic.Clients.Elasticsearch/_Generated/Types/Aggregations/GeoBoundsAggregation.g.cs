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
	internal sealed class GeoBoundsAggregationConverter : JsonConverter<GeoBoundsAggregation>
	{
		public override GeoBoundsAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var agg = new GeoBoundsAggregation("");
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("wrap_longitude"))
					{
						var value = JsonSerializer.Deserialize<bool?>(ref reader, options);
						if (value is not null)
						{
							agg.WrapLongitude = value;
						}
					}

					if (reader.ValueTextEquals("field"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Field?>(ref reader, options);
						if (value is not null)
						{
							agg.Field = value;
						}
					}

					if (reader.ValueTextEquals("missing"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.Missing?>(ref reader, options);
						if (value is not null)
						{
							agg.Missing = value;
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

		public override void Write(Utf8JsonWriter writer, GeoBoundsAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("geo_bounds");
			writer.WriteStartObject();
			if (value.WrapLongitude.HasValue)
			{
				writer.WritePropertyName("wrap_longitude");
				writer.WriteBooleanValue(value.WrapLongitude.Value);
			}

			if (value.Field is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, value.Field, options);
			}

			if (value.Missing is not null)
			{
				writer.WritePropertyName("missing");
				JsonSerializer.Serialize(writer, value.Missing, options);
			}

			if (value.Script is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, value.Script, options);
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

	[JsonConverter(typeof(GeoBoundsAggregationConverter))]
	public partial class GeoBoundsAggregation : Aggregations.MetricAggregationBase
	{
		public GeoBoundsAggregation(string name, Field field) : base(name) => Field = field;
		public GeoBoundsAggregation(string name) : base(name)
		{
		}

		[JsonInclude]
		[JsonPropertyName("wrap_longitude")]
		public bool? WrapLongitude { get; set; }
	}

	public sealed partial class GeoBoundsAggregationDescriptor<T> : DescriptorBase<GeoBoundsAggregationDescriptor<T>>
	{
		public GeoBoundsAggregationDescriptor()
		{
		}

		internal GeoBoundsAggregationDescriptor(Action<GeoBoundsAggregationDescriptor<T>> configure) => configure.Invoke(this);
		internal bool? WrapLongitudeValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Field? FieldValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.Missing? MissingValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Script? ScriptValue { get; private set; }

		internal Dictionary<string, object>? MetaValue { get; private set; }

		public GeoBoundsAggregationDescriptor<T> WrapLongitude(bool? wrapLongitude = true) => Assign(wrapLongitude, (a, v) => a.WrapLongitudeValue = v);
		public GeoBoundsAggregationDescriptor<T> Field(Elastic.Clients.Elasticsearch.Field? field) => Assign(field, (a, v) => a.FieldValue = v);
		public GeoBoundsAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.FieldValue = v);
		public GeoBoundsAggregationDescriptor<T> Missing(Elastic.Clients.Elasticsearch.Aggregations.Missing? missing) => Assign(missing, (a, v) => a.MissingValue = v);
		public GeoBoundsAggregationDescriptor<T> Script(Elastic.Clients.Elasticsearch.Script? script) => Assign(script, (a, v) => a.ScriptValue = v);
		public GeoBoundsAggregationDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) => Assign(selector, (a, v) => a.MetaValue = v?.Invoke(new FluentDictionary<string, object>()));
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("geo_bounds");
			writer.WriteStartObject();
			if (WrapLongitudeValue.HasValue)
			{
				writer.WritePropertyName("wrap_longitude");
				writer.WriteBooleanValue(WrapLongitudeValue.Value);
			}

			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (MissingValue is not null)
			{
				writer.WritePropertyName("missing");
				JsonSerializer.Serialize(writer, MissingValue, options);
			}

			if (ScriptValue is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
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
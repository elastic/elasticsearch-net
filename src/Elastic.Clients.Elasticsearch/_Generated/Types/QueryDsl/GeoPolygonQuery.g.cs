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
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	internal sealed class GeoPolygonQueryConverter : JsonConverter<GeoPolygonQuery>
	{
		public override GeoPolygonQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var variant = new GeoPolygonQuery();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var property = reader.GetString();
					if (property == "ignore_unmapped")
					{
						variant.IgnoreUnmapped = JsonSerializer.Deserialize<bool?>(ref reader, options);
						continue;
					}

					if (property == "validation_method")
					{
						variant.ValidationMethod = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod?>(ref reader, options);
						continue;
					}

					if (property == "_name")
					{
						variant.QueryName = JsonSerializer.Deserialize<string?>(ref reader, options);
						continue;
					}

					if (property == "boost")
					{
						variant.Boost = JsonSerializer.Deserialize<float?>(ref reader, options);
						continue;
					}

					variant.Field = property;
					reader.Read();
					variant.Polygon = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.GeoPolygonPoints>(ref reader, options);
				}
			}

			reader.Read();
			return variant;
		}

		public override void Write(Utf8JsonWriter writer, GeoPolygonQuery value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value.Field is not null && value.Polygon is not null)
			{
				if (!options.TryGetClientSettings(out var settings))
				{
					throw new JsonException("Unable to retrive client settings for JsonSerializerOptions.");
				}

				var propertyName = settings.Inferrer.Field(value.Field);
				writer.WritePropertyName(propertyName);
				JsonSerializer.Serialize(writer, value.Polygon, options);
			}

			if (value.IgnoreUnmapped.HasValue)
			{
				writer.WritePropertyName("ignore_unmapped");
				writer.WriteBooleanValue(value.IgnoreUnmapped.Value);
			}

			if (value.ValidationMethod is not null)
			{
				writer.WritePropertyName("validation_method");
				JsonSerializer.Serialize(writer, value.ValidationMethod, options);
			}

			if (!string.IsNullOrEmpty(value.QueryName))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(value.QueryName);
			}

			if (value.Boost.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(value.Boost.Value);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(GeoPolygonQueryConverter))]
	public partial class GeoPolygonQuery : QueryBase, IQueryVariant
	{
		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_unmapped")]
		public bool? IgnoreUnmapped { get; set; }

		[JsonInclude]
		[JsonPropertyName("polygon")]
		public Elastic.Clients.Elasticsearch.QueryDsl.GeoPolygonPoints Polygon { get; set; }

		[JsonInclude]
		[JsonPropertyName("validation_method")]
		public Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? ValidationMethod { get; set; }
	}

	public sealed partial class GeoPolygonQueryDescriptor<TDocument> : SerializableDescriptorBase<GeoPolygonQueryDescriptor<TDocument>>
	{
		internal GeoPolygonQueryDescriptor(Action<GeoPolygonQueryDescriptor<TDocument>> configure) => configure.Invoke(this);
		public GeoPolygonQueryDescriptor() : base()
		{
		}

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private bool? IgnoreUnmappedValue { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.GeoPolygonPoints PolygonValue { get; set; }

		private GeoPolygonPointsDescriptor PolygonDescriptor { get; set; }

		private Action<GeoPolygonPointsDescriptor> PolygonDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? ValidationMethodValue { get; set; }

		public GeoPolygonQueryDescriptor<TDocument> QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public GeoPolygonQueryDescriptor<TDocument> Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public GeoPolygonQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public GeoPolygonQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public GeoPolygonQueryDescriptor<TDocument> IgnoreUnmapped(bool? ignoreUnmapped = true)
		{
			IgnoreUnmappedValue = ignoreUnmapped;
			return Self;
		}

		public GeoPolygonQueryDescriptor<TDocument> Polygon(Elastic.Clients.Elasticsearch.QueryDsl.GeoPolygonPoints polygon)
		{
			PolygonDescriptor = null;
			PolygonDescriptorAction = null;
			PolygonValue = polygon;
			return Self;
		}

		public GeoPolygonQueryDescriptor<TDocument> Polygon(GeoPolygonPointsDescriptor descriptor)
		{
			PolygonValue = null;
			PolygonDescriptorAction = null;
			PolygonDescriptor = descriptor;
			return Self;
		}

		public GeoPolygonQueryDescriptor<TDocument> Polygon(Action<GeoPolygonPointsDescriptor> configure)
		{
			PolygonValue = null;
			PolygonDescriptor = null;
			PolygonDescriptorAction = configure;
			return Self;
		}

		public GeoPolygonQueryDescriptor<TDocument> ValidationMethod(Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? validationMethod)
		{
			ValidationMethodValue = validationMethod;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(QueryNameValue))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(QueryNameValue);
			}

			if (BoostValue.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(BoostValue.Value);
			}

			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			if (IgnoreUnmappedValue.HasValue)
			{
				writer.WritePropertyName("ignore_unmapped");
				writer.WriteBooleanValue(IgnoreUnmappedValue.Value);
			}

			if (PolygonDescriptor is not null)
			{
				writer.WritePropertyName("polygon");
				JsonSerializer.Serialize(writer, PolygonDescriptor, options);
			}
			else if (PolygonDescriptorAction is not null)
			{
				writer.WritePropertyName("polygon");
				JsonSerializer.Serialize(writer, new GeoPolygonPointsDescriptor(PolygonDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("polygon");
				JsonSerializer.Serialize(writer, PolygonValue, options);
			}

			if (ValidationMethodValue is not null)
			{
				writer.WritePropertyName("validation_method");
				JsonSerializer.Serialize(writer, ValidationMethodValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class GeoPolygonQueryDescriptor : SerializableDescriptorBase<GeoPolygonQueryDescriptor>
	{
		internal GeoPolygonQueryDescriptor(Action<GeoPolygonQueryDescriptor> configure) => configure.Invoke(this);
		public GeoPolygonQueryDescriptor() : base()
		{
		}

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private bool? IgnoreUnmappedValue { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.GeoPolygonPoints PolygonValue { get; set; }

		private GeoPolygonPointsDescriptor PolygonDescriptor { get; set; }

		private Action<GeoPolygonPointsDescriptor> PolygonDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? ValidationMethodValue { get; set; }

		public GeoPolygonQueryDescriptor QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public GeoPolygonQueryDescriptor Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public GeoPolygonQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public GeoPolygonQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public GeoPolygonQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public GeoPolygonQueryDescriptor IgnoreUnmapped(bool? ignoreUnmapped = true)
		{
			IgnoreUnmappedValue = ignoreUnmapped;
			return Self;
		}

		public GeoPolygonQueryDescriptor Polygon(Elastic.Clients.Elasticsearch.QueryDsl.GeoPolygonPoints polygon)
		{
			PolygonDescriptor = null;
			PolygonDescriptorAction = null;
			PolygonValue = polygon;
			return Self;
		}

		public GeoPolygonQueryDescriptor Polygon(GeoPolygonPointsDescriptor descriptor)
		{
			PolygonValue = null;
			PolygonDescriptorAction = null;
			PolygonDescriptor = descriptor;
			return Self;
		}

		public GeoPolygonQueryDescriptor Polygon(Action<GeoPolygonPointsDescriptor> configure)
		{
			PolygonValue = null;
			PolygonDescriptor = null;
			PolygonDescriptorAction = configure;
			return Self;
		}

		public GeoPolygonQueryDescriptor ValidationMethod(Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? validationMethod)
		{
			ValidationMethodValue = validationMethod;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(QueryNameValue))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(QueryNameValue);
			}

			if (BoostValue.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(BoostValue.Value);
			}

			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			if (IgnoreUnmappedValue.HasValue)
			{
				writer.WritePropertyName("ignore_unmapped");
				writer.WriteBooleanValue(IgnoreUnmappedValue.Value);
			}

			if (PolygonDescriptor is not null)
			{
				writer.WritePropertyName("polygon");
				JsonSerializer.Serialize(writer, PolygonDescriptor, options);
			}
			else if (PolygonDescriptorAction is not null)
			{
				writer.WritePropertyName("polygon");
				JsonSerializer.Serialize(writer, new GeoPolygonPointsDescriptor(PolygonDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("polygon");
				JsonSerializer.Serialize(writer, PolygonValue, options);
			}

			if (ValidationMethodValue is not null)
			{
				writer.WritePropertyName("validation_method");
				JsonSerializer.Serialize(writer, ValidationMethodValue, options);
			}

			writer.WriteEndObject();
		}
	}
}
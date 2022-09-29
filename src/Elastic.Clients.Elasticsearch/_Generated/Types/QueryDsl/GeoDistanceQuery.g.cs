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
	internal sealed class GeoDistanceQueryConverter : JsonConverter<GeoDistanceQuery>
	{
		public override GeoDistanceQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var variant = new GeoDistanceQuery();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var property = reader.GetString();
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

					if (property == "distance")
					{
						variant.Distance = JsonSerializer.Deserialize<string?>(ref reader, options);
						continue;
					}

					if (property == "distance_type")
					{
						variant.DistanceType = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.GeoDistanceType?>(ref reader, options);
						continue;
					}

					if (property == "validation_method")
					{
						variant.ValidationMethod = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod?>(ref reader, options);
						continue;
					}

					variant.Field = property;
					reader.Read();
					variant.Location = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.GeoLocation>(ref reader, options);
				}
			}

			return variant;
		}

		public override void Write(Utf8JsonWriter writer, GeoDistanceQuery value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value.Field is not null && value.Location is not null)
			{
				if (!options.TryGetClientSettings(out var settings))
				{
					throw new JsonException("Unable to retrive client settings for JsonSerializerOptions.");
				}

				var propertyName = settings.Inferrer.Field(value.Field);
				writer.WritePropertyName(propertyName);
				JsonSerializer.Serialize(writer, value.Location, options);
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

			if (value.Distance is not null)
			{
				writer.WritePropertyName("distance");
				JsonSerializer.Serialize(writer, value.Distance, options);
			}

			if (value.DistanceType is not null)
			{
				writer.WritePropertyName("distance_type");
				JsonSerializer.Serialize(writer, value.DistanceType, options);
			}

			if (value.ValidationMethod is not null)
			{
				writer.WritePropertyName("validation_method");
				JsonSerializer.Serialize(writer, value.ValidationMethod, options);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(GeoDistanceQueryConverter))]
	public sealed partial class GeoDistanceQuery : Query
	{
		public string? QueryName { get; set; }

		public float? Boost { get; set; }

		public string? Distance { get; set; }

		public Elastic.Clients.Elasticsearch.GeoDistanceType? DistanceType { get; set; }

		public Elastic.Clients.Elasticsearch.Field Field { get; set; }

		public Elastic.Clients.Elasticsearch.GeoLocation Location { get; set; }

		public Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? ValidationMethod { get; set; }
	}

	public sealed partial class GeoDistanceQueryDescriptor<TDocument> : SerializableDescriptorBase<GeoDistanceQueryDescriptor<TDocument>>
	{
		internal GeoDistanceQueryDescriptor(Action<GeoDistanceQueryDescriptor<TDocument>> configure) => configure.Invoke(this);
		public GeoDistanceQueryDescriptor() : base()
		{
		}

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		private string? DistanceValue { get; set; }

		private Elastic.Clients.Elasticsearch.GeoDistanceType? DistanceTypeValue { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? ValidationMethodValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private Elastic.Clients.Elasticsearch.GeoLocation LocationValue { get; set; }

		public GeoDistanceQueryDescriptor<TDocument> QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public GeoDistanceQueryDescriptor<TDocument> Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public GeoDistanceQueryDescriptor<TDocument> Distance(string? distance)
		{
			DistanceValue = distance;
			return Self;
		}

		public GeoDistanceQueryDescriptor<TDocument> DistanceType(Elastic.Clients.Elasticsearch.GeoDistanceType? distanceType)
		{
			DistanceTypeValue = distanceType;
			return Self;
		}

		public GeoDistanceQueryDescriptor<TDocument> ValidationMethod(Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? validationMethod)
		{
			ValidationMethodValue = validationMethod;
			return Self;
		}

		public GeoDistanceQueryDescriptor<TDocument> Location(Elastic.Clients.Elasticsearch.GeoLocation location)
		{
			LocationValue = location;
			return Self;
		}

		public GeoDistanceQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public GeoDistanceQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (FieldValue is not null && LocationValue is not null)
			{
				var propertyName = settings.Inferrer.Field(FieldValue);
				writer.WritePropertyName(propertyName);
				JsonSerializer.Serialize(writer, LocationValue, options);
			}

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

			if (DistanceValue is not null)
			{
				writer.WritePropertyName("distance");
				JsonSerializer.Serialize(writer, DistanceValue, options);
			}

			if (DistanceTypeValue is not null)
			{
				writer.WritePropertyName("distance_type");
				JsonSerializer.Serialize(writer, DistanceTypeValue, options);
			}

			if (ValidationMethodValue is not null)
			{
				writer.WritePropertyName("validation_method");
				JsonSerializer.Serialize(writer, ValidationMethodValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class GeoDistanceQueryDescriptor : SerializableDescriptorBase<GeoDistanceQueryDescriptor>
	{
		internal GeoDistanceQueryDescriptor(Action<GeoDistanceQueryDescriptor> configure) => configure.Invoke(this);
		public GeoDistanceQueryDescriptor() : base()
		{
		}

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		private string? DistanceValue { get; set; }

		private Elastic.Clients.Elasticsearch.GeoDistanceType? DistanceTypeValue { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? ValidationMethodValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private Elastic.Clients.Elasticsearch.GeoLocation LocationValue { get; set; }

		public GeoDistanceQueryDescriptor QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public GeoDistanceQueryDescriptor Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public GeoDistanceQueryDescriptor Distance(string? distance)
		{
			DistanceValue = distance;
			return Self;
		}

		public GeoDistanceQueryDescriptor DistanceType(Elastic.Clients.Elasticsearch.GeoDistanceType? distanceType)
		{
			DistanceTypeValue = distanceType;
			return Self;
		}

		public GeoDistanceQueryDescriptor ValidationMethod(Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? validationMethod)
		{
			ValidationMethodValue = validationMethod;
			return Self;
		}

		public GeoDistanceQueryDescriptor Location(Elastic.Clients.Elasticsearch.GeoLocation location)
		{
			LocationValue = location;
			return Self;
		}

		public GeoDistanceQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public GeoDistanceQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public GeoDistanceQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (FieldValue is not null && LocationValue is not null)
			{
				var propertyName = settings.Inferrer.Field(FieldValue);
				writer.WritePropertyName(propertyName);
				JsonSerializer.Serialize(writer, LocationValue, options);
			}

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

			if (DistanceValue is not null)
			{
				writer.WritePropertyName("distance");
				JsonSerializer.Serialize(writer, DistanceValue, options);
			}

			if (DistanceTypeValue is not null)
			{
				writer.WritePropertyName("distance_type");
				JsonSerializer.Serialize(writer, DistanceTypeValue, options);
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
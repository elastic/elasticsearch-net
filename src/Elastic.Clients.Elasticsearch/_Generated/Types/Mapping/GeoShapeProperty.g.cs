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
namespace Elastic.Clients.Elasticsearch.Mapping
{
	public sealed partial class GeoShapeProperty : IProperty
	{
		[JsonInclude]
		[JsonPropertyName("coerce")]
		public bool? Coerce { get; set; }

		[JsonInclude]
		[JsonPropertyName("copy_to")]
		public Elastic.Clients.Elasticsearch.Fields? CopyTo { get; set; }

		[JsonInclude]
		[JsonPropertyName("doc_values")]
		public bool? DocValues { get; set; }

		[JsonInclude]
		[JsonPropertyName("dynamic")]
		public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }

		[JsonInclude]
		[JsonPropertyName("fields")]
		public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_above")]
		public int? IgnoreAbove { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_malformed")]
		public bool? IgnoreMalformed { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_z_value")]
		public bool? IgnoreZValue { get; set; }

		[JsonInclude]
		[JsonPropertyName("local_metadata")]
		public Dictionary<string, object>? LocalMetadata { get; set; }

		[JsonInclude]
		[JsonPropertyName("meta")]
		public Dictionary<string, string>? Meta { get; set; }

		[JsonInclude]
		[JsonPropertyName("orientation")]
		public Elastic.Clients.Elasticsearch.Mapping.GeoOrientation? Orientation { get; set; }

		[JsonInclude]
		[JsonPropertyName("properties")]
		public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }

		[JsonInclude]
		[JsonPropertyName("similarity")]
		public string? Similarity { get; set; }

		[JsonInclude]
		[JsonPropertyName("store")]
		public bool? Store { get; set; }

		[JsonInclude]
		[JsonPropertyName("strategy")]
		public Elastic.Clients.Elasticsearch.Mapping.GeoStrategy? Strategy { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "geo_shape";
	}

	public sealed partial class GeoShapePropertyDescriptor<TDocument> : SerializableDescriptorBase<GeoShapePropertyDescriptor<TDocument>>, IBuildableDescriptor<GeoShapeProperty>
	{
		internal GeoShapePropertyDescriptor(Action<GeoShapePropertyDescriptor<TDocument>> configure) => configure.Invoke(this);
		public GeoShapePropertyDescriptor() : base()
		{
		}

		private bool? CoerceValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }

		private bool? DocValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private bool? IgnoreMalformedValue { get; set; }

		private bool? IgnoreZValueValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.GeoOrientation? OrientationValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		private string? SimilarityValue { get; set; }

		private bool? StoreValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.GeoStrategy? StrategyValue { get; set; }

		public GeoShapePropertyDescriptor<TDocument> Coerce(bool? coerce = true)
		{
			CoerceValue = coerce;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> DocValues(bool? docValues = true)
		{
			DocValuesValue = docValues;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> Fields(PropertiesDescriptor<TDocument> descriptor)
		{
			FieldsValue = descriptor.PromisedValue;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> Fields(Action<PropertiesDescriptor<TDocument>> configure)
		{
			var descriptor = new PropertiesDescriptor<TDocument>();
			configure?.Invoke(descriptor);
			FieldsValue = descriptor.PromisedValue;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> IgnoreMalformed(bool? ignoreMalformed = true)
		{
			IgnoreMalformedValue = ignoreMalformed;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> IgnoreZValue(bool? ignoreZValue = true)
		{
			IgnoreZValueValue = ignoreZValue;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> Orientation(Elastic.Clients.Elasticsearch.Mapping.GeoOrientation? orientation)
		{
			OrientationValue = orientation;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> Properties(PropertiesDescriptor<TDocument> descriptor)
		{
			PropertiesValue = descriptor.PromisedValue;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> Properties(Action<PropertiesDescriptor<TDocument>> configure)
		{
			var descriptor = new PropertiesDescriptor<TDocument>();
			configure?.Invoke(descriptor);
			PropertiesValue = descriptor.PromisedValue;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> Similarity(string? similarity)
		{
			SimilarityValue = similarity;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> Store(bool? store = true)
		{
			StoreValue = store;
			return Self;
		}

		public GeoShapePropertyDescriptor<TDocument> Strategy(Elastic.Clients.Elasticsearch.Mapping.GeoStrategy? strategy)
		{
			StrategyValue = strategy;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (CoerceValue.HasValue)
			{
				writer.WritePropertyName("coerce");
				writer.WriteBooleanValue(CoerceValue.Value);
			}

			if (CopyToValue is not null)
			{
				writer.WritePropertyName("copy_to");
				JsonSerializer.Serialize(writer, CopyToValue, options);
			}

			if (DocValuesValue.HasValue)
			{
				writer.WritePropertyName("doc_values");
				writer.WriteBooleanValue(DocValuesValue.Value);
			}

			if (DynamicValue is not null)
			{
				writer.WritePropertyName("dynamic");
				JsonSerializer.Serialize(writer, DynamicValue, options);
			}

			if (FieldsValue is not null)
			{
				writer.WritePropertyName("fields");
				JsonSerializer.Serialize(writer, FieldsValue, options);
			}

			if (IgnoreAboveValue.HasValue)
			{
				writer.WritePropertyName("ignore_above");
				writer.WriteNumberValue(IgnoreAboveValue.Value);
			}

			if (IgnoreMalformedValue.HasValue)
			{
				writer.WritePropertyName("ignore_malformed");
				writer.WriteBooleanValue(IgnoreMalformedValue.Value);
			}

			if (IgnoreZValueValue.HasValue)
			{
				writer.WritePropertyName("ignore_z_value");
				writer.WriteBooleanValue(IgnoreZValueValue.Value);
			}

			if (LocalMetadataValue is not null)
			{
				writer.WritePropertyName("local_metadata");
				JsonSerializer.Serialize(writer, LocalMetadataValue, options);
			}

			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			if (OrientationValue is not null)
			{
				writer.WritePropertyName("orientation");
				JsonSerializer.Serialize(writer, OrientationValue, options);
			}

			if (PropertiesValue is not null)
			{
				writer.WritePropertyName("properties");
				JsonSerializer.Serialize(writer, PropertiesValue, options);
			}

			if (!string.IsNullOrEmpty(SimilarityValue))
			{
				writer.WritePropertyName("similarity");
				writer.WriteStringValue(SimilarityValue);
			}

			if (StoreValue.HasValue)
			{
				writer.WritePropertyName("store");
				writer.WriteBooleanValue(StoreValue.Value);
			}

			if (StrategyValue is not null)
			{
				writer.WritePropertyName("strategy");
				JsonSerializer.Serialize(writer, StrategyValue, options);
			}

			writer.WritePropertyName("type");
			writer.WriteStringValue("geo_shape");
			writer.WriteEndObject();
		}

		GeoShapeProperty IBuildableDescriptor<GeoShapeProperty>.Build() => new()
		{ Coerce = CoerceValue, CopyTo = CopyToValue, DocValues = DocValuesValue, Dynamic = DynamicValue, Fields = FieldsValue, IgnoreAbove = IgnoreAboveValue, IgnoreMalformed = IgnoreMalformedValue, IgnoreZValue = IgnoreZValueValue, LocalMetadata = LocalMetadataValue, Meta = MetaValue, Orientation = OrientationValue, Properties = PropertiesValue, Similarity = SimilarityValue, Store = StoreValue, Strategy = StrategyValue };
	}

	public sealed partial class GeoShapePropertyDescriptor : SerializableDescriptorBase<GeoShapePropertyDescriptor>, IBuildableDescriptor<GeoShapeProperty>
	{
		internal GeoShapePropertyDescriptor(Action<GeoShapePropertyDescriptor> configure) => configure.Invoke(this);
		public GeoShapePropertyDescriptor() : base()
		{
		}

		private bool? CoerceValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }

		private bool? DocValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private bool? IgnoreMalformedValue { get; set; }

		private bool? IgnoreZValueValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.GeoOrientation? OrientationValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		private string? SimilarityValue { get; set; }

		private bool? StoreValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.GeoStrategy? StrategyValue { get; set; }

		public GeoShapePropertyDescriptor Coerce(bool? coerce = true)
		{
			CoerceValue = coerce;
			return Self;
		}

		public GeoShapePropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public GeoShapePropertyDescriptor DocValues(bool? docValues = true)
		{
			DocValuesValue = docValues;
			return Self;
		}

		public GeoShapePropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public GeoShapePropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public GeoShapePropertyDescriptor Fields<TDocument>(PropertiesDescriptor<TDocument> descriptor)
		{
			FieldsValue = descriptor.PromisedValue;
			return Self;
		}

		public GeoShapePropertyDescriptor Fields<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
		{
			var descriptor = new PropertiesDescriptor<TDocument>();
			configure?.Invoke(descriptor);
			FieldsValue = descriptor.PromisedValue;
			return Self;
		}

		public GeoShapePropertyDescriptor IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public GeoShapePropertyDescriptor IgnoreMalformed(bool? ignoreMalformed = true)
		{
			IgnoreMalformedValue = ignoreMalformed;
			return Self;
		}

		public GeoShapePropertyDescriptor IgnoreZValue(bool? ignoreZValue = true)
		{
			IgnoreZValueValue = ignoreZValue;
			return Self;
		}

		public GeoShapePropertyDescriptor LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public GeoShapePropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public GeoShapePropertyDescriptor Orientation(Elastic.Clients.Elasticsearch.Mapping.GeoOrientation? orientation)
		{
			OrientationValue = orientation;
			return Self;
		}

		public GeoShapePropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public GeoShapePropertyDescriptor Properties<TDocument>(PropertiesDescriptor<TDocument> descriptor)
		{
			PropertiesValue = descriptor.PromisedValue;
			return Self;
		}

		public GeoShapePropertyDescriptor Properties<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
		{
			var descriptor = new PropertiesDescriptor<TDocument>();
			configure?.Invoke(descriptor);
			PropertiesValue = descriptor.PromisedValue;
			return Self;
		}

		public GeoShapePropertyDescriptor Similarity(string? similarity)
		{
			SimilarityValue = similarity;
			return Self;
		}

		public GeoShapePropertyDescriptor Store(bool? store = true)
		{
			StoreValue = store;
			return Self;
		}

		public GeoShapePropertyDescriptor Strategy(Elastic.Clients.Elasticsearch.Mapping.GeoStrategy? strategy)
		{
			StrategyValue = strategy;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (CoerceValue.HasValue)
			{
				writer.WritePropertyName("coerce");
				writer.WriteBooleanValue(CoerceValue.Value);
			}

			if (CopyToValue is not null)
			{
				writer.WritePropertyName("copy_to");
				JsonSerializer.Serialize(writer, CopyToValue, options);
			}

			if (DocValuesValue.HasValue)
			{
				writer.WritePropertyName("doc_values");
				writer.WriteBooleanValue(DocValuesValue.Value);
			}

			if (DynamicValue is not null)
			{
				writer.WritePropertyName("dynamic");
				JsonSerializer.Serialize(writer, DynamicValue, options);
			}

			if (FieldsValue is not null)
			{
				writer.WritePropertyName("fields");
				JsonSerializer.Serialize(writer, FieldsValue, options);
			}

			if (IgnoreAboveValue.HasValue)
			{
				writer.WritePropertyName("ignore_above");
				writer.WriteNumberValue(IgnoreAboveValue.Value);
			}

			if (IgnoreMalformedValue.HasValue)
			{
				writer.WritePropertyName("ignore_malformed");
				writer.WriteBooleanValue(IgnoreMalformedValue.Value);
			}

			if (IgnoreZValueValue.HasValue)
			{
				writer.WritePropertyName("ignore_z_value");
				writer.WriteBooleanValue(IgnoreZValueValue.Value);
			}

			if (LocalMetadataValue is not null)
			{
				writer.WritePropertyName("local_metadata");
				JsonSerializer.Serialize(writer, LocalMetadataValue, options);
			}

			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			if (OrientationValue is not null)
			{
				writer.WritePropertyName("orientation");
				JsonSerializer.Serialize(writer, OrientationValue, options);
			}

			if (PropertiesValue is not null)
			{
				writer.WritePropertyName("properties");
				JsonSerializer.Serialize(writer, PropertiesValue, options);
			}

			if (!string.IsNullOrEmpty(SimilarityValue))
			{
				writer.WritePropertyName("similarity");
				writer.WriteStringValue(SimilarityValue);
			}

			if (StoreValue.HasValue)
			{
				writer.WritePropertyName("store");
				writer.WriteBooleanValue(StoreValue.Value);
			}

			if (StrategyValue is not null)
			{
				writer.WritePropertyName("strategy");
				JsonSerializer.Serialize(writer, StrategyValue, options);
			}

			writer.WritePropertyName("type");
			writer.WriteStringValue("geo_shape");
			writer.WriteEndObject();
		}

		GeoShapeProperty IBuildableDescriptor<GeoShapeProperty>.Build() => new()
		{ Coerce = CoerceValue, CopyTo = CopyToValue, DocValues = DocValuesValue, Dynamic = DynamicValue, Fields = FieldsValue, IgnoreAbove = IgnoreAboveValue, IgnoreMalformed = IgnoreMalformedValue, IgnoreZValue = IgnoreZValueValue, LocalMetadata = LocalMetadataValue, Meta = MetaValue, Orientation = OrientationValue, Properties = PropertiesValue, Similarity = SimilarityValue, Store = StoreValue, Strategy = StrategyValue };
	}
}
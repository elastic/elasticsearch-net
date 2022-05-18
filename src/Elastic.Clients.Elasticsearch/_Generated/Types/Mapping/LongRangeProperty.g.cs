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
	public partial class LongRangeProperty : RangePropertyBase, IProperty
	{
		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "long_range";
	}

	public sealed partial class LongRangePropertyDescriptor<TDocument> : SerializableDescriptorBase<LongRangePropertyDescriptor<TDocument>>, IBuildableDescriptor<LongRangeProperty>
	{
		internal LongRangePropertyDescriptor(Action<LongRangePropertyDescriptor<TDocument>> configure) => configure.Invoke(this);
		public LongRangePropertyDescriptor() : base()
		{
		}

		private double? BoostValue { get; set; }

		private bool? CoerceValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }

		private bool? DocValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private bool? IndexValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		private string? SimilarityValue { get; set; }

		private bool? StoreValue { get; set; }

		private PropertyName VariantPropertyNameValue { get; set; }

		public LongRangePropertyDescriptor<TDocument> Boost(double? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> Coerce(bool? coerce = true)
		{
			CoerceValue = coerce;
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> CopyTo<TValue>(Expression<Func<TDocument, TValue>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> DocValues(bool? docValues = true)
		{
			DocValuesValue = docValues;
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> Index(bool? index = true)
		{
			IndexValue = index;
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> Similarity(string? similarity)
		{
			SimilarityValue = similarity;
			return Self;
		}

		public LongRangePropertyDescriptor<TDocument> Store(bool? store = true)
		{
			StoreValue = store;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (BoostValue.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(BoostValue.Value);
			}

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

			if (IndexValue.HasValue)
			{
				writer.WritePropertyName("index");
				writer.WriteBooleanValue(IndexValue.Value);
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

			writer.WritePropertyName("type");
			writer.WriteStringValue("long_range");
			writer.WriteEndObject();
		}

		LongRangeProperty IBuildableDescriptor<LongRangeProperty>.Build() => new()
		{ Boost = BoostValue, Coerce = CoerceValue, CopyTo = CopyToValue, DocValues = DocValuesValue, Dynamic = DynamicValue, Fields = FieldsValue, IgnoreAbove = IgnoreAboveValue, Index = IndexValue, LocalMetadata = LocalMetadataValue, Meta = MetaValue, Properties = PropertiesValue, Similarity = SimilarityValue, Store = StoreValue };
	}

	public sealed partial class LongRangePropertyDescriptor : SerializableDescriptorBase<LongRangePropertyDescriptor>, IBuildableDescriptor<LongRangeProperty>
	{
		internal LongRangePropertyDescriptor(Action<LongRangePropertyDescriptor> configure) => configure.Invoke(this);
		public LongRangePropertyDescriptor() : base()
		{
		}

		private double? BoostValue { get; set; }

		private bool? CoerceValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }

		private bool? DocValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private bool? IndexValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		private string? SimilarityValue { get; set; }

		private bool? StoreValue { get; set; }

		private PropertyName VariantPropertyNameValue { get; set; }

		public LongRangePropertyDescriptor Boost(double? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public LongRangePropertyDescriptor Coerce(bool? coerce = true)
		{
			CoerceValue = coerce;
			return Self;
		}

		public LongRangePropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public LongRangePropertyDescriptor CopyTo<TDocument, TValue>(Expression<Func<TDocument, TValue>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public LongRangePropertyDescriptor CopyTo<TDocument>(Expression<Func<TDocument, object>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public LongRangePropertyDescriptor DocValues(bool? docValues = true)
		{
			DocValuesValue = docValues;
			return Self;
		}

		public LongRangePropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public LongRangePropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public LongRangePropertyDescriptor IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public LongRangePropertyDescriptor Index(bool? index = true)
		{
			IndexValue = index;
			return Self;
		}

		public LongRangePropertyDescriptor LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public LongRangePropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public LongRangePropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public LongRangePropertyDescriptor Similarity(string? similarity)
		{
			SimilarityValue = similarity;
			return Self;
		}

		public LongRangePropertyDescriptor Store(bool? store = true)
		{
			StoreValue = store;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (BoostValue.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(BoostValue.Value);
			}

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

			if (IndexValue.HasValue)
			{
				writer.WritePropertyName("index");
				writer.WriteBooleanValue(IndexValue.Value);
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

			writer.WritePropertyName("type");
			writer.WriteStringValue("long_range");
			writer.WriteEndObject();
		}

		LongRangeProperty IBuildableDescriptor<LongRangeProperty>.Build() => new()
		{ Boost = BoostValue, Coerce = CoerceValue, CopyTo = CopyToValue, DocValues = DocValuesValue, Dynamic = DynamicValue, Fields = FieldsValue, IgnoreAbove = IgnoreAboveValue, Index = IndexValue, LocalMetadata = LocalMetadataValue, Meta = MetaValue, Properties = PropertiesValue, Similarity = SimilarityValue, Store = StoreValue };
	}
}
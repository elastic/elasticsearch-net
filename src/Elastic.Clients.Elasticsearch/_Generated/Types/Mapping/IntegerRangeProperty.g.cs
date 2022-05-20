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
	public partial class IntegerRangeProperty : RangePropertyBase, IProperty
	{
		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "integer_range";
	}

	public sealed partial class IntegerRangePropertyDescriptor<TDocument> : SerializableDescriptorBase<IntegerRangePropertyDescriptor<TDocument>>, IBuildableDescriptor<IntegerRangeProperty>
	{
		internal IntegerRangePropertyDescriptor(Action<IntegerRangePropertyDescriptor<TDocument>> configure) => configure.Invoke(this);
		public IntegerRangePropertyDescriptor() : base()
		{
		}

		private double? BoostValue { get; set; }

		private bool? CoerceValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }

		private bool? DocValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private PropertiesDescriptor<TDocument> FieldsDescriptor { get; set; }

		private Action<PropertiesDescriptor<TDocument>> FieldsDescriptorAction { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private bool? IndexValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		private PropertiesDescriptor<TDocument> PropertiesDescriptor { get; set; }

		private Action<PropertiesDescriptor<TDocument>> PropertiesDescriptorAction { get; set; }

		private string? SimilarityValue { get; set; }

		private bool? StoreValue { get; set; }

		public IntegerRangePropertyDescriptor<TDocument> Boost(double? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> Coerce(bool? coerce = true)
		{
			CoerceValue = coerce;
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> CopyTo<TValue>(Expression<Func<TDocument, TValue>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> DocValues(bool? docValues = true)
		{
			DocValuesValue = docValues;
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> Index(bool? index = true)
		{
			IndexValue = index;
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> Similarity(string? similarity)
		{
			SimilarityValue = similarity;
			return Self;
		}

		public IntegerRangePropertyDescriptor<TDocument> Store(bool? store = true)
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
			writer.WriteStringValue("integer_range");
			writer.WriteEndObject();
		}

		IntegerRangeProperty IBuildableDescriptor<IntegerRangeProperty>.Build() => new()
		{ Boost = BoostValue, Coerce = CoerceValue, CopyTo = CopyToValue, DocValues = DocValuesValue, Dynamic = DynamicValue, Fields = FieldsValue, IgnoreAbove = IgnoreAboveValue, Index = IndexValue, LocalMetadata = LocalMetadataValue, Meta = MetaValue, Properties = PropertiesValue, Similarity = SimilarityValue, Store = StoreValue };
	}

	public sealed partial class IntegerRangePropertyDescriptor : SerializableDescriptorBase<IntegerRangePropertyDescriptor>, IBuildableDescriptor<IntegerRangeProperty>
	{
		internal IntegerRangePropertyDescriptor(Action<IntegerRangePropertyDescriptor> configure) => configure.Invoke(this);
		public IntegerRangePropertyDescriptor() : base()
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

		public IntegerRangePropertyDescriptor Boost(double? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public IntegerRangePropertyDescriptor Coerce(bool? coerce = true)
		{
			CoerceValue = coerce;
			return Self;
		}

		public IntegerRangePropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public IntegerRangePropertyDescriptor CopyTo<TDocument, TValue>(Expression<Func<TDocument, TValue>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public IntegerRangePropertyDescriptor CopyTo<TDocument>(Expression<Func<TDocument, object>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public IntegerRangePropertyDescriptor DocValues(bool? docValues = true)
		{
			DocValuesValue = docValues;
			return Self;
		}

		public IntegerRangePropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public IntegerRangePropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public IntegerRangePropertyDescriptor IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public IntegerRangePropertyDescriptor Index(bool? index = true)
		{
			IndexValue = index;
			return Self;
		}

		public IntegerRangePropertyDescriptor LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public IntegerRangePropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public IntegerRangePropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public IntegerRangePropertyDescriptor Similarity(string? similarity)
		{
			SimilarityValue = similarity;
			return Self;
		}

		public IntegerRangePropertyDescriptor Store(bool? store = true)
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
			writer.WriteStringValue("integer_range");
			writer.WriteEndObject();
		}

		IntegerRangeProperty IBuildableDescriptor<IntegerRangeProperty>.Build() => new()
		{ Boost = BoostValue, Coerce = CoerceValue, CopyTo = CopyToValue, DocValues = DocValuesValue, Dynamic = DynamicValue, Fields = FieldsValue, IgnoreAbove = IgnoreAboveValue, Index = IndexValue, LocalMetadata = LocalMetadataValue, Meta = MetaValue, Properties = PropertiesValue, Similarity = SimilarityValue, Store = StoreValue };
	}
}
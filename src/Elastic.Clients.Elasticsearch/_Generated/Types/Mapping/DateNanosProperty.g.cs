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
	public partial class DateNanosProperty : DocValuesPropertyBase, IProperty
	{
		[JsonInclude]
		[JsonPropertyName("boost")]
		public double? Boost { get; set; }

		[JsonInclude]
		[JsonPropertyName("format")]
		public string? Format { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_malformed")]
		public bool? IgnoreMalformed { get; set; }

		[JsonInclude]
		[JsonPropertyName("index")]
		public bool? Index { get; set; }

		[JsonInclude]
		[JsonPropertyName("null_value")]
		public string? NullValue { get; set; }

		[JsonInclude]
		[JsonPropertyName("precision_step")]
		public int? PrecisionStep { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "date_nanos";
	}

	public sealed partial class DateNanosPropertyDescriptor<TDocument> : SerializableDescriptorBase<DateNanosPropertyDescriptor<TDocument>>, IBuildableDescriptor<DateNanosProperty>
	{
		internal DateNanosPropertyDescriptor(Action<DateNanosPropertyDescriptor<TDocument>> configure) => configure.Invoke(this);
		public DateNanosPropertyDescriptor() : base()
		{
		}

		private double? BoostValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }

		private bool? DocValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private PropertiesDescriptor<TDocument> FieldsDescriptor { get; set; }

		private Action<PropertiesDescriptor<TDocument>> FieldsDescriptorAction { get; set; }

		private string? FormatValue { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private bool? IgnoreMalformedValue { get; set; }

		private bool? IndexValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private string? NullValueValue { get; set; }

		private int? PrecisionStepValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		private PropertiesDescriptor<TDocument> PropertiesDescriptor { get; set; }

		private Action<PropertiesDescriptor<TDocument>> PropertiesDescriptorAction { get; set; }

		private string? SimilarityValue { get; set; }

		private bool? StoreValue { get; set; }

		public DateNanosPropertyDescriptor<TDocument> Boost(double? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> CopyTo<TValue>(Expression<Func<TDocument, TValue>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> DocValues(bool? docValues = true)
		{
			DocValuesValue = docValues;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> Format(string? format)
		{
			FormatValue = format;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> IgnoreMalformed(bool? ignoreMalformed = true)
		{
			IgnoreMalformedValue = ignoreMalformed;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> Index(bool? index = true)
		{
			IndexValue = index;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> NullValue(string? nullValue)
		{
			NullValueValue = nullValue;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> PrecisionStep(int? precisionStep)
		{
			PrecisionStepValue = precisionStep;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> Similarity(string? similarity)
		{
			SimilarityValue = similarity;
			return Self;
		}

		public DateNanosPropertyDescriptor<TDocument> Store(bool? store = true)
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

			if (!string.IsNullOrEmpty(FormatValue))
			{
				writer.WritePropertyName("format");
				writer.WriteStringValue(FormatValue);
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

			if (NullValueValue is not null)
			{
				writer.WritePropertyName("null_value");
				JsonSerializer.Serialize(writer, NullValueValue, options);
			}

			if (PrecisionStepValue.HasValue)
			{
				writer.WritePropertyName("precision_step");
				writer.WriteNumberValue(PrecisionStepValue.Value);
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
			writer.WriteStringValue("date_nanos");
			writer.WriteEndObject();
		}

		DateNanosProperty IBuildableDescriptor<DateNanosProperty>.Build() => new()
		{ Boost = BoostValue, CopyTo = CopyToValue, DocValues = DocValuesValue, Dynamic = DynamicValue, Fields = FieldsValue, Format = FormatValue, IgnoreAbove = IgnoreAboveValue, IgnoreMalformed = IgnoreMalformedValue, Index = IndexValue, LocalMetadata = LocalMetadataValue, Meta = MetaValue, NullValue = NullValueValue, PrecisionStep = PrecisionStepValue, Properties = PropertiesValue, Similarity = SimilarityValue, Store = StoreValue };
	}

	public sealed partial class DateNanosPropertyDescriptor : SerializableDescriptorBase<DateNanosPropertyDescriptor>, IBuildableDescriptor<DateNanosProperty>
	{
		internal DateNanosPropertyDescriptor(Action<DateNanosPropertyDescriptor> configure) => configure.Invoke(this);
		public DateNanosPropertyDescriptor() : base()
		{
		}

		private double? BoostValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }

		private bool? DocValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private string? FormatValue { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private bool? IgnoreMalformedValue { get; set; }

		private bool? IndexValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private string? NullValueValue { get; set; }

		private int? PrecisionStepValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		private string? SimilarityValue { get; set; }

		private bool? StoreValue { get; set; }

		public DateNanosPropertyDescriptor Boost(double? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public DateNanosPropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public DateNanosPropertyDescriptor CopyTo<TDocument, TValue>(Expression<Func<TDocument, TValue>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public DateNanosPropertyDescriptor CopyTo<TDocument>(Expression<Func<TDocument, object>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public DateNanosPropertyDescriptor DocValues(bool? docValues = true)
		{
			DocValuesValue = docValues;
			return Self;
		}

		public DateNanosPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public DateNanosPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public DateNanosPropertyDescriptor Format(string? format)
		{
			FormatValue = format;
			return Self;
		}

		public DateNanosPropertyDescriptor IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public DateNanosPropertyDescriptor IgnoreMalformed(bool? ignoreMalformed = true)
		{
			IgnoreMalformedValue = ignoreMalformed;
			return Self;
		}

		public DateNanosPropertyDescriptor Index(bool? index = true)
		{
			IndexValue = index;
			return Self;
		}

		public DateNanosPropertyDescriptor LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public DateNanosPropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public DateNanosPropertyDescriptor NullValue(string? nullValue)
		{
			NullValueValue = nullValue;
			return Self;
		}

		public DateNanosPropertyDescriptor PrecisionStep(int? precisionStep)
		{
			PrecisionStepValue = precisionStep;
			return Self;
		}

		public DateNanosPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public DateNanosPropertyDescriptor Similarity(string? similarity)
		{
			SimilarityValue = similarity;
			return Self;
		}

		public DateNanosPropertyDescriptor Store(bool? store = true)
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

			if (!string.IsNullOrEmpty(FormatValue))
			{
				writer.WritePropertyName("format");
				writer.WriteStringValue(FormatValue);
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

			if (NullValueValue is not null)
			{
				writer.WritePropertyName("null_value");
				JsonSerializer.Serialize(writer, NullValueValue, options);
			}

			if (PrecisionStepValue.HasValue)
			{
				writer.WritePropertyName("precision_step");
				writer.WriteNumberValue(PrecisionStepValue.Value);
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
			writer.WriteStringValue("date_nanos");
			writer.WriteEndObject();
		}

		DateNanosProperty IBuildableDescriptor<DateNanosProperty>.Build() => new()
		{ Boost = BoostValue, CopyTo = CopyToValue, DocValues = DocValuesValue, Dynamic = DynamicValue, Fields = FieldsValue, Format = FormatValue, IgnoreAbove = IgnoreAboveValue, IgnoreMalformed = IgnoreMalformedValue, Index = IndexValue, LocalMetadata = LocalMetadataValue, Meta = MetaValue, NullValue = NullValueValue, PrecisionStep = PrecisionStepValue, Properties = PropertiesValue, Similarity = SimilarityValue, Store = StoreValue };
	}
}
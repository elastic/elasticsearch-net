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
	public sealed partial class FloatNumberProperty
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
		[JsonPropertyName("index")]
		public bool? Index { get; set; }

		[JsonInclude]
		[JsonPropertyName("local_metadata")]
		public Dictionary<string, object>? LocalMetadata { get; set; }

		[JsonInclude]
		[JsonPropertyName("meta")]
		public Dictionary<string, string>? Meta { get; set; }

		[JsonInclude]
		[JsonPropertyName("null_value")]
		public float? NullValue { get; set; }

		[JsonInclude]
		[JsonPropertyName("on_script_error")]
		public Elastic.Clients.Elasticsearch.Mapping.OnScriptError? OnScriptError { get; set; }

		[JsonInclude]
		[JsonPropertyName("properties")]
		public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }

		[JsonInclude]
		[JsonPropertyName("script")]
		public Elastic.Clients.Elasticsearch.Script? Script { get; set; }

		[JsonInclude]
		[JsonPropertyName("similarity")]
		public string? Similarity { get; set; }

		[JsonInclude]
		[JsonPropertyName("store")]
		public bool? Store { get; set; }

		[JsonInclude]
		[JsonPropertyName("time_series_metric")]
		public Elastic.Clients.Elasticsearch.Mapping.TimeSeriesMetricType? TimeSeriesMetric { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "float";
	}

	public sealed partial class FloatNumberPropertyDescriptor<TDocument> : SerializableDescriptorBase<FloatNumberPropertyDescriptor<TDocument>>, IBuildableDescriptor<FloatNumberProperty>
	{
		internal FloatNumberPropertyDescriptor(Action<FloatNumberPropertyDescriptor<TDocument>> configure) => configure.Invoke(this);
		public FloatNumberPropertyDescriptor() : base()
		{
		}

		private bool? CoerceValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }

		private bool? DocValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private bool? IgnoreMalformedValue { get; set; }

		private bool? IndexValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private float? NullValueValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.OnScriptError? OnScriptErrorValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }

		private string? SimilarityValue { get; set; }

		private bool? StoreValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.TimeSeriesMetricType? TimeSeriesMetricValue { get; set; }

		public FloatNumberPropertyDescriptor<TDocument> Coerce(bool? coerce = true)
		{
			CoerceValue = coerce;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> CopyTo<TValue>(Expression<Func<TDocument, TValue>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> DocValues(bool? docValues = true)
		{
			DocValuesValue = docValues;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> Fields(PropertiesDescriptor<TDocument> descriptor)
		{
			FieldsValue = descriptor.PromisedValue;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> Fields(Action<PropertiesDescriptor<TDocument>> configure)
		{
			var descriptor = new PropertiesDescriptor<TDocument>();
			configure?.Invoke(descriptor);
			FieldsValue = descriptor.PromisedValue;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> IgnoreMalformed(bool? ignoreMalformed = true)
		{
			IgnoreMalformedValue = ignoreMalformed;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> Index(bool? index = true)
		{
			IndexValue = index;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> NullValue(float? nullValue)
		{
			NullValueValue = nullValue;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> OnScriptError(Elastic.Clients.Elasticsearch.Mapping.OnScriptError? onScriptError)
		{
			OnScriptErrorValue = onScriptError;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> Properties(PropertiesDescriptor<TDocument> descriptor)
		{
			PropertiesValue = descriptor.PromisedValue;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> Properties(Action<PropertiesDescriptor<TDocument>> configure)
		{
			var descriptor = new PropertiesDescriptor<TDocument>();
			configure?.Invoke(descriptor);
			PropertiesValue = descriptor.PromisedValue;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? script)
		{
			ScriptValue = script;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> Similarity(string? similarity)
		{
			SimilarityValue = similarity;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> Store(bool? store = true)
		{
			StoreValue = store;
			return Self;
		}

		public FloatNumberPropertyDescriptor<TDocument> TimeSeriesMetric(Elastic.Clients.Elasticsearch.Mapping.TimeSeriesMetricType? timeSeriesMetric)
		{
			TimeSeriesMetricValue = timeSeriesMetric;
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

			if (NullValueValue.HasValue)
			{
				writer.WritePropertyName("null_value");
				writer.WriteNumberValue(NullValueValue.Value);
			}

			if (OnScriptErrorValue is not null)
			{
				writer.WritePropertyName("on_script_error");
				JsonSerializer.Serialize(writer, OnScriptErrorValue, options);
			}

			if (PropertiesValue is not null)
			{
				writer.WritePropertyName("properties");
				JsonSerializer.Serialize(writer, PropertiesValue, options);
			}

			if (ScriptValue is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
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

			if (TimeSeriesMetricValue is not null)
			{
				writer.WritePropertyName("time_series_metric");
				JsonSerializer.Serialize(writer, TimeSeriesMetricValue, options);
			}

			writer.WritePropertyName("type");
			writer.WriteStringValue("float");
			writer.WriteEndObject();
		}

		FloatNumberProperty IBuildableDescriptor<FloatNumberProperty>.Build() => new()
		{ Coerce = CoerceValue, CopyTo = CopyToValue, DocValues = DocValuesValue, Dynamic = DynamicValue, Fields = FieldsValue, IgnoreAbove = IgnoreAboveValue, IgnoreMalformed = IgnoreMalformedValue, Index = IndexValue, LocalMetadata = LocalMetadataValue, Meta = MetaValue, NullValue = NullValueValue, OnScriptError = OnScriptErrorValue, Properties = PropertiesValue, Script = ScriptValue, Similarity = SimilarityValue, Store = StoreValue, TimeSeriesMetric = TimeSeriesMetricValue };
	}

	public sealed partial class FloatNumberPropertyDescriptor : SerializableDescriptorBase<FloatNumberPropertyDescriptor>, IBuildableDescriptor<FloatNumberProperty>
	{
		internal FloatNumberPropertyDescriptor(Action<FloatNumberPropertyDescriptor> configure) => configure.Invoke(this);
		public FloatNumberPropertyDescriptor() : base()
		{
		}

		private bool? CoerceValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }

		private bool? DocValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private bool? IgnoreMalformedValue { get; set; }

		private bool? IndexValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private float? NullValueValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.OnScriptError? OnScriptErrorValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }

		private string? SimilarityValue { get; set; }

		private bool? StoreValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.TimeSeriesMetricType? TimeSeriesMetricValue { get; set; }

		public FloatNumberPropertyDescriptor Coerce(bool? coerce = true)
		{
			CoerceValue = coerce;
			return Self;
		}

		public FloatNumberPropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public FloatNumberPropertyDescriptor CopyTo<TDocument, TValue>(Expression<Func<TDocument, TValue>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public FloatNumberPropertyDescriptor CopyTo<TDocument>(Expression<Func<TDocument, object>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public FloatNumberPropertyDescriptor DocValues(bool? docValues = true)
		{
			DocValuesValue = docValues;
			return Self;
		}

		public FloatNumberPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public FloatNumberPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public FloatNumberPropertyDescriptor Fields<TDocument>(PropertiesDescriptor<TDocument> descriptor)
		{
			FieldsValue = descriptor.PromisedValue;
			return Self;
		}

		public FloatNumberPropertyDescriptor Fields<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
		{
			var descriptor = new PropertiesDescriptor<TDocument>();
			configure?.Invoke(descriptor);
			FieldsValue = descriptor.PromisedValue;
			return Self;
		}

		public FloatNumberPropertyDescriptor IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public FloatNumberPropertyDescriptor IgnoreMalformed(bool? ignoreMalformed = true)
		{
			IgnoreMalformedValue = ignoreMalformed;
			return Self;
		}

		public FloatNumberPropertyDescriptor Index(bool? index = true)
		{
			IndexValue = index;
			return Self;
		}

		public FloatNumberPropertyDescriptor LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public FloatNumberPropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public FloatNumberPropertyDescriptor NullValue(float? nullValue)
		{
			NullValueValue = nullValue;
			return Self;
		}

		public FloatNumberPropertyDescriptor OnScriptError(Elastic.Clients.Elasticsearch.Mapping.OnScriptError? onScriptError)
		{
			OnScriptErrorValue = onScriptError;
			return Self;
		}

		public FloatNumberPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public FloatNumberPropertyDescriptor Properties<TDocument>(PropertiesDescriptor<TDocument> descriptor)
		{
			PropertiesValue = descriptor.PromisedValue;
			return Self;
		}

		public FloatNumberPropertyDescriptor Properties<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
		{
			var descriptor = new PropertiesDescriptor<TDocument>();
			configure?.Invoke(descriptor);
			PropertiesValue = descriptor.PromisedValue;
			return Self;
		}

		public FloatNumberPropertyDescriptor Script(Elastic.Clients.Elasticsearch.Script? script)
		{
			ScriptValue = script;
			return Self;
		}

		public FloatNumberPropertyDescriptor Similarity(string? similarity)
		{
			SimilarityValue = similarity;
			return Self;
		}

		public FloatNumberPropertyDescriptor Store(bool? store = true)
		{
			StoreValue = store;
			return Self;
		}

		public FloatNumberPropertyDescriptor TimeSeriesMetric(Elastic.Clients.Elasticsearch.Mapping.TimeSeriesMetricType? timeSeriesMetric)
		{
			TimeSeriesMetricValue = timeSeriesMetric;
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

			if (NullValueValue.HasValue)
			{
				writer.WritePropertyName("null_value");
				writer.WriteNumberValue(NullValueValue.Value);
			}

			if (OnScriptErrorValue is not null)
			{
				writer.WritePropertyName("on_script_error");
				JsonSerializer.Serialize(writer, OnScriptErrorValue, options);
			}

			if (PropertiesValue is not null)
			{
				writer.WritePropertyName("properties");
				JsonSerializer.Serialize(writer, PropertiesValue, options);
			}

			if (ScriptValue is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
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

			if (TimeSeriesMetricValue is not null)
			{
				writer.WritePropertyName("time_series_metric");
				JsonSerializer.Serialize(writer, TimeSeriesMetricValue, options);
			}

			writer.WritePropertyName("type");
			writer.WriteStringValue("float");
			writer.WriteEndObject();
		}

		FloatNumberProperty IBuildableDescriptor<FloatNumberProperty>.Build() => new()
		{ Coerce = CoerceValue, CopyTo = CopyToValue, DocValues = DocValuesValue, Dynamic = DynamicValue, Fields = FieldsValue, IgnoreAbove = IgnoreAboveValue, IgnoreMalformed = IgnoreMalformedValue, Index = IndexValue, LocalMetadata = LocalMetadataValue, Meta = MetaValue, NullValue = NullValueValue, OnScriptError = OnScriptErrorValue, Properties = PropertiesValue, Script = ScriptValue, Similarity = SimilarityValue, Store = StoreValue, TimeSeriesMetric = TimeSeriesMetricValue };
	}
}
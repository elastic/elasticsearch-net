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

#nullable restore

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Mapping;

public sealed partial class ShortNumberProperty : IProperty
{
	[JsonInclude, JsonPropertyName("boost")]
	public double? Boost { get; set; }
	[JsonInclude, JsonPropertyName("coerce")]
	public bool? Coerce { get; set; }
	[JsonInclude, JsonPropertyName("copy_to")]
	public Elastic.Clients.Elasticsearch.Fields? CopyTo { get; set; }
	[JsonInclude, JsonPropertyName("doc_values")]
	public bool? DocValues { get; set; }
	[JsonInclude, JsonPropertyName("dynamic")]
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }
	[JsonInclude, JsonPropertyName("fields")]
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }
	[JsonInclude, JsonPropertyName("ignore_above")]
	public int? IgnoreAbove { get; set; }
	[JsonInclude, JsonPropertyName("ignore_malformed")]
	public bool? IgnoreMalformed { get; set; }
	[JsonInclude, JsonPropertyName("index")]
	public bool? Index { get; set; }

	/// <summary>
	/// <para>Metadata about the field.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("meta")]
	public IDictionary<string, string>? Meta { get; set; }
	[JsonInclude, JsonPropertyName("null_value")]
	public short? NullValue { get; set; }
	[JsonInclude, JsonPropertyName("on_script_error")]
	public Elastic.Clients.Elasticsearch.Mapping.OnScriptError? OnScriptError { get; set; }
	[JsonInclude, JsonPropertyName("properties")]
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }
	[JsonInclude, JsonPropertyName("script")]
	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }
	[JsonInclude, JsonPropertyName("similarity")]
	public string? Similarity { get; set; }
	[JsonInclude, JsonPropertyName("store")]
	public bool? Store { get; set; }

	/// <summary>
	/// <para>For internal use by Elastic only. Marks the field as a time series dimension. Defaults to false.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("time_series_dimension")]
	public bool? TimeSeriesDimension { get; set; }

	/// <summary>
	/// <para>For internal use by Elastic only. Marks the field as a time series dimension. Defaults to false.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("time_series_metric")]
	public Elastic.Clients.Elasticsearch.Mapping.TimeSeriesMetricType? TimeSeriesMetric { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "short";
}

public sealed partial class ShortNumberPropertyDescriptor<TDocument> : SerializableDescriptor<ShortNumberPropertyDescriptor<TDocument>>, IBuildableDescriptor<ShortNumberProperty>
{
	internal ShortNumberPropertyDescriptor(Action<ShortNumberPropertyDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ShortNumberPropertyDescriptor() : base()
	{
	}

	private double? BoostValue { get; set; }
	private bool? CoerceValue { get; set; }
	private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }
	private bool? DocValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private bool? IgnoreMalformedValue { get; set; }
	private bool? IndexValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private short? NullValueValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.OnScriptError? OnScriptErrorValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }
	private string? SimilarityValue { get; set; }
	private bool? StoreValue { get; set; }
	private bool? TimeSeriesDimensionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.TimeSeriesMetricType? TimeSeriesMetricValue { get; set; }

	public ShortNumberPropertyDescriptor<TDocument> Boost(double? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> Coerce(bool? coerce = true)
	{
		CoerceValue = coerce;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
	{
		CopyToValue = copyTo;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> DocValues(bool? docValues = true)
	{
		DocValuesValue = docValues;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> Fields(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> IgnoreMalformed(bool? ignoreMalformed = true)
	{
		IgnoreMalformedValue = ignoreMalformed;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> Index(bool? index = true)
	{
		IndexValue = index;
		return Self;
	}

	/// <summary>
	/// <para>Metadata about the field.</para>
	/// </summary>
	public ShortNumberPropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> NullValue(short? nullValue)
	{
		NullValueValue = nullValue;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> OnScriptError(Elastic.Clients.Elasticsearch.Mapping.OnScriptError? onScriptError)
	{
		OnScriptErrorValue = onScriptError;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> Properties(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptValue = script;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> Similarity(string? similarity)
	{
		SimilarityValue = similarity;
		return Self;
	}

	public ShortNumberPropertyDescriptor<TDocument> Store(bool? store = true)
	{
		StoreValue = store;
		return Self;
	}

	/// <summary>
	/// <para>For internal use by Elastic only. Marks the field as a time series dimension. Defaults to false.</para>
	/// </summary>
	public ShortNumberPropertyDescriptor<TDocument> TimeSeriesDimension(bool? timeSeriesDimension = true)
	{
		TimeSeriesDimensionValue = timeSeriesDimension;
		return Self;
	}

	/// <summary>
	/// <para>For internal use by Elastic only. Marks the field as a time series dimension. Defaults to false.</para>
	/// </summary>
	public ShortNumberPropertyDescriptor<TDocument> TimeSeriesMetric(Elastic.Clients.Elasticsearch.Mapping.TimeSeriesMetricType? timeSeriesMetric)
	{
		TimeSeriesMetricValue = timeSeriesMetric;
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

		if (TimeSeriesDimensionValue.HasValue)
		{
			writer.WritePropertyName("time_series_dimension");
			writer.WriteBooleanValue(TimeSeriesDimensionValue.Value);
		}

		if (TimeSeriesMetricValue is not null)
		{
			writer.WritePropertyName("time_series_metric");
			JsonSerializer.Serialize(writer, TimeSeriesMetricValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("short");
		writer.WriteEndObject();
	}

	ShortNumberProperty IBuildableDescriptor<ShortNumberProperty>.Build() => new()
	{
		Boost = BoostValue,
		Coerce = CoerceValue,
		CopyTo = CopyToValue,
		DocValues = DocValuesValue,
		Dynamic = DynamicValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		IgnoreMalformed = IgnoreMalformedValue,
		Index = IndexValue,
		Meta = MetaValue,
		NullValue = NullValueValue,
		OnScriptError = OnScriptErrorValue,
		Properties = PropertiesValue,
		Script = ScriptValue,
		Similarity = SimilarityValue,
		Store = StoreValue,
		TimeSeriesDimension = TimeSeriesDimensionValue,
		TimeSeriesMetric = TimeSeriesMetricValue
	};
}

public sealed partial class ShortNumberPropertyDescriptor : SerializableDescriptor<ShortNumberPropertyDescriptor>, IBuildableDescriptor<ShortNumberProperty>
{
	internal ShortNumberPropertyDescriptor(Action<ShortNumberPropertyDescriptor> configure) => configure.Invoke(this);

	public ShortNumberPropertyDescriptor() : base()
	{
	}

	private double? BoostValue { get; set; }
	private bool? CoerceValue { get; set; }
	private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }
	private bool? DocValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private bool? IgnoreMalformedValue { get; set; }
	private bool? IndexValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private short? NullValueValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.OnScriptError? OnScriptErrorValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }
	private string? SimilarityValue { get; set; }
	private bool? StoreValue { get; set; }
	private bool? TimeSeriesDimensionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.TimeSeriesMetricType? TimeSeriesMetricValue { get; set; }

	public ShortNumberPropertyDescriptor Boost(double? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public ShortNumberPropertyDescriptor Coerce(bool? coerce = true)
	{
		CoerceValue = coerce;
		return Self;
	}

	public ShortNumberPropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
	{
		CopyToValue = copyTo;
		return Self;
	}

	public ShortNumberPropertyDescriptor DocValues(bool? docValues = true)
	{
		DocValuesValue = docValues;
		return Self;
	}

	public ShortNumberPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public ShortNumberPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public ShortNumberPropertyDescriptor Fields<TDocument>(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public ShortNumberPropertyDescriptor Fields<TDocument>(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public ShortNumberPropertyDescriptor IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public ShortNumberPropertyDescriptor IgnoreMalformed(bool? ignoreMalformed = true)
	{
		IgnoreMalformedValue = ignoreMalformed;
		return Self;
	}

	public ShortNumberPropertyDescriptor Index(bool? index = true)
	{
		IndexValue = index;
		return Self;
	}

	/// <summary>
	/// <para>Metadata about the field.</para>
	/// </summary>
	public ShortNumberPropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public ShortNumberPropertyDescriptor NullValue(short? nullValue)
	{
		NullValueValue = nullValue;
		return Self;
	}

	public ShortNumberPropertyDescriptor OnScriptError(Elastic.Clients.Elasticsearch.Mapping.OnScriptError? onScriptError)
	{
		OnScriptErrorValue = onScriptError;
		return Self;
	}

	public ShortNumberPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public ShortNumberPropertyDescriptor Properties<TDocument>(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public ShortNumberPropertyDescriptor Properties<TDocument>(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public ShortNumberPropertyDescriptor Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptValue = script;
		return Self;
	}

	public ShortNumberPropertyDescriptor Similarity(string? similarity)
	{
		SimilarityValue = similarity;
		return Self;
	}

	public ShortNumberPropertyDescriptor Store(bool? store = true)
	{
		StoreValue = store;
		return Self;
	}

	/// <summary>
	/// <para>For internal use by Elastic only. Marks the field as a time series dimension. Defaults to false.</para>
	/// </summary>
	public ShortNumberPropertyDescriptor TimeSeriesDimension(bool? timeSeriesDimension = true)
	{
		TimeSeriesDimensionValue = timeSeriesDimension;
		return Self;
	}

	/// <summary>
	/// <para>For internal use by Elastic only. Marks the field as a time series dimension. Defaults to false.</para>
	/// </summary>
	public ShortNumberPropertyDescriptor TimeSeriesMetric(Elastic.Clients.Elasticsearch.Mapping.TimeSeriesMetricType? timeSeriesMetric)
	{
		TimeSeriesMetricValue = timeSeriesMetric;
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

		if (TimeSeriesDimensionValue.HasValue)
		{
			writer.WritePropertyName("time_series_dimension");
			writer.WriteBooleanValue(TimeSeriesDimensionValue.Value);
		}

		if (TimeSeriesMetricValue is not null)
		{
			writer.WritePropertyName("time_series_metric");
			JsonSerializer.Serialize(writer, TimeSeriesMetricValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("short");
		writer.WriteEndObject();
	}

	ShortNumberProperty IBuildableDescriptor<ShortNumberProperty>.Build() => new()
	{
		Boost = BoostValue,
		Coerce = CoerceValue,
		CopyTo = CopyToValue,
		DocValues = DocValuesValue,
		Dynamic = DynamicValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		IgnoreMalformed = IgnoreMalformedValue,
		Index = IndexValue,
		Meta = MetaValue,
		NullValue = NullValueValue,
		OnScriptError = OnScriptErrorValue,
		Properties = PropertiesValue,
		Script = ScriptValue,
		Similarity = SimilarityValue,
		Store = StoreValue,
		TimeSeriesDimension = TimeSeriesDimensionValue,
		TimeSeriesMetric = TimeSeriesMetricValue
	};
}
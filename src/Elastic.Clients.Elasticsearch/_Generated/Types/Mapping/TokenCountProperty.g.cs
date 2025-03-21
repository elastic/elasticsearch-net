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

public sealed partial class TokenCountProperty : IProperty
{
	[JsonInclude, JsonPropertyName("analyzer")]
	public string? Analyzer { get; set; }
	[JsonInclude, JsonPropertyName("boost")]
	public double? Boost { get; set; }
	[JsonInclude, JsonPropertyName("copy_to")]
	[JsonConverter(typeof(SingleOrManyFieldsConverter))]
	public Elastic.Clients.Elasticsearch.Fields? CopyTo { get; set; }
	[JsonInclude, JsonPropertyName("doc_values")]
	public bool? DocValues { get; set; }
	[JsonInclude, JsonPropertyName("dynamic")]
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }
	[JsonInclude, JsonPropertyName("enable_position_increments")]
	public bool? EnablePositionIncrements { get; set; }
	[JsonInclude, JsonPropertyName("fields")]
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }
	[JsonInclude, JsonPropertyName("ignore_above")]
	public int? IgnoreAbove { get; set; }
	[JsonInclude, JsonPropertyName("index")]
	public bool? Index { get; set; }

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("meta")]
	public IDictionary<string, string>? Meta { get; set; }
	[JsonInclude, JsonPropertyName("null_value")]
	public double? NullValue { get; set; }
	[JsonInclude, JsonPropertyName("properties")]
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }
	[JsonInclude, JsonPropertyName("store")]
	public bool? Store { get; set; }
	[JsonInclude, JsonPropertyName("synthetic_source_keep")]
	public Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? SyntheticSourceKeep { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "token_count";
}

public sealed partial class TokenCountPropertyDescriptor<TDocument> : SerializableDescriptor<TokenCountPropertyDescriptor<TDocument>>, IBuildableDescriptor<TokenCountProperty>
{
	internal TokenCountPropertyDescriptor(Action<TokenCountPropertyDescriptor<TDocument>> configure) => configure.Invoke(this);

	public TokenCountPropertyDescriptor() : base()
	{
	}

	private string? AnalyzerValue { get; set; }
	private double? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }
	private bool? DocValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private bool? EnablePositionIncrementsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private bool? IndexValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private double? NullValueValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private bool? StoreValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? SyntheticSourceKeepValue { get; set; }

	public TokenCountPropertyDescriptor<TDocument> Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> Boost(double? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
	{
		CopyToValue = copyTo;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> DocValues(bool? docValues = true)
	{
		DocValuesValue = docValues;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> EnablePositionIncrements(bool? enablePositionIncrements = true)
	{
		EnablePositionIncrementsValue = enablePositionIncrements;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> Fields(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> Index(bool? index = true)
	{
		IndexValue = index;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public TokenCountPropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> NullValue(double? nullValue)
	{
		NullValueValue = nullValue;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> Properties(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> Store(bool? store = true)
	{
		StoreValue = store;
		return Self;
	}

	public TokenCountPropertyDescriptor<TDocument> SyntheticSourceKeep(Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? syntheticSourceKeep)
	{
		SyntheticSourceKeepValue = syntheticSourceKeep;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(AnalyzerValue))
		{
			writer.WritePropertyName("analyzer");
			writer.WriteStringValue(AnalyzerValue);
		}

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

		if (EnablePositionIncrementsValue.HasValue)
		{
			writer.WritePropertyName("enable_position_increments");
			writer.WriteBooleanValue(EnablePositionIncrementsValue.Value);
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

		if (PropertiesValue is not null)
		{
			writer.WritePropertyName("properties");
			JsonSerializer.Serialize(writer, PropertiesValue, options);
		}

		if (StoreValue.HasValue)
		{
			writer.WritePropertyName("store");
			writer.WriteBooleanValue(StoreValue.Value);
		}

		if (SyntheticSourceKeepValue is not null)
		{
			writer.WritePropertyName("synthetic_source_keep");
			JsonSerializer.Serialize(writer, SyntheticSourceKeepValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("token_count");
		writer.WriteEndObject();
	}

	TokenCountProperty IBuildableDescriptor<TokenCountProperty>.Build() => new()
	{
		Analyzer = AnalyzerValue,
		Boost = BoostValue,
		CopyTo = CopyToValue,
		DocValues = DocValuesValue,
		Dynamic = DynamicValue,
		EnablePositionIncrements = EnablePositionIncrementsValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		Index = IndexValue,
		Meta = MetaValue,
		NullValue = NullValueValue,
		Properties = PropertiesValue,
		Store = StoreValue,
		SyntheticSourceKeep = SyntheticSourceKeepValue
	};
}

public sealed partial class TokenCountPropertyDescriptor : SerializableDescriptor<TokenCountPropertyDescriptor>, IBuildableDescriptor<TokenCountProperty>
{
	internal TokenCountPropertyDescriptor(Action<TokenCountPropertyDescriptor> configure) => configure.Invoke(this);

	public TokenCountPropertyDescriptor() : base()
	{
	}

	private string? AnalyzerValue { get; set; }
	private double? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }
	private bool? DocValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private bool? EnablePositionIncrementsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private bool? IndexValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private double? NullValueValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private bool? StoreValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? SyntheticSourceKeepValue { get; set; }

	public TokenCountPropertyDescriptor Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	public TokenCountPropertyDescriptor Boost(double? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public TokenCountPropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
	{
		CopyToValue = copyTo;
		return Self;
	}

	public TokenCountPropertyDescriptor DocValues(bool? docValues = true)
	{
		DocValuesValue = docValues;
		return Self;
	}

	public TokenCountPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public TokenCountPropertyDescriptor EnablePositionIncrements(bool? enablePositionIncrements = true)
	{
		EnablePositionIncrementsValue = enablePositionIncrements;
		return Self;
	}

	public TokenCountPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public TokenCountPropertyDescriptor Fields<TDocument>(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public TokenCountPropertyDescriptor Fields<TDocument>(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public TokenCountPropertyDescriptor IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public TokenCountPropertyDescriptor Index(bool? index = true)
	{
		IndexValue = index;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public TokenCountPropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public TokenCountPropertyDescriptor NullValue(double? nullValue)
	{
		NullValueValue = nullValue;
		return Self;
	}

	public TokenCountPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public TokenCountPropertyDescriptor Properties<TDocument>(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public TokenCountPropertyDescriptor Properties<TDocument>(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public TokenCountPropertyDescriptor Store(bool? store = true)
	{
		StoreValue = store;
		return Self;
	}

	public TokenCountPropertyDescriptor SyntheticSourceKeep(Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? syntheticSourceKeep)
	{
		SyntheticSourceKeepValue = syntheticSourceKeep;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(AnalyzerValue))
		{
			writer.WritePropertyName("analyzer");
			writer.WriteStringValue(AnalyzerValue);
		}

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

		if (EnablePositionIncrementsValue.HasValue)
		{
			writer.WritePropertyName("enable_position_increments");
			writer.WriteBooleanValue(EnablePositionIncrementsValue.Value);
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

		if (PropertiesValue is not null)
		{
			writer.WritePropertyName("properties");
			JsonSerializer.Serialize(writer, PropertiesValue, options);
		}

		if (StoreValue.HasValue)
		{
			writer.WritePropertyName("store");
			writer.WriteBooleanValue(StoreValue.Value);
		}

		if (SyntheticSourceKeepValue is not null)
		{
			writer.WritePropertyName("synthetic_source_keep");
			JsonSerializer.Serialize(writer, SyntheticSourceKeepValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("token_count");
		writer.WriteEndObject();
	}

	TokenCountProperty IBuildableDescriptor<TokenCountProperty>.Build() => new()
	{
		Analyzer = AnalyzerValue,
		Boost = BoostValue,
		CopyTo = CopyToValue,
		DocValues = DocValuesValue,
		Dynamic = DynamicValue,
		EnablePositionIncrements = EnablePositionIncrementsValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		Index = IndexValue,
		Meta = MetaValue,
		NullValue = NullValueValue,
		Properties = PropertiesValue,
		Store = StoreValue,
		SyntheticSourceKeep = SyntheticSourceKeepValue
	};
}
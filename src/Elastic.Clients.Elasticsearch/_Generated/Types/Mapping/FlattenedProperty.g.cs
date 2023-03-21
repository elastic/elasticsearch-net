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

public sealed partial class FlattenedProperty : IProperty
{
	[JsonInclude, JsonPropertyName("boost")]
	public double? Boost { get; set; }
	[JsonInclude, JsonPropertyName("depth_limit")]
	public int? DepthLimit { get; set; }
	[JsonInclude, JsonPropertyName("doc_values")]
	public bool? DocValues { get; set; }
	[JsonInclude, JsonPropertyName("dynamic")]
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }
	[JsonInclude, JsonPropertyName("eager_global_ordinals")]
	public bool? EagerGlobalOrdinals { get; set; }
	[JsonInclude, JsonPropertyName("fields")]
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }
	[JsonInclude, JsonPropertyName("ignore_above")]
	public int? IgnoreAbove { get; set; }
	[JsonInclude, JsonPropertyName("index")]
	public bool? Index { get; set; }
	[JsonInclude, JsonPropertyName("index_options")]
	public Elastic.Clients.Elasticsearch.Mapping.IndexOptions? IndexOptions { get; set; }
	[JsonInclude, JsonPropertyName("meta")]
	public IDictionary<string, string>? Meta { get; set; }
	[JsonInclude, JsonPropertyName("null_value")]
	public string? NullValue { get; set; }
	[JsonInclude, JsonPropertyName("properties")]
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }
	[JsonInclude, JsonPropertyName("similarity")]
	public string? Similarity { get; set; }
	[JsonInclude, JsonPropertyName("split_queries_on_whitespace")]
	public bool? SplitQueriesOnWhitespace { get; set; }

	[JsonInclude]
	[JsonPropertyName("type")]
	public string Type => "flattened";
}

public sealed partial class FlattenedPropertyDescriptor<TDocument> : SerializableDescriptor<FlattenedPropertyDescriptor<TDocument>>, IBuildableDescriptor<FlattenedProperty>
{
	internal FlattenedPropertyDescriptor(Action<FlattenedPropertyDescriptor<TDocument>> configure) => configure.Invoke(this);

	public FlattenedPropertyDescriptor() : base()
	{
	}

	private double? BoostValue { get; set; }
	private int? DepthLimitValue { get; set; }
	private bool? DocValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private bool? EagerGlobalOrdinalsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private bool? IndexValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.IndexOptions? IndexOptionsValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private string? NullValueValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private string? SimilarityValue { get; set; }
	private bool? SplitQueriesOnWhitespaceValue { get; set; }

	public FlattenedPropertyDescriptor<TDocument> Boost(double? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> DepthLimit(int? depthLimit)
	{
		DepthLimitValue = depthLimit;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> DocValues(bool? docValues = true)
	{
		DocValuesValue = docValues;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> EagerGlobalOrdinals(bool? eagerGlobalOrdinals = true)
	{
		EagerGlobalOrdinalsValue = eagerGlobalOrdinals;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> Fields(PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> Fields(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> Index(bool? index = true)
	{
		IndexValue = index;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> IndexOptions(Elastic.Clients.Elasticsearch.Mapping.IndexOptions? indexOptions)
	{
		IndexOptionsValue = indexOptions;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> NullValue(string? nullValue)
	{
		NullValueValue = nullValue;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> Properties(PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> Properties(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> Similarity(string? similarity)
	{
		SimilarityValue = similarity;
		return Self;
	}

	public FlattenedPropertyDescriptor<TDocument> SplitQueriesOnWhitespace(bool? splitQueriesOnWhitespace = true)
	{
		SplitQueriesOnWhitespaceValue = splitQueriesOnWhitespace;
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

		if (DepthLimitValue.HasValue)
		{
			writer.WritePropertyName("depth_limit");
			writer.WriteNumberValue(DepthLimitValue.Value);
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

		if (EagerGlobalOrdinalsValue.HasValue)
		{
			writer.WritePropertyName("eager_global_ordinals");
			writer.WriteBooleanValue(EagerGlobalOrdinalsValue.Value);
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

		if (IndexOptionsValue is not null)
		{
			writer.WritePropertyName("index_options");
			JsonSerializer.Serialize(writer, IndexOptionsValue, options);
		}

		if (MetaValue is not null)
		{
			writer.WritePropertyName("meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		if (!string.IsNullOrEmpty(NullValueValue))
		{
			writer.WritePropertyName("null_value");
			writer.WriteStringValue(NullValueValue);
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

		if (SplitQueriesOnWhitespaceValue.HasValue)
		{
			writer.WritePropertyName("split_queries_on_whitespace");
			writer.WriteBooleanValue(SplitQueriesOnWhitespaceValue.Value);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("flattened");
		writer.WriteEndObject();
	}

	FlattenedProperty IBuildableDescriptor<FlattenedProperty>.Build() => new()
	{
		Boost = BoostValue,
		DepthLimit = DepthLimitValue,
		DocValues = DocValuesValue,
		Dynamic = DynamicValue,
		EagerGlobalOrdinals = EagerGlobalOrdinalsValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		Index = IndexValue,
		IndexOptions = IndexOptionsValue,
		Meta = MetaValue,
		NullValue = NullValueValue,
		Properties = PropertiesValue,
		Similarity = SimilarityValue,
		SplitQueriesOnWhitespace = SplitQueriesOnWhitespaceValue
	};
}

public sealed partial class FlattenedPropertyDescriptor : SerializableDescriptor<FlattenedPropertyDescriptor>, IBuildableDescriptor<FlattenedProperty>
{
	internal FlattenedPropertyDescriptor(Action<FlattenedPropertyDescriptor> configure) => configure.Invoke(this);

	public FlattenedPropertyDescriptor() : base()
	{
	}

	private double? BoostValue { get; set; }
	private int? DepthLimitValue { get; set; }
	private bool? DocValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private bool? EagerGlobalOrdinalsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private bool? IndexValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.IndexOptions? IndexOptionsValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private string? NullValueValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private string? SimilarityValue { get; set; }
	private bool? SplitQueriesOnWhitespaceValue { get; set; }

	public FlattenedPropertyDescriptor Boost(double? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public FlattenedPropertyDescriptor DepthLimit(int? depthLimit)
	{
		DepthLimitValue = depthLimit;
		return Self;
	}

	public FlattenedPropertyDescriptor DocValues(bool? docValues = true)
	{
		DocValuesValue = docValues;
		return Self;
	}

	public FlattenedPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public FlattenedPropertyDescriptor EagerGlobalOrdinals(bool? eagerGlobalOrdinals = true)
	{
		EagerGlobalOrdinalsValue = eagerGlobalOrdinals;
		return Self;
	}

	public FlattenedPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public FlattenedPropertyDescriptor Fields<TDocument>(PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public FlattenedPropertyDescriptor Fields<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public FlattenedPropertyDescriptor IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public FlattenedPropertyDescriptor Index(bool? index = true)
	{
		IndexValue = index;
		return Self;
	}

	public FlattenedPropertyDescriptor IndexOptions(Elastic.Clients.Elasticsearch.Mapping.IndexOptions? indexOptions)
	{
		IndexOptionsValue = indexOptions;
		return Self;
	}

	public FlattenedPropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public FlattenedPropertyDescriptor NullValue(string? nullValue)
	{
		NullValueValue = nullValue;
		return Self;
	}

	public FlattenedPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public FlattenedPropertyDescriptor Properties<TDocument>(PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public FlattenedPropertyDescriptor Properties<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public FlattenedPropertyDescriptor Similarity(string? similarity)
	{
		SimilarityValue = similarity;
		return Self;
	}

	public FlattenedPropertyDescriptor SplitQueriesOnWhitespace(bool? splitQueriesOnWhitespace = true)
	{
		SplitQueriesOnWhitespaceValue = splitQueriesOnWhitespace;
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

		if (DepthLimitValue.HasValue)
		{
			writer.WritePropertyName("depth_limit");
			writer.WriteNumberValue(DepthLimitValue.Value);
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

		if (EagerGlobalOrdinalsValue.HasValue)
		{
			writer.WritePropertyName("eager_global_ordinals");
			writer.WriteBooleanValue(EagerGlobalOrdinalsValue.Value);
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

		if (IndexOptionsValue is not null)
		{
			writer.WritePropertyName("index_options");
			JsonSerializer.Serialize(writer, IndexOptionsValue, options);
		}

		if (MetaValue is not null)
		{
			writer.WritePropertyName("meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		if (!string.IsNullOrEmpty(NullValueValue))
		{
			writer.WritePropertyName("null_value");
			writer.WriteStringValue(NullValueValue);
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

		if (SplitQueriesOnWhitespaceValue.HasValue)
		{
			writer.WritePropertyName("split_queries_on_whitespace");
			writer.WriteBooleanValue(SplitQueriesOnWhitespaceValue.Value);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("flattened");
		writer.WriteEndObject();
	}

	FlattenedProperty IBuildableDescriptor<FlattenedProperty>.Build() => new()
	{
		Boost = BoostValue,
		DepthLimit = DepthLimitValue,
		DocValues = DocValuesValue,
		Dynamic = DynamicValue,
		EagerGlobalOrdinals = EagerGlobalOrdinalsValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		Index = IndexValue,
		IndexOptions = IndexOptionsValue,
		Meta = MetaValue,
		NullValue = NullValueValue,
		Properties = PropertiesValue,
		Similarity = SimilarityValue,
		SplitQueriesOnWhitespace = SplitQueriesOnWhitespaceValue
	};
}
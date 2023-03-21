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

public sealed partial class ConstantKeywordProperty : IProperty
{
	[JsonInclude, JsonPropertyName("dynamic")]
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }
	[JsonInclude, JsonPropertyName("fields")]
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }
	[JsonInclude, JsonPropertyName("ignore_above")]
	public int? IgnoreAbove { get; set; }
	[JsonInclude, JsonPropertyName("meta")]
	public IDictionary<string, string>? Meta { get; set; }
	[JsonInclude, JsonPropertyName("properties")]
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }

	[JsonInclude]
	[JsonPropertyName("type")]
	public string Type => "constant_keyword";

	[JsonInclude, JsonPropertyName("value")]
	public object? Value { get; set; }
}

public sealed partial class ConstantKeywordPropertyDescriptor<TDocument> : SerializableDescriptor<ConstantKeywordPropertyDescriptor<TDocument>>, IBuildableDescriptor<ConstantKeywordProperty>
{
	internal ConstantKeywordPropertyDescriptor(Action<ConstantKeywordPropertyDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ConstantKeywordPropertyDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private object? ValueValue { get; set; }

	public ConstantKeywordPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor<TDocument> Fields(PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor<TDocument> Fields(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public ConstantKeywordPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor<TDocument> Properties(PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor<TDocument> Properties(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor<TDocument> Value(object? value)
	{
		ValueValue = value;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
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

		writer.WritePropertyName("type");
		writer.WriteStringValue("constant_keyword");
		if (ValueValue is not null)
		{
			writer.WritePropertyName("value");
			JsonSerializer.Serialize(writer, ValueValue, options);
		}

		writer.WriteEndObject();
	}

	ConstantKeywordProperty IBuildableDescriptor<ConstantKeywordProperty>.Build() => new()
	{
		Dynamic = DynamicValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		Meta = MetaValue,
		Properties = PropertiesValue,
		Value = ValueValue
	};
}

public sealed partial class ConstantKeywordPropertyDescriptor : SerializableDescriptor<ConstantKeywordPropertyDescriptor>, IBuildableDescriptor<ConstantKeywordProperty>
{
	internal ConstantKeywordPropertyDescriptor(Action<ConstantKeywordPropertyDescriptor> configure) => configure.Invoke(this);

	public ConstantKeywordPropertyDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private object? ValueValue { get; set; }

	public ConstantKeywordPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor Fields<TDocument>(PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor Fields<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public ConstantKeywordPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor Properties<TDocument>(PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor Properties<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public ConstantKeywordPropertyDescriptor Value(object? value)
	{
		ValueValue = value;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
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

		writer.WritePropertyName("type");
		writer.WriteStringValue("constant_keyword");
		if (ValueValue is not null)
		{
			writer.WritePropertyName("value");
			JsonSerializer.Serialize(writer, ValueValue, options);
		}

		writer.WriteEndObject();
	}

	ConstantKeywordProperty IBuildableDescriptor<ConstantKeywordProperty>.Build() => new()
	{
		Dynamic = DynamicValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		Meta = MetaValue,
		Properties = PropertiesValue,
		Value = ValueValue
	};
}
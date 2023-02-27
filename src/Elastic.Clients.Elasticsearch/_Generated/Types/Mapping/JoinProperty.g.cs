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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Mapping;
public sealed partial class JoinProperty : IProperty
{
	[JsonInclude, JsonPropertyName("dynamic")]
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }

	[JsonInclude, JsonPropertyName("eager_global_ordinals")]
	public bool? EagerGlobalOrdinals { get; set; }

	[JsonInclude, JsonPropertyName("fields")]
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }

	[JsonInclude, JsonPropertyName("ignore_above")]
	public int? IgnoreAbove { get; set; }

	[JsonInclude, JsonPropertyName("meta")]
	public IDictionary<string, string>? Meta { get; set; }

	[JsonInclude, JsonPropertyName("properties")]
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }

	[JsonInclude, JsonPropertyName("relations")]
	public IDictionary<string, ICollection<string>>? Relations { get; set; }

	[JsonInclude]
	[JsonPropertyName("type")]
	public string Type => "join";
}

public sealed partial class JoinPropertyDescriptor<TDocument> : SerializableDescriptor<JoinPropertyDescriptor<TDocument>>, IBuildableDescriptor<JoinProperty>
{
	internal JoinPropertyDescriptor(Action<JoinPropertyDescriptor<TDocument>> configure) => configure.Invoke(this);
	public JoinPropertyDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

	private bool? EagerGlobalOrdinalsValue { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

	private int? IgnoreAboveValue { get; set; }

	private IDictionary<string, string>? MetaValue { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

	private IDictionary<string, ICollection<string>>? RelationsValue { get; set; }

	public JoinPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public JoinPropertyDescriptor<TDocument> EagerGlobalOrdinals(bool? eagerGlobalOrdinals = true)
	{
		EagerGlobalOrdinalsValue = eagerGlobalOrdinals;
		return Self;
	}

	public JoinPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public JoinPropertyDescriptor<TDocument> Fields(PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public JoinPropertyDescriptor<TDocument> Fields(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public JoinPropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public JoinPropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public JoinPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public JoinPropertyDescriptor<TDocument> Properties(PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public JoinPropertyDescriptor<TDocument> Properties(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public JoinPropertyDescriptor<TDocument> Relations(Func<FluentDictionary<string, ICollection<string>>, FluentDictionary<string, ICollection<string>>> selector)
	{
		RelationsValue = selector?.Invoke(new FluentDictionary<string, ICollection<string>>());
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

		if (RelationsValue is not null)
		{
			writer.WritePropertyName("relations");
			JsonSerializer.Serialize(writer, RelationsValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("join");
		writer.WriteEndObject();
	}

	JoinProperty IBuildableDescriptor<JoinProperty>.Build() => new()
	{
		Dynamic = DynamicValue,
		EagerGlobalOrdinals = EagerGlobalOrdinalsValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		Meta = MetaValue,
		Properties = PropertiesValue,
		Relations = RelationsValue
	};
}

public sealed partial class JoinPropertyDescriptor : SerializableDescriptor<JoinPropertyDescriptor>, IBuildableDescriptor<JoinProperty>
{
	internal JoinPropertyDescriptor(Action<JoinPropertyDescriptor> configure) => configure.Invoke(this);
	public JoinPropertyDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

	private bool? EagerGlobalOrdinalsValue { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

	private int? IgnoreAboveValue { get; set; }

	private IDictionary<string, string>? MetaValue { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

	private IDictionary<string, ICollection<string>>? RelationsValue { get; set; }

	public JoinPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public JoinPropertyDescriptor EagerGlobalOrdinals(bool? eagerGlobalOrdinals = true)
	{
		EagerGlobalOrdinalsValue = eagerGlobalOrdinals;
		return Self;
	}

	public JoinPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public JoinPropertyDescriptor Fields<TDocument>(PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public JoinPropertyDescriptor Fields<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public JoinPropertyDescriptor IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public JoinPropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public JoinPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public JoinPropertyDescriptor Properties<TDocument>(PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public JoinPropertyDescriptor Properties<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public JoinPropertyDescriptor Relations(Func<FluentDictionary<string, ICollection<string>>, FluentDictionary<string, ICollection<string>>> selector)
	{
		RelationsValue = selector?.Invoke(new FluentDictionary<string, ICollection<string>>());
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

		if (RelationsValue is not null)
		{
			writer.WritePropertyName("relations");
			JsonSerializer.Serialize(writer, RelationsValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("join");
		writer.WriteEndObject();
	}

	JoinProperty IBuildableDescriptor<JoinProperty>.Build() => new()
	{
		Dynamic = DynamicValue,
		EagerGlobalOrdinals = EagerGlobalOrdinalsValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		Meta = MetaValue,
		Properties = PropertiesValue,
		Relations = RelationsValue
	};
}
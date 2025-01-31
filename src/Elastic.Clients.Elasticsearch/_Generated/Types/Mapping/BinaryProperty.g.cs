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

internal sealed partial class BinaryPropertyConverter : System.Text.Json.Serialization.JsonConverter<BinaryProperty>
{
	private static readonly System.Text.Json.JsonEncodedText PropCopyTo = System.Text.Json.JsonEncodedText.Encode("copy_to");
	private static readonly System.Text.Json.JsonEncodedText PropDocValues = System.Text.Json.JsonEncodedText.Encode("doc_values");
	private static readonly System.Text.Json.JsonEncodedText PropDynamic = System.Text.Json.JsonEncodedText.Encode("dynamic");
	private static readonly System.Text.Json.JsonEncodedText PropFields = System.Text.Json.JsonEncodedText.Encode("fields");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreAbove = System.Text.Json.JsonEncodedText.Encode("ignore_above");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropProperties = System.Text.Json.JsonEncodedText.Encode("properties");
	private static readonly System.Text.Json.JsonEncodedText PropStore = System.Text.Json.JsonEncodedText.Encode("store");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override BinaryProperty Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propCopyTo = default;
		LocalJsonValue<bool?> propDocValues = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.DynamicMapping?> propDynamic = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propFields = default;
		LocalJsonValue<int?> propIgnoreAbove = default;
		LocalJsonValue<IDictionary<string, string>?> propMeta = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propProperties = default;
		LocalJsonValue<bool?> propStore = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCopyTo.TryRead(ref reader, options, PropCopyTo, typeof(SingleOrManyFieldsMarker)))
			{
				continue;
			}

			if (propDocValues.TryRead(ref reader, options, PropDocValues))
			{
				continue;
			}

			if (propDynamic.TryRead(ref reader, options, PropDynamic))
			{
				continue;
			}

			if (propFields.TryRead(ref reader, options, PropFields))
			{
				continue;
			}

			if (propIgnoreAbove.TryRead(ref reader, options, PropIgnoreAbove))
			{
				continue;
			}

			if (propMeta.TryRead(ref reader, options, PropMeta))
			{
				continue;
			}

			if (propProperties.TryRead(ref reader, options, PropProperties))
			{
				continue;
			}

			if (propStore.TryRead(ref reader, options, PropStore))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new BinaryProperty
		{
			CopyTo = propCopyTo.Value
,
			DocValues = propDocValues.Value
,
			Dynamic = propDynamic.Value
,
			Fields = propFields.Value
,
			IgnoreAbove = propIgnoreAbove.Value
,
			Meta = propMeta.Value
,
			Properties = propProperties.Value
,
			Store = propStore.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, BinaryProperty value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCopyTo, value.CopyTo, null, typeof(SingleOrManyFieldsMarker));
		writer.WriteProperty(options, PropDocValues, value.DocValues);
		writer.WriteProperty(options, PropDynamic, value.Dynamic);
		writer.WriteProperty(options, PropFields, value.Fields);
		writer.WriteProperty(options, PropIgnoreAbove, value.IgnoreAbove);
		writer.WriteProperty(options, PropMeta, value.Meta);
		writer.WriteProperty(options, PropProperties, value.Properties);
		writer.WriteProperty(options, PropStore, value.Store);
		writer.WriteProperty(options, PropType, value.Type);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(BinaryPropertyConverter))]
public sealed partial class BinaryProperty : IProperty
{
	public Elastic.Clients.Elasticsearch.Fields? CopyTo { get; set; }
	public bool? DocValues { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }
	public int? IgnoreAbove { get; set; }

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public IDictionary<string, string>? Meta { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }
	public bool? Store { get; set; }

	public string Type => "binary";
}

public sealed partial class BinaryPropertyDescriptor<TDocument> : SerializableDescriptor<BinaryPropertyDescriptor<TDocument>>, IBuildableDescriptor<BinaryProperty>
{
	internal BinaryPropertyDescriptor(Action<BinaryPropertyDescriptor<TDocument>> configure) => configure.Invoke(this);

	public BinaryPropertyDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }
	private bool? DocValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private bool? StoreValue { get; set; }

	public BinaryPropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
	{
		CopyToValue = copyTo;
		return Self;
	}

	public BinaryPropertyDescriptor<TDocument> DocValues(bool? docValues = true)
	{
		DocValuesValue = docValues;
		return Self;
	}

	public BinaryPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public BinaryPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public BinaryPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public BinaryPropertyDescriptor<TDocument> Fields(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public BinaryPropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public BinaryPropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public BinaryPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public BinaryPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public BinaryPropertyDescriptor<TDocument> Properties(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public BinaryPropertyDescriptor<TDocument> Store(bool? store = true)
	{
		StoreValue = store;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
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

		if (StoreValue.HasValue)
		{
			writer.WritePropertyName("store");
			writer.WriteBooleanValue(StoreValue.Value);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("binary");
		writer.WriteEndObject();
	}

	BinaryProperty IBuildableDescriptor<BinaryProperty>.Build() => new()
	{
		CopyTo = CopyToValue,
		DocValues = DocValuesValue,
		Dynamic = DynamicValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		Meta = MetaValue,
		Properties = PropertiesValue,
		Store = StoreValue
	};
}

public sealed partial class BinaryPropertyDescriptor : SerializableDescriptor<BinaryPropertyDescriptor>, IBuildableDescriptor<BinaryProperty>
{
	internal BinaryPropertyDescriptor(Action<BinaryPropertyDescriptor> configure) => configure.Invoke(this);

	public BinaryPropertyDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }
	private bool? DocValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private bool? StoreValue { get; set; }

	public BinaryPropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
	{
		CopyToValue = copyTo;
		return Self;
	}

	public BinaryPropertyDescriptor DocValues(bool? docValues = true)
	{
		DocValuesValue = docValues;
		return Self;
	}

	public BinaryPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public BinaryPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public BinaryPropertyDescriptor Fields<TDocument>(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public BinaryPropertyDescriptor Fields<TDocument>(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public BinaryPropertyDescriptor IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public BinaryPropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public BinaryPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public BinaryPropertyDescriptor Properties<TDocument>(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public BinaryPropertyDescriptor Properties<TDocument>(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public BinaryPropertyDescriptor Store(bool? store = true)
	{
		StoreValue = store;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
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

		if (StoreValue.HasValue)
		{
			writer.WritePropertyName("store");
			writer.WriteBooleanValue(StoreValue.Value);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("binary");
		writer.WriteEndObject();
	}

	BinaryProperty IBuildableDescriptor<BinaryProperty>.Build() => new()
	{
		CopyTo = CopyToValue,
		DocValues = DocValuesValue,
		Dynamic = DynamicValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		Meta = MetaValue,
		Properties = PropertiesValue,
		Store = StoreValue
	};
}
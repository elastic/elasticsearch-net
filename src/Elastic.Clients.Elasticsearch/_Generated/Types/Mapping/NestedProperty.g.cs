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

internal sealed partial class NestedPropertyConverter : System.Text.Json.Serialization.JsonConverter<NestedProperty>
{
	private static readonly System.Text.Json.JsonEncodedText PropCopyTo = System.Text.Json.JsonEncodedText.Encode("copy_to");
	private static readonly System.Text.Json.JsonEncodedText PropDynamic = System.Text.Json.JsonEncodedText.Encode("dynamic");
	private static readonly System.Text.Json.JsonEncodedText PropEnabled = System.Text.Json.JsonEncodedText.Encode("enabled");
	private static readonly System.Text.Json.JsonEncodedText PropFields = System.Text.Json.JsonEncodedText.Encode("fields");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreAbove = System.Text.Json.JsonEncodedText.Encode("ignore_above");
	private static readonly System.Text.Json.JsonEncodedText PropIncludeInParent = System.Text.Json.JsonEncodedText.Encode("include_in_parent");
	private static readonly System.Text.Json.JsonEncodedText PropIncludeInRoot = System.Text.Json.JsonEncodedText.Encode("include_in_root");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropProperties = System.Text.Json.JsonEncodedText.Encode("properties");
	private static readonly System.Text.Json.JsonEncodedText PropStore = System.Text.Json.JsonEncodedText.Encode("store");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override NestedProperty Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propCopyTo = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.DynamicMapping?> propDynamic = default;
		LocalJsonValue<bool?> propEnabled = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propFields = default;
		LocalJsonValue<int?> propIgnoreAbove = default;
		LocalJsonValue<bool?> propIncludeInParent = default;
		LocalJsonValue<bool?> propIncludeInRoot = default;
		LocalJsonValue<IDictionary<string, string>?> propMeta = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propProperties = default;
		LocalJsonValue<bool?> propStore = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCopyTo.TryRead(ref reader, options, PropCopyTo, typeof(SingleOrManyFieldsMarker)))
			{
				continue;
			}

			if (propDynamic.TryRead(ref reader, options, PropDynamic))
			{
				continue;
			}

			if (propEnabled.TryRead(ref reader, options, PropEnabled))
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

			if (propIncludeInParent.TryRead(ref reader, options, PropIncludeInParent))
			{
				continue;
			}

			if (propIncludeInRoot.TryRead(ref reader, options, PropIncludeInRoot))
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
		return new NestedProperty
		{
			CopyTo = propCopyTo.Value
,
			Dynamic = propDynamic.Value
,
			Enabled = propEnabled.Value
,
			Fields = propFields.Value
,
			IgnoreAbove = propIgnoreAbove.Value
,
			IncludeInParent = propIncludeInParent.Value
,
			IncludeInRoot = propIncludeInRoot.Value
,
			Meta = propMeta.Value
,
			Properties = propProperties.Value
,
			Store = propStore.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, NestedProperty value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCopyTo, value.CopyTo, null, typeof(SingleOrManyFieldsMarker));
		writer.WriteProperty(options, PropDynamic, value.Dynamic);
		writer.WriteProperty(options, PropEnabled, value.Enabled);
		writer.WriteProperty(options, PropFields, value.Fields);
		writer.WriteProperty(options, PropIgnoreAbove, value.IgnoreAbove);
		writer.WriteProperty(options, PropIncludeInParent, value.IncludeInParent);
		writer.WriteProperty(options, PropIncludeInRoot, value.IncludeInRoot);
		writer.WriteProperty(options, PropMeta, value.Meta);
		writer.WriteProperty(options, PropProperties, value.Properties);
		writer.WriteProperty(options, PropStore, value.Store);
		writer.WriteProperty(options, PropType, value.Type);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(NestedPropertyConverter))]
public sealed partial class NestedProperty : IProperty
{
	public Elastic.Clients.Elasticsearch.Fields? CopyTo { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }
	public bool? Enabled { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }
	public int? IgnoreAbove { get; set; }
	public bool? IncludeInParent { get; set; }
	public bool? IncludeInRoot { get; set; }

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public IDictionary<string, string>? Meta { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }
	public bool? Store { get; set; }

	public string Type => "nested";
}

public sealed partial class NestedPropertyDescriptor<TDocument> : SerializableDescriptor<NestedPropertyDescriptor<TDocument>>, IBuildableDescriptor<NestedProperty>
{
	internal NestedPropertyDescriptor(Action<NestedPropertyDescriptor<TDocument>> configure) => configure.Invoke(this);

	public NestedPropertyDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private bool? EnabledValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private bool? IncludeInParentValue { get; set; }
	private bool? IncludeInRootValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private bool? StoreValue { get; set; }

	public NestedPropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
	{
		CopyToValue = copyTo;
		return Self;
	}

	public NestedPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public NestedPropertyDescriptor<TDocument> Enabled(bool? enabled = true)
	{
		EnabledValue = enabled;
		return Self;
	}

	public NestedPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public NestedPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public NestedPropertyDescriptor<TDocument> Fields(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public NestedPropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public NestedPropertyDescriptor<TDocument> IncludeInParent(bool? includeInParent = true)
	{
		IncludeInParentValue = includeInParent;
		return Self;
	}

	public NestedPropertyDescriptor<TDocument> IncludeInRoot(bool? includeInRoot = true)
	{
		IncludeInRootValue = includeInRoot;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public NestedPropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public NestedPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public NestedPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public NestedPropertyDescriptor<TDocument> Properties(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public NestedPropertyDescriptor<TDocument> Store(bool? store = true)
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

		if (DynamicValue is not null)
		{
			writer.WritePropertyName("dynamic");
			JsonSerializer.Serialize(writer, DynamicValue, options);
		}

		if (EnabledValue.HasValue)
		{
			writer.WritePropertyName("enabled");
			writer.WriteBooleanValue(EnabledValue.Value);
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

		if (IncludeInParentValue.HasValue)
		{
			writer.WritePropertyName("include_in_parent");
			writer.WriteBooleanValue(IncludeInParentValue.Value);
		}

		if (IncludeInRootValue.HasValue)
		{
			writer.WritePropertyName("include_in_root");
			writer.WriteBooleanValue(IncludeInRootValue.Value);
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
		writer.WriteStringValue("nested");
		writer.WriteEndObject();
	}

	NestedProperty IBuildableDescriptor<NestedProperty>.Build() => new()
	{
		CopyTo = CopyToValue,
		Dynamic = DynamicValue,
		Enabled = EnabledValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		IncludeInParent = IncludeInParentValue,
		IncludeInRoot = IncludeInRootValue,
		Meta = MetaValue,
		Properties = PropertiesValue,
		Store = StoreValue
	};
}

public sealed partial class NestedPropertyDescriptor : SerializableDescriptor<NestedPropertyDescriptor>, IBuildableDescriptor<NestedProperty>
{
	internal NestedPropertyDescriptor(Action<NestedPropertyDescriptor> configure) => configure.Invoke(this);

	public NestedPropertyDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private bool? EnabledValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private bool? IncludeInParentValue { get; set; }
	private bool? IncludeInRootValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private bool? StoreValue { get; set; }

	public NestedPropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
	{
		CopyToValue = copyTo;
		return Self;
	}

	public NestedPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public NestedPropertyDescriptor Enabled(bool? enabled = true)
	{
		EnabledValue = enabled;
		return Self;
	}

	public NestedPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public NestedPropertyDescriptor Fields<TDocument>(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public NestedPropertyDescriptor Fields<TDocument>(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public NestedPropertyDescriptor IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public NestedPropertyDescriptor IncludeInParent(bool? includeInParent = true)
	{
		IncludeInParentValue = includeInParent;
		return Self;
	}

	public NestedPropertyDescriptor IncludeInRoot(bool? includeInRoot = true)
	{
		IncludeInRootValue = includeInRoot;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public NestedPropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public NestedPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public NestedPropertyDescriptor Properties<TDocument>(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public NestedPropertyDescriptor Properties<TDocument>(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public NestedPropertyDescriptor Store(bool? store = true)
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

		if (DynamicValue is not null)
		{
			writer.WritePropertyName("dynamic");
			JsonSerializer.Serialize(writer, DynamicValue, options);
		}

		if (EnabledValue.HasValue)
		{
			writer.WritePropertyName("enabled");
			writer.WriteBooleanValue(EnabledValue.Value);
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

		if (IncludeInParentValue.HasValue)
		{
			writer.WritePropertyName("include_in_parent");
			writer.WriteBooleanValue(IncludeInParentValue.Value);
		}

		if (IncludeInRootValue.HasValue)
		{
			writer.WritePropertyName("include_in_root");
			writer.WriteBooleanValue(IncludeInRootValue.Value);
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
		writer.WriteStringValue("nested");
		writer.WriteEndObject();
	}

	NestedProperty IBuildableDescriptor<NestedProperty>.Build() => new()
	{
		CopyTo = CopyToValue,
		Dynamic = DynamicValue,
		Enabled = EnabledValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		IncludeInParent = IncludeInParentValue,
		IncludeInRoot = IncludeInRootValue,
		Meta = MetaValue,
		Properties = PropertiesValue,
		Store = StoreValue
	};
}
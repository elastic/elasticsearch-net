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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Mapping;

public sealed partial class ByteNumberProperty : IProperty
{
	[JsonInclude, JsonPropertyName("boost")]
	public double? Boost { get; set; }
	[JsonInclude, JsonPropertyName("coerce")]
	public bool? Coerce { get; set; }
	[JsonInclude, JsonPropertyName("copy_to")]
	[JsonConverter(typeof(SingleOrManyFieldsConverter))]
	public Elastic.Clients.Elasticsearch.Serverless.Fields? CopyTo { get; set; }
	[JsonInclude, JsonPropertyName("doc_values")]
	public bool? DocValues { get; set; }
	[JsonInclude, JsonPropertyName("dynamic")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? Dynamic { get; set; }
	[JsonInclude, JsonPropertyName("fields")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? Fields { get; set; }
	[JsonInclude, JsonPropertyName("ignore_above")]
	public int? IgnoreAbove { get; set; }
	[JsonInclude, JsonPropertyName("ignore_malformed")]
	public bool? IgnoreMalformed { get; set; }
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
	public byte? NullValue { get; set; }
	[JsonInclude, JsonPropertyName("on_script_error")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.OnScriptError? OnScriptError { get; set; }
	[JsonInclude, JsonPropertyName("properties")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? Properties { get; set; }
	[JsonInclude, JsonPropertyName("script")]
	public Elastic.Clients.Elasticsearch.Serverless.Script? Script { get; set; }
	[JsonInclude, JsonPropertyName("similarity")]
	public string? Similarity { get; set; }
	[JsonInclude, JsonPropertyName("store")]
	public bool? Store { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "byte";
}

public sealed partial class ByteNumberPropertyDescriptor<TDocument> : SerializableDescriptor<ByteNumberPropertyDescriptor<TDocument>>, IBuildableDescriptor<ByteNumberProperty>
{
	internal ByteNumberPropertyDescriptor(Action<ByteNumberPropertyDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ByteNumberPropertyDescriptor() : base()
	{
	}

	private double? BoostValue { get; set; }
	private bool? CoerceValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Fields? CopyToValue { get; set; }
	private bool? DocValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? DynamicValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private bool? IgnoreMalformedValue { get; set; }
	private bool? IndexValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private byte? NullValueValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.OnScriptError? OnScriptErrorValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? PropertiesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Script? ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> ScriptDescriptorAction { get; set; }
	private string? SimilarityValue { get; set; }
	private bool? StoreValue { get; set; }

	public ByteNumberPropertyDescriptor<TDocument> Boost(double? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Coerce(bool? coerce = true)
	{
		CoerceValue = coerce;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Serverless.Fields? copyTo)
	{
		CopyToValue = copyTo;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> DocValues(bool? docValues = true)
	{
		DocValuesValue = docValues;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Fields(Action<Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> IgnoreMalformed(bool? ignoreMalformed = true)
	{
		IgnoreMalformedValue = ignoreMalformed;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Index(bool? index = true)
	{
		IndexValue = index;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public ByteNumberPropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> NullValue(byte? nullValue)
	{
		NullValueValue = nullValue;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> OnScriptError(Elastic.Clients.Elasticsearch.Serverless.Mapping.OnScriptError? onScriptError)
	{
		OnScriptErrorValue = onScriptError;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Properties(Action<Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Serverless.Script? script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Script(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Similarity(string? similarity)
	{
		SimilarityValue = similarity;
		return Self;
	}

	public ByteNumberPropertyDescriptor<TDocument> Store(bool? store = true)
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

		if (ScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptDescriptor, options);
		}
		else if (ScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(ScriptDescriptorAction), options);
		}
		else if (ScriptValue is not null)
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

		writer.WritePropertyName("type");
		writer.WriteStringValue("byte");
		writer.WriteEndObject();
	}

	private Elastic.Clients.Elasticsearch.Serverless.Script? BuildScript()
	{
		if (ScriptValue is not null)
		{
			return ScriptValue;
		}

		if ((object)ScriptDescriptor is IBuildableDescriptor<Elastic.Clients.Elasticsearch.Serverless.Script?> buildable)
		{
			return buildable.Build();
		}

		if (ScriptDescriptorAction is not null)
		{
			var descriptor = new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(ScriptDescriptorAction);
			if ((object)descriptor is IBuildableDescriptor<Elastic.Clients.Elasticsearch.Serverless.Script?> buildableFromAction)
			{
				return buildableFromAction.Build();
			}
		}

		return null;
	}

	ByteNumberProperty IBuildableDescriptor<ByteNumberProperty>.Build() => new()
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
		Script = BuildScript(),
		Similarity = SimilarityValue,
		Store = StoreValue
	};
}

public sealed partial class ByteNumberPropertyDescriptor : SerializableDescriptor<ByteNumberPropertyDescriptor>, IBuildableDescriptor<ByteNumberProperty>
{
	internal ByteNumberPropertyDescriptor(Action<ByteNumberPropertyDescriptor> configure) => configure.Invoke(this);

	public ByteNumberPropertyDescriptor() : base()
	{
	}

	private double? BoostValue { get; set; }
	private bool? CoerceValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Fields? CopyToValue { get; set; }
	private bool? DocValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? DynamicValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private bool? IgnoreMalformedValue { get; set; }
	private bool? IndexValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private byte? NullValueValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.OnScriptError? OnScriptErrorValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? PropertiesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Script? ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> ScriptDescriptorAction { get; set; }
	private string? SimilarityValue { get; set; }
	private bool? StoreValue { get; set; }

	public ByteNumberPropertyDescriptor Boost(double? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public ByteNumberPropertyDescriptor Coerce(bool? coerce = true)
	{
		CoerceValue = coerce;
		return Self;
	}

	public ByteNumberPropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Serverless.Fields? copyTo)
	{
		CopyToValue = copyTo;
		return Self;
	}

	public ByteNumberPropertyDescriptor DocValues(bool? docValues = true)
	{
		DocValuesValue = docValues;
		return Self;
	}

	public ByteNumberPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public ByteNumberPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public ByteNumberPropertyDescriptor Fields<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public ByteNumberPropertyDescriptor Fields<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public ByteNumberPropertyDescriptor IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	public ByteNumberPropertyDescriptor IgnoreMalformed(bool? ignoreMalformed = true)
	{
		IgnoreMalformedValue = ignoreMalformed;
		return Self;
	}

	public ByteNumberPropertyDescriptor Index(bool? index = true)
	{
		IndexValue = index;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public ByteNumberPropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public ByteNumberPropertyDescriptor NullValue(byte? nullValue)
	{
		NullValueValue = nullValue;
		return Self;
	}

	public ByteNumberPropertyDescriptor OnScriptError(Elastic.Clients.Elasticsearch.Serverless.Mapping.OnScriptError? onScriptError)
	{
		OnScriptErrorValue = onScriptError;
		return Self;
	}

	public ByteNumberPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public ByteNumberPropertyDescriptor Properties<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public ByteNumberPropertyDescriptor Properties<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public ByteNumberPropertyDescriptor Script(Elastic.Clients.Elasticsearch.Serverless.Script? script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public ByteNumberPropertyDescriptor Script(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public ByteNumberPropertyDescriptor Script(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	public ByteNumberPropertyDescriptor Similarity(string? similarity)
	{
		SimilarityValue = similarity;
		return Self;
	}

	public ByteNumberPropertyDescriptor Store(bool? store = true)
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

		if (ScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptDescriptor, options);
		}
		else if (ScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(ScriptDescriptorAction), options);
		}
		else if (ScriptValue is not null)
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

		writer.WritePropertyName("type");
		writer.WriteStringValue("byte");
		writer.WriteEndObject();
	}

	private Elastic.Clients.Elasticsearch.Serverless.Script? BuildScript()
	{
		if (ScriptValue is not null)
		{
			return ScriptValue;
		}

		if ((object)ScriptDescriptor is IBuildableDescriptor<Elastic.Clients.Elasticsearch.Serverless.Script?> buildable)
		{
			return buildable.Build();
		}

		if (ScriptDescriptorAction is not null)
		{
			var descriptor = new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(ScriptDescriptorAction);
			if ((object)descriptor is IBuildableDescriptor<Elastic.Clients.Elasticsearch.Serverless.Script?> buildableFromAction)
			{
				return buildableFromAction.Build();
			}
		}

		return null;
	}

	ByteNumberProperty IBuildableDescriptor<ByteNumberProperty>.Build() => new()
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
		Script = BuildScript(),
		Similarity = SimilarityValue,
		Store = StoreValue
	};
}
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

internal sealed partial class IcuCollationPropertyConverter : System.Text.Json.Serialization.JsonConverter<IcuCollationProperty>
{
	private static readonly System.Text.Json.JsonEncodedText PropAlternate = System.Text.Json.JsonEncodedText.Encode("alternate");
	private static readonly System.Text.Json.JsonEncodedText PropCaseFirst = System.Text.Json.JsonEncodedText.Encode("case_first");
	private static readonly System.Text.Json.JsonEncodedText PropCaseLevel = System.Text.Json.JsonEncodedText.Encode("case_level");
	private static readonly System.Text.Json.JsonEncodedText PropCopyTo = System.Text.Json.JsonEncodedText.Encode("copy_to");
	private static readonly System.Text.Json.JsonEncodedText PropCountry = System.Text.Json.JsonEncodedText.Encode("country");
	private static readonly System.Text.Json.JsonEncodedText PropDecomposition = System.Text.Json.JsonEncodedText.Encode("decomposition");
	private static readonly System.Text.Json.JsonEncodedText PropDocValues = System.Text.Json.JsonEncodedText.Encode("doc_values");
	private static readonly System.Text.Json.JsonEncodedText PropDynamic = System.Text.Json.JsonEncodedText.Encode("dynamic");
	private static readonly System.Text.Json.JsonEncodedText PropFields = System.Text.Json.JsonEncodedText.Encode("fields");
	private static readonly System.Text.Json.JsonEncodedText PropHiraganaQuaternaryMode = System.Text.Json.JsonEncodedText.Encode("hiragana_quaternary_mode");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreAbove = System.Text.Json.JsonEncodedText.Encode("ignore_above");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropIndexOptions = System.Text.Json.JsonEncodedText.Encode("index_options");
	private static readonly System.Text.Json.JsonEncodedText PropLanguage = System.Text.Json.JsonEncodedText.Encode("language");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropNorms = System.Text.Json.JsonEncodedText.Encode("norms");
	private static readonly System.Text.Json.JsonEncodedText PropNullValue = System.Text.Json.JsonEncodedText.Encode("null_value");
	private static readonly System.Text.Json.JsonEncodedText PropNumeric = System.Text.Json.JsonEncodedText.Encode("numeric");
	private static readonly System.Text.Json.JsonEncodedText PropProperties = System.Text.Json.JsonEncodedText.Encode("properties");
	private static readonly System.Text.Json.JsonEncodedText PropRules = System.Text.Json.JsonEncodedText.Encode("rules");
	private static readonly System.Text.Json.JsonEncodedText PropStore = System.Text.Json.JsonEncodedText.Encode("store");
	private static readonly System.Text.Json.JsonEncodedText PropStrength = System.Text.Json.JsonEncodedText.Encode("strength");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVariableTop = System.Text.Json.JsonEncodedText.Encode("variable_top");
	private static readonly System.Text.Json.JsonEncodedText PropVariant = System.Text.Json.JsonEncodedText.Encode("variant");

	public override IcuCollationProperty Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate?> propAlternate = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst?> propCaseFirst = default;
		LocalJsonValue<bool?> propCaseLevel = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propCopyTo = default;
		LocalJsonValue<string?> propCountry = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition?> propDecomposition = default;
		LocalJsonValue<bool?> propDocValues = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.DynamicMapping?> propDynamic = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propFields = default;
		LocalJsonValue<bool?> propHiraganaQuaternaryMode = default;
		LocalJsonValue<int?> propIgnoreAbove = default;
		LocalJsonValue<bool?> propIndex = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.IndexOptions?> propIndexOptions = default;
		LocalJsonValue<string?> propLanguage = default;
		LocalJsonValue<IDictionary<string, string>?> propMeta = default;
		LocalJsonValue<bool?> propNorms = default;
		LocalJsonValue<string?> propNullValue = default;
		LocalJsonValue<bool?> propNumeric = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propProperties = default;
		LocalJsonValue<string?> propRules = default;
		LocalJsonValue<bool?> propStore = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength?> propStrength = default;
		LocalJsonValue<string?> propVariableTop = default;
		LocalJsonValue<string?> propVariant = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAlternate.TryReadProperty(ref reader, options, PropAlternate, null))
			{
				continue;
			}

			if (propCaseFirst.TryReadProperty(ref reader, options, PropCaseFirst, null))
			{
				continue;
			}

			if (propCaseLevel.TryReadProperty(ref reader, options, PropCaseLevel, null))
			{
				continue;
			}

			if (propCopyTo.TryReadProperty(ref reader, options, PropCopyTo, static Elastic.Clients.Elasticsearch.Fields? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, typeof(SingleOrManyFieldsMarker))))
			{
				continue;
			}

			if (propCountry.TryReadProperty(ref reader, options, PropCountry, null))
			{
				continue;
			}

			if (propDecomposition.TryReadProperty(ref reader, options, PropDecomposition, null))
			{
				continue;
			}

			if (propDocValues.TryReadProperty(ref reader, options, PropDocValues, null))
			{
				continue;
			}

			if (propDynamic.TryReadProperty(ref reader, options, PropDynamic, null))
			{
				continue;
			}

			if (propFields.TryReadProperty(ref reader, options, PropFields, null))
			{
				continue;
			}

			if (propHiraganaQuaternaryMode.TryReadProperty(ref reader, options, PropHiraganaQuaternaryMode, null))
			{
				continue;
			}

			if (propIgnoreAbove.TryReadProperty(ref reader, options, PropIgnoreAbove, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propIndexOptions.TryReadProperty(ref reader, options, PropIndexOptions, null))
			{
				continue;
			}

			if (propLanguage.TryReadProperty(ref reader, options, PropLanguage, null))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static IDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propNorms.TryReadProperty(ref reader, options, PropNorms, null))
			{
				continue;
			}

			if (propNullValue.TryReadProperty(ref reader, options, PropNullValue, null))
			{
				continue;
			}

			if (propNumeric.TryReadProperty(ref reader, options, PropNumeric, null))
			{
				continue;
			}

			if (propProperties.TryReadProperty(ref reader, options, PropProperties, null))
			{
				continue;
			}

			if (propRules.TryReadProperty(ref reader, options, PropRules, null))
			{
				continue;
			}

			if (propStore.TryReadProperty(ref reader, options, PropStore, null))
			{
				continue;
			}

			if (propStrength.TryReadProperty(ref reader, options, PropStrength, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			if (propVariableTop.TryReadProperty(ref reader, options, PropVariableTop, null))
			{
				continue;
			}

			if (propVariant.TryReadProperty(ref reader, options, PropVariant, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new IcuCollationProperty
		{
			Alternate = propAlternate.Value
,
			CaseFirst = propCaseFirst.Value
,
			CaseLevel = propCaseLevel.Value
,
			CopyTo = propCopyTo.Value
,
			Country = propCountry.Value
,
			Decomposition = propDecomposition.Value
,
			DocValues = propDocValues.Value
,
			Dynamic = propDynamic.Value
,
			Fields = propFields.Value
,
			HiraganaQuaternaryMode = propHiraganaQuaternaryMode.Value
,
			IgnoreAbove = propIgnoreAbove.Value
,
			Index = propIndex.Value
,
			IndexOptions = propIndexOptions.Value
,
			Language = propLanguage.Value
,
			Meta = propMeta.Value
,
			Norms = propNorms.Value
,
			NullValue = propNullValue.Value
,
			Numeric = propNumeric.Value
,
			Properties = propProperties.Value
,
			Rules = propRules.Value
,
			Store = propStore.Value
,
			Strength = propStrength.Value
,
			VariableTop = propVariableTop.Value
,
			Variant = propVariant.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, IcuCollationProperty value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAlternate, value.Alternate, null, null);
		writer.WriteProperty(options, PropCaseFirst, value.CaseFirst, null, null);
		writer.WriteProperty(options, PropCaseLevel, value.CaseLevel, null, null);
		writer.WriteProperty(options, PropCopyTo, value.CopyTo, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields? v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, v, typeof(SingleOrManyFieldsMarker)));
		writer.WriteProperty(options, PropCountry, value.Country, null, null);
		writer.WriteProperty(options, PropDecomposition, value.Decomposition, null, null);
		writer.WriteProperty(options, PropDocValues, value.DocValues, null, null);
		writer.WriteProperty(options, PropDynamic, value.Dynamic, null, null);
		writer.WriteProperty(options, PropFields, value.Fields, null, null);
		writer.WriteProperty(options, PropHiraganaQuaternaryMode, value.HiraganaQuaternaryMode, null, null);
		writer.WriteProperty(options, PropIgnoreAbove, value.IgnoreAbove, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropIndexOptions, value.IndexOptions, null, null);
		writer.WriteProperty(options, PropLanguage, value.Language, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropNorms, value.Norms, null, null);
		writer.WriteProperty(options, PropNullValue, value.NullValue, null, null);
		writer.WriteProperty(options, PropNumeric, value.Numeric, null, null);
		writer.WriteProperty(options, PropProperties, value.Properties, null, null);
		writer.WriteProperty(options, PropRules, value.Rules, null, null);
		writer.WriteProperty(options, PropStore, value.Store, null, null);
		writer.WriteProperty(options, PropStrength, value.Strength, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVariableTop, value.VariableTop, null, null);
		writer.WriteProperty(options, PropVariant, value.Variant, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(IcuCollationPropertyConverter))]
public sealed partial class IcuCollationProperty : IProperty
{
	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate? Alternate { get; set; }
	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst? CaseFirst { get; set; }
	public bool? CaseLevel { get; set; }
	public Elastic.Clients.Elasticsearch.Fields? CopyTo { get; set; }
	public string? Country { get; set; }
	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition? Decomposition { get; set; }
	public bool? DocValues { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }
	public bool? HiraganaQuaternaryMode { get; set; }
	public int? IgnoreAbove { get; set; }

	/// <summary>
	/// <para>
	/// Should the field be searchable?
	/// </para>
	/// </summary>
	public bool? Index { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.IndexOptions? IndexOptions { get; set; }
	public string? Language { get; set; }

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public IDictionary<string, string>? Meta { get; set; }
	public bool? Norms { get; set; }

	/// <summary>
	/// <para>
	/// Accepts a string value which is substituted for any explicit null values. Defaults to null, which means the field is treated as missing.
	/// </para>
	/// </summary>
	public string? NullValue { get; set; }
	public bool? Numeric { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }
	public string? Rules { get; set; }
	public bool? Store { get; set; }
	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength? Strength { get; set; }

	public string Type => "icu_collation_keyword";

	public string? VariableTop { get; set; }
	public string? Variant { get; set; }
}

public sealed partial class IcuCollationPropertyDescriptor<TDocument> : SerializableDescriptor<IcuCollationPropertyDescriptor<TDocument>>, IBuildableDescriptor<IcuCollationProperty>
{
	internal IcuCollationPropertyDescriptor(Action<IcuCollationPropertyDescriptor<TDocument>> configure) => configure.Invoke(this);

	public IcuCollationPropertyDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate? AlternateValue { get; set; }
	private Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst? CaseFirstValue { get; set; }
	private bool? CaseLevelValue { get; set; }
	private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }
	private string? CountryValue { get; set; }
	private Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition? DecompositionValue { get; set; }
	private bool? DocValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private bool? HiraganaQuaternaryModeValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private bool? IndexValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.IndexOptions? IndexOptionsValue { get; set; }
	private string? LanguageValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private bool? NormsValue { get; set; }
	private string? NullValueValue { get; set; }
	private bool? NumericValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private string? RulesValue { get; set; }
	private bool? StoreValue { get; set; }
	private Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength? StrengthValue { get; set; }
	private string? VariableTopValue { get; set; }
	private string? VariantValue { get; set; }

	public IcuCollationPropertyDescriptor<TDocument> Alternate(Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate? alternate)
	{
		AlternateValue = alternate;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> CaseFirst(Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst? caseFirst)
	{
		CaseFirstValue = caseFirst;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> CaseLevel(bool? caseLevel = true)
	{
		CaseLevelValue = caseLevel;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
	{
		CopyToValue = copyTo;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Country(string? country)
	{
		CountryValue = country;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Decomposition(Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition? decomposition)
	{
		DecompositionValue = decomposition;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> DocValues(bool? docValues = true)
	{
		DocValuesValue = docValues;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Fields(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> HiraganaQuaternaryMode(bool? hiraganaQuaternaryMode = true)
	{
		HiraganaQuaternaryModeValue = hiraganaQuaternaryMode;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Should the field be searchable?
	/// </para>
	/// </summary>
	public IcuCollationPropertyDescriptor<TDocument> Index(bool? index = true)
	{
		IndexValue = index;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> IndexOptions(Elastic.Clients.Elasticsearch.Mapping.IndexOptions? indexOptions)
	{
		IndexOptionsValue = indexOptions;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Language(string? language)
	{
		LanguageValue = language;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public IcuCollationPropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Norms(bool? norms = true)
	{
		NormsValue = norms;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Accepts a string value which is substituted for any explicit null values. Defaults to null, which means the field is treated as missing.
	/// </para>
	/// </summary>
	public IcuCollationPropertyDescriptor<TDocument> NullValue(string? nullValue)
	{
		NullValueValue = nullValue;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Numeric(bool? numeric = true)
	{
		NumericValue = numeric;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Properties(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Rules(string? rules)
	{
		RulesValue = rules;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Store(bool? store = true)
	{
		StoreValue = store;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Strength(Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength? strength)
	{
		StrengthValue = strength;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> VariableTop(string? variableTop)
	{
		VariableTopValue = variableTop;
		return Self;
	}

	public IcuCollationPropertyDescriptor<TDocument> Variant(string? variant)
	{
		VariantValue = variant;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AlternateValue is not null)
		{
			writer.WritePropertyName("alternate");
			JsonSerializer.Serialize(writer, AlternateValue, options);
		}

		if (CaseFirstValue is not null)
		{
			writer.WritePropertyName("case_first");
			JsonSerializer.Serialize(writer, CaseFirstValue, options);
		}

		if (CaseLevelValue.HasValue)
		{
			writer.WritePropertyName("case_level");
			writer.WriteBooleanValue(CaseLevelValue.Value);
		}

		if (CopyToValue is not null)
		{
			writer.WritePropertyName("copy_to");
			JsonSerializer.Serialize(writer, CopyToValue, options);
		}

		if (!string.IsNullOrEmpty(CountryValue))
		{
			writer.WritePropertyName("country");
			writer.WriteStringValue(CountryValue);
		}

		if (DecompositionValue is not null)
		{
			writer.WritePropertyName("decomposition");
			JsonSerializer.Serialize(writer, DecompositionValue, options);
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

		if (HiraganaQuaternaryModeValue.HasValue)
		{
			writer.WritePropertyName("hiragana_quaternary_mode");
			writer.WriteBooleanValue(HiraganaQuaternaryModeValue.Value);
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

		if (!string.IsNullOrEmpty(LanguageValue))
		{
			writer.WritePropertyName("language");
			writer.WriteStringValue(LanguageValue);
		}

		if (MetaValue is not null)
		{
			writer.WritePropertyName("meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		if (NormsValue.HasValue)
		{
			writer.WritePropertyName("norms");
			writer.WriteBooleanValue(NormsValue.Value);
		}

		if (!string.IsNullOrEmpty(NullValueValue))
		{
			writer.WritePropertyName("null_value");
			writer.WriteStringValue(NullValueValue);
		}

		if (NumericValue.HasValue)
		{
			writer.WritePropertyName("numeric");
			writer.WriteBooleanValue(NumericValue.Value);
		}

		if (PropertiesValue is not null)
		{
			writer.WritePropertyName("properties");
			JsonSerializer.Serialize(writer, PropertiesValue, options);
		}

		if (!string.IsNullOrEmpty(RulesValue))
		{
			writer.WritePropertyName("rules");
			writer.WriteStringValue(RulesValue);
		}

		if (StoreValue.HasValue)
		{
			writer.WritePropertyName("store");
			writer.WriteBooleanValue(StoreValue.Value);
		}

		if (StrengthValue is not null)
		{
			writer.WritePropertyName("strength");
			JsonSerializer.Serialize(writer, StrengthValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("icu_collation_keyword");
		if (!string.IsNullOrEmpty(VariableTopValue))
		{
			writer.WritePropertyName("variable_top");
			writer.WriteStringValue(VariableTopValue);
		}

		if (!string.IsNullOrEmpty(VariantValue))
		{
			writer.WritePropertyName("variant");
			writer.WriteStringValue(VariantValue);
		}

		writer.WriteEndObject();
	}

	IcuCollationProperty IBuildableDescriptor<IcuCollationProperty>.Build() => new()
	{
		Alternate = AlternateValue,
		CaseFirst = CaseFirstValue,
		CaseLevel = CaseLevelValue,
		CopyTo = CopyToValue,
		Country = CountryValue,
		Decomposition = DecompositionValue,
		DocValues = DocValuesValue,
		Dynamic = DynamicValue,
		Fields = FieldsValue,
		HiraganaQuaternaryMode = HiraganaQuaternaryModeValue,
		IgnoreAbove = IgnoreAboveValue,
		Index = IndexValue,
		IndexOptions = IndexOptionsValue,
		Language = LanguageValue,
		Meta = MetaValue,
		Norms = NormsValue,
		NullValue = NullValueValue,
		Numeric = NumericValue,
		Properties = PropertiesValue,
		Rules = RulesValue,
		Store = StoreValue,
		Strength = StrengthValue,
		VariableTop = VariableTopValue,
		Variant = VariantValue
	};
}

public sealed partial class IcuCollationPropertyDescriptor : SerializableDescriptor<IcuCollationPropertyDescriptor>, IBuildableDescriptor<IcuCollationProperty>
{
	internal IcuCollationPropertyDescriptor(Action<IcuCollationPropertyDescriptor> configure) => configure.Invoke(this);

	public IcuCollationPropertyDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate? AlternateValue { get; set; }
	private Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst? CaseFirstValue { get; set; }
	private bool? CaseLevelValue { get; set; }
	private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }
	private string? CountryValue { get; set; }
	private Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition? DecompositionValue { get; set; }
	private bool? DocValuesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }
	private bool? HiraganaQuaternaryModeValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private bool? IndexValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.IndexOptions? IndexOptionsValue { get; set; }
	private string? LanguageValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private bool? NormsValue { get; set; }
	private string? NullValueValue { get; set; }
	private bool? NumericValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }
	private string? RulesValue { get; set; }
	private bool? StoreValue { get; set; }
	private Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength? StrengthValue { get; set; }
	private string? VariableTopValue { get; set; }
	private string? VariantValue { get; set; }

	public IcuCollationPropertyDescriptor Alternate(Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate? alternate)
	{
		AlternateValue = alternate;
		return Self;
	}

	public IcuCollationPropertyDescriptor CaseFirst(Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst? caseFirst)
	{
		CaseFirstValue = caseFirst;
		return Self;
	}

	public IcuCollationPropertyDescriptor CaseLevel(bool? caseLevel = true)
	{
		CaseLevelValue = caseLevel;
		return Self;
	}

	public IcuCollationPropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
	{
		CopyToValue = copyTo;
		return Self;
	}

	public IcuCollationPropertyDescriptor Country(string? country)
	{
		CountryValue = country;
		return Self;
	}

	public IcuCollationPropertyDescriptor Decomposition(Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition? decomposition)
	{
		DecompositionValue = decomposition;
		return Self;
	}

	public IcuCollationPropertyDescriptor DocValues(bool? docValues = true)
	{
		DocValuesValue = docValues;
		return Self;
	}

	public IcuCollationPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public IcuCollationPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public IcuCollationPropertyDescriptor Fields<TDocument>(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public IcuCollationPropertyDescriptor Fields<TDocument>(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public IcuCollationPropertyDescriptor HiraganaQuaternaryMode(bool? hiraganaQuaternaryMode = true)
	{
		HiraganaQuaternaryModeValue = hiraganaQuaternaryMode;
		return Self;
	}

	public IcuCollationPropertyDescriptor IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Should the field be searchable?
	/// </para>
	/// </summary>
	public IcuCollationPropertyDescriptor Index(bool? index = true)
	{
		IndexValue = index;
		return Self;
	}

	public IcuCollationPropertyDescriptor IndexOptions(Elastic.Clients.Elasticsearch.Mapping.IndexOptions? indexOptions)
	{
		IndexOptionsValue = indexOptions;
		return Self;
	}

	public IcuCollationPropertyDescriptor Language(string? language)
	{
		LanguageValue = language;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public IcuCollationPropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public IcuCollationPropertyDescriptor Norms(bool? norms = true)
	{
		NormsValue = norms;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Accepts a string value which is substituted for any explicit null values. Defaults to null, which means the field is treated as missing.
	/// </para>
	/// </summary>
	public IcuCollationPropertyDescriptor NullValue(string? nullValue)
	{
		NullValueValue = nullValue;
		return Self;
	}

	public IcuCollationPropertyDescriptor Numeric(bool? numeric = true)
	{
		NumericValue = numeric;
		return Self;
	}

	public IcuCollationPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public IcuCollationPropertyDescriptor Properties<TDocument>(Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public IcuCollationPropertyDescriptor Properties<TDocument>(Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public IcuCollationPropertyDescriptor Rules(string? rules)
	{
		RulesValue = rules;
		return Self;
	}

	public IcuCollationPropertyDescriptor Store(bool? store = true)
	{
		StoreValue = store;
		return Self;
	}

	public IcuCollationPropertyDescriptor Strength(Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength? strength)
	{
		StrengthValue = strength;
		return Self;
	}

	public IcuCollationPropertyDescriptor VariableTop(string? variableTop)
	{
		VariableTopValue = variableTop;
		return Self;
	}

	public IcuCollationPropertyDescriptor Variant(string? variant)
	{
		VariantValue = variant;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AlternateValue is not null)
		{
			writer.WritePropertyName("alternate");
			JsonSerializer.Serialize(writer, AlternateValue, options);
		}

		if (CaseFirstValue is not null)
		{
			writer.WritePropertyName("case_first");
			JsonSerializer.Serialize(writer, CaseFirstValue, options);
		}

		if (CaseLevelValue.HasValue)
		{
			writer.WritePropertyName("case_level");
			writer.WriteBooleanValue(CaseLevelValue.Value);
		}

		if (CopyToValue is not null)
		{
			writer.WritePropertyName("copy_to");
			JsonSerializer.Serialize(writer, CopyToValue, options);
		}

		if (!string.IsNullOrEmpty(CountryValue))
		{
			writer.WritePropertyName("country");
			writer.WriteStringValue(CountryValue);
		}

		if (DecompositionValue is not null)
		{
			writer.WritePropertyName("decomposition");
			JsonSerializer.Serialize(writer, DecompositionValue, options);
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

		if (HiraganaQuaternaryModeValue.HasValue)
		{
			writer.WritePropertyName("hiragana_quaternary_mode");
			writer.WriteBooleanValue(HiraganaQuaternaryModeValue.Value);
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

		if (!string.IsNullOrEmpty(LanguageValue))
		{
			writer.WritePropertyName("language");
			writer.WriteStringValue(LanguageValue);
		}

		if (MetaValue is not null)
		{
			writer.WritePropertyName("meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		if (NormsValue.HasValue)
		{
			writer.WritePropertyName("norms");
			writer.WriteBooleanValue(NormsValue.Value);
		}

		if (!string.IsNullOrEmpty(NullValueValue))
		{
			writer.WritePropertyName("null_value");
			writer.WriteStringValue(NullValueValue);
		}

		if (NumericValue.HasValue)
		{
			writer.WritePropertyName("numeric");
			writer.WriteBooleanValue(NumericValue.Value);
		}

		if (PropertiesValue is not null)
		{
			writer.WritePropertyName("properties");
			JsonSerializer.Serialize(writer, PropertiesValue, options);
		}

		if (!string.IsNullOrEmpty(RulesValue))
		{
			writer.WritePropertyName("rules");
			writer.WriteStringValue(RulesValue);
		}

		if (StoreValue.HasValue)
		{
			writer.WritePropertyName("store");
			writer.WriteBooleanValue(StoreValue.Value);
		}

		if (StrengthValue is not null)
		{
			writer.WritePropertyName("strength");
			JsonSerializer.Serialize(writer, StrengthValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("icu_collation_keyword");
		if (!string.IsNullOrEmpty(VariableTopValue))
		{
			writer.WritePropertyName("variable_top");
			writer.WriteStringValue(VariableTopValue);
		}

		if (!string.IsNullOrEmpty(VariantValue))
		{
			writer.WritePropertyName("variant");
			writer.WriteStringValue(VariantValue);
		}

		writer.WriteEndObject();
	}

	IcuCollationProperty IBuildableDescriptor<IcuCollationProperty>.Build() => new()
	{
		Alternate = AlternateValue,
		CaseFirst = CaseFirstValue,
		CaseLevel = CaseLevelValue,
		CopyTo = CopyToValue,
		Country = CountryValue,
		Decomposition = DecompositionValue,
		DocValues = DocValuesValue,
		Dynamic = DynamicValue,
		Fields = FieldsValue,
		HiraganaQuaternaryMode = HiraganaQuaternaryModeValue,
		IgnoreAbove = IgnoreAboveValue,
		Index = IndexValue,
		IndexOptions = IndexOptionsValue,
		Language = LanguageValue,
		Meta = MetaValue,
		Norms = NormsValue,
		NullValue = NullValueValue,
		Numeric = NumericValue,
		Properties = PropertiesValue,
		Rules = RulesValue,
		Store = StoreValue,
		Strength = StrengthValue,
		VariableTop = VariableTopValue,
		Variant = VariantValue
	};
}
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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Mapping;

internal sealed partial class TextPropertyConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Mapping.TextProperty>
{
	private static readonly System.Text.Json.JsonEncodedText PropAnalyzer = System.Text.Json.JsonEncodedText.Encode("analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropCopyTo = System.Text.Json.JsonEncodedText.Encode("copy_to");
	private static readonly System.Text.Json.JsonEncodedText PropDynamic = System.Text.Json.JsonEncodedText.Encode("dynamic");
	private static readonly System.Text.Json.JsonEncodedText PropEagerGlobalOrdinals = System.Text.Json.JsonEncodedText.Encode("eager_global_ordinals");
	private static readonly System.Text.Json.JsonEncodedText PropFielddata = System.Text.Json.JsonEncodedText.Encode("fielddata");
	private static readonly System.Text.Json.JsonEncodedText PropFielddataFrequencyFilter = System.Text.Json.JsonEncodedText.Encode("fielddata_frequency_filter");
	private static readonly System.Text.Json.JsonEncodedText PropFields = System.Text.Json.JsonEncodedText.Encode("fields");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreAbove = System.Text.Json.JsonEncodedText.Encode("ignore_above");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropIndexOptions = System.Text.Json.JsonEncodedText.Encode("index_options");
	private static readonly System.Text.Json.JsonEncodedText PropIndexPhrases = System.Text.Json.JsonEncodedText.Encode("index_phrases");
	private static readonly System.Text.Json.JsonEncodedText PropIndexPrefixes = System.Text.Json.JsonEncodedText.Encode("index_prefixes");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropNorms = System.Text.Json.JsonEncodedText.Encode("norms");
	private static readonly System.Text.Json.JsonEncodedText PropPositionIncrementGap = System.Text.Json.JsonEncodedText.Encode("position_increment_gap");
	private static readonly System.Text.Json.JsonEncodedText PropProperties = System.Text.Json.JsonEncodedText.Encode("properties");
	private static readonly System.Text.Json.JsonEncodedText PropSearchAnalyzer = System.Text.Json.JsonEncodedText.Encode("search_analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropSearchQuoteAnalyzer = System.Text.Json.JsonEncodedText.Encode("search_quote_analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropSimilarity = System.Text.Json.JsonEncodedText.Encode("similarity");
	private static readonly System.Text.Json.JsonEncodedText PropStore = System.Text.Json.JsonEncodedText.Encode("store");
	private static readonly System.Text.Json.JsonEncodedText PropSyntheticSourceKeep = System.Text.Json.JsonEncodedText.Encode("synthetic_source_keep");
	private static readonly System.Text.Json.JsonEncodedText PropTermVector = System.Text.Json.JsonEncodedText.Encode("term_vector");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.Mapping.TextProperty Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propAnalyzer = default;
		LocalJsonValue<double?> propBoost = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propCopyTo = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.DynamicMapping?> propDynamic = default;
		LocalJsonValue<bool?> propEagerGlobalOrdinals = default;
		LocalJsonValue<bool?> propFielddata = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.FielddataFrequencyFilter?> propFielddataFrequencyFilter = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propFields = default;
		LocalJsonValue<int?> propIgnoreAbove = default;
		LocalJsonValue<bool?> propIndex = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.IndexOptions?> propIndexOptions = default;
		LocalJsonValue<bool?> propIndexPhrases = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.TextIndexPrefixes?> propIndexPrefixes = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, string>?> propMeta = default;
		LocalJsonValue<bool?> propNorms = default;
		LocalJsonValue<int?> propPositionIncrementGap = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propProperties = default;
		LocalJsonValue<string?> propSearchAnalyzer = default;
		LocalJsonValue<string?> propSearchQuoteAnalyzer = default;
		LocalJsonValue<string?> propSimilarity = default;
		LocalJsonValue<bool?> propStore = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum?> propSyntheticSourceKeep = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.TermVectorOption?> propTermVector = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAnalyzer.TryReadProperty(ref reader, options, PropAnalyzer, null))
			{
				continue;
			}

			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propCopyTo.TryReadProperty(ref reader, options, PropCopyTo, static Elastic.Clients.Elasticsearch.Fields? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker))))
			{
				continue;
			}

			if (propDynamic.TryReadProperty(ref reader, options, PropDynamic, null))
			{
				continue;
			}

			if (propEagerGlobalOrdinals.TryReadProperty(ref reader, options, PropEagerGlobalOrdinals, null))
			{
				continue;
			}

			if (propFielddata.TryReadProperty(ref reader, options, PropFielddata, null))
			{
				continue;
			}

			if (propFielddataFrequencyFilter.TryReadProperty(ref reader, options, PropFielddataFrequencyFilter, null))
			{
				continue;
			}

			if (propFields.TryReadProperty(ref reader, options, PropFields, null))
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

			if (propIndexPhrases.TryReadProperty(ref reader, options, PropIndexPhrases, null))
			{
				continue;
			}

			if (propIndexPrefixes.TryReadProperty(ref reader, options, PropIndexPrefixes, null))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propNorms.TryReadProperty(ref reader, options, PropNorms, null))
			{
				continue;
			}

			if (propPositionIncrementGap.TryReadProperty(ref reader, options, PropPositionIncrementGap, null))
			{
				continue;
			}

			if (propProperties.TryReadProperty(ref reader, options, PropProperties, null))
			{
				continue;
			}

			if (propSearchAnalyzer.TryReadProperty(ref reader, options, PropSearchAnalyzer, null))
			{
				continue;
			}

			if (propSearchQuoteAnalyzer.TryReadProperty(ref reader, options, PropSearchQuoteAnalyzer, null))
			{
				continue;
			}

			if (propSimilarity.TryReadProperty(ref reader, options, PropSimilarity, null))
			{
				continue;
			}

			if (propStore.TryReadProperty(ref reader, options, PropStore, null))
			{
				continue;
			}

			if (propSyntheticSourceKeep.TryReadProperty(ref reader, options, PropSyntheticSourceKeep, null))
			{
				continue;
			}

			if (propTermVector.TryReadProperty(ref reader, options, PropTermVector, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Mapping.TextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Analyzer = propAnalyzer.Value,
			Boost = propBoost.Value,
			CopyTo = propCopyTo.Value,
			Dynamic = propDynamic.Value,
			EagerGlobalOrdinals = propEagerGlobalOrdinals.Value,
			Fielddata = propFielddata.Value,
			FielddataFrequencyFilter = propFielddataFrequencyFilter.Value,
			Fields = propFields.Value,
			IgnoreAbove = propIgnoreAbove.Value,
			Index = propIndex.Value,
			IndexOptions = propIndexOptions.Value,
			IndexPhrases = propIndexPhrases.Value,
			IndexPrefixes = propIndexPrefixes.Value,
			Meta = propMeta.Value,
			Norms = propNorms.Value,
			PositionIncrementGap = propPositionIncrementGap.Value,
			Properties = propProperties.Value,
			SearchAnalyzer = propSearchAnalyzer.Value,
			SearchQuoteAnalyzer = propSearchQuoteAnalyzer.Value,
			Similarity = propSimilarity.Value,
			Store = propStore.Value,
			SyntheticSourceKeep = propSyntheticSourceKeep.Value,
			TermVector = propTermVector.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Mapping.TextProperty value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAnalyzer, value.Analyzer, null, null);
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropCopyTo, value.CopyTo, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields? v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
		writer.WriteProperty(options, PropDynamic, value.Dynamic, null, null);
		writer.WriteProperty(options, PropEagerGlobalOrdinals, value.EagerGlobalOrdinals, null, null);
		writer.WriteProperty(options, PropFielddata, value.Fielddata, null, null);
		writer.WriteProperty(options, PropFielddataFrequencyFilter, value.FielddataFrequencyFilter, null, null);
		writer.WriteProperty(options, PropFields, value.Fields, null, null);
		writer.WriteProperty(options, PropIgnoreAbove, value.IgnoreAbove, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropIndexOptions, value.IndexOptions, null, null);
		writer.WriteProperty(options, PropIndexPhrases, value.IndexPhrases, null, null);
		writer.WriteProperty(options, PropIndexPrefixes, value.IndexPrefixes, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropNorms, value.Norms, null, null);
		writer.WriteProperty(options, PropPositionIncrementGap, value.PositionIncrementGap, null, null);
		writer.WriteProperty(options, PropProperties, value.Properties, null, null);
		writer.WriteProperty(options, PropSearchAnalyzer, value.SearchAnalyzer, null, null);
		writer.WriteProperty(options, PropSearchQuoteAnalyzer, value.SearchQuoteAnalyzer, null, null);
		writer.WriteProperty(options, PropSimilarity, value.Similarity, null, null);
		writer.WriteProperty(options, PropStore, value.Store, null, null);
		writer.WriteProperty(options, PropSyntheticSourceKeep, value.SyntheticSourceKeep, null, null);
		writer.WriteProperty(options, PropTermVector, value.TermVector, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Mapping.TextPropertyConverter))]
public sealed partial class TextProperty : Elastic.Clients.Elasticsearch.Mapping.IProperty
{
#if NET7_0_OR_GREATER
	public TextProperty()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public TextProperty()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public string? Analyzer { get; set; }
	public double? Boost { get; set; }
	public Elastic.Clients.Elasticsearch.Fields? CopyTo { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }
	public bool? EagerGlobalOrdinals { get; set; }
	public bool? Fielddata { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.FielddataFrequencyFilter? FielddataFrequencyFilter { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }
	public int? IgnoreAbove { get; set; }
	public bool? Index { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.IndexOptions? IndexOptions { get; set; }
	public bool? IndexPhrases { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.TextIndexPrefixes? IndexPrefixes { get; set; }

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, string>? Meta { get; set; }
	public bool? Norms { get; set; }
	public int? PositionIncrementGap { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }
	public string? SearchAnalyzer { get; set; }
	public string? SearchQuoteAnalyzer { get; set; }
	public string? Similarity { get; set; }
	public bool? Store { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? SyntheticSourceKeep { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.TermVectorOption? TermVector { get; set; }

	public string Type => "text";
}

public readonly partial struct TextPropertyDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Mapping.TextProperty Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TextPropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.TextProperty instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TextPropertyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.TextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Mapping.TextProperty instance) => new Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.TextProperty(Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Analyzer(string? value)
	{
		Instance.Analyzer = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Boost(double? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> CopyTo(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? value)
	{
		Instance.Dynamic = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> EagerGlobalOrdinals(bool? value = true)
	{
		Instance.EagerGlobalOrdinals = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Fielddata(bool? value = true)
	{
		Instance.Fielddata = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> FielddataFrequencyFilter(Elastic.Clients.Elasticsearch.IndexManagement.FielddataFrequencyFilter? value)
	{
		Instance.FielddataFrequencyFilter = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> FielddataFrequencyFilter(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.FielddataFrequencyFilterDescriptor> action)
	{
		Instance.FielddataFrequencyFilter = Elastic.Clients.Elasticsearch.IndexManagement.FielddataFrequencyFilterDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Fields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Fields(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> IgnoreAbove(int? value)
	{
		Instance.IgnoreAbove = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Index(bool? value = true)
	{
		Instance.Index = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> IndexOptions(Elastic.Clients.Elasticsearch.Mapping.IndexOptions? value)
	{
		Instance.IndexOptions = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> IndexPhrases(bool? value = true)
	{
		Instance.IndexPhrases = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> IndexPrefixes(Elastic.Clients.Elasticsearch.Mapping.TextIndexPrefixes? value)
	{
		Instance.IndexPrefixes = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> IndexPrefixes()
	{
		Instance.IndexPrefixes = Elastic.Clients.Elasticsearch.Mapping.TextIndexPrefixesDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> IndexPrefixes(System.Action<Elastic.Clients.Elasticsearch.Mapping.TextIndexPrefixesDescriptor>? action)
	{
		Instance.IndexPrefixes = Elastic.Clients.Elasticsearch.Mapping.TextIndexPrefixesDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Meta(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> AddMeta(string key, string value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Norms(bool? value = true)
	{
		Instance.Norms = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> PositionIncrementGap(int? value)
	{
		Instance.PositionIncrementGap = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Properties = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Properties(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> SearchAnalyzer(string? value)
	{
		Instance.SearchAnalyzer = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> SearchQuoteAnalyzer(string? value)
	{
		Instance.SearchQuoteAnalyzer = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Similarity(string? value)
	{
		Instance.Similarity = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> Store(bool? value = true)
	{
		Instance.Store = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> SyntheticSourceKeep(Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? value)
	{
		Instance.SyntheticSourceKeep = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument> TermVector(Elastic.Clients.Elasticsearch.Mapping.TermVectorOption? value)
	{
		Instance.TermVector = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.TextProperty Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Mapping.TextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Mapping.TextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct TextPropertyDescriptor
{
	internal Elastic.Clients.Elasticsearch.Mapping.TextProperty Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TextPropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.TextProperty instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TextPropertyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.TextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.TextProperty instance) => new Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.TextProperty(Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Analyzer(string? value)
	{
		Instance.Analyzer = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Boost(double? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor CopyTo<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? value)
	{
		Instance.Dynamic = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor EagerGlobalOrdinals(bool? value = true)
	{
		Instance.EagerGlobalOrdinals = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Fielddata(bool? value = true)
	{
		Instance.Fielddata = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor FielddataFrequencyFilter(Elastic.Clients.Elasticsearch.IndexManagement.FielddataFrequencyFilter? value)
	{
		Instance.FielddataFrequencyFilter = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor FielddataFrequencyFilter(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.FielddataFrequencyFilterDescriptor> action)
	{
		Instance.FielddataFrequencyFilter = Elastic.Clients.Elasticsearch.IndexManagement.FielddataFrequencyFilterDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Fields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Fields(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Fields<T>(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor IgnoreAbove(int? value)
	{
		Instance.IgnoreAbove = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Index(bool? value = true)
	{
		Instance.Index = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor IndexOptions(Elastic.Clients.Elasticsearch.Mapping.IndexOptions? value)
	{
		Instance.IndexOptions = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor IndexPhrases(bool? value = true)
	{
		Instance.IndexPhrases = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor IndexPrefixes(Elastic.Clients.Elasticsearch.Mapping.TextIndexPrefixes? value)
	{
		Instance.IndexPrefixes = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor IndexPrefixes()
	{
		Instance.IndexPrefixes = Elastic.Clients.Elasticsearch.Mapping.TextIndexPrefixesDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor IndexPrefixes(System.Action<Elastic.Clients.Elasticsearch.Mapping.TextIndexPrefixesDescriptor>? action)
	{
		Instance.IndexPrefixes = Elastic.Clients.Elasticsearch.Mapping.TextIndexPrefixesDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Meta(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor AddMeta(string key, string value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Norms(bool? value = true)
	{
		Instance.Norms = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor PositionIncrementGap(int? value)
	{
		Instance.PositionIncrementGap = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Properties = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Properties(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Properties<T>(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor SearchAnalyzer(string? value)
	{
		Instance.SearchAnalyzer = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor SearchQuoteAnalyzer(string? value)
	{
		Instance.SearchQuoteAnalyzer = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Similarity(string? value)
	{
		Instance.Similarity = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor Store(bool? value = true)
	{
		Instance.Store = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor SyntheticSourceKeep(Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? value)
	{
		Instance.SyntheticSourceKeep = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor TermVector(Elastic.Clients.Elasticsearch.Mapping.TermVectorOption? value)
	{
		Instance.TermVector = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.TextProperty Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Mapping.TextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Mapping.TextPropertyDescriptor(new Elastic.Clients.Elasticsearch.Mapping.TextProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
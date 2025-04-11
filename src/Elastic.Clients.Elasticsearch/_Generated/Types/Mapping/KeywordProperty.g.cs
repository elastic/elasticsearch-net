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

internal sealed partial class KeywordPropertyConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Mapping.KeywordProperty>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropCopyTo = System.Text.Json.JsonEncodedText.Encode("copy_to");
	private static readonly System.Text.Json.JsonEncodedText PropDocValues = System.Text.Json.JsonEncodedText.Encode("doc_values");
	private static readonly System.Text.Json.JsonEncodedText PropDynamic = System.Text.Json.JsonEncodedText.Encode("dynamic");
	private static readonly System.Text.Json.JsonEncodedText PropEagerGlobalOrdinals = System.Text.Json.JsonEncodedText.Encode("eager_global_ordinals");
	private static readonly System.Text.Json.JsonEncodedText PropFields = System.Text.Json.JsonEncodedText.Encode("fields");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreAbove = System.Text.Json.JsonEncodedText.Encode("ignore_above");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropIndexOptions = System.Text.Json.JsonEncodedText.Encode("index_options");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropNormalizer = System.Text.Json.JsonEncodedText.Encode("normalizer");
	private static readonly System.Text.Json.JsonEncodedText PropNorms = System.Text.Json.JsonEncodedText.Encode("norms");
	private static readonly System.Text.Json.JsonEncodedText PropNullValue = System.Text.Json.JsonEncodedText.Encode("null_value");
	private static readonly System.Text.Json.JsonEncodedText PropOnScriptError = System.Text.Json.JsonEncodedText.Encode("on_script_error");
	private static readonly System.Text.Json.JsonEncodedText PropProperties = System.Text.Json.JsonEncodedText.Encode("properties");
	private static readonly System.Text.Json.JsonEncodedText PropScript = System.Text.Json.JsonEncodedText.Encode("script");
	private static readonly System.Text.Json.JsonEncodedText PropSimilarity = System.Text.Json.JsonEncodedText.Encode("similarity");
	private static readonly System.Text.Json.JsonEncodedText PropSplitQueriesOnWhitespace = System.Text.Json.JsonEncodedText.Encode("split_queries_on_whitespace");
	private static readonly System.Text.Json.JsonEncodedText PropStore = System.Text.Json.JsonEncodedText.Encode("store");
	private static readonly System.Text.Json.JsonEncodedText PropSyntheticSourceKeep = System.Text.Json.JsonEncodedText.Encode("synthetic_source_keep");
	private static readonly System.Text.Json.JsonEncodedText PropTimeSeriesDimension = System.Text.Json.JsonEncodedText.Encode("time_series_dimension");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.Mapping.KeywordProperty Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double?> propBoost = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propCopyTo = default;
		LocalJsonValue<bool?> propDocValues = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.DynamicMapping?> propDynamic = default;
		LocalJsonValue<bool?> propEagerGlobalOrdinals = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propFields = default;
		LocalJsonValue<int?> propIgnoreAbove = default;
		LocalJsonValue<bool?> propIndex = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.IndexOptions?> propIndexOptions = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, string>?> propMeta = default;
		LocalJsonValue<string?> propNormalizer = default;
		LocalJsonValue<bool?> propNorms = default;
		LocalJsonValue<string?> propNullValue = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.OnScriptError?> propOnScriptError = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propProperties = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propScript = default;
		LocalJsonValue<string?> propSimilarity = default;
		LocalJsonValue<bool?> propSplitQueriesOnWhitespace = default;
		LocalJsonValue<bool?> propStore = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum?> propSyntheticSourceKeep = default;
		LocalJsonValue<bool?> propTimeSeriesDimension = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propCopyTo.TryReadProperty(ref reader, options, PropCopyTo, static Elastic.Clients.Elasticsearch.Fields? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker))))
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

			if (propEagerGlobalOrdinals.TryReadProperty(ref reader, options, PropEagerGlobalOrdinals, null))
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

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propNormalizer.TryReadProperty(ref reader, options, PropNormalizer, null))
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

			if (propOnScriptError.TryReadProperty(ref reader, options, PropOnScriptError, null))
			{
				continue;
			}

			if (propProperties.TryReadProperty(ref reader, options, PropProperties, null))
			{
				continue;
			}

			if (propScript.TryReadProperty(ref reader, options, PropScript, null))
			{
				continue;
			}

			if (propSimilarity.TryReadProperty(ref reader, options, PropSimilarity, null))
			{
				continue;
			}

			if (propSplitQueriesOnWhitespace.TryReadProperty(ref reader, options, PropSplitQueriesOnWhitespace, null))
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

			if (propTimeSeriesDimension.TryReadProperty(ref reader, options, PropTimeSeriesDimension, null))
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
		return new Elastic.Clients.Elasticsearch.Mapping.KeywordProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			CopyTo = propCopyTo.Value,
			DocValues = propDocValues.Value,
			Dynamic = propDynamic.Value,
			EagerGlobalOrdinals = propEagerGlobalOrdinals.Value,
			Fields = propFields.Value,
			IgnoreAbove = propIgnoreAbove.Value,
			Index = propIndex.Value,
			IndexOptions = propIndexOptions.Value,
			Meta = propMeta.Value,
			Normalizer = propNormalizer.Value,
			Norms = propNorms.Value,
			NullValue = propNullValue.Value,
			OnScriptError = propOnScriptError.Value,
			Properties = propProperties.Value,
			Script = propScript.Value,
			Similarity = propSimilarity.Value,
			SplitQueriesOnWhitespace = propSplitQueriesOnWhitespace.Value,
			Store = propStore.Value,
			SyntheticSourceKeep = propSyntheticSourceKeep.Value,
			TimeSeriesDimension = propTimeSeriesDimension.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Mapping.KeywordProperty value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropCopyTo, value.CopyTo, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields? v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
		writer.WriteProperty(options, PropDocValues, value.DocValues, null, null);
		writer.WriteProperty(options, PropDynamic, value.Dynamic, null, null);
		writer.WriteProperty(options, PropEagerGlobalOrdinals, value.EagerGlobalOrdinals, null, null);
		writer.WriteProperty(options, PropFields, value.Fields, null, null);
		writer.WriteProperty(options, PropIgnoreAbove, value.IgnoreAbove, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropIndexOptions, value.IndexOptions, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropNormalizer, value.Normalizer, null, null);
		writer.WriteProperty(options, PropNorms, value.Norms, null, null);
		writer.WriteProperty(options, PropNullValue, value.NullValue, null, null);
		writer.WriteProperty(options, PropOnScriptError, value.OnScriptError, null, null);
		writer.WriteProperty(options, PropProperties, value.Properties, null, null);
		writer.WriteProperty(options, PropScript, value.Script, null, null);
		writer.WriteProperty(options, PropSimilarity, value.Similarity, null, null);
		writer.WriteProperty(options, PropSplitQueriesOnWhitespace, value.SplitQueriesOnWhitespace, null, null);
		writer.WriteProperty(options, PropStore, value.Store, null, null);
		writer.WriteProperty(options, PropSyntheticSourceKeep, value.SyntheticSourceKeep, null, null);
		writer.WriteProperty(options, PropTimeSeriesDimension, value.TimeSeriesDimension, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyConverter))]
public sealed partial class KeywordProperty : Elastic.Clients.Elasticsearch.Mapping.IProperty
{
#if NET7_0_OR_GREATER
	public KeywordProperty()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public KeywordProperty()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal KeywordProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public double? Boost { get; set; }
	public Elastic.Clients.Elasticsearch.Fields? CopyTo { get; set; }
	public bool? DocValues { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }
	public bool? EagerGlobalOrdinals { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }
	public int? IgnoreAbove { get; set; }
	public bool? Index { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.IndexOptions? IndexOptions { get; set; }

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, string>? Meta { get; set; }
	public string? Normalizer { get; set; }
	public bool? Norms { get; set; }
	public string? NullValue { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.OnScriptError? OnScriptError { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }
	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }
	public string? Similarity { get; set; }
	public bool? SplitQueriesOnWhitespace { get; set; }
	public bool? Store { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? SyntheticSourceKeep { get; set; }

	/// <summary>
	/// <para>
	/// For internal use by Elastic only. Marks the field as a time series dimension. Defaults to false.
	/// </para>
	/// </summary>
	public bool? TimeSeriesDimension { get; set; }

	public string Type => "keyword";
}

public readonly partial struct KeywordPropertyDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Mapping.KeywordProperty Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KeywordPropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.KeywordProperty instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KeywordPropertyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.KeywordProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Mapping.KeywordProperty instance) => new Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.KeywordProperty(Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Boost(double? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> CopyTo(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> DocValues(bool? value = true)
	{
		Instance.DocValues = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? value)
	{
		Instance.Dynamic = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> EagerGlobalOrdinals(bool? value = true)
	{
		Instance.EagerGlobalOrdinals = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Fields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Fields(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> IgnoreAbove(int? value)
	{
		Instance.IgnoreAbove = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Index(bool? value = true)
	{
		Instance.Index = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> IndexOptions(Elastic.Clients.Elasticsearch.Mapping.IndexOptions? value)
	{
		Instance.IndexOptions = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Meta(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> AddMeta(string key, string value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Normalizer(string? value)
	{
		Instance.Normalizer = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Norms(bool? value = true)
	{
		Instance.Norms = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> NullValue(string? value)
	{
		Instance.NullValue = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> OnScriptError(Elastic.Clients.Elasticsearch.Mapping.OnScriptError? value)
	{
		Instance.OnScriptError = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Properties = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Properties(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Similarity(string? value)
	{
		Instance.Similarity = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> SplitQueriesOnWhitespace(bool? value = true)
	{
		Instance.SplitQueriesOnWhitespace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> Store(bool? value = true)
	{
		Instance.Store = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> SyntheticSourceKeep(Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? value)
	{
		Instance.SyntheticSourceKeep = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// For internal use by Elastic only. Marks the field as a time series dimension. Defaults to false.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument> TimeSeriesDimension(bool? value = true)
	{
		Instance.TimeSeriesDimension = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.KeywordProperty Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Mapping.KeywordProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Mapping.KeywordProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct KeywordPropertyDescriptor
{
	internal Elastic.Clients.Elasticsearch.Mapping.KeywordProperty Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KeywordPropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.KeywordProperty instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KeywordPropertyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.KeywordProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.KeywordProperty instance) => new Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.KeywordProperty(Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Boost(double? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor CopyTo<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor DocValues(bool? value = true)
	{
		Instance.DocValues = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? value)
	{
		Instance.Dynamic = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor EagerGlobalOrdinals(bool? value = true)
	{
		Instance.EagerGlobalOrdinals = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Fields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Fields(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Fields<T>(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor IgnoreAbove(int? value)
	{
		Instance.IgnoreAbove = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Index(bool? value = true)
	{
		Instance.Index = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor IndexOptions(Elastic.Clients.Elasticsearch.Mapping.IndexOptions? value)
	{
		Instance.IndexOptions = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Meta(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor AddMeta(string key, string value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Normalizer(string? value)
	{
		Instance.Normalizer = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Norms(bool? value = true)
	{
		Instance.Norms = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor NullValue(string? value)
	{
		Instance.NullValue = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor OnScriptError(Elastic.Clients.Elasticsearch.Mapping.OnScriptError? value)
	{
		Instance.OnScriptError = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Properties = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Properties(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Properties<T>(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Similarity(string? value)
	{
		Instance.Similarity = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor SplitQueriesOnWhitespace(bool? value = true)
	{
		Instance.SplitQueriesOnWhitespace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor Store(bool? value = true)
	{
		Instance.Store = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor SyntheticSourceKeep(Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? value)
	{
		Instance.SyntheticSourceKeep = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// For internal use by Elastic only. Marks the field as a time series dimension. Defaults to false.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor TimeSeriesDimension(bool? value = true)
	{
		Instance.TimeSeriesDimension = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.KeywordProperty Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Mapping.KeywordProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Mapping.KeywordPropertyDescriptor(new Elastic.Clients.Elasticsearch.Mapping.KeywordProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
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

internal sealed partial class DatePropertyConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Mapping.DateProperty>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropCopyTo = System.Text.Json.JsonEncodedText.Encode("copy_to");
	private static readonly System.Text.Json.JsonEncodedText PropDocValues = System.Text.Json.JsonEncodedText.Encode("doc_values");
	private static readonly System.Text.Json.JsonEncodedText PropDynamic = System.Text.Json.JsonEncodedText.Encode("dynamic");
	private static readonly System.Text.Json.JsonEncodedText PropFielddata = System.Text.Json.JsonEncodedText.Encode("fielddata");
	private static readonly System.Text.Json.JsonEncodedText PropFields = System.Text.Json.JsonEncodedText.Encode("fields");
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreAbove = System.Text.Json.JsonEncodedText.Encode("ignore_above");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreMalformed = System.Text.Json.JsonEncodedText.Encode("ignore_malformed");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropLocale = System.Text.Json.JsonEncodedText.Encode("locale");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropNullValue = System.Text.Json.JsonEncodedText.Encode("null_value");
	private static readonly System.Text.Json.JsonEncodedText PropOnScriptError = System.Text.Json.JsonEncodedText.Encode("on_script_error");
	private static readonly System.Text.Json.JsonEncodedText PropPrecisionStep = System.Text.Json.JsonEncodedText.Encode("precision_step");
	private static readonly System.Text.Json.JsonEncodedText PropProperties = System.Text.Json.JsonEncodedText.Encode("properties");
	private static readonly System.Text.Json.JsonEncodedText PropScript = System.Text.Json.JsonEncodedText.Encode("script");
	private static readonly System.Text.Json.JsonEncodedText PropStore = System.Text.Json.JsonEncodedText.Encode("store");
	private static readonly System.Text.Json.JsonEncodedText PropSyntheticSourceKeep = System.Text.Json.JsonEncodedText.Encode("synthetic_source_keep");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.Mapping.DateProperty Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double?> propBoost = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propCopyTo = default;
		LocalJsonValue<bool?> propDocValues = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.DynamicMapping?> propDynamic = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata?> propFielddata = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propFields = default;
		LocalJsonValue<string?> propFormat = default;
		LocalJsonValue<int?> propIgnoreAbove = default;
		LocalJsonValue<bool?> propIgnoreMalformed = default;
		LocalJsonValue<bool?> propIndex = default;
		LocalJsonValue<string?> propLocale = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, string>?> propMeta = default;
		LocalJsonValue<System.DateTime?> propNullValue = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.OnScriptError?> propOnScriptError = default;
		LocalJsonValue<int?> propPrecisionStep = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propProperties = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propScript = default;
		LocalJsonValue<bool?> propStore = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum?> propSyntheticSourceKeep = default;
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

			if (propFielddata.TryReadProperty(ref reader, options, PropFielddata, null))
			{
				continue;
			}

			if (propFields.TryReadProperty(ref reader, options, PropFields, null))
			{
				continue;
			}

			if (propFormat.TryReadProperty(ref reader, options, PropFormat, null))
			{
				continue;
			}

			if (propIgnoreAbove.TryReadProperty(ref reader, options, PropIgnoreAbove, null))
			{
				continue;
			}

			if (propIgnoreMalformed.TryReadProperty(ref reader, options, PropIgnoreMalformed, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propLocale.TryReadProperty(ref reader, options, PropLocale, null))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propNullValue.TryReadProperty(ref reader, options, PropNullValue, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propOnScriptError.TryReadProperty(ref reader, options, PropOnScriptError, null))
			{
				continue;
			}

			if (propPrecisionStep.TryReadProperty(ref reader, options, PropPrecisionStep, null))
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

			if (propStore.TryReadProperty(ref reader, options, PropStore, null))
			{
				continue;
			}

			if (propSyntheticSourceKeep.TryReadProperty(ref reader, options, PropSyntheticSourceKeep, null))
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
		return new Elastic.Clients.Elasticsearch.Mapping.DateProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			CopyTo = propCopyTo.Value,
			DocValues = propDocValues.Value,
			Dynamic = propDynamic.Value,
			Fielddata = propFielddata.Value,
			Fields = propFields.Value,
			Format = propFormat.Value,
			IgnoreAbove = propIgnoreAbove.Value,
			IgnoreMalformed = propIgnoreMalformed.Value,
			Index = propIndex.Value,
			Locale = propLocale.Value,
			Meta = propMeta.Value,
			NullValue = propNullValue.Value,
			OnScriptError = propOnScriptError.Value,
			PrecisionStep = propPrecisionStep.Value,
			Properties = propProperties.Value,
			Script = propScript.Value,
			Store = propStore.Value,
			SyntheticSourceKeep = propSyntheticSourceKeep.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Mapping.DateProperty value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropCopyTo, value.CopyTo, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields? v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
		writer.WriteProperty(options, PropDocValues, value.DocValues, null, null);
		writer.WriteProperty(options, PropDynamic, value.Dynamic, null, null);
		writer.WriteProperty(options, PropFielddata, value.Fielddata, null, null);
		writer.WriteProperty(options, PropFields, value.Fields, null, null);
		writer.WriteProperty(options, PropFormat, value.Format, null, null);
		writer.WriteProperty(options, PropIgnoreAbove, value.IgnoreAbove, null, null);
		writer.WriteProperty(options, PropIgnoreMalformed, value.IgnoreMalformed, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropLocale, value.Locale, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropNullValue, value.NullValue, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropOnScriptError, value.OnScriptError, null, null);
		writer.WriteProperty(options, PropPrecisionStep, value.PrecisionStep, null, null);
		writer.WriteProperty(options, PropProperties, value.Properties, null, null);
		writer.WriteProperty(options, PropScript, value.Script, null, null);
		writer.WriteProperty(options, PropStore, value.Store, null, null);
		writer.WriteProperty(options, PropSyntheticSourceKeep, value.SyntheticSourceKeep, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Mapping.DatePropertyConverter))]
public sealed partial class DateProperty : Elastic.Clients.Elasticsearch.Mapping.IProperty
{
#if NET7_0_OR_GREATER
	public DateProperty()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public DateProperty()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DateProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public double? Boost { get; set; }
	public Elastic.Clients.Elasticsearch.Fields? CopyTo { get; set; }
	public bool? DocValues { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata? Fielddata { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }
	public string? Format { get; set; }
	public int? IgnoreAbove { get; set; }
	public bool? IgnoreMalformed { get; set; }
	public bool? Index { get; set; }
	public string? Locale { get; set; }

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, string>? Meta { get; set; }
	public System.DateTime? NullValue { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.OnScriptError? OnScriptError { get; set; }
	public int? PrecisionStep { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }
	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }
	public bool? Store { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? SyntheticSourceKeep { get; set; }

	public string Type => "date";
}

public readonly partial struct DatePropertyDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Mapping.DateProperty Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DatePropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.DateProperty instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DatePropertyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.DateProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Mapping.DateProperty instance) => new Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.DateProperty(Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Boost(double? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> CopyTo(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> DocValues(bool? value = true)
	{
		Instance.DocValues = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? value)
	{
		Instance.Dynamic = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Fielddata(Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata? value)
	{
		Instance.Fielddata = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Fielddata(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataDescriptor> action)
	{
		Instance.Fielddata = Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Fields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Fields(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> IgnoreAbove(int? value)
	{
		Instance.IgnoreAbove = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> IgnoreMalformed(bool? value = true)
	{
		Instance.IgnoreMalformed = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Index(bool? value = true)
	{
		Instance.Index = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Locale(string? value)
	{
		Instance.Locale = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Meta(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> AddMeta(string key, string value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> NullValue(System.DateTime? value)
	{
		Instance.NullValue = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> OnScriptError(Elastic.Clients.Elasticsearch.Mapping.OnScriptError? value)
	{
		Instance.OnScriptError = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> PrecisionStep(int? value)
	{
		Instance.PrecisionStep = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Properties = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Properties(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> Store(bool? value = true)
	{
		Instance.Store = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument> SyntheticSourceKeep(Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? value)
	{
		Instance.SyntheticSourceKeep = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.DateProperty Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Mapping.DateProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Mapping.DateProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct DatePropertyDescriptor
{
	internal Elastic.Clients.Elasticsearch.Mapping.DateProperty Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DatePropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.DateProperty instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DatePropertyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.DateProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.DateProperty instance) => new Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.DateProperty(Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Boost(double? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor CopyTo<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor DocValues(bool? value = true)
	{
		Instance.DocValues = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? value)
	{
		Instance.Dynamic = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Fielddata(Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata? value)
	{
		Instance.Fielddata = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Fielddata(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataDescriptor> action)
	{
		Instance.Fielddata = Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Fields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Fields(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Fields<T>(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor IgnoreAbove(int? value)
	{
		Instance.IgnoreAbove = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor IgnoreMalformed(bool? value = true)
	{
		Instance.IgnoreMalformed = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Index(bool? value = true)
	{
		Instance.Index = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Locale(string? value)
	{
		Instance.Locale = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Meta(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor AddMeta(string key, string value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor NullValue(System.DateTime? value)
	{
		Instance.NullValue = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor OnScriptError(Elastic.Clients.Elasticsearch.Mapping.OnScriptError? value)
	{
		Instance.OnScriptError = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor PrecisionStep(int? value)
	{
		Instance.PrecisionStep = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Properties = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Properties(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Properties<T>(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor Store(bool? value = true)
	{
		Instance.Store = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor SyntheticSourceKeep(Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? value)
	{
		Instance.SyntheticSourceKeep = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.DateProperty Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Mapping.DateProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Mapping.DatePropertyDescriptor(new Elastic.Clients.Elasticsearch.Mapping.DateProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
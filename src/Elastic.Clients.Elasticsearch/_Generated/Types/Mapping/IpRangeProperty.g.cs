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

internal sealed partial class IpRangePropertyConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropCoerce = System.Text.Json.JsonEncodedText.Encode("coerce");
	private static readonly System.Text.Json.JsonEncodedText PropCopyTo = System.Text.Json.JsonEncodedText.Encode("copy_to");
	private static readonly System.Text.Json.JsonEncodedText PropDocValues = System.Text.Json.JsonEncodedText.Encode("doc_values");
	private static readonly System.Text.Json.JsonEncodedText PropDynamic = System.Text.Json.JsonEncodedText.Encode("dynamic");
	private static readonly System.Text.Json.JsonEncodedText PropFields = System.Text.Json.JsonEncodedText.Encode("fields");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreAbove = System.Text.Json.JsonEncodedText.Encode("ignore_above");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropProperties = System.Text.Json.JsonEncodedText.Encode("properties");
	private static readonly System.Text.Json.JsonEncodedText PropStore = System.Text.Json.JsonEncodedText.Encode("store");
	private static readonly System.Text.Json.JsonEncodedText PropSyntheticSourceKeep = System.Text.Json.JsonEncodedText.Encode("synthetic_source_keep");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double?> propBoost = default;
		LocalJsonValue<bool?> propCoerce = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propCopyTo = default;
		LocalJsonValue<bool?> propDocValues = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.DynamicMapping?> propDynamic = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propFields = default;
		LocalJsonValue<int?> propIgnoreAbove = default;
		LocalJsonValue<bool?> propIndex = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, string>?> propMeta = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propProperties = default;
		LocalJsonValue<bool?> propStore = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum?> propSyntheticSourceKeep = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propCoerce.TryReadProperty(ref reader, options, PropCoerce, null))
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

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propProperties.TryReadProperty(ref reader, options, PropProperties, null))
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
		return new Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			Coerce = propCoerce.Value,
			CopyTo = propCopyTo.Value,
			DocValues = propDocValues.Value,
			Dynamic = propDynamic.Value,
			Fields = propFields.Value,
			IgnoreAbove = propIgnoreAbove.Value,
			Index = propIndex.Value,
			Meta = propMeta.Value,
			Properties = propProperties.Value,
			Store = propStore.Value,
			SyntheticSourceKeep = propSyntheticSourceKeep.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropCoerce, value.Coerce, null, null);
		writer.WriteProperty(options, PropCopyTo, value.CopyTo, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields? v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
		writer.WriteProperty(options, PropDocValues, value.DocValues, null, null);
		writer.WriteProperty(options, PropDynamic, value.Dynamic, null, null);
		writer.WriteProperty(options, PropFields, value.Fields, null, null);
		writer.WriteProperty(options, PropIgnoreAbove, value.IgnoreAbove, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropProperties, value.Properties, null, null);
		writer.WriteProperty(options, PropStore, value.Store, null, null);
		writer.WriteProperty(options, PropSyntheticSourceKeep, value.SyntheticSourceKeep, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyConverter))]
public sealed partial class IpRangeProperty : Elastic.Clients.Elasticsearch.Mapping.IProperty
{
#if NET7_0_OR_GREATER
	public IpRangeProperty()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public IpRangeProperty()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal IpRangeProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public double? Boost { get; set; }
	public bool? Coerce { get; set; }
	public Elastic.Clients.Elasticsearch.Fields? CopyTo { get; set; }
	public bool? DocValues { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }
	public int? IgnoreAbove { get; set; }
	public bool? Index { get; set; }

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, string>? Meta { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }
	public bool? Store { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? SyntheticSourceKeep { get; set; }

	public string Type => "ip_range";
}

public readonly partial struct IpRangePropertyDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IpRangePropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IpRangePropertyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty instance) => new Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty(Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> Boost(double? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> Coerce(bool? value = true)
	{
		Instance.Coerce = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> CopyTo(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> DocValues(bool? value = true)
	{
		Instance.DocValues = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? value)
	{
		Instance.Dynamic = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Fields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> Fields(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> IgnoreAbove(int? value)
	{
		Instance.IgnoreAbove = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> Index(bool? value = true)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> Meta(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> AddMeta(string key, string value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Properties = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> Properties(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> Store(bool? value = true)
	{
		Instance.Store = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument> SyntheticSourceKeep(Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? value)
	{
		Instance.SyntheticSourceKeep = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct IpRangePropertyDescriptor
{
	internal Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IpRangePropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IpRangePropertyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty instance) => new Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty(Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Boost(double? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Coerce(bool? value = true)
	{
		Instance.Coerce = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor CopyTo<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.CopyTo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor DocValues(bool? value = true)
	{
		Instance.DocValues = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? value)
	{
		Instance.Dynamic = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Fields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Fields(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Fields<T>(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor IgnoreAbove(int? value)
	{
		Instance.IgnoreAbove = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Index(bool? value = true)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Meta(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor AddMeta(string key, string value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Properties = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Properties(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Properties<T>(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor Store(bool? value = true)
	{
		Instance.Store = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor SyntheticSourceKeep(Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? value)
	{
		Instance.SyntheticSourceKeep = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Mapping.IpRangePropertyDescriptor(new Elastic.Clients.Elasticsearch.Mapping.IpRangeProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
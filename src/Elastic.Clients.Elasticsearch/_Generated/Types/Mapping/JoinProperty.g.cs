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

internal sealed partial class JoinPropertyConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Mapping.JoinProperty>
{
	private static readonly System.Text.Json.JsonEncodedText PropDynamic = System.Text.Json.JsonEncodedText.Encode("dynamic");
	private static readonly System.Text.Json.JsonEncodedText PropEagerGlobalOrdinals = System.Text.Json.JsonEncodedText.Encode("eager_global_ordinals");
	private static readonly System.Text.Json.JsonEncodedText PropFields = System.Text.Json.JsonEncodedText.Encode("fields");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreAbove = System.Text.Json.JsonEncodedText.Encode("ignore_above");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropProperties = System.Text.Json.JsonEncodedText.Encode("properties");
	private static readonly System.Text.Json.JsonEncodedText PropRelations = System.Text.Json.JsonEncodedText.Encode("relations");
	private static readonly System.Text.Json.JsonEncodedText PropSyntheticSourceKeep = System.Text.Json.JsonEncodedText.Encode("synthetic_source_keep");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.Mapping.JoinProperty Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.DynamicMapping?> propDynamic = default;
		LocalJsonValue<bool?> propEagerGlobalOrdinals = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propFields = default;
		LocalJsonValue<int?> propIgnoreAbove = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, string>?> propMeta = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.Properties?> propProperties = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, System.Collections.Generic.ICollection<string>>?> propRelations = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum?> propSyntheticSourceKeep = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
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

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propProperties.TryReadProperty(ref reader, options, PropProperties, null))
			{
				continue;
			}

			if (propRelations.TryReadProperty(ref reader, options, PropRelations, static System.Collections.Generic.IDictionary<string, System.Collections.Generic.ICollection<string>>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, System.Collections.Generic.ICollection<string>>(o, null, static System.Collections.Generic.ICollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<string>(o, null)!)))
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
		return new Elastic.Clients.Elasticsearch.Mapping.JoinProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Dynamic = propDynamic.Value,
			EagerGlobalOrdinals = propEagerGlobalOrdinals.Value,
			Fields = propFields.Value,
			IgnoreAbove = propIgnoreAbove.Value,
			Meta = propMeta.Value,
			Properties = propProperties.Value,
			Relations = propRelations.Value,
			SyntheticSourceKeep = propSyntheticSourceKeep.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Mapping.JoinProperty value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDynamic, value.Dynamic, null, null);
		writer.WriteProperty(options, PropEagerGlobalOrdinals, value.EagerGlobalOrdinals, null, null);
		writer.WriteProperty(options, PropFields, value.Fields, null, null);
		writer.WriteProperty(options, PropIgnoreAbove, value.IgnoreAbove, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropProperties, value.Properties, null, null);
		writer.WriteProperty(options, PropRelations, value.Relations, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, System.Collections.Generic.ICollection<string>>? v) => w.WriteDictionaryValue<string, System.Collections.Generic.ICollection<string>>(o, v, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteSingleOrManyCollectionValue<string>(o, v, null)));
		writer.WriteProperty(options, PropSyntheticSourceKeep, value.SyntheticSourceKeep, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Mapping.JoinPropertyConverter))]
public sealed partial class JoinProperty : Elastic.Clients.Elasticsearch.Mapping.IProperty
{
#if NET7_0_OR_GREATER
	public JoinProperty()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public JoinProperty()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal JoinProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }
	public bool? EagerGlobalOrdinals { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }
	public int? IgnoreAbove { get; set; }

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, string>? Meta { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }
	public System.Collections.Generic.IDictionary<string, System.Collections.Generic.ICollection<string>>? Relations { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? SyntheticSourceKeep { get; set; }

	public string Type => "join";
}

public readonly partial struct JoinPropertyDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Mapping.JoinProperty Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public JoinPropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.JoinProperty instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public JoinPropertyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.JoinProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Mapping.JoinProperty instance) => new Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.JoinProperty(Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? value)
	{
		Instance.Dynamic = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> EagerGlobalOrdinals(bool? value = true)
	{
		Instance.EagerGlobalOrdinals = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Fields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> Fields(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> IgnoreAbove(int? value)
	{
		Instance.IgnoreAbove = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> Meta(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> AddMeta(string key, string value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Properties = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> Properties(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> Relations(System.Collections.Generic.IDictionary<string, System.Collections.Generic.ICollection<string>>? value)
	{
		Instance.Relations = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> Relations()
	{
		Instance.Relations = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringCollectionOfString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> Relations(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringCollectionOfString>? action)
	{
		Instance.Relations = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringCollectionOfString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> AddRelation(string key, System.Collections.Generic.ICollection<string> value)
	{
		Instance.Relations ??= new System.Collections.Generic.Dictionary<string, System.Collections.Generic.ICollection<string>>();
		Instance.Relations.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> AddRelation(string key, params string[] values)
	{
		Instance.Relations ??= new System.Collections.Generic.Dictionary<string, System.Collections.Generic.ICollection<string>>();
		Instance.Relations.Add(key, [.. values]);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument> SyntheticSourceKeep(Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? value)
	{
		Instance.SyntheticSourceKeep = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.JoinProperty Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Mapping.JoinProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Mapping.JoinProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct JoinPropertyDescriptor
{
	internal Elastic.Clients.Elasticsearch.Mapping.JoinProperty Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public JoinPropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.JoinProperty instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public JoinPropertyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.JoinProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor(Elastic.Clients.Elasticsearch.Mapping.JoinProperty instance) => new Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.JoinProperty(Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? value)
	{
		Instance.Dynamic = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor EagerGlobalOrdinals(bool? value = true)
	{
		Instance.EagerGlobalOrdinals = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Fields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Fields(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Fields<T>(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>> action)
	{
		Instance.Fields = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor IgnoreAbove(int? value)
	{
		Instance.IgnoreAbove = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Meta(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Metadata about the field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor AddMeta(string key, string value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? value)
	{
		Instance.Properties = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Properties(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Properties<T>(System.Action<Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>> action)
	{
		Instance.Properties = Elastic.Clients.Elasticsearch.Mapping.PropertiesDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Relations(System.Collections.Generic.IDictionary<string, System.Collections.Generic.ICollection<string>>? value)
	{
		Instance.Relations = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Relations()
	{
		Instance.Relations = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringCollectionOfString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor Relations(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringCollectionOfString>? action)
	{
		Instance.Relations = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringCollectionOfString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor AddRelation(string key, System.Collections.Generic.ICollection<string> value)
	{
		Instance.Relations ??= new System.Collections.Generic.Dictionary<string, System.Collections.Generic.ICollection<string>>();
		Instance.Relations.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor AddRelation(string key, params string[] values)
	{
		Instance.Relations ??= new System.Collections.Generic.Dictionary<string, System.Collections.Generic.ICollection<string>>();
		Instance.Relations.Add(key, [.. values]);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor SyntheticSourceKeep(Elastic.Clients.Elasticsearch.Mapping.SyntheticSourceKeepEnum? value)
	{
		Instance.SyntheticSourceKeep = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.JoinProperty Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Mapping.JoinProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Mapping.JoinPropertyDescriptor(new Elastic.Clients.Elasticsearch.Mapping.JoinProperty(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
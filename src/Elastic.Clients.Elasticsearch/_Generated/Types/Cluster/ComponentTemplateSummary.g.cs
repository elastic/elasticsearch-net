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

namespace Elastic.Clients.Elasticsearch.Cluster;

internal sealed partial class ComponentTemplateSummaryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary>
{
	private static readonly System.Text.Json.JsonEncodedText PropAliases = System.Text.Json.JsonEncodedText.Encode("aliases");
	private static readonly System.Text.Json.JsonEncodedText PropLifecycle = System.Text.Json.JsonEncodedText.Encode("lifecycle");
	private static readonly System.Text.Json.JsonEncodedText PropMappings = System.Text.Json.JsonEncodedText.Encode("mappings");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("_meta");
	private static readonly System.Text.Json.JsonEncodedText PropSettings = System.Text.Json.JsonEncodedText.Encode("settings");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>?> propAliases = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover?> propLifecycle = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.TypeMapping?> propMappings = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propMeta = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>?> propSettings = default;
		LocalJsonValue<long?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAliases.TryReadProperty(ref reader, options, PropAliases, static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>(o, null, null)))
			{
				continue;
			}

			if (propLifecycle.TryReadProperty(ref reader, options, PropLifecycle, null))
			{
				continue;
			}

			if (propMappings.TryReadProperty(ref reader, options, PropMappings, null))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propSettings.TryReadProperty(ref reader, options, PropSettings, static System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>(o, null, null)))
			{
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
			{
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
		return new Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Aliases = propAliases.Value,
			Lifecycle = propLifecycle.Value,
			Mappings = propMappings.Value,
			Meta = propMeta.Value,
			Settings = propSettings.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAliases, value.Aliases, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>(o, v, null, null));
		writer.WriteProperty(options, PropLifecycle, value.Lifecycle, null, null);
		writer.WriteProperty(options, PropMappings, value.Mappings, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropSettings, value.Settings, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>? v) => w.WriteDictionaryValue<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>(o, v, null, null));
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryConverter))]
public sealed partial class ComponentTemplateSummary
{
#if NET7_0_OR_GREATER
	public ComponentTemplateSummary()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public ComponentTemplateSummary()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ComponentTemplateSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>? Aliases { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover? Lifecycle { get; set; }
	public Elastic.Clients.Elasticsearch.Mapping.TypeMapping? Mappings { get; set; }
	public System.Collections.Generic.IDictionary<string, object>? Meta { get; set; }
	public System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>? Settings { get; set; }
	public long? Version { get; set; }
}

public readonly partial struct ComponentTemplateSummaryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ComponentTemplateSummaryDescriptor(Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ComponentTemplateSummaryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary instance) => new Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary(Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Aliases(System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>? value)
	{
		Instance.Aliases = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Aliases()
	{
		Instance.Aliases = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringAliasDefinition<TDocument>.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Aliases(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringAliasDefinition<TDocument>>? action)
	{
		Instance.Aliases = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringAliasDefinition<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> AddAlias(string key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition value)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>();
		Instance.Aliases.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Aliases(string key)
	{
		Instance.Aliases = new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition> { { key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument>.Build(null) } };
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Aliases(params string[] keys)
	{
		var items = new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>();
		foreach (var key in keys)
		{
			items.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument>.Build(null));
		}

		Instance.Aliases = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> AddAlias(string key)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>();
		Instance.Aliases.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument>.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> AddAlias(string key, System.Action<Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument>>? action)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>();
		Instance.Aliases.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument>.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Lifecycle(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover? value)
	{
		Instance.Lifecycle = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Lifecycle()
	{
		Instance.Lifecycle = Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Lifecycle(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor>? action)
	{
		Instance.Lifecycle = Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Mappings(Elastic.Clients.Elasticsearch.Mapping.TypeMapping? value)
	{
		Instance.Mappings = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Mappings()
	{
		Instance.Mappings = Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<TDocument>.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Mappings(System.Action<Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<TDocument>>? action)
	{
		Instance.Mappings = Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Meta(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Meta = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> AddMeta(string key, object value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Settings(System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>? value)
	{
		Instance.Settings = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Settings()
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfIndexNameIndexSettings<TDocument>.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Settings(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfIndexNameIndexSettings<TDocument>>? action)
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfIndexNameIndexSettings<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> AddSetting(Elastic.Clients.Elasticsearch.IndexName key, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings value)
	{
		Instance.Settings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>();
		Instance.Settings.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Settings(Elastic.Clients.Elasticsearch.IndexName key)
	{
		Instance.Settings = new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings> { { key, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>.Build(null) } };
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Settings(params Elastic.Clients.Elasticsearch.IndexName[] keys)
	{
		var items = new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>();
		foreach (var key in keys)
		{
			items.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>.Build(null));
		}

		Instance.Settings = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> AddSetting(Elastic.Clients.Elasticsearch.IndexName key)
	{
		Instance.Settings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>();
		Instance.Settings.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> AddSetting(Elastic.Clients.Elasticsearch.IndexName key, System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>>? action)
	{
		Instance.Settings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>();
		Instance.Settings.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument> Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary Build(System.Action<Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct ComponentTemplateSummaryDescriptor
{
	internal Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ComponentTemplateSummaryDescriptor(Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ComponentTemplateSummaryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor(Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary instance) => new Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary(Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Aliases(System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>? value)
	{
		Instance.Aliases = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Aliases()
	{
		Instance.Aliases = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringAliasDefinition.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Aliases(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringAliasDefinition>? action)
	{
		Instance.Aliases = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringAliasDefinition.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Aliases<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringAliasDefinition<T>>? action)
	{
		Instance.Aliases = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringAliasDefinition<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor AddAlias(string key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition value)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>();
		Instance.Aliases.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Aliases(string key)
	{
		Instance.Aliases = new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition> { { key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor.Build(null) } };
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Aliases(params string[] keys)
	{
		var items = new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>();
		foreach (var key in keys)
		{
			items.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor.Build(null));
		}

		Instance.Aliases = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor AddAlias(string key)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>();
		Instance.Aliases.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor AddAlias(string key, System.Action<Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor>? action)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>();
		Instance.Aliases.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor AddAlias<T>(string key, System.Action<Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<T>>? action)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>();
		Instance.Aliases.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<T>.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Lifecycle(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover? value)
	{
		Instance.Lifecycle = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Lifecycle()
	{
		Instance.Lifecycle = Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Lifecycle(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor>? action)
	{
		Instance.Lifecycle = Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Mappings(Elastic.Clients.Elasticsearch.Mapping.TypeMapping? value)
	{
		Instance.Mappings = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Mappings()
	{
		Instance.Mappings = Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Mappings(System.Action<Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor>? action)
	{
		Instance.Mappings = Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Mappings<T>(System.Action<Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<T>>? action)
	{
		Instance.Mappings = Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Meta(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Meta = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor AddMeta(string key, object value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Settings(System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>? value)
	{
		Instance.Settings = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Settings()
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfIndexNameIndexSettings.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Settings(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfIndexNameIndexSettings>? action)
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfIndexNameIndexSettings.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Settings<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfIndexNameIndexSettings<T>>? action)
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfIndexNameIndexSettings<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor AddSetting(Elastic.Clients.Elasticsearch.IndexName key, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings value)
	{
		Instance.Settings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>();
		Instance.Settings.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Settings(Elastic.Clients.Elasticsearch.IndexName key)
	{
		Instance.Settings = new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings> { { key, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor.Build(null) } };
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Settings(params Elastic.Clients.Elasticsearch.IndexName[] keys)
	{
		var items = new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>();
		foreach (var key in keys)
		{
			items.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor.Build(null));
		}

		Instance.Settings = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor AddSetting(Elastic.Clients.Elasticsearch.IndexName key)
	{
		Instance.Settings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>();
		Instance.Settings.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor AddSetting(Elastic.Clients.Elasticsearch.IndexName key, System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor>? action)
	{
		Instance.Settings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>();
		Instance.Settings.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor AddSetting<T>(Elastic.Clients.Elasticsearch.IndexName key, System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<T>>? action)
	{
		Instance.Settings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings>();
		Instance.Settings.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<T>.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary Build(System.Action<Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummaryDescriptor(new Elastic.Clients.Elasticsearch.Cluster.ComponentTemplateSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
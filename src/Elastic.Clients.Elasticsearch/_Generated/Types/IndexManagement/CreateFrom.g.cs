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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class CreateFromConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom>
{
	private static readonly System.Text.Json.JsonEncodedText PropMappingsOverride = System.Text.Json.JsonEncodedText.Encode("mappings_override");
	private static readonly System.Text.Json.JsonEncodedText PropRemoveIndexBlocks = System.Text.Json.JsonEncodedText.Encode("remove_index_blocks");
	private static readonly System.Text.Json.JsonEncodedText PropSettingsOverride = System.Text.Json.JsonEncodedText.Encode("settings_override");

	public override Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.TypeMapping?> propMappingsOverride = default;
		LocalJsonValue<bool?> propRemoveIndexBlocks = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings?> propSettingsOverride = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMappingsOverride.TryReadProperty(ref reader, options, PropMappingsOverride, null))
			{
				continue;
			}

			if (propRemoveIndexBlocks.TryReadProperty(ref reader, options, PropRemoveIndexBlocks, null))
			{
				continue;
			}

			if (propSettingsOverride.TryReadProperty(ref reader, options, PropSettingsOverride, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			MappingsOverride = propMappingsOverride.Value,
			RemoveIndexBlocks = propRemoveIndexBlocks.Value,
			SettingsOverride = propSettingsOverride.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMappingsOverride, value.MappingsOverride, null, null);
		writer.WriteProperty(options, PropRemoveIndexBlocks, value.RemoveIndexBlocks, null, null);
		writer.WriteProperty(options, PropSettingsOverride, value.SettingsOverride, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.CreateFromConverter))]
public sealed partial class CreateFrom
{
#if NET7_0_OR_GREATER
	public CreateFrom()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public CreateFrom()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CreateFrom(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Mappings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.TypeMapping? MappingsOverride { get; set; }

	/// <summary>
	/// <para>
	/// If index blocks should be removed when creating destination index (optional)
	/// </para>
	/// </summary>
	public bool? RemoveIndexBlocks { get; set; }

	/// <summary>
	/// <para>
	/// Settings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? SettingsOverride { get; set; }
}

public readonly partial struct CreateFromDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateFromDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateFromDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor<TDocument>(Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom instance) => new Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom(Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Mappings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor<TDocument> MappingsOverride(Elastic.Clients.Elasticsearch.Mapping.TypeMapping? value)
	{
		Instance.MappingsOverride = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Mappings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor<TDocument> MappingsOverride()
	{
		Instance.MappingsOverride = Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Mappings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor<TDocument> MappingsOverride(System.Action<Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<TDocument>>? action)
	{
		Instance.MappingsOverride = Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// If index blocks should be removed when creating destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor<TDocument> RemoveIndexBlocks(bool? value = true)
	{
		Instance.RemoveIndexBlocks = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor<TDocument> SettingsOverride(Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? value)
	{
		Instance.SettingsOverride = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor<TDocument> SettingsOverride()
	{
		Instance.SettingsOverride = Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor<TDocument> SettingsOverride(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>>? action)
	{
		Instance.SettingsOverride = Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct CreateFromDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateFromDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateFromDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom instance) => new Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom(Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Mappings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor MappingsOverride(Elastic.Clients.Elasticsearch.Mapping.TypeMapping? value)
	{
		Instance.MappingsOverride = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Mappings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor MappingsOverride()
	{
		Instance.MappingsOverride = Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Mappings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor MappingsOverride(System.Action<Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor>? action)
	{
		Instance.MappingsOverride = Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Mappings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor MappingsOverride<T>(System.Action<Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<T>>? action)
	{
		Instance.MappingsOverride = Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// If index blocks should be removed when creating destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor RemoveIndexBlocks(bool? value = true)
	{
		Instance.RemoveIndexBlocks = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor SettingsOverride(Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? value)
	{
		Instance.SettingsOverride = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor SettingsOverride()
	{
		Instance.SettingsOverride = Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor SettingsOverride(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor>? action)
	{
		Instance.SettingsOverride = Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings overrides to be applied to the destination index (optional)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor SettingsOverride<T>(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<T>>? action)
	{
		Instance.SettingsOverride = Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<T>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.CreateFromDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.CreateFrom(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
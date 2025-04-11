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

internal sealed partial class AliasDefinitionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition>
{
	private static readonly System.Text.Json.JsonEncodedText PropFilter = System.Text.Json.JsonEncodedText.Encode("filter");
	private static readonly System.Text.Json.JsonEncodedText PropIndexRouting = System.Text.Json.JsonEncodedText.Encode("index_routing");
	private static readonly System.Text.Json.JsonEncodedText PropIsHidden = System.Text.Json.JsonEncodedText.Encode("is_hidden");
	private static readonly System.Text.Json.JsonEncodedText PropIsWriteIndex = System.Text.Json.JsonEncodedText.Encode("is_write_index");
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("routing");
	private static readonly System.Text.Json.JsonEncodedText PropSearchRouting = System.Text.Json.JsonEncodedText.Encode("search_routing");

	public override Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query?> propFilter = default;
		LocalJsonValue<string?> propIndexRouting = default;
		LocalJsonValue<bool?> propIsHidden = default;
		LocalJsonValue<bool?> propIsWriteIndex = default;
		LocalJsonValue<string?> propRouting = default;
		LocalJsonValue<string?> propSearchRouting = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFilter.TryReadProperty(ref reader, options, PropFilter, null))
			{
				continue;
			}

			if (propIndexRouting.TryReadProperty(ref reader, options, PropIndexRouting, null))
			{
				continue;
			}

			if (propIsHidden.TryReadProperty(ref reader, options, PropIsHidden, null))
			{
				continue;
			}

			if (propIsWriteIndex.TryReadProperty(ref reader, options, PropIsWriteIndex, null))
			{
				continue;
			}

			if (propRouting.TryReadProperty(ref reader, options, PropRouting, null))
			{
				continue;
			}

			if (propSearchRouting.TryReadProperty(ref reader, options, PropSearchRouting, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Filter = propFilter.Value,
			IndexRouting = propIndexRouting.Value,
			IsHidden = propIsHidden.Value,
			IsWriteIndex = propIsWriteIndex.Value,
			Routing = propRouting.Value,
			SearchRouting = propSearchRouting.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFilter, value.Filter, null, null);
		writer.WriteProperty(options, PropIndexRouting, value.IndexRouting, null, null);
		writer.WriteProperty(options, PropIsHidden, value.IsHidden, null, null);
		writer.WriteProperty(options, PropIsWriteIndex, value.IsWriteIndex, null, null);
		writer.WriteProperty(options, PropRouting, value.Routing, null, null);
		writer.WriteProperty(options, PropSearchRouting, value.SearchRouting, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionConverter))]
public sealed partial class AliasDefinition
{
#if NET7_0_OR_GREATER
	public AliasDefinition()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public AliasDefinition()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AliasDefinition(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Query used to limit documents the alias can access.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Filter { get; set; }

	/// <summary>
	/// <para>
	/// Value used to route indexing operations to a specific shard.
	/// If specified, this overwrites the <c>routing</c> value for indexing operations.
	/// </para>
	/// </summary>
	public string? IndexRouting { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the alias is hidden.
	/// All indices for the alias must have the same <c>is_hidden</c> value.
	/// </para>
	/// </summary>
	public bool? IsHidden { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the index is the write index for the alias.
	/// </para>
	/// </summary>
	public bool? IsWriteIndex { get; set; }

	/// <summary>
	/// <para>
	/// Value used to route indexing and search operations to a specific shard.
	/// </para>
	/// </summary>
	public string? Routing { get; set; }

	/// <summary>
	/// <para>
	/// Value used to route search operations to a specific shard.
	/// If specified, this overwrites the <c>routing</c> value for search operations.
	/// </para>
	/// </summary>
	public string? SearchRouting { get; set; }
}

public readonly partial struct AliasDefinitionDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AliasDefinitionDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AliasDefinitionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument>(Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition instance) => new Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition(Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Query used to limit documents the alias can access.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query used to limit documents the alias can access.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument> Filter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Value used to route indexing operations to a specific shard.
	/// If specified, this overwrites the <c>routing</c> value for indexing operations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument> IndexRouting(string? value)
	{
		Instance.IndexRouting = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the alias is hidden.
	/// All indices for the alias must have the same <c>is_hidden</c> value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument> IsHidden(bool? value = true)
	{
		Instance.IsHidden = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the index is the write index for the alias.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument> IsWriteIndex(bool? value = true)
	{
		Instance.IsWriteIndex = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Value used to route indexing and search operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument> Routing(string? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Value used to route search operations to a specific shard.
	/// If specified, this overwrites the <c>routing</c> value for search operations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument> SearchRouting(string? value)
	{
		Instance.SearchRouting = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct AliasDefinitionDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AliasDefinitionDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AliasDefinitionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition instance) => new Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition(Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Query used to limit documents the alias can access.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query used to limit documents the alias can access.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor Filter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query used to limit documents the alias can access.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor Filter<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Value used to route indexing operations to a specific shard.
	/// If specified, this overwrites the <c>routing</c> value for indexing operations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor IndexRouting(string? value)
	{
		Instance.IndexRouting = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the alias is hidden.
	/// All indices for the alias must have the same <c>is_hidden</c> value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor IsHidden(bool? value = true)
	{
		Instance.IsHidden = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the index is the write index for the alias.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor IsWriteIndex(bool? value = true)
	{
		Instance.IsWriteIndex = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Value used to route indexing and search operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor Routing(string? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Value used to route search operations to a specific shard.
	/// If specified, this overwrites the <c>routing</c> value for search operations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor SearchRouting(string? value)
	{
		Instance.SearchRouting = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinitionDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.AliasDefinition(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
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

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class FieldCollapseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCollapse = System.Text.Json.JsonEncodedText.Encode("collapse");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropInnerHits = System.Text.Json.JsonEncodedText.Encode("inner_hits");
	private static readonly System.Text.Json.JsonEncodedText PropMaxConcurrentGroupSearches = System.Text.Json.JsonEncodedText.Encode("max_concurrent_group_searches");

	public override Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse?> propCollapse = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.InnerHits>?> propInnerHits = default;
		LocalJsonValue<int?> propMaxConcurrentGroupSearches = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCollapse.TryReadProperty(ref reader, options, PropCollapse, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propInnerHits.TryReadProperty(ref reader, options, PropInnerHits, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.InnerHits>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.InnerHits>(o, null)))
			{
				continue;
			}

			if (propMaxConcurrentGroupSearches.TryReadProperty(ref reader, options, PropMaxConcurrentGroupSearches, null))
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
		return new Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Collapse = propCollapse.Value,
			Field = propField.Value,
			InnerHits = propInnerHits.Value,
			MaxConcurrentGroupSearches = propMaxConcurrentGroupSearches.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCollapse, value.Collapse, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropInnerHits, value.InnerHits, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.InnerHits>? v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.InnerHits>(o, v, null));
		writer.WriteProperty(options, PropMaxConcurrentGroupSearches, value.MaxConcurrentGroupSearches, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseConverter))]
public sealed partial class FieldCollapse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldCollapse(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public FieldCollapse()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public FieldCollapse()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FieldCollapse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse? Collapse { get; set; }

	/// <summary>
	/// <para>
	/// The field to collapse the result set on
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.InnerHits>? InnerHits { get; set; }

	/// <summary>
	/// <para>
	/// The number of concurrent requests allowed to retrieve the inner_hits per group
	/// </para>
	/// </summary>
	public int? MaxConcurrentGroupSearches { get; set; }
}

public readonly partial struct FieldCollapseDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldCollapseDescriptor(Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldCollapseDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse instance) => new Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse(Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument> Collapse(Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse? value)
	{
		Instance.Collapse = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument> Collapse(System.Action<Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument>> action)
	{
		Instance.Collapse = Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to collapse the result set on
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to collapse the result set on
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument> InnerHits(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.InnerHits>? value)
	{
		Instance.InnerHits = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument> InnerHits()
	{
		Instance.InnerHits = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfInnerHits<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument> InnerHits(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfInnerHits<TDocument>>? action)
	{
		Instance.InnerHits = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfInnerHits<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument> InnerHits(params Elastic.Clients.Elasticsearch.Core.Search.InnerHits[] values)
	{
		Instance.InnerHits = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument> InnerHits(params System.Action<Elastic.Clients.Elasticsearch.Core.Search.InnerHitsDescriptor<TDocument>>?[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Core.Search.InnerHits>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Core.Search.InnerHitsDescriptor<TDocument>.Build(action));
		}

		Instance.InnerHits = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of concurrent requests allowed to retrieve the inner_hits per group
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument> MaxConcurrentGroupSearches(int? value)
	{
		Instance.MaxConcurrentGroupSearches = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct FieldCollapseDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldCollapseDescriptor(Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldCollapseDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor(Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse instance) => new Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse(Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor Collapse(Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse? value)
	{
		Instance.Collapse = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor Collapse(System.Action<Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor> action)
	{
		Instance.Collapse = Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor Collapse<T>(System.Action<Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<T>> action)
	{
		Instance.Collapse = Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to collapse the result set on
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to collapse the result set on
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor InnerHits(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.InnerHits>? value)
	{
		Instance.InnerHits = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor InnerHits()
	{
		Instance.InnerHits = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfInnerHits.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor InnerHits(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfInnerHits>? action)
	{
		Instance.InnerHits = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfInnerHits.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor InnerHits<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfInnerHits<T>>? action)
	{
		Instance.InnerHits = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfInnerHits<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor InnerHits(params Elastic.Clients.Elasticsearch.Core.Search.InnerHits[] values)
	{
		Instance.InnerHits = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor InnerHits(params System.Action<Elastic.Clients.Elasticsearch.Core.Search.InnerHitsDescriptor>?[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Core.Search.InnerHits>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Core.Search.InnerHitsDescriptor.Build(action));
		}

		Instance.InnerHits = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of inner hits and their sort order
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor InnerHits<T>(params System.Action<Elastic.Clients.Elasticsearch.Core.Search.InnerHitsDescriptor<T>>?[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Core.Search.InnerHits>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Core.Search.InnerHitsDescriptor<T>.Build(action));
		}

		Instance.InnerHits = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of concurrent requests allowed to retrieve the inner_hits per group
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor MaxConcurrentGroupSearches(int? value)
	{
		Instance.MaxConcurrentGroupSearches = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.FieldCollapseDescriptor(new Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
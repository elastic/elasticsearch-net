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

namespace Elastic.Clients.Elasticsearch.QueryDsl;

internal sealed partial class SpanNearQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropClauses = System.Text.Json.JsonEncodedText.Encode("clauses");
	private static readonly System.Text.Json.JsonEncodedText PropInOrder = System.Text.Json.JsonEncodedText.Encode("in_order");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropSlop = System.Text.Json.JsonEncodedText.Encode("slop");

	public override Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery>> propClauses = default;
		LocalJsonValue<bool?> propInOrder = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<int?> propSlop = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propClauses.TryReadProperty(ref reader, options, PropClauses, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery>(o, null)!))
			{
				continue;
			}

			if (propInOrder.TryReadProperty(ref reader, options, PropInOrder, null))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
			{
				continue;
			}

			if (propSlop.TryReadProperty(ref reader, options, PropSlop, null))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			Clauses = propClauses.Value,
			InOrder = propInOrder.Value,
			QueryName = propQueryName.Value,
			Slop = propSlop.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropClauses, value.Clauses, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery>(o, v, null));
		writer.WriteProperty(options, PropInOrder, value.InOrder, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteProperty(options, PropSlop, value.Slop, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryConverter))]
public sealed partial class SpanNearQuery
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanNearQuery(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> clauses)
	{
		Clauses = clauses;
	}
#if NET7_0_OR_GREATER
	public SpanNearQuery()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SpanNearQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SpanNearQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public float? Boost { get; set; }

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> Clauses { get; set; }

	/// <summary>
	/// <para>
	/// Controls whether matches are required to be in-order.
	/// </para>
	/// </summary>
	public bool? InOrder { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Controls the maximum number of intervening unmatched positions permitted.
	/// </para>
	/// </summary>
	public int? Slop { get; set; }
}

public readonly partial struct SpanNearQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanNearQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanNearQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery(Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument> Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument> Clauses(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> value)
	{
		Instance.Clauses = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument> Clauses()
	{
		Instance.Clauses = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument> Clauses(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery<TDocument>>? action)
	{
		Instance.Clauses = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument> Clauses(params Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery[] values)
	{
		Instance.Clauses = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument> Clauses(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor<TDocument>.Build(action));
		}

		Instance.Clauses = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// Controls whether matches are required to be in-order.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument> InOrder(bool? value = true)
	{
		Instance.InOrder = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument> QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Controls the maximum number of intervening unmatched positions permitted.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument> Slop(int? value)
	{
		Instance.Slop = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct SpanNearQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanNearQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanNearQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery(Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor Clauses(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> value)
	{
		Instance.Clauses = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor Clauses()
	{
		Instance.Clauses = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor Clauses(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery>? action)
	{
		Instance.Clauses = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor Clauses<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery<T>>? action)
	{
		Instance.Clauses = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor Clauses(params Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery[] values)
	{
		Instance.Clauses = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor Clauses(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor.Build(action));
		}

		Instance.Clauses = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor Clauses<T>(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor<T>.Build(action));
		}

		Instance.Clauses = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// Controls whether matches are required to be in-order.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor InOrder(bool? value = true)
	{
		Instance.InOrder = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Controls the maximum number of intervening unmatched positions permitted.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor Slop(int? value)
	{
		Instance.Slop = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQueryDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.SpanNearQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
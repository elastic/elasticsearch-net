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

internal sealed partial class SpanOrQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropClauses = System.Text.Json.JsonEncodedText.Encode("clauses");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");

	public override Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery>> propClauses = default;
		LocalJsonValue<string?> propQueryName = default;
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

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			Clauses = propClauses.Value,
			QueryName = propQueryName.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropClauses, value.Clauses, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery>(o, v, null));
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryConverter))]
public sealed partial class SpanOrQuery
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanOrQuery(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> clauses)
	{
		Clauses = clauses;
	}
#if NET7_0_OR_GREATER
	public SpanOrQuery()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SpanOrQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SpanOrQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
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
	public string? QueryName { get; set; }
}

public readonly partial struct SpanOrQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanOrQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanOrQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery(Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor<TDocument> Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor<TDocument> Clauses(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> value)
	{
		Instance.Clauses = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor<TDocument> Clauses()
	{
		Instance.Clauses = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor<TDocument> Clauses(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery<TDocument>>? action)
	{
		Instance.Clauses = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor<TDocument> Clauses(params Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery[] values)
	{
		Instance.Clauses = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor<TDocument> Clauses(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor<TDocument>.Build(action));
		}

		Instance.Clauses = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor<TDocument> QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct SpanOrQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanOrQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanOrQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery(Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor Clauses(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> value)
	{
		Instance.Clauses = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor Clauses()
	{
		Instance.Clauses = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor Clauses(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery>? action)
	{
		Instance.Clauses = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor Clauses<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery<T>>? action)
	{
		Instance.Clauses = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfSpanQuery<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor Clauses(params Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery[] values)
	{
		Instance.Clauses = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of one or more other span type queries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor Clauses(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor>[] actions)
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
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor Clauses<T>(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor<T>.Build(action));
		}

		Instance.Clauses = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQueryDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.SpanOrQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
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

namespace Elastic.Clients.Elasticsearch.Aggregations;

internal sealed partial class AdjacencyMatrixAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropFilters = System.Text.Json.JsonEncodedText.Encode("filters");
	private static readonly System.Text.Json.JsonEncodedText PropSeparator = System.Text.Json.JsonEncodedText.Encode("separator");

	public override Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>?> propFilters = default;
		LocalJsonValue<string?> propSeparator = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFilters.TryReadProperty(ref reader, options, PropFilters, static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>(o, null, null)))
			{
				continue;
			}

			if (propSeparator.TryReadProperty(ref reader, options, PropSeparator, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Filters = propFilters.Value,
			Separator = propSeparator.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFilters, value.Filters, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>(o, v, null, null));
		writer.WriteProperty(options, PropSeparator, value.Separator, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationConverter))]
public sealed partial class AdjacencyMatrixAggregation
{
#if NET7_0_OR_GREATER
	public AdjacencyMatrixAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public AdjacencyMatrixAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AdjacencyMatrixAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Filters used to create buckets.
	/// At least one filter is required.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>? Filters { get; set; }

	/// <summary>
	/// <para>
	/// Separator used to concatenate filter names. Defaults to &amp;.
	/// </para>
	/// </summary>
	public string? Separator { get; set; }
}

public readonly partial struct AdjacencyMatrixAggregationDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AdjacencyMatrixAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AdjacencyMatrixAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation(Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Filters used to create buckets.
	/// At least one filter is required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor<TDocument> Filters(System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>? value)
	{
		Instance.Filters = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters used to create buckets.
	/// At least one filter is required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor<TDocument> Filters()
	{
		Instance.Filters = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters used to create buckets.
	/// At least one filter is required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor<TDocument> Filters(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery<TDocument>>? action)
	{
		Instance.Filters = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor<TDocument> AddFilter(string key, Elastic.Clients.Elasticsearch.QueryDsl.Query value)
	{
		Instance.Filters ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>();
		Instance.Filters.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor<TDocument> AddFilter(string key, System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.Filters ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>();
		Instance.Filters.Add(key, Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action));
		return this;
	}

	/// <summary>
	/// <para>
	/// Separator used to concatenate filter names. Defaults to &amp;.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor<TDocument> Separator(string? value)
	{
		Instance.Separator = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct AdjacencyMatrixAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AdjacencyMatrixAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AdjacencyMatrixAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation(Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Filters used to create buckets.
	/// At least one filter is required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor Filters(System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>? value)
	{
		Instance.Filters = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters used to create buckets.
	/// At least one filter is required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor Filters()
	{
		Instance.Filters = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters used to create buckets.
	/// At least one filter is required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor Filters(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery>? action)
	{
		Instance.Filters = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters used to create buckets.
	/// At least one filter is required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor Filters<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery<T>>? action)
	{
		Instance.Filters = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor AddFilter(string key, Elastic.Clients.Elasticsearch.QueryDsl.Query value)
	{
		Instance.Filters ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>();
		Instance.Filters.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor AddFilter(string key, System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.Filters ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>();
		Instance.Filters.Add(key, Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor AddFilter<T>(string key, System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.Filters ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query>();
		Instance.Filters.Add(key, Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action));
		return this;
	}

	/// <summary>
	/// <para>
	/// Separator used to concatenate filter names. Defaults to &amp;.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor Separator(string? value)
	{
		Instance.Separator = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
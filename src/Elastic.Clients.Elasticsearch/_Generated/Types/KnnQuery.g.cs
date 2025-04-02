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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class KnnQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.KnnQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropFilter = System.Text.Json.JsonEncodedText.Encode("filter");
	private static readonly System.Text.Json.JsonEncodedText PropK = System.Text.Json.JsonEncodedText.Encode("k");
	private static readonly System.Text.Json.JsonEncodedText PropNumCandidates = System.Text.Json.JsonEncodedText.Encode("num_candidates");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropQueryVector = System.Text.Json.JsonEncodedText.Encode("query_vector");
	private static readonly System.Text.Json.JsonEncodedText PropQueryVectorBuilder = System.Text.Json.JsonEncodedText.Encode("query_vector_builder");
	private static readonly System.Text.Json.JsonEncodedText PropRescoreVector = System.Text.Json.JsonEncodedText.Encode("rescore_vector");
	private static readonly System.Text.Json.JsonEncodedText PropSimilarity = System.Text.Json.JsonEncodedText.Encode("similarity");

	public override Elastic.Clients.Elasticsearch.KnnQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>?> propFilter = default;
		LocalJsonValue<int?> propK = default;
		LocalJsonValue<int?> propNumCandidates = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<System.Collections.Generic.ICollection<float>?> propQueryVector = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryVectorBuilder?> propQueryVectorBuilder = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.RescoreVector?> propRescoreVector = default;
		LocalJsonValue<float?> propSimilarity = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propFilter.TryReadProperty(ref reader, options, PropFilter, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.QueryDsl.Query>(o, null)))
			{
				continue;
			}

			if (propK.TryReadProperty(ref reader, options, PropK, null))
			{
				continue;
			}

			if (propNumCandidates.TryReadProperty(ref reader, options, PropNumCandidates, null))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
			{
				continue;
			}

			if (propQueryVector.TryReadProperty(ref reader, options, PropQueryVector, static System.Collections.Generic.ICollection<float>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<float>(o, null)))
			{
				continue;
			}

			if (propQueryVectorBuilder.TryReadProperty(ref reader, options, PropQueryVectorBuilder, null))
			{
				continue;
			}

			if (propRescoreVector.TryReadProperty(ref reader, options, PropRescoreVector, null))
			{
				continue;
			}

			if (propSimilarity.TryReadProperty(ref reader, options, PropSimilarity, null))
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
		return new Elastic.Clients.Elasticsearch.KnnQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			Field = propField.Value,
			Filter = propFilter.Value,
			K = propK.Value,
			NumCandidates = propNumCandidates.Value,
			QueryName = propQueryName.Value,
			QueryVector = propQueryVector.Value,
			QueryVectorBuilder = propQueryVectorBuilder.Value,
			RescoreVector = propRescoreVector.Value,
			Similarity = propSimilarity.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.KnnQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropFilter, value.Filter, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.QueryDsl.Query>(o, v, null));
		writer.WriteProperty(options, PropK, value.K, null, null);
		writer.WriteProperty(options, PropNumCandidates, value.NumCandidates, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteProperty(options, PropQueryVector, value.QueryVector, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<float>? v) => w.WriteCollectionValue<float>(o, v, null));
		writer.WriteProperty(options, PropQueryVectorBuilder, value.QueryVectorBuilder, null, null);
		writer.WriteProperty(options, PropRescoreVector, value.RescoreVector, null, null);
		writer.WriteProperty(options, PropSimilarity, value.Similarity, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.KnnQueryConverter))]
public sealed partial class KnnQuery
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnQuery(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public KnnQuery()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public KnnQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal KnnQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
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
	/// The name of the vector field to search against
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? Filter { get; set; }

	/// <summary>
	/// <para>
	/// The final number of nearest neighbors to return as top hits
	/// </para>
	/// </summary>
	public int? K { get; set; }

	/// <summary>
	/// <para>
	/// The number of nearest neighbor candidates to consider per shard
	/// </para>
	/// </summary>
	public int? NumCandidates { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// The query vector
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<float>? QueryVector { get; set; }

	/// <summary>
	/// <para>
	/// The query vector builder. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryVectorBuilder? QueryVectorBuilder { get; set; }

	/// <summary>
	/// <para>
	/// Apply oversampling and rescoring to quantized vectors *
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RescoreVector? RescoreVector { get; set; }

	/// <summary>
	/// <para>
	/// The minimum similarity for a vector to be considered a match
	/// </para>
	/// </summary>
	public float? Similarity { get; set; }
}

public readonly partial struct KnnQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.KnnQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnQueryDescriptor(Elastic.Clients.Elasticsearch.KnnQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.KnnQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.KnnQuery instance) => new Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.KnnQuery(Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the vector field to search against
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the vector field to search against
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> Filter(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> Filter()
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> Filter(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>>? action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> Filter(params Elastic.Clients.Elasticsearch.QueryDsl.Query[] values)
	{
		Instance.Filter = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> Filter(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.Query>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action));
		}

		Instance.Filter = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The final number of nearest neighbors to return as top hits
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> K(int? value)
	{
		Instance.K = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of nearest neighbor candidates to consider per shard
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> NumCandidates(int? value)
	{
		Instance.NumCandidates = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query vector
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> QueryVector(System.Collections.Generic.ICollection<float>? value)
	{
		Instance.QueryVector = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query vector
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> QueryVector()
	{
		Instance.QueryVector = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFloat.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The query vector
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> QueryVector(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFloat>? action)
	{
		Instance.QueryVector = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFloat.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The query vector
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> QueryVector(params float[] values)
	{
		Instance.QueryVector = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// The query vector builder. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> QueryVectorBuilder(Elastic.Clients.Elasticsearch.QueryVectorBuilder? value)
	{
		Instance.QueryVectorBuilder = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query vector builder. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> QueryVectorBuilder(System.Action<Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor> action)
	{
		Instance.QueryVectorBuilder = Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Apply oversampling and rescoring to quantized vectors *
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> RescoreVector(Elastic.Clients.Elasticsearch.RescoreVector? value)
	{
		Instance.RescoreVector = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Apply oversampling and rescoring to quantized vectors *
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> RescoreVector(System.Action<Elastic.Clients.Elasticsearch.RescoreVectorDescriptor> action)
	{
		Instance.RescoreVector = Elastic.Clients.Elasticsearch.RescoreVectorDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum similarity for a vector to be considered a match
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument> Similarity(float? value)
	{
		Instance.Similarity = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.KnnQuery Build(System.Action<Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.KnnQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.KnnQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct KnnQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.KnnQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnQueryDescriptor(Elastic.Clients.Elasticsearch.KnnQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.KnnQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.KnnQueryDescriptor(Elastic.Clients.Elasticsearch.KnnQuery instance) => new Elastic.Clients.Elasticsearch.KnnQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.KnnQuery(Elastic.Clients.Elasticsearch.KnnQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the vector field to search against
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the vector field to search against
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor Filter(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor Filter()
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor Filter(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery>? action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor Filter<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<T>>? action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor Filter(params Elastic.Clients.Elasticsearch.QueryDsl.Query[] values)
	{
		Instance.Filter = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor Filter(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.Query>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action));
		}

		Instance.Filter = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor Filter<T>(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.Query>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action));
		}

		Instance.Filter = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The final number of nearest neighbors to return as top hits
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor K(int? value)
	{
		Instance.K = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of nearest neighbor candidates to consider per shard
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor NumCandidates(int? value)
	{
		Instance.NumCandidates = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query vector
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor QueryVector(System.Collections.Generic.ICollection<float>? value)
	{
		Instance.QueryVector = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query vector
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor QueryVector()
	{
		Instance.QueryVector = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFloat.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The query vector
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor QueryVector(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFloat>? action)
	{
		Instance.QueryVector = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFloat.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The query vector
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor QueryVector(params float[] values)
	{
		Instance.QueryVector = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// The query vector builder. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor QueryVectorBuilder(Elastic.Clients.Elasticsearch.QueryVectorBuilder? value)
	{
		Instance.QueryVectorBuilder = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query vector builder. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor QueryVectorBuilder(System.Action<Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor> action)
	{
		Instance.QueryVectorBuilder = Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Apply oversampling and rescoring to quantized vectors *
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor RescoreVector(Elastic.Clients.Elasticsearch.RescoreVector? value)
	{
		Instance.RescoreVector = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Apply oversampling and rescoring to quantized vectors *
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor RescoreVector(System.Action<Elastic.Clients.Elasticsearch.RescoreVectorDescriptor> action)
	{
		Instance.RescoreVector = Elastic.Clients.Elasticsearch.RescoreVectorDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum similarity for a vector to be considered a match
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnQueryDescriptor Similarity(float? value)
	{
		Instance.Similarity = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.KnnQuery Build(System.Action<Elastic.Clients.Elasticsearch.KnnQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.KnnQueryDescriptor(new Elastic.Clients.Elasticsearch.KnnQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
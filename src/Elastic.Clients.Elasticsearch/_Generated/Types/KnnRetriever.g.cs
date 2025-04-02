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

internal sealed partial class KnnRetrieverConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.KnnRetriever>
{
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropFilter = System.Text.Json.JsonEncodedText.Encode("filter");
	private static readonly System.Text.Json.JsonEncodedText PropK = System.Text.Json.JsonEncodedText.Encode("k");
	private static readonly System.Text.Json.JsonEncodedText PropMinScore = System.Text.Json.JsonEncodedText.Encode("min_score");
	private static readonly System.Text.Json.JsonEncodedText PropNumCandidates = System.Text.Json.JsonEncodedText.Encode("num_candidates");
	private static readonly System.Text.Json.JsonEncodedText PropQueryVector = System.Text.Json.JsonEncodedText.Encode("query_vector");
	private static readonly System.Text.Json.JsonEncodedText PropQueryVectorBuilder = System.Text.Json.JsonEncodedText.Encode("query_vector_builder");
	private static readonly System.Text.Json.JsonEncodedText PropRescoreVector = System.Text.Json.JsonEncodedText.Encode("rescore_vector");
	private static readonly System.Text.Json.JsonEncodedText PropSimilarity = System.Text.Json.JsonEncodedText.Encode("similarity");

	public override Elastic.Clients.Elasticsearch.KnnRetriever Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propField = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>?> propFilter = default;
		LocalJsonValue<int> propK = default;
		LocalJsonValue<float?> propMinScore = default;
		LocalJsonValue<int> propNumCandidates = default;
		LocalJsonValue<System.Collections.Generic.ICollection<float>?> propQueryVector = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryVectorBuilder?> propQueryVectorBuilder = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.RescoreVector?> propRescoreVector = default;
		LocalJsonValue<float?> propSimilarity = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
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

			if (propMinScore.TryReadProperty(ref reader, options, PropMinScore, null))
			{
				continue;
			}

			if (propNumCandidates.TryReadProperty(ref reader, options, PropNumCandidates, null))
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
		return new Elastic.Clients.Elasticsearch.KnnRetriever(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Field = propField.Value,
			Filter = propFilter.Value,
			K = propK.Value,
			MinScore = propMinScore.Value,
			NumCandidates = propNumCandidates.Value,
			QueryVector = propQueryVector.Value,
			QueryVectorBuilder = propQueryVectorBuilder.Value,
			RescoreVector = propRescoreVector.Value,
			Similarity = propSimilarity.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.KnnRetriever value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropFilter, value.Filter, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.QueryDsl.Query>(o, v, null));
		writer.WriteProperty(options, PropK, value.K, null, null);
		writer.WriteProperty(options, PropMinScore, value.MinScore, null, null);
		writer.WriteProperty(options, PropNumCandidates, value.NumCandidates, null, null);
		writer.WriteProperty(options, PropQueryVector, value.QueryVector, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<float>? v) => w.WriteCollectionValue<float>(o, v, null));
		writer.WriteProperty(options, PropQueryVectorBuilder, value.QueryVectorBuilder, null, null);
		writer.WriteProperty(options, PropRescoreVector, value.RescoreVector, null, null);
		writer.WriteProperty(options, PropSimilarity, value.Similarity, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.KnnRetrieverConverter))]
public sealed partial class KnnRetriever
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnRetriever(string field, int k, int numCandidates)
	{
		Field = field;
		K = k;
		NumCandidates = numCandidates;
	}
#if NET7_0_OR_GREATER
	public KnnRetriever()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public KnnRetriever()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal KnnRetriever(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The name of the vector field to search against.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Field { get; set; }

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? Filter { get; set; }

	/// <summary>
	/// <para>
	/// Number of nearest neighbors to return as top hits.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int K { get; set; }

	/// <summary>
	/// <para>
	/// Minimum _score for matching documents. Documents with a lower _score are not included in the top documents.
	/// </para>
	/// </summary>
	public float? MinScore { get; set; }

	/// <summary>
	/// <para>
	/// Number of nearest neighbor candidates to consider per shard.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int NumCandidates { get; set; }

	/// <summary>
	/// <para>
	/// Query vector. Must have the same number of dimensions as the vector field you are searching against. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<float>? QueryVector { get; set; }

	/// <summary>
	/// <para>
	/// Defines a model to build a query vector.
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
	/// The minimum similarity required for a document to be considered a match.
	/// </para>
	/// </summary>
	public float? Similarity { get; set; }
}

public readonly partial struct KnnRetrieverDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.KnnRetriever Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnRetrieverDescriptor(Elastic.Clients.Elasticsearch.KnnRetriever instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnRetrieverDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.KnnRetriever(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument>(Elastic.Clients.Elasticsearch.KnnRetriever instance) => new Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.KnnRetriever(Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the vector field to search against.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> Field(string value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> Filter(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> Filter()
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> Filter(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>>? action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> Filter(params Elastic.Clients.Elasticsearch.QueryDsl.Query[] values)
	{
		Instance.Filter = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> Filter(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>>[] actions)
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
	/// Number of nearest neighbors to return as top hits.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> K(int value)
	{
		Instance.K = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Minimum _score for matching documents. Documents with a lower _score are not included in the top documents.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> MinScore(float? value)
	{
		Instance.MinScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Number of nearest neighbor candidates to consider per shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> NumCandidates(int value)
	{
		Instance.NumCandidates = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query vector. Must have the same number of dimensions as the vector field you are searching against. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> QueryVector(System.Collections.Generic.ICollection<float>? value)
	{
		Instance.QueryVector = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query vector. Must have the same number of dimensions as the vector field you are searching against. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> QueryVector()
	{
		Instance.QueryVector = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFloat.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query vector. Must have the same number of dimensions as the vector field you are searching against. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> QueryVector(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFloat>? action)
	{
		Instance.QueryVector = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFloat.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query vector. Must have the same number of dimensions as the vector field you are searching against. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> QueryVector(params float[] values)
	{
		Instance.QueryVector = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines a model to build a query vector.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> QueryVectorBuilder(Elastic.Clients.Elasticsearch.QueryVectorBuilder? value)
	{
		Instance.QueryVectorBuilder = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines a model to build a query vector.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> QueryVectorBuilder(System.Action<Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor> action)
	{
		Instance.QueryVectorBuilder = Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Apply oversampling and rescoring to quantized vectors *
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> RescoreVector(Elastic.Clients.Elasticsearch.RescoreVector? value)
	{
		Instance.RescoreVector = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Apply oversampling and rescoring to quantized vectors *
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> RescoreVector(System.Action<Elastic.Clients.Elasticsearch.RescoreVectorDescriptor> action)
	{
		Instance.RescoreVector = Elastic.Clients.Elasticsearch.RescoreVectorDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum similarity required for a document to be considered a match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument> Similarity(float? value)
	{
		Instance.Similarity = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.KnnRetriever Build(System.Action<Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.KnnRetriever(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct KnnRetrieverDescriptor
{
	internal Elastic.Clients.Elasticsearch.KnnRetriever Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnRetrieverDescriptor(Elastic.Clients.Elasticsearch.KnnRetriever instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnRetrieverDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.KnnRetriever(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor(Elastic.Clients.Elasticsearch.KnnRetriever instance) => new Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.KnnRetriever(Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the vector field to search against.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor Field(string value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor Filter(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor Filter()
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor Filter(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery>? action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor Filter<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<T>>? action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor Filter(params Elastic.Clients.Elasticsearch.QueryDsl.Query[] values)
	{
		Instance.Filter = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor Filter(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor>[] actions)
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
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor Filter<T>(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>>[] actions)
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
	/// Number of nearest neighbors to return as top hits.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor K(int value)
	{
		Instance.K = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Minimum _score for matching documents. Documents with a lower _score are not included in the top documents.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor MinScore(float? value)
	{
		Instance.MinScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Number of nearest neighbor candidates to consider per shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor NumCandidates(int value)
	{
		Instance.NumCandidates = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query vector. Must have the same number of dimensions as the vector field you are searching against. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor QueryVector(System.Collections.Generic.ICollection<float>? value)
	{
		Instance.QueryVector = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query vector. Must have the same number of dimensions as the vector field you are searching against. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor QueryVector()
	{
		Instance.QueryVector = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFloat.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query vector. Must have the same number of dimensions as the vector field you are searching against. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor QueryVector(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFloat>? action)
	{
		Instance.QueryVector = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFloat.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query vector. Must have the same number of dimensions as the vector field you are searching against. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor QueryVector(params float[] values)
	{
		Instance.QueryVector = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines a model to build a query vector.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor QueryVectorBuilder(Elastic.Clients.Elasticsearch.QueryVectorBuilder? value)
	{
		Instance.QueryVectorBuilder = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines a model to build a query vector.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor QueryVectorBuilder(System.Action<Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor> action)
	{
		Instance.QueryVectorBuilder = Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Apply oversampling and rescoring to quantized vectors *
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor RescoreVector(Elastic.Clients.Elasticsearch.RescoreVector? value)
	{
		Instance.RescoreVector = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Apply oversampling and rescoring to quantized vectors *
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor RescoreVector(System.Action<Elastic.Clients.Elasticsearch.RescoreVectorDescriptor> action)
	{
		Instance.RescoreVector = Elastic.Clients.Elasticsearch.RescoreVectorDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum similarity required for a document to be considered a match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor Similarity(float? value)
	{
		Instance.Similarity = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.KnnRetriever Build(System.Action<Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor(new Elastic.Clients.Elasticsearch.KnnRetriever(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
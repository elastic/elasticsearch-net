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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class KnnQueryConverter : System.Text.Json.Serialization.JsonConverter<KnnQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropFilter = System.Text.Json.JsonEncodedText.Encode("filter");
	private static readonly System.Text.Json.JsonEncodedText Propk = System.Text.Json.JsonEncodedText.Encode("k");
	private static readonly System.Text.Json.JsonEncodedText PropNumCandidates = System.Text.Json.JsonEncodedText.Encode("num_candidates");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropQueryVector = System.Text.Json.JsonEncodedText.Encode("query_vector");
	private static readonly System.Text.Json.JsonEncodedText PropQueryVectorBuilder = System.Text.Json.JsonEncodedText.Encode("query_vector_builder");
	private static readonly System.Text.Json.JsonEncodedText PropSimilarity = System.Text.Json.JsonEncodedText.Encode("similarity");

	public override KnnQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>?> propFilter = default;
		LocalJsonValue<int?> propk = default;
		LocalJsonValue<int?> propNumCandidates = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<ICollection<float>?> propQueryVector = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryVectorBuilder?> propQueryVectorBuilder = default;
		LocalJsonValue<float?> propSimilarity = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryRead(ref reader, options, PropBoost))
			{
				continue;
			}

			if (propField.TryRead(ref reader, options, PropField))
			{
				continue;
			}

			if (propFilter.TryRead(ref reader, options, PropFilter, typeof(SingleOrManyMarker<ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>?, Elastic.Clients.Elasticsearch.QueryDsl.Query>)))
			{
				continue;
			}

			if (propk.TryRead(ref reader, options, Propk))
			{
				continue;
			}

			if (propNumCandidates.TryRead(ref reader, options, PropNumCandidates))
			{
				continue;
			}

			if (propQueryName.TryRead(ref reader, options, PropQueryName))
			{
				continue;
			}

			if (propQueryVector.TryRead(ref reader, options, PropQueryVector))
			{
				continue;
			}

			if (propQueryVectorBuilder.TryRead(ref reader, options, PropQueryVectorBuilder))
			{
				continue;
			}

			if (propSimilarity.TryRead(ref reader, options, PropSimilarity))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new KnnQuery
		{
			Boost = propBoost.Value
,
			Field = propField.Value
,
			Filter = propFilter.Value
,
			k = propk.Value
,
			NumCandidates = propNumCandidates.Value
,
			QueryName = propQueryName.Value
,
			QueryVector = propQueryVector.Value
,
			QueryVectorBuilder = propQueryVectorBuilder.Value
,
			Similarity = propSimilarity.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, KnnQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost);
		writer.WriteProperty(options, PropField, value.Field);
		writer.WriteProperty(options, PropFilter, value.Filter, null, typeof(SingleOrManyMarker<ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>?, Elastic.Clients.Elasticsearch.QueryDsl.Query>));
		writer.WriteProperty(options, Propk, value.k);
		writer.WriteProperty(options, PropNumCandidates, value.NumCandidates);
		writer.WriteProperty(options, PropQueryName, value.QueryName);
		writer.WriteProperty(options, PropQueryVector, value.QueryVector);
		writer.WriteProperty(options, PropQueryVectorBuilder, value.QueryVectorBuilder);
		writer.WriteProperty(options, PropSimilarity, value.Similarity);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(KnnQueryConverter))]
public sealed partial class KnnQuery
{
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
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? Filter { get; set; }

	/// <summary>
	/// <para>
	/// The final number of nearest neighbors to return as top hits
	/// </para>
	/// </summary>
	public int? k { get; set; }

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
	public ICollection<float>? QueryVector { get; set; }

	/// <summary>
	/// <para>
	/// The query vector builder. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryVectorBuilder? QueryVectorBuilder { get; set; }

	/// <summary>
	/// <para>
	/// The minimum similarity for a vector to be considered a match
	/// </para>
	/// </summary>
	public float? Similarity { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Query(KnnQuery knnQuery) => Elastic.Clients.Elasticsearch.QueryDsl.Query.Knn(knnQuery);
}

public sealed partial class KnnQueryDescriptor<TDocument> : SerializableDescriptor<KnnQueryDescriptor<TDocument>>
{
	internal KnnQueryDescriptor(Action<KnnQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public KnnQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? FilterValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument> FilterDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> FilterDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>>[] FilterDescriptorActions { get; set; }
	private int? kValue { get; set; }
	private int? NumCandidatesValue { get; set; }
	private string? QueryNameValue { get; set; }
	private ICollection<float>? QueryVectorValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryVectorBuilder? QueryVectorBuilderValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor QueryVectorBuilderDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor> QueryVectorBuilderDescriptorAction { get; set; }
	private float? SimilarityValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public KnnQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the vector field to search against
	/// </para>
	/// </summary>
	public KnnQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the vector field to search against
	/// </para>
	/// </summary>
	public KnnQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the vector field to search against
	/// </para>
	/// </summary>
	public KnnQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public KnnQueryDescriptor<TDocument> Filter(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterDescriptorActions = null;
		FilterValue = filter;
		return Self;
	}

	public KnnQueryDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptorActions = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public KnnQueryDescriptor<TDocument> Filter(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorActions = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	public KnnQueryDescriptor<TDocument> Filter(params Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>>[] configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The final number of nearest neighbors to return as top hits
	/// </para>
	/// </summary>
	public KnnQueryDescriptor<TDocument> k(int? k)
	{
		kValue = k;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The number of nearest neighbor candidates to consider per shard
	/// </para>
	/// </summary>
	public KnnQueryDescriptor<TDocument> NumCandidates(int? numCandidates)
	{
		NumCandidatesValue = numCandidates;
		return Self;
	}

	public KnnQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The query vector
	/// </para>
	/// </summary>
	public KnnQueryDescriptor<TDocument> QueryVector(ICollection<float>? queryVector)
	{
		QueryVectorValue = queryVector;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The query vector builder. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public KnnQueryDescriptor<TDocument> QueryVectorBuilder(Elastic.Clients.Elasticsearch.QueryVectorBuilder? queryVectorBuilder)
	{
		QueryVectorBuilderDescriptor = null;
		QueryVectorBuilderDescriptorAction = null;
		QueryVectorBuilderValue = queryVectorBuilder;
		return Self;
	}

	public KnnQueryDescriptor<TDocument> QueryVectorBuilder(Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor descriptor)
	{
		QueryVectorBuilderValue = null;
		QueryVectorBuilderDescriptorAction = null;
		QueryVectorBuilderDescriptor = descriptor;
		return Self;
	}

	public KnnQueryDescriptor<TDocument> QueryVectorBuilder(Action<Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor> configure)
	{
		QueryVectorBuilderValue = null;
		QueryVectorBuilderDescriptor = null;
		QueryVectorBuilderDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The minimum similarity for a vector to be considered a match
	/// </para>
	/// </summary>
	public KnnQueryDescriptor<TDocument> Similarity(float? similarity)
	{
		SimilarityValue = similarity;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (FilterDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterDescriptor, options);
		}
		else if (FilterDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>(FilterDescriptorAction), options);
		}
		else if (FilterDescriptorActions is not null)
		{
			writer.WritePropertyName("filter");
			if (FilterDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in FilterDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>(action), options);
			}

			if (FilterDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else if (FilterValue is not null)
		{
			writer.WritePropertyName("filter");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.Query>(FilterValue, writer, options);
		}

		if (kValue.HasValue)
		{
			writer.WritePropertyName("k");
			writer.WriteNumberValue(kValue.Value);
		}

		if (NumCandidatesValue.HasValue)
		{
			writer.WritePropertyName("num_candidates");
			writer.WriteNumberValue(NumCandidatesValue.Value);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (QueryVectorValue is not null)
		{
			writer.WritePropertyName("query_vector");
			JsonSerializer.Serialize(writer, QueryVectorValue, options);
		}

		if (QueryVectorBuilderDescriptor is not null)
		{
			writer.WritePropertyName("query_vector_builder");
			JsonSerializer.Serialize(writer, QueryVectorBuilderDescriptor, options);
		}
		else if (QueryVectorBuilderDescriptorAction is not null)
		{
			writer.WritePropertyName("query_vector_builder");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor(QueryVectorBuilderDescriptorAction), options);
		}
		else if (QueryVectorBuilderValue is not null)
		{
			writer.WritePropertyName("query_vector_builder");
			JsonSerializer.Serialize(writer, QueryVectorBuilderValue, options);
		}

		if (SimilarityValue.HasValue)
		{
			writer.WritePropertyName("similarity");
			writer.WriteNumberValue(SimilarityValue.Value);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class KnnQueryDescriptor : SerializableDescriptor<KnnQueryDescriptor>
{
	internal KnnQueryDescriptor(Action<KnnQueryDescriptor> configure) => configure.Invoke(this);

	public KnnQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? FilterValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor FilterDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> FilterDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor>[] FilterDescriptorActions { get; set; }
	private int? kValue { get; set; }
	private int? NumCandidatesValue { get; set; }
	private string? QueryNameValue { get; set; }
	private ICollection<float>? QueryVectorValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryVectorBuilder? QueryVectorBuilderValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor QueryVectorBuilderDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor> QueryVectorBuilderDescriptorAction { get; set; }
	private float? SimilarityValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public KnnQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the vector field to search against
	/// </para>
	/// </summary>
	public KnnQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the vector field to search against
	/// </para>
	/// </summary>
	public KnnQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the vector field to search against
	/// </para>
	/// </summary>
	public KnnQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Filters for the kNN search query
	/// </para>
	/// </summary>
	public KnnQueryDescriptor Filter(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterDescriptorActions = null;
		FilterValue = filter;
		return Self;
	}

	public KnnQueryDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptorActions = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public KnnQueryDescriptor Filter(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorActions = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	public KnnQueryDescriptor Filter(params Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor>[] configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The final number of nearest neighbors to return as top hits
	/// </para>
	/// </summary>
	public KnnQueryDescriptor k(int? k)
	{
		kValue = k;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The number of nearest neighbor candidates to consider per shard
	/// </para>
	/// </summary>
	public KnnQueryDescriptor NumCandidates(int? numCandidates)
	{
		NumCandidatesValue = numCandidates;
		return Self;
	}

	public KnnQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The query vector
	/// </para>
	/// </summary>
	public KnnQueryDescriptor QueryVector(ICollection<float>? queryVector)
	{
		QueryVectorValue = queryVector;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The query vector builder. You must provide a query_vector_builder or query_vector, but not both.
	/// </para>
	/// </summary>
	public KnnQueryDescriptor QueryVectorBuilder(Elastic.Clients.Elasticsearch.QueryVectorBuilder? queryVectorBuilder)
	{
		QueryVectorBuilderDescriptor = null;
		QueryVectorBuilderDescriptorAction = null;
		QueryVectorBuilderValue = queryVectorBuilder;
		return Self;
	}

	public KnnQueryDescriptor QueryVectorBuilder(Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor descriptor)
	{
		QueryVectorBuilderValue = null;
		QueryVectorBuilderDescriptorAction = null;
		QueryVectorBuilderDescriptor = descriptor;
		return Self;
	}

	public KnnQueryDescriptor QueryVectorBuilder(Action<Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor> configure)
	{
		QueryVectorBuilderValue = null;
		QueryVectorBuilderDescriptor = null;
		QueryVectorBuilderDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The minimum similarity for a vector to be considered a match
	/// </para>
	/// </summary>
	public KnnQueryDescriptor Similarity(float? similarity)
	{
		SimilarityValue = similarity;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (FilterDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterDescriptor, options);
		}
		else if (FilterDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor(FilterDescriptorAction), options);
		}
		else if (FilterDescriptorActions is not null)
		{
			writer.WritePropertyName("filter");
			if (FilterDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in FilterDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor(action), options);
			}

			if (FilterDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else if (FilterValue is not null)
		{
			writer.WritePropertyName("filter");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.Query>(FilterValue, writer, options);
		}

		if (kValue.HasValue)
		{
			writer.WritePropertyName("k");
			writer.WriteNumberValue(kValue.Value);
		}

		if (NumCandidatesValue.HasValue)
		{
			writer.WritePropertyName("num_candidates");
			writer.WriteNumberValue(NumCandidatesValue.Value);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (QueryVectorValue is not null)
		{
			writer.WritePropertyName("query_vector");
			JsonSerializer.Serialize(writer, QueryVectorValue, options);
		}

		if (QueryVectorBuilderDescriptor is not null)
		{
			writer.WritePropertyName("query_vector_builder");
			JsonSerializer.Serialize(writer, QueryVectorBuilderDescriptor, options);
		}
		else if (QueryVectorBuilderDescriptorAction is not null)
		{
			writer.WritePropertyName("query_vector_builder");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryVectorBuilderDescriptor(QueryVectorBuilderDescriptorAction), options);
		}
		else if (QueryVectorBuilderValue is not null)
		{
			writer.WritePropertyName("query_vector_builder");
			JsonSerializer.Serialize(writer, QueryVectorBuilderValue, options);
		}

		if (SimilarityValue.HasValue)
		{
			writer.WritePropertyName("similarity");
			writer.WriteNumberValue(SimilarityValue.Value);
		}

		writer.WriteEndObject();
	}
}
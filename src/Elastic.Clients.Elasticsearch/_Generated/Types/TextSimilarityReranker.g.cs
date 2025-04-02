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

internal sealed partial class TextSimilarityRerankerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TextSimilarityReranker>
{
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropFilter = System.Text.Json.JsonEncodedText.Encode("filter");
	private static readonly System.Text.Json.JsonEncodedText PropInferenceId = System.Text.Json.JsonEncodedText.Encode("inference_id");
	private static readonly System.Text.Json.JsonEncodedText PropInferenceText = System.Text.Json.JsonEncodedText.Encode("inference_text");
	private static readonly System.Text.Json.JsonEncodedText PropMinScore = System.Text.Json.JsonEncodedText.Encode("min_score");
	private static readonly System.Text.Json.JsonEncodedText PropRankWindowSize = System.Text.Json.JsonEncodedText.Encode("rank_window_size");
	private static readonly System.Text.Json.JsonEncodedText PropRetriever = System.Text.Json.JsonEncodedText.Encode("retriever");

	public override Elastic.Clients.Elasticsearch.TextSimilarityReranker Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propField = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>?> propFilter = default;
		LocalJsonValue<string?> propInferenceId = default;
		LocalJsonValue<string?> propInferenceText = default;
		LocalJsonValue<float?> propMinScore = default;
		LocalJsonValue<int?> propRankWindowSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Retriever> propRetriever = default;
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

			if (propInferenceId.TryReadProperty(ref reader, options, PropInferenceId, null))
			{
				continue;
			}

			if (propInferenceText.TryReadProperty(ref reader, options, PropInferenceText, null))
			{
				continue;
			}

			if (propMinScore.TryReadProperty(ref reader, options, PropMinScore, null))
			{
				continue;
			}

			if (propRankWindowSize.TryReadProperty(ref reader, options, PropRankWindowSize, null))
			{
				continue;
			}

			if (propRetriever.TryReadProperty(ref reader, options, PropRetriever, null))
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
		return new Elastic.Clients.Elasticsearch.TextSimilarityReranker(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Field = propField.Value,
			Filter = propFilter.Value,
			InferenceId = propInferenceId.Value,
			InferenceText = propInferenceText.Value,
			MinScore = propMinScore.Value,
			RankWindowSize = propRankWindowSize.Value,
			Retriever = propRetriever.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TextSimilarityReranker value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropFilter, value.Filter, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.QueryDsl.Query>(o, v, null));
		writer.WriteProperty(options, PropInferenceId, value.InferenceId, null, null);
		writer.WriteProperty(options, PropInferenceText, value.InferenceText, null, null);
		writer.WriteProperty(options, PropMinScore, value.MinScore, null, null);
		writer.WriteProperty(options, PropRankWindowSize, value.RankWindowSize, null, null);
		writer.WriteProperty(options, PropRetriever, value.Retriever, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TextSimilarityRerankerConverter))]
public sealed partial class TextSimilarityReranker
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TextSimilarityReranker(Elastic.Clients.Elasticsearch.Retriever retriever)
	{
		Retriever = retriever;
	}
#if NET7_0_OR_GREATER
	public TextSimilarityReranker()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TextSimilarityReranker()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TextSimilarityReranker(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The document field to be used for text similarity comparisons. This field should contain the text that will be evaluated against the inference_text
	/// </para>
	/// </summary>
	public string? Field { get; set; }

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? Filter { get; set; }

	/// <summary>
	/// <para>
	/// Unique identifier of the inference endpoint created using the inference API.
	/// </para>
	/// </summary>
	public string? InferenceId { get; set; }

	/// <summary>
	/// <para>
	/// The text snippet used as the basis for similarity comparison
	/// </para>
	/// </summary>
	public string? InferenceText { get; set; }

	/// <summary>
	/// <para>
	/// Minimum _score for matching documents. Documents with a lower _score are not included in the top documents.
	/// </para>
	/// </summary>
	public float? MinScore { get; set; }

	/// <summary>
	/// <para>
	/// This value determines how many documents we will consider from the nested retriever.
	/// </para>
	/// </summary>
	public int? RankWindowSize { get; set; }

	/// <summary>
	/// <para>
	/// The nested retriever which will produce the first-level results, that will later be used for reranking.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Retriever Retriever { get; set; }
}

public readonly partial struct TextSimilarityRerankerDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.TextSimilarityReranker Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TextSimilarityRerankerDescriptor(Elastic.Clients.Elasticsearch.TextSimilarityReranker instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TextSimilarityRerankerDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.TextSimilarityReranker(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument>(Elastic.Clients.Elasticsearch.TextSimilarityReranker instance) => new Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TextSimilarityReranker(Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The document field to be used for text similarity comparisons. This field should contain the text that will be evaluated against the inference_text
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> Field(string? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> Filter(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> Filter()
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> Filter(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>>? action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> Filter(params Elastic.Clients.Elasticsearch.QueryDsl.Query[] values)
	{
		Instance.Filter = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> Filter(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>>[] actions)
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
	/// Unique identifier of the inference endpoint created using the inference API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> InferenceId(string? value)
	{
		Instance.InferenceId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The text snippet used as the basis for similarity comparison
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> InferenceText(string? value)
	{
		Instance.InferenceText = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Minimum _score for matching documents. Documents with a lower _score are not included in the top documents.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> MinScore(float? value)
	{
		Instance.MinScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// This value determines how many documents we will consider from the nested retriever.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> RankWindowSize(int? value)
	{
		Instance.RankWindowSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The nested retriever which will produce the first-level results, that will later be used for reranking.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> Retriever(Elastic.Clients.Elasticsearch.Retriever value)
	{
		Instance.Retriever = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The nested retriever which will produce the first-level results, that will later be used for reranking.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument> Retriever(System.Action<Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument>> action)
	{
		Instance.Retriever = Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TextSimilarityReranker Build(System.Action<Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.TextSimilarityReranker(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct TextSimilarityRerankerDescriptor
{
	internal Elastic.Clients.Elasticsearch.TextSimilarityReranker Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TextSimilarityRerankerDescriptor(Elastic.Clients.Elasticsearch.TextSimilarityReranker instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TextSimilarityRerankerDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.TextSimilarityReranker(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor(Elastic.Clients.Elasticsearch.TextSimilarityReranker instance) => new Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TextSimilarityReranker(Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The document field to be used for text similarity comparisons. This field should contain the text that will be evaluated against the inference_text
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor Field(string? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor Filter(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor Filter()
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor Filter(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery>? action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor Filter<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<T>>? action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor Filter(params Elastic.Clients.Elasticsearch.QueryDsl.Query[] values)
	{
		Instance.Filter = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor Filter(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor>[] actions)
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
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor Filter<T>(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>>[] actions)
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
	/// Unique identifier of the inference endpoint created using the inference API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor InferenceId(string? value)
	{
		Instance.InferenceId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The text snippet used as the basis for similarity comparison
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor InferenceText(string? value)
	{
		Instance.InferenceText = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Minimum _score for matching documents. Documents with a lower _score are not included in the top documents.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor MinScore(float? value)
	{
		Instance.MinScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// This value determines how many documents we will consider from the nested retriever.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor RankWindowSize(int? value)
	{
		Instance.RankWindowSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The nested retriever which will produce the first-level results, that will later be used for reranking.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor Retriever(Elastic.Clients.Elasticsearch.Retriever value)
	{
		Instance.Retriever = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The nested retriever which will produce the first-level results, that will later be used for reranking.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor Retriever(System.Action<Elastic.Clients.Elasticsearch.RetrieverDescriptor> action)
	{
		Instance.Retriever = Elastic.Clients.Elasticsearch.RetrieverDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The nested retriever which will produce the first-level results, that will later be used for reranking.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor Retriever<T>(System.Action<Elastic.Clients.Elasticsearch.RetrieverDescriptor<T>> action)
	{
		Instance.Retriever = Elastic.Clients.Elasticsearch.RetrieverDescriptor<T>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TextSimilarityReranker Build(System.Action<Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor(new Elastic.Clients.Elasticsearch.TextSimilarityReranker(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
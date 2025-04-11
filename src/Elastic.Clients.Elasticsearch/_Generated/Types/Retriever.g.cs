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

internal sealed partial class RetrieverConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Retriever>
{
	private static readonly System.Text.Json.JsonEncodedText VariantKnn = System.Text.Json.JsonEncodedText.Encode("knn");
	private static readonly System.Text.Json.JsonEncodedText VariantRrf = System.Text.Json.JsonEncodedText.Encode("rrf");
	private static readonly System.Text.Json.JsonEncodedText VariantRule = System.Text.Json.JsonEncodedText.Encode("rule");
	private static readonly System.Text.Json.JsonEncodedText VariantStandard = System.Text.Json.JsonEncodedText.Encode("standard");
	private static readonly System.Text.Json.JsonEncodedText VariantTextSimilarityReranker = System.Text.Json.JsonEncodedText.Encode("text_similarity_reranker");

	public override Elastic.Clients.Elasticsearch.Retriever Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		string? variantType = null;
		object? variant = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals(VariantKnn))
			{
				variantType = VariantKnn.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.KnnRetriever>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantRrf))
			{
				variantType = VariantRrf.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.RRFRetriever>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantRule))
			{
				variantType = VariantRule.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.RuleRetriever>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantStandard))
			{
				variantType = VariantStandard.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.StandardRetriever>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantTextSimilarityReranker))
			{
				variantType = VariantTextSimilarityReranker.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.TextSimilarityReranker>(options, null);
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
		return new Elastic.Clients.Elasticsearch.Retriever(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			VariantType = variantType,
			Variant = variant
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Retriever value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case null:
				break;
			case "knn":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.KnnRetriever)value.Variant, null, null);
				break;
			case "rrf":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.RRFRetriever)value.Variant, null, null);
				break;
			case "rule":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.RuleRetriever)value.Variant, null, null);
				break;
			case "standard":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.StandardRetriever)value.Variant, null, null);
				break;
			case "text_similarity_reranker":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.TextSimilarityReranker)value.Variant, null, null);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(Elastic.Clients.Elasticsearch.Retriever)}'.");
		}

		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.RetrieverConverter))]
public sealed partial class Retriever
{
	internal string? VariantType { get; set; }
	internal object? Variant { get; set; }
#if NET7_0_OR_GREATER
	public Retriever()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public Retriever()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Retriever(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality  of a knn search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.KnnRetriever? Knn { get => GetVariant<Elastic.Clients.Elasticsearch.KnnRetriever>("knn"); set => SetVariant("knn", value); }

	/// <summary>
	/// <para>
	/// A retriever that produces top documents from reciprocal rank fusion (RRF).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RRFRetriever? Rrf { get => GetVariant<Elastic.Clients.Elasticsearch.RRFRetriever>("rrf"); set => SetVariant("rrf", value); }

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a rule query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RuleRetriever? Rule { get => GetVariant<Elastic.Clients.Elasticsearch.RuleRetriever>("rule"); set => SetVariant("rule", value); }

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a traditional query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.StandardRetriever? Standard { get => GetVariant<Elastic.Clients.Elasticsearch.StandardRetriever>("standard"); set => SetVariant("standard", value); }

	/// <summary>
	/// <para>
	/// A retriever that reranks the top documents based on a reranking model using the InferenceAPI
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextSimilarityReranker? TextSimilarityReranker { get => GetVariant<Elastic.Clients.Elasticsearch.TextSimilarityReranker>("text_similarity_reranker"); set => SetVariant("text_similarity_reranker", value); }

	public static implicit operator Elastic.Clients.Elasticsearch.Retriever(Elastic.Clients.Elasticsearch.KnnRetriever value) => new Elastic.Clients.Elasticsearch.Retriever { Knn = value };
	public static implicit operator Elastic.Clients.Elasticsearch.Retriever(Elastic.Clients.Elasticsearch.RRFRetriever value) => new Elastic.Clients.Elasticsearch.Retriever { Rrf = value };
	public static implicit operator Elastic.Clients.Elasticsearch.Retriever(Elastic.Clients.Elasticsearch.RuleRetriever value) => new Elastic.Clients.Elasticsearch.Retriever { Rule = value };
	public static implicit operator Elastic.Clients.Elasticsearch.Retriever(Elastic.Clients.Elasticsearch.StandardRetriever value) => new Elastic.Clients.Elasticsearch.Retriever { Standard = value };
	public static implicit operator Elastic.Clients.Elasticsearch.Retriever(Elastic.Clients.Elasticsearch.TextSimilarityReranker value) => new Elastic.Clients.Elasticsearch.Retriever { TextSimilarityReranker = value };

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private T? GetVariant<T>(string type)
	{
		if (string.Equals(VariantType, type, System.StringComparison.Ordinal) && Variant is T result)
		{
			return result;
		}

		return default;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private void SetVariant<T>(string type, T? value)
	{
		VariantType = type;
		Variant = value;
	}
}

public readonly partial struct RetrieverDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Retriever Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RetrieverDescriptor(Elastic.Clients.Elasticsearch.Retriever instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RetrieverDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Retriever(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Retriever instance) => new Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Retriever(Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality  of a knn search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> Knn(Elastic.Clients.Elasticsearch.KnnRetriever? value)
	{
		Instance.Knn = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality  of a knn search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> Knn(System.Action<Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument>> action)
	{
		Instance.Knn = Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that produces top documents from reciprocal rank fusion (RRF).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> Rrf(Elastic.Clients.Elasticsearch.RRFRetriever? value)
	{
		Instance.Rrf = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that produces top documents from reciprocal rank fusion (RRF).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> Rrf(System.Action<Elastic.Clients.Elasticsearch.RrfRetrieverDescriptor<TDocument>> action)
	{
		Instance.Rrf = Elastic.Clients.Elasticsearch.RrfRetrieverDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a rule query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> Rule(Elastic.Clients.Elasticsearch.RuleRetriever? value)
	{
		Instance.Rule = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a rule query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> Rule(System.Action<Elastic.Clients.Elasticsearch.RuleRetrieverDescriptor<TDocument>> action)
	{
		Instance.Rule = Elastic.Clients.Elasticsearch.RuleRetrieverDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a traditional query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> Standard(Elastic.Clients.Elasticsearch.StandardRetriever? value)
	{
		Instance.Standard = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a traditional query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> Standard()
	{
		Instance.Standard = Elastic.Clients.Elasticsearch.StandardRetrieverDescriptor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a traditional query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> Standard(System.Action<Elastic.Clients.Elasticsearch.StandardRetrieverDescriptor<TDocument>>? action)
	{
		Instance.Standard = Elastic.Clients.Elasticsearch.StandardRetrieverDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that reranks the top documents based on a reranking model using the InferenceAPI
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> TextSimilarityReranker(Elastic.Clients.Elasticsearch.TextSimilarityReranker? value)
	{
		Instance.TextSimilarityReranker = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that reranks the top documents based on a reranking model using the InferenceAPI
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> TextSimilarityReranker(System.Action<Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument>> action)
	{
		Instance.TextSimilarityReranker = Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<TDocument>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Retriever Build(System.Action<Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Retriever(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct RetrieverDescriptor
{
	internal Elastic.Clients.Elasticsearch.Retriever Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RetrieverDescriptor(Elastic.Clients.Elasticsearch.Retriever instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RetrieverDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Retriever(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.RetrieverDescriptor(Elastic.Clients.Elasticsearch.Retriever instance) => new Elastic.Clients.Elasticsearch.RetrieverDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Retriever(Elastic.Clients.Elasticsearch.RetrieverDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality  of a knn search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Knn(Elastic.Clients.Elasticsearch.KnnRetriever? value)
	{
		Instance.Knn = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality  of a knn search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Knn(System.Action<Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor> action)
	{
		Instance.Knn = Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality  of a knn search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Knn<T>(System.Action<Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<T>> action)
	{
		Instance.Knn = Elastic.Clients.Elasticsearch.KnnRetrieverDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that produces top documents from reciprocal rank fusion (RRF).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Rrf(Elastic.Clients.Elasticsearch.RRFRetriever? value)
	{
		Instance.Rrf = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that produces top documents from reciprocal rank fusion (RRF).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Rrf(System.Action<Elastic.Clients.Elasticsearch.RrfRetrieverDescriptor> action)
	{
		Instance.Rrf = Elastic.Clients.Elasticsearch.RrfRetrieverDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that produces top documents from reciprocal rank fusion (RRF).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Rrf<T>(System.Action<Elastic.Clients.Elasticsearch.RrfRetrieverDescriptor<T>> action)
	{
		Instance.Rrf = Elastic.Clients.Elasticsearch.RrfRetrieverDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a rule query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Rule(Elastic.Clients.Elasticsearch.RuleRetriever? value)
	{
		Instance.Rule = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a rule query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Rule(System.Action<Elastic.Clients.Elasticsearch.RuleRetrieverDescriptor> action)
	{
		Instance.Rule = Elastic.Clients.Elasticsearch.RuleRetrieverDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a rule query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Rule<T>(System.Action<Elastic.Clients.Elasticsearch.RuleRetrieverDescriptor<T>> action)
	{
		Instance.Rule = Elastic.Clients.Elasticsearch.RuleRetrieverDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a traditional query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Standard(Elastic.Clients.Elasticsearch.StandardRetriever? value)
	{
		Instance.Standard = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a traditional query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Standard()
	{
		Instance.Standard = Elastic.Clients.Elasticsearch.StandardRetrieverDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a traditional query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Standard(System.Action<Elastic.Clients.Elasticsearch.StandardRetrieverDescriptor>? action)
	{
		Instance.Standard = Elastic.Clients.Elasticsearch.StandardRetrieverDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that replaces the functionality of a traditional query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor Standard<T>(System.Action<Elastic.Clients.Elasticsearch.StandardRetrieverDescriptor<T>>? action)
	{
		Instance.Standard = Elastic.Clients.Elasticsearch.StandardRetrieverDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that reranks the top documents based on a reranking model using the InferenceAPI
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor TextSimilarityReranker(Elastic.Clients.Elasticsearch.TextSimilarityReranker? value)
	{
		Instance.TextSimilarityReranker = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that reranks the top documents based on a reranking model using the InferenceAPI
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor TextSimilarityReranker(System.Action<Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor> action)
	{
		Instance.TextSimilarityReranker = Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A retriever that reranks the top documents based on a reranking model using the InferenceAPI
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RetrieverDescriptor TextSimilarityReranker<T>(System.Action<Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<T>> action)
	{
		Instance.TextSimilarityReranker = Elastic.Clients.Elasticsearch.TextSimilarityRerankerDescriptor<T>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Retriever Build(System.Action<Elastic.Clients.Elasticsearch.RetrieverDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.RetrieverDescriptor(new Elastic.Clients.Elasticsearch.Retriever(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
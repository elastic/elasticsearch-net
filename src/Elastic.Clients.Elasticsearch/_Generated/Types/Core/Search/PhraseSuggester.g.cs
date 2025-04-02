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

internal sealed partial class PhraseSuggesterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester>
{
	private static readonly System.Text.Json.JsonEncodedText PropAnalyzer = System.Text.Json.JsonEncodedText.Encode("analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropCollate = System.Text.Json.JsonEncodedText.Encode("collate");
	private static readonly System.Text.Json.JsonEncodedText PropConfidence = System.Text.Json.JsonEncodedText.Encode("confidence");
	private static readonly System.Text.Json.JsonEncodedText PropDirectGenerator = System.Text.Json.JsonEncodedText.Encode("direct_generator");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropForceUnigrams = System.Text.Json.JsonEncodedText.Encode("force_unigrams");
	private static readonly System.Text.Json.JsonEncodedText PropGramSize = System.Text.Json.JsonEncodedText.Encode("gram_size");
	private static readonly System.Text.Json.JsonEncodedText PropHighlight = System.Text.Json.JsonEncodedText.Encode("highlight");
	private static readonly System.Text.Json.JsonEncodedText PropMaxErrors = System.Text.Json.JsonEncodedText.Encode("max_errors");
	private static readonly System.Text.Json.JsonEncodedText PropRealWordErrorLikelihood = System.Text.Json.JsonEncodedText.Encode("real_word_error_likelihood");
	private static readonly System.Text.Json.JsonEncodedText PropSeparator = System.Text.Json.JsonEncodedText.Encode("separator");
	private static readonly System.Text.Json.JsonEncodedText PropShardSize = System.Text.Json.JsonEncodedText.Encode("shard_size");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");
	private static readonly System.Text.Json.JsonEncodedText PropSmoothing = System.Text.Json.JsonEncodedText.Encode("smoothing");
	private static readonly System.Text.Json.JsonEncodedText PropText = System.Text.Json.JsonEncodedText.Encode("text");
	private static readonly System.Text.Json.JsonEncodedText PropTokenLimit = System.Text.Json.JsonEncodedText.Encode("token_limit");

	public override Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propAnalyzer = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollate?> propCollate = default;
		LocalJsonValue<double?> propConfidence = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>?> propDirectGenerator = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<bool?> propForceUnigrams = default;
		LocalJsonValue<int?> propGramSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight?> propHighlight = default;
		LocalJsonValue<double?> propMaxErrors = default;
		LocalJsonValue<double?> propRealWordErrorLikelihood = default;
		LocalJsonValue<string?> propSeparator = default;
		LocalJsonValue<int?> propShardSize = default;
		LocalJsonValue<int?> propSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.SmoothingModel?> propSmoothing = default;
		LocalJsonValue<string?> propText = default;
		LocalJsonValue<int?> propTokenLimit = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAnalyzer.TryReadProperty(ref reader, options, PropAnalyzer, null))
			{
				continue;
			}

			if (propCollate.TryReadProperty(ref reader, options, PropCollate, null))
			{
				continue;
			}

			if (propConfidence.TryReadProperty(ref reader, options, PropConfidence, null))
			{
				continue;
			}

			if (propDirectGenerator.TryReadProperty(ref reader, options, PropDirectGenerator, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>(o, null)))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propForceUnigrams.TryReadProperty(ref reader, options, PropForceUnigrams, null))
			{
				continue;
			}

			if (propGramSize.TryReadProperty(ref reader, options, PropGramSize, null))
			{
				continue;
			}

			if (propHighlight.TryReadProperty(ref reader, options, PropHighlight, null))
			{
				continue;
			}

			if (propMaxErrors.TryReadProperty(ref reader, options, PropMaxErrors, null))
			{
				continue;
			}

			if (propRealWordErrorLikelihood.TryReadProperty(ref reader, options, PropRealWordErrorLikelihood, null))
			{
				continue;
			}

			if (propSeparator.TryReadProperty(ref reader, options, PropSeparator, null))
			{
				continue;
			}

			if (propShardSize.TryReadProperty(ref reader, options, PropShardSize, null))
			{
				continue;
			}

			if (propSize.TryReadProperty(ref reader, options, PropSize, null))
			{
				continue;
			}

			if (propSmoothing.TryReadProperty(ref reader, options, PropSmoothing, null))
			{
				continue;
			}

			if (propText.TryReadProperty(ref reader, options, PropText, null))
			{
				continue;
			}

			if (propTokenLimit.TryReadProperty(ref reader, options, PropTokenLimit, null))
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
		return new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Analyzer = propAnalyzer.Value,
			Collate = propCollate.Value,
			Confidence = propConfidence.Value,
			DirectGenerator = propDirectGenerator.Value,
			Field = propField.Value,
			ForceUnigrams = propForceUnigrams.Value,
			GramSize = propGramSize.Value,
			Highlight = propHighlight.Value,
			MaxErrors = propMaxErrors.Value,
			RealWordErrorLikelihood = propRealWordErrorLikelihood.Value,
			Separator = propSeparator.Value,
			ShardSize = propShardSize.Value,
			Size = propSize.Value,
			Smoothing = propSmoothing.Value,
			Text = propText.Value,
			TokenLimit = propTokenLimit.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAnalyzer, value.Analyzer, null, null);
		writer.WriteProperty(options, PropCollate, value.Collate, null, null);
		writer.WriteProperty(options, PropConfidence, value.Confidence, null, null);
		writer.WriteProperty(options, PropDirectGenerator, value.DirectGenerator, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>(o, v, null));
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropForceUnigrams, value.ForceUnigrams, null, null);
		writer.WriteProperty(options, PropGramSize, value.GramSize, null, null);
		writer.WriteProperty(options, PropHighlight, value.Highlight, null, null);
		writer.WriteProperty(options, PropMaxErrors, value.MaxErrors, null, null);
		writer.WriteProperty(options, PropRealWordErrorLikelihood, value.RealWordErrorLikelihood, null, null);
		writer.WriteProperty(options, PropSeparator, value.Separator, null, null);
		writer.WriteProperty(options, PropShardSize, value.ShardSize, null, null);
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteProperty(options, PropSmoothing, value.Smoothing, null, null);
		writer.WriteProperty(options, PropText, value.Text, null, null);
		writer.WriteProperty(options, PropTokenLimit, value.TokenLimit, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterConverter))]
public sealed partial class PhraseSuggester
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PhraseSuggester(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public PhraseSuggester()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public PhraseSuggester()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PhraseSuggester(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The analyzer to analyze the suggest text with.
	/// Defaults to the search analyzer of the suggest field.
	/// </para>
	/// </summary>
	public string? Analyzer { get; set; }

	/// <summary>
	/// <para>
	/// Checks each suggestion against the specified query to prune suggestions for which no matching docs exist in the index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollate? Collate { get; set; }

	/// <summary>
	/// <para>
	/// Defines a factor applied to the input phrases score, which is used as a threshold for other suggest candidates.
	/// Only candidates that score higher than the threshold will be included in the result.
	/// </para>
	/// </summary>
	public double? Confidence { get; set; }

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>? DirectGenerator { get; set; }

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }
	public bool? ForceUnigrams { get; set; }

	/// <summary>
	/// <para>
	/// Sets max size of the n-grams (shingles) in the field.
	/// If the field doesn’t contain n-grams (shingles), this should be omitted or set to <c>1</c>.
	/// If the field uses a shingle filter, the <c>gram_size</c> is set to the <c>max_shingle_size</c> if not explicitly set.
	/// </para>
	/// </summary>
	public int? GramSize { get; set; }

	/// <summary>
	/// <para>
	/// Sets up suggestion highlighting.
	/// If not provided, no highlighted field is returned.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight? Highlight { get; set; }

	/// <summary>
	/// <para>
	/// The maximum percentage of the terms considered to be misspellings in order to form a correction.
	/// This method accepts a float value in the range <c>[0..1)</c> as a fraction of the actual query terms or a number <c>>=1</c> as an absolute number of query terms.
	/// </para>
	/// </summary>
	public double? MaxErrors { get; set; }

	/// <summary>
	/// <para>
	/// The likelihood of a term being misspelled even if the term exists in the dictionary.
	/// </para>
	/// </summary>
	public double? RealWordErrorLikelihood { get; set; }

	/// <summary>
	/// <para>
	/// The separator that is used to separate terms in the bigram field.
	/// If not set, the whitespace character is used as a separator.
	/// </para>
	/// </summary>
	public string? Separator { get; set; }

	/// <summary>
	/// <para>
	/// Sets the maximum number of suggested terms to be retrieved from each individual shard.
	/// </para>
	/// </summary>
	public int? ShardSize { get; set; }

	/// <summary>
	/// <para>
	/// The maximum corrections to be returned per suggest text token.
	/// </para>
	/// </summary>
	public int? Size { get; set; }

	/// <summary>
	/// <para>
	/// The smoothing model used to balance weight between infrequent grams (grams (shingles) are not existing in the index) and frequent grams (appear at least once in the index).
	/// The default model is Stupid Backoff.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SmoothingModel? Smoothing { get; set; }

	/// <summary>
	/// <para>
	/// The text/query to provide suggestions for.
	/// </para>
	/// </summary>
	public string? Text { get; set; }
	public int? TokenLimit { get; set; }
}

public readonly partial struct PhraseSuggesterDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PhraseSuggesterDescriptor(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PhraseSuggesterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester instance) => new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The analyzer to analyze the suggest text with.
	/// Defaults to the search analyzer of the suggest field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Analyzer(string? value)
	{
		Instance.Analyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Checks each suggestion against the specified query to prune suggestions for which no matching docs exist in the index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Collate(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollate? value)
	{
		Instance.Collate = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Checks each suggestion against the specified query to prune suggestions for which no matching docs exist in the index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Collate(System.Action<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateDescriptor> action)
	{
		Instance.Collate = Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines a factor applied to the input phrases score, which is used as a threshold for other suggest candidates.
	/// Only candidates that score higher than the threshold will be included in the result.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Confidence(double? value)
	{
		Instance.Confidence = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> DirectGenerator(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>? value)
	{
		Instance.DirectGenerator = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> DirectGenerator()
	{
		Instance.DirectGenerator = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfDirectGenerator<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> DirectGenerator(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfDirectGenerator<TDocument>>? action)
	{
		Instance.DirectGenerator = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfDirectGenerator<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> DirectGenerator(params Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator[] values)
	{
		Instance.DirectGenerator = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> DirectGenerator(params System.Action<Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument>.Build(action));
		}

		Instance.DirectGenerator = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> ForceUnigrams(bool? value = true)
	{
		Instance.ForceUnigrams = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Sets max size of the n-grams (shingles) in the field.
	/// If the field doesn’t contain n-grams (shingles), this should be omitted or set to <c>1</c>.
	/// If the field uses a shingle filter, the <c>gram_size</c> is set to the <c>max_shingle_size</c> if not explicitly set.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> GramSize(int? value)
	{
		Instance.GramSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Sets up suggestion highlighting.
	/// If not provided, no highlighted field is returned.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Highlight(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight? value)
	{
		Instance.Highlight = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Sets up suggestion highlighting.
	/// If not provided, no highlighted field is returned.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Highlight(System.Action<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlightDescriptor> action)
	{
		Instance.Highlight = Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlightDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum percentage of the terms considered to be misspellings in order to form a correction.
	/// This method accepts a float value in the range <c>[0..1)</c> as a fraction of the actual query terms or a number <c>>=1</c> as an absolute number of query terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> MaxErrors(double? value)
	{
		Instance.MaxErrors = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The likelihood of a term being misspelled even if the term exists in the dictionary.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> RealWordErrorLikelihood(double? value)
	{
		Instance.RealWordErrorLikelihood = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The separator that is used to separate terms in the bigram field.
	/// If not set, the whitespace character is used as a separator.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Separator(string? value)
	{
		Instance.Separator = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Sets the maximum number of suggested terms to be retrieved from each individual shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> ShardSize(int? value)
	{
		Instance.ShardSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum corrections to be returned per suggest text token.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The smoothing model used to balance weight between infrequent grams (grams (shingles) are not existing in the index) and frequent grams (appear at least once in the index).
	/// The default model is Stupid Backoff.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Smoothing(Elastic.Clients.Elasticsearch.Core.Search.SmoothingModel? value)
	{
		Instance.Smoothing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The smoothing model used to balance weight between infrequent grams (grams (shingles) are not existing in the index) and frequent grams (appear at least once in the index).
	/// The default model is Stupid Backoff.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Smoothing(System.Action<Elastic.Clients.Elasticsearch.Core.Search.SmoothingModelDescriptor> action)
	{
		Instance.Smoothing = Elastic.Clients.Elasticsearch.Core.Search.SmoothingModelDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The text/query to provide suggestions for.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> Text(string? value)
	{
		Instance.Text = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument> TokenLimit(int? value)
	{
		Instance.TokenLimit = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct PhraseSuggesterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PhraseSuggesterDescriptor(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PhraseSuggesterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester instance) => new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The analyzer to analyze the suggest text with.
	/// Defaults to the search analyzer of the suggest field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Analyzer(string? value)
	{
		Instance.Analyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Checks each suggestion against the specified query to prune suggestions for which no matching docs exist in the index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Collate(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollate? value)
	{
		Instance.Collate = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Checks each suggestion against the specified query to prune suggestions for which no matching docs exist in the index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Collate(System.Action<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateDescriptor> action)
	{
		Instance.Collate = Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines a factor applied to the input phrases score, which is used as a threshold for other suggest candidates.
	/// Only candidates that score higher than the threshold will be included in the result.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Confidence(double? value)
	{
		Instance.Confidence = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor DirectGenerator(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>? value)
	{
		Instance.DirectGenerator = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor DirectGenerator()
	{
		Instance.DirectGenerator = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfDirectGenerator.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor DirectGenerator(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfDirectGenerator>? action)
	{
		Instance.DirectGenerator = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfDirectGenerator.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor DirectGenerator<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfDirectGenerator<T>>? action)
	{
		Instance.DirectGenerator = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfDirectGenerator<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor DirectGenerator(params Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator[] values)
	{
		Instance.DirectGenerator = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor DirectGenerator(params System.Action<Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor.Build(action));
		}

		Instance.DirectGenerator = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of candidate generators that produce a list of possible terms per term in the given text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor DirectGenerator<T>(params System.Action<Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<T>.Build(action));
		}

		Instance.DirectGenerator = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor ForceUnigrams(bool? value = true)
	{
		Instance.ForceUnigrams = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Sets max size of the n-grams (shingles) in the field.
	/// If the field doesn’t contain n-grams (shingles), this should be omitted or set to <c>1</c>.
	/// If the field uses a shingle filter, the <c>gram_size</c> is set to the <c>max_shingle_size</c> if not explicitly set.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor GramSize(int? value)
	{
		Instance.GramSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Sets up suggestion highlighting.
	/// If not provided, no highlighted field is returned.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Highlight(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight? value)
	{
		Instance.Highlight = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Sets up suggestion highlighting.
	/// If not provided, no highlighted field is returned.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Highlight(System.Action<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlightDescriptor> action)
	{
		Instance.Highlight = Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlightDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum percentage of the terms considered to be misspellings in order to form a correction.
	/// This method accepts a float value in the range <c>[0..1)</c> as a fraction of the actual query terms or a number <c>>=1</c> as an absolute number of query terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor MaxErrors(double? value)
	{
		Instance.MaxErrors = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The likelihood of a term being misspelled even if the term exists in the dictionary.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor RealWordErrorLikelihood(double? value)
	{
		Instance.RealWordErrorLikelihood = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The separator that is used to separate terms in the bigram field.
	/// If not set, the whitespace character is used as a separator.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Separator(string? value)
	{
		Instance.Separator = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Sets the maximum number of suggested terms to be retrieved from each individual shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor ShardSize(int? value)
	{
		Instance.ShardSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum corrections to be returned per suggest text token.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The smoothing model used to balance weight between infrequent grams (grams (shingles) are not existing in the index) and frequent grams (appear at least once in the index).
	/// The default model is Stupid Backoff.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Smoothing(Elastic.Clients.Elasticsearch.Core.Search.SmoothingModel? value)
	{
		Instance.Smoothing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The smoothing model used to balance weight between infrequent grams (grams (shingles) are not existing in the index) and frequent grams (appear at least once in the index).
	/// The default model is Stupid Backoff.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Smoothing(System.Action<Elastic.Clients.Elasticsearch.Core.Search.SmoothingModelDescriptor> action)
	{
		Instance.Smoothing = Elastic.Clients.Elasticsearch.Core.Search.SmoothingModelDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The text/query to provide suggestions for.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor Text(string? value)
	{
		Instance.Text = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor TokenLimit(int? value)
	{
		Instance.TokenLimit = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor(new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
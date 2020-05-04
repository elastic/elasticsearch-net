// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The phrase suggester adds additional logic on top of the <see cref="ITermSuggester">term suggester</see> to select entire corrected phrases instead of
	/// individual tokens weighted based on ngram-language models. In practice this suggester will be able to make better decisions
	/// about which tokens to pick based on co-occurrence and frequencies.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(PhraseSuggester))]
	public interface IPhraseSuggester : ISuggester
	{
		/// <summary>
		/// Checks each suggestion against the specified query to prune suggestions for which no matching docs exist in the index.
		/// The collate query for a suggestion is run only on the local shard from which the suggestion has been generated from.
		/// The query must be specified and can be templated. The current suggestion is automatically made available as
		/// the {{suggestion}} variable, which should be used in your query. You can still specify your own template params ,
		/// the suggestion value will be added to the variables you specify.
		/// Additionally, you can specify a prune to control if all phrase suggestions will be returned; when set to true the
		/// suggestions will have an additional option collate_match, which will be true if matching documents for the phrase was found,
		/// false otherwise. The default value for prune is false.
		/// </summary>
		[DataMember(Name = "collate")]
		IPhraseSuggestCollate Collate { get; set; }

		/// <summary>
		/// The confidence level defines a factor applied to the input phrases score which is used as a threshold for other suggest candidates.
		/// Only candidates that score higher than the threshold will be included in the result.
		/// For instance a confidence level of 1.0 will only return suggestions that score higher than the input phrase.
		/// If set to 0.0 the top N candidates are returned. The default is 1.0.
		/// </summary>
		[DataMember(Name = "confidence")]
		double? Confidence { get; set; }

		/// <summary>
		/// Candidate generators used to produce a list of possible terms per term in the given text.
		/// A single candidate generator is similar to a term suggester called for each individual term in the text.
		/// The output of the generators is subsequently scored in combination with the candidates from the other terms for suggestion candidates.
		/// Each of the generators is called per term in the original text.
		/// </summary>
		[DataMember(Name = "direct_generator")]
		IEnumerable<IDirectGenerator> DirectGenerator { get; set; }

		/// <summary>
		/// </summary>
		[DataMember(Name = "force_unigrams")]
		bool? ForceUnigrams { get; set; }

		/// <summary>
		/// Sets max size of the n-grams (shingles) in the field. If the field doesn’t contain n-grams (shingles), this should be omitted or set to 1.
		/// Note that Elasticsearch tries to detect the gram size based on the specified field. If the field uses a shingle filter,
		/// the gram_size is set to the max_shingle_size if not explicitly set.
		/// </summary>
		[DataMember(Name = "gram_size")]
		int? GramSize { get; set; }

		/// <summary>
		/// Sets up suggestion highlighting. If not provided then no highlighted field is returned.
		/// If provided must contain exactly pre_tag and post_tag, which are wrapped around the changed tokens.
		/// If multiple tokens in a row are changed the entire phrase of changed tokens is wrapped rather than each token.
		/// </summary>
		[DataMember(Name = "highlight")]
		IPhraseSuggestHighlight Highlight { get; set; }

		/// <summary>
		/// The maximum percentage of the terms considered to be misspellings in order to form a correction.
		/// This method accepts a float value in the range [0..1) as a fraction of the actual query terms or a number >=1 as an absolute number
		/// of query terms. The default is set to 1.0, meaning only corrections with at most one misspelled term are returned.
		/// Note that setting this too high can negatively impact performance. Low values like 1 or 2 are recommended; otherwise the time spend
		/// in suggest calls might exceed the time spend in query execution.
		/// </summary>
		[DataMember(Name = "max_errors")]
		double? MaxErrors { get; set; }

		/// <summary>
		/// The likelihood of a term being a misspelled even if the term exists in the dictionary.
		/// The default is 0.95, meaning 5% of the real words are misspelled.
		/// </summary>
		[DataMember(Name = "real_word_error_likelihood")]
		double? RealWordErrorLikelihood { get; set; }

		/// <summary>
		/// The separator that is used to separate terms in the bigram field.
		/// If not set the whitespace character is used as a separator.
		/// </summary>
		[DataMember(Name = "separator")]
		char? Separator { get; set; }

		/// <summary>
		/// Sets the maximum number of suggested terms to be retrieved from each individual shard.
		/// During the reduce phase, only the top N suggestions are returned based on the size option. Defaults to 5.
		/// </summary>
		[DataMember(Name = "shard_size")]
		int? ShardSize { get; set; }

		/// <summary>
		/// Smoothing model to balance weight between infrequent grams (grams (shingles) are not existing in the index)
		/// and frequent grams (appear at least once in the index).
		/// </summary>
		[DataMember(Name = "smoothing")]
		SmoothingModelContainer Smoothing { get; set; }

		/// <summary>
		/// Sets the text / query to provide suggestions for.
		/// </summary>
		[IgnoreDataMember]
		string Text { get; set; }

		/// <summary>
		/// </summary>
		[DataMember(Name = "token_limit")]
		int? TokenLimit { get; set; }
	}

	/// <inheritdoc cref="IPhraseSuggester"/>
	public class PhraseSuggester : SuggesterBase, IPhraseSuggester
	{
		/// <inheritdoc />
		public IPhraseSuggestCollate Collate { get; set; }
		/// <inheritdoc />
		public double? Confidence { get; set; }
		/// <inheritdoc />
		public IEnumerable<IDirectGenerator> DirectGenerator { get; set; }
		/// <inheritdoc />
		public bool? ForceUnigrams { get; set; }
		/// <inheritdoc />
		public int? GramSize { get; set; }
		/// <inheritdoc />
		public IPhraseSuggestHighlight Highlight { get; set; }
		/// <inheritdoc />
		public double? MaxErrors { get; set; }
		/// <inheritdoc />
		public double? RealWordErrorLikelihood { get; set; }
		/// <inheritdoc />
		public char? Separator { get; set; }
		/// <inheritdoc />
		public int? ShardSize { get; set; }
		/// <inheritdoc />
		public SmoothingModelContainer Smoothing { get; set; }
		/// <inheritdoc />
		public string Text { get; set; }
		/// <inheritdoc />
		public int? TokenLimit { get; set; }
	}

	/// <inheritdoc cref="IPhraseSuggester"/>
	public class PhraseSuggesterDescriptor<T> : SuggestDescriptorBase<PhraseSuggesterDescriptor<T>, IPhraseSuggester, T>, IPhraseSuggester
		where T : class
	{
		IPhraseSuggestCollate IPhraseSuggester.Collate { get; set; }
		double? IPhraseSuggester.Confidence { get; set; }
		IEnumerable<IDirectGenerator> IPhraseSuggester.DirectGenerator { get; set; }
		bool? IPhraseSuggester.ForceUnigrams { get; set; }
		int? IPhraseSuggester.GramSize { get; set; }
		IPhraseSuggestHighlight IPhraseSuggester.Highlight { get; set; }
		double? IPhraseSuggester.MaxErrors { get; set; }
		double? IPhraseSuggester.RealWordErrorLikelihood { get; set; }
		char? IPhraseSuggester.Separator { get; set; }
		int? IPhraseSuggester.ShardSize { get; set; }
		SmoothingModelContainer IPhraseSuggester.Smoothing { get; set; }
		string IPhraseSuggester.Text { get; set; }
		int? IPhraseSuggester.TokenLimit { get; set; }

		/// <inheritdoc cref="IPhraseSuggester.Text"/>
		public PhraseSuggesterDescriptor<T> Text(string text) => Assign(text, (a, v) => a.Text = v);
		/// <inheritdoc cref="IPhraseSuggester.ShardSize"/>
		public PhraseSuggesterDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);
		/// <inheritdoc cref="IPhraseSuggester.GramSize"/>
		public PhraseSuggesterDescriptor<T> GramSize(int? gramSize) => Assign(gramSize, (a, v) => a.GramSize = v);
		/// <inheritdoc cref="IPhraseSuggester.Confidence"/>
		public PhraseSuggesterDescriptor<T> Confidence(double? confidence) => Assign(confidence, (a, v) => a.Confidence = v);
		/// <inheritdoc cref="IPhraseSuggester.MaxErrors"/>
		public PhraseSuggesterDescriptor<T> MaxErrors(double? maxErrors) => Assign(maxErrors, (a, v) => a.MaxErrors = v);
		/// <inheritdoc cref="IPhraseSuggester.Separator"/>
		public PhraseSuggesterDescriptor<T> Separator(char? separator) => Assign(separator, (a, v) => a.Separator = v);
		/// <inheritdoc cref="IPhraseSuggester.DirectGenerator"/>
		public PhraseSuggesterDescriptor<T> DirectGenerator(params Func<DirectGeneratorDescriptor<T>, IDirectGenerator>[] generators) =>
			Assign(generators.Select(g => g(new DirectGeneratorDescriptor<T>())).ToList(), (a, v) => a.DirectGenerator = v);
		/// <inheritdoc cref="IPhraseSuggester.RealWordErrorLikelihood"/>
		public PhraseSuggesterDescriptor<T> RealWordErrorLikelihood(double? realWordErrorLikelihood) =>
			Assign(realWordErrorLikelihood, (a, v) => a.RealWordErrorLikelihood = v);
		/// <inheritdoc cref="IPhraseSuggester.Highlight"/>
		public PhraseSuggesterDescriptor<T> Highlight(Func<PhraseSuggestHighlightDescriptor, IPhraseSuggestHighlight> selector) =>
			Assign(selector, (a, v) => a.Highlight = v?.Invoke(new PhraseSuggestHighlightDescriptor()));
		/// <inheritdoc cref="IPhraseSuggester.Collate"/>
		public PhraseSuggesterDescriptor<T> Collate(Func<PhraseSuggestCollateDescriptor<T>, IPhraseSuggestCollate> selector) =>
			Assign(selector, (a, v) => a.Collate = v?.Invoke(new PhraseSuggestCollateDescriptor<T>()));
		/// <inheritdoc cref="IPhraseSuggester.Smoothing"/>
		public PhraseSuggesterDescriptor<T> Smoothing(Func<SmoothingModelContainerDescriptor, SmoothingModelContainer> selector) =>
			Assign(selector, (a, v) => a.Smoothing = v?.Invoke(new SmoothingModelContainerDescriptor()));
		/// <inheritdoc cref="IPhraseSuggester.TokenLimit"/>
		public PhraseSuggesterDescriptor<T> TokenLimit(int? tokenLimit) => Assign(tokenLimit, (a, v) => a.TokenLimit = v);
		/// <inheritdoc cref="IPhraseSuggester.ForceUnigrams"/>
		public PhraseSuggesterDescriptor<T> ForceUnigrams(bool? forceUnigrams = true) => Assign(forceUnigrams, (a, v) => a.ForceUnigrams = v);
	}
}

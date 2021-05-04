// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The term suggester suggests terms based on edit distance. The provided suggest text is analyzed before terms are suggested.
	/// The suggested terms are provided per analyzed suggest text token.
	/// The term suggester doesn’t take the query into account that is part of request.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(TermSuggester))]
	public interface ITermSuggester : ISuggester
	{
		/// <summary>
		/// Lower cases the suggest text terms after text analysis.
		/// </summary>
		[DataMember(Name = "lowercase_terms")]
		bool? LowercaseTerms { get; set; }

		/// <summary>
		/// The maximum edit distance candidate suggestions can have in order to be considered as a suggestion.
		/// Can only be a value between 1 and 2. Any other value result in an bad request error being thrown.
		/// Defaults to 2.
		/// </summary>
		[DataMember(Name = "max_edits")]
		int? MaxEdits { get; set; }

		/// <summary>
		/// A factor that is used to multiply with the shards_size in order to inspect more candidate spell
		/// corrections on the shard level. Can improve accuracy at the cost of performance. Defaults to 5.
		/// </summary>
		[DataMember(Name = "max_inspections")]
		int? MaxInspections { get; set; }

		/// <summary>
		/// The maximum threshold in number of documents a suggest text token can exist in order to be included.
		/// Can be a relative percentage number (e.g 0.4) or an absolute number to represent document frequencies.
		/// If an value higher than 1 is specified then fractional can not be specified. Defaults to 0.01f. This can be used to
		/// exclude high frequency terms from being spellchecked. High frequency terms are usually spelled correctly on top of this
		/// also improves the spellcheck performance. The shard level document frequencies are used for this option.
		/// </summary>
		[DataMember(Name = "max_term_freq")]
		float? MaxTermFrequency { get; set; }

		/// <summary>
		/// The minimal threshold in number of documents a suggestion should appear in. This can be specified as an
		/// absolute number or as a relative percentage of number of documents. This can improve quality by only
		/// suggesting high frequency terms. Defaults to 0f and is not enabled. If a value higher than 1 is specified then the
		/// number cannot be fractional. The shard level document frequencies are used for this option.
		/// </summary>
		[DataMember(Name = "min_doc_freq")]
		float? MinDocFrequency { get; set; }

		/// <summary>
		/// The minimum length a suggest text term must have in order to be included. Defaults to 4.
		/// </summary>
		[DataMember(Name = "min_word_length")]
		int? MinWordLength { get; set; }

		/// <summary>
		/// The number of minimal prefix characters that must match in order be a candidate suggestions. Defaults to 1.
		/// Increasing this number improves spellcheck performance. Usually misspellings don’t occur in the
		/// beginning of terms.
		/// </summary>
		[DataMember(Name = "prefix_length")]
		int? PrefixLength { get; set; }

		/// <summary>
		/// Sets the maximum number of suggestions to be retrieved from each individual shard.
		/// During the reduce phase only the top N suggestions are returned based on the size option.
		/// Defaults to the size option. Setting this to a value higher than the size can be useful in order to
		/// get a more accurate document frequency for spelling corrections at the cost of performance.
		/// Due to the fact that terms are partitioned amongst shards, the shard level document frequencies of spelling corrections
		/// may not be precise. Increasing this will make these document frequencies more precise.
		/// </summary>
		[DataMember(Name = "shard_size")]
		int? ShardSize { get; set; }

		/// <summary>
		/// Defines how suggestions should be sorted per suggest text term
		/// </summary>
		[DataMember(Name = "sort")]
		SuggestSort? Sort { get; set; }

		/// <summary>
		/// Which string distance implementation to use for comparing how similar suggested terms are.
		/// </summary>
		[DataMember(Name = "string_distance")]
		StringDistance? StringDistance { get; set; }

		/// <summary>
		/// Controls what suggestions are included or controls for what suggest text terms, suggestions should be suggested.
		/// </summary>
		[DataMember(Name = "suggest_mode")]
		SuggestMode? SuggestMode { get; set; }

		/// <summary>
		/// The suggest text
		/// </summary>
		[IgnoreDataMember]
		string Text { get; set; }
	}

	/// <inheritdoc cref="ITermSuggester" />
	public class TermSuggester : SuggesterBase, ITermSuggester
	{
		/// <inheritdoc />
		public bool? LowercaseTerms { get; set; }
		/// <inheritdoc />
		public int? MaxEdits { get; set; }
		/// <inheritdoc />
		public int? MaxInspections { get; set; }
		/// <inheritdoc />
		public float? MaxTermFrequency { get; set; }
		/// <inheritdoc />
		public float? MinDocFrequency { get; set; }
		/// <inheritdoc />
		public int? MinWordLength { get; set; }
		/// <inheritdoc />
		public int? PrefixLength { get; set; }
		/// <inheritdoc />
		public int? ShardSize { get; set; }
		/// <inheritdoc />
		public SuggestSort? Sort { get; set; }
		/// <inheritdoc />
		public StringDistance? StringDistance { get; set; }
		/// <inheritdoc />
		public SuggestMode? SuggestMode { get; set; }
		/// <inheritdoc />
		public string Text { get; set; }
	}

	/// <inheritdoc cref="ITermSuggester" />
	public class TermSuggesterDescriptor<T>
		: SuggestDescriptorBase<TermSuggesterDescriptor<T>, ITermSuggester, T>, ITermSuggester
		where T : class
	{
		bool? ITermSuggester.LowercaseTerms { get; set; }
		int? ITermSuggester.MaxEdits { get; set; }
		int? ITermSuggester.MaxInspections { get; set; }
		float? ITermSuggester.MaxTermFrequency { get; set; }
		float? ITermSuggester.MinDocFrequency { get; set; }
		int? ITermSuggester.MinWordLength { get; set; }
		int? ITermSuggester.PrefixLength { get; set; }
		int? ITermSuggester.ShardSize { get; set; }
		SuggestSort? ITermSuggester.Sort { get; set; }
		StringDistance? ITermSuggester.StringDistance { get; set; }
		SuggestMode? ITermSuggester.SuggestMode { get; set; }
		string ITermSuggester.Text { get; set; }

		/// <inheritdoc cref="ITermSuggester.Text" />
		public TermSuggesterDescriptor<T> Text(string text) => Assign(text, (a, v) => a.Text = v);

		/// <inheritdoc cref="ITermSuggester.ShardSize" />
		public TermSuggesterDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);

		/// <inheritdoc cref="ITermSuggester.SuggestMode" />
		public TermSuggesterDescriptor<T> SuggestMode(SuggestMode? mode) => Assign(mode, (a, v) => a.SuggestMode = v);

		/// <inheritdoc cref="ITermSuggester.MinWordLength" />
		public TermSuggesterDescriptor<T> MinWordLength(int? length) => Assign(length, (a, v) => a.MinWordLength = v);

		/// <inheritdoc cref="ITermSuggester.PrefixLength" />
		public TermSuggesterDescriptor<T> PrefixLength(int? length) => Assign(length, (a, v) => a.PrefixLength = v);

		/// <inheritdoc cref="ITermSuggester.MaxEdits" />
		public TermSuggesterDescriptor<T> MaxEdits(int? maxEdits) => Assign(maxEdits, (a, v) => a.MaxEdits = v);

		/// <inheritdoc cref="ITermSuggester.MaxInspections" />
		public TermSuggesterDescriptor<T> MaxInspections(int? maxInspections) => Assign(maxInspections, (a, v) => a.MaxInspections = v);

		/// <inheritdoc cref="ITermSuggester.MinDocFrequency" />
		public TermSuggesterDescriptor<T> MinDocFrequency(float? frequency) => Assign(frequency, (a, v) => a.MinDocFrequency = v);

		/// <inheritdoc cref="ITermSuggester.MaxTermFrequency" />
		public TermSuggesterDescriptor<T> MaxTermFrequency(float? frequency) => Assign(frequency, (a, v) => a.MaxTermFrequency = v);

		/// <inheritdoc cref="ITermSuggester.Sort" />
		public TermSuggesterDescriptor<T> Sort(SuggestSort? sort) => Assign(sort, (a, v) => a.Sort = v);

		/// <inheritdoc cref="ITermSuggester.LowercaseTerms" />
		public TermSuggesterDescriptor<T> LowercaseTerms(bool? lowercaseTerms = true) => Assign(lowercaseTerms, (a, v) => a.LowercaseTerms = v);

		/// <inheritdoc cref="ITermSuggester.StringDistance" />
		public TermSuggesterDescriptor<T> StringDistance(StringDistance? distance) => Assign(distance, (a, v) => a.StringDistance = v);
	}
}

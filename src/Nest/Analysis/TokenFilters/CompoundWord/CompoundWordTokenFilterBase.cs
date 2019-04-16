using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Token filters that allow to decompose compound words.
	/// </summary>
	public interface ICompoundWordTokenFilter : ITokenFilter
	{
		/// <summary>
		/// A path (either relative to config location, or absolute) to a FOP XML hyphenation pattern file.
		/// </summary>
		[DataMember(Name ="hyphenation_patterns_path")]
		string HyphenationPatternsPath { get; set; }

		/// <summary>
		/// Maximum subword size.
		/// </summary>
		[DataMember(Name ="max_subword_size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxSubwordSize { get; set; }

		/// <summary>
		/// Minimum subword size.
		/// </summary>
		[DataMember(Name ="min_subword_size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MinSubwordSize { get; set; }

		/// <summary>
		/// Minimum word size.
		/// </summary>
		[DataMember(Name ="min_word_size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MinWordSize { get; set; }

		/// <summary>
		/// Only matching the longest.
		/// </summary>
		[DataMember(Name ="only_longest_match")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? OnlyLongestMatch { get; set; }

		/// <summary>
		/// A list of words to use.
		/// </summary>
		[DataMember(Name ="word_list")]
		IEnumerable<string> WordList { get; set; }

		/// <summary>
		/// A path (either relative to config location, or absolute) to a list of words.
		/// </summary>
		[DataMember(Name ="word_list_path")]
		string WordListPath { get; set; }
	}

	public abstract class CompoundWordTokenFilterBase : TokenFilterBase, ICompoundWordTokenFilter
	{
		protected CompoundWordTokenFilterBase(string type) : base(type) { }

		public string HyphenationPatternsPath { get; set; }

		/// <inheritdoc />
		public int? MaxSubwordSize { get; set; }

		/// <inheritdoc />
		public int? MinSubwordSize { get; set; }

		/// <inheritdoc />
		public int? MinWordSize { get; set; }

		/// <inheritdoc />
		public bool? OnlyLongestMatch { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> WordList { get; set; }

		/// <inheritdoc />
		public string WordListPath { get; set; }
	}

	/// <inheritdoc />
	public abstract class CompoundWordTokenFilterDescriptorBase<TCompound, TCompoundInterface>
		: TokenFilterDescriptorBase<TCompound, TCompoundInterface>, ICompoundWordTokenFilter
		where TCompound : CompoundWordTokenFilterDescriptorBase<TCompound, TCompoundInterface>, TCompoundInterface
		where TCompoundInterface : class, ICompoundWordTokenFilter
	{
		string ICompoundWordTokenFilter.HyphenationPatternsPath { get; set; }
		int? ICompoundWordTokenFilter.MaxSubwordSize { get; set; }
		int? ICompoundWordTokenFilter.MinSubwordSize { get; set; }
		int? ICompoundWordTokenFilter.MinWordSize { get; set; }
		bool? ICompoundWordTokenFilter.OnlyLongestMatch { get; set; }
		IEnumerable<string> ICompoundWordTokenFilter.WordList { get; set; }
		string ICompoundWordTokenFilter.WordListPath { get; set; }

		/// <inheritdoc />
		public TCompound WordList(IEnumerable<string> wordList) => Assign(a => a.WordList = wordList);

		/// <inheritdoc />
		public TCompound WordList(params string[] wordList) => Assign(a => a.WordList = wordList);

		/// <inheritdoc />
		public TCompound WordListPath(string path) => Assign(a => a.WordListPath = path);

		/// <inheritdoc />
		public TCompound HyphenationPatternsPath(string path) => Assign(a => a.HyphenationPatternsPath = path);

		/// <inheritdoc />
		public TCompound MinWordSize(int? minWordSize) => Assign(a => a.MinWordSize = minWordSize);

		/// <inheritdoc />
		public TCompound MinSubwordSize(int? minSubwordSize) => Assign(a => a.MinSubwordSize = minSubwordSize);

		/// <inheritdoc />
		public TCompound MaxSubwordSize(int? maxSubwordSize) => Assign(a => a.MaxSubwordSize = maxSubwordSize);

		/// <inheritdoc />
		public TCompound OnlyLongestMatch(bool? onlyLongest = true) => Assign(a => a.OnlyLongestMatch = onlyLongest);
	}
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type stop that removes stop words from token streams.
	/// </summary>
	public interface IStopTokenFilter : ITokenFilter
	{
		/// <summary>
		/// A list of stop words to use. Defaults to `_english_` stop words.
		/// </summary>
		[JsonProperty("stopwords")]
		StopWords StopWords { get; set; }

		/// <summary>
		/// Set to true to lower case all words first. Defaults to false.
		/// </summary>
		[JsonProperty("ignore_case")]
		bool? IgnoreCase { get; set; }

		/// <summary>
		/// A path (either relative to config location, or absolute) to a stopwords 
		/// file configuration. Each stop word should be in its own "line" 
		/// (separated by a line break). The file must be UTF-8 encoded.
		/// </summary>
		[JsonProperty("stopwords_path")]
		string StopWordsPath { get; set; }

		/// <summary>
		/// Set to false in order to not ignore the last term of a search if it is a stop word. 
		/// This is very useful for  the completion suggester as a query like green a can
		/// be extended to green apple even though  you remove stop words in general. Defaults to true.
		/// </summary>
		[JsonProperty("remove_trailing")]
		bool? RemoveTrailing { get; set; }
	}

	/// <inheritdoc/>
	public class StopTokenFilter : TokenFilterBase, IStopTokenFilter
	{
		public StopTokenFilter() : base("stop") { }

		/// <inheritdoc/>
		public StopWords StopWords { get; set; }

		/// <inheritdoc/>
		public bool? IgnoreCase { get; set; }

		/// <inheritdoc/>
		public string StopWordsPath { get; set; }

		/// <inheritdoc/>
		public bool? RemoveTrailing { get; set; }
	}
	///<inheritdoc/>
	public class StopTokenFilterDescriptor 
		: TokenFilterDescriptorBase<StopTokenFilterDescriptor, IStopTokenFilter>, IStopTokenFilter
	{
		protected override string Type => "stop";

		bool? IStopTokenFilter.IgnoreCase { get; set; }
		bool? IStopTokenFilter.RemoveTrailing { get; set; }
		StopWords IStopTokenFilter.StopWords { get; set; }
		string IStopTokenFilter.StopWordsPath { get; set; }

		///<inheritdoc/>
		public StopTokenFilterDescriptor IgnoreCase(bool? ignoreCase = true) => Assign(a => a.IgnoreCase = ignoreCase);

		///<inheritdoc/>
		public StopTokenFilterDescriptor RemoveTrailing(bool? removeTrailing = true) => Assign(a => a.RemoveTrailing = removeTrailing);

		///<inheritdoc/>
		public StopTokenFilterDescriptor StopWords(StopWords stopWords) => Assign(a => a.StopWords = stopWords);

		///<inheritdoc/>
		public StopTokenFilterDescriptor StopWords(IEnumerable<string> stopWords) => Assign(a => a.StopWords = stopWords.ToListOrNullIfEmpty());

		///<inheritdoc/>
		public StopTokenFilterDescriptor StopWords(params string[] stopWords) => Assign(a => a.StopWords = stopWords);

		///<inheritdoc/>
		public StopTokenFilterDescriptor StopWordsPath(string path) => Assign(a => a.StopWordsPath = path);

	}

}
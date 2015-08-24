using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An analyzer of type standard that is built of using Standard Tokenizer, with Standard Token Filter, Lower Case Token Filter, and Stop Token Filter.
	/// </summary>
	public interface IStandardAnalyzer : IAnalyzer
	{
		/// <summary>
		/// A list of stopword to initialize the stop filter with. Defaults to the english stop words.
		/// </summary>
		[JsonProperty("stopwords")]
		StopWords StopWords { get; set; }

		/// <summary>
		/// The maximum token length. If a token is seen that exceeds this length then it is discarded. Defaults to 255.
		/// </summary>
		[JsonProperty("max_token_length")]
		int? MaxTokenLength { get; set; }
	}
	
	/// <inheritdoc/>
	public class StandardAnalyzer : AnalyzerBase, IStandardAnalyzer
	{
		public StandardAnalyzer() { Type = "standard"; }

		/// <inheritdoc/>
		public StopWords StopWords { get; set; }

		/// <inheritdoc/>
		public int? MaxTokenLength { get; set; }
	}

	/// <inheritdoc/>
	public class StandardAnalyzerDescriptor :
		AnalyzerDescriptorBase<StandardAnalyzerDescriptor, IStandardAnalyzer>, IStandardAnalyzer
	{
		protected override string Type => "standard";

		StopWords IStandardAnalyzer.StopWords { get; set; }
		int? IStandardAnalyzer.MaxTokenLength { get; set; }

		public StandardAnalyzerDescriptor StopWords(params string[] stopWords) =>
			Assign(a => a.StopWords = stopWords);

		public StandardAnalyzerDescriptor StopWords(IEnumerable<string> stopWords) =>
			Assign(a => a.StopWords = stopWords.ToListOrNullIfEmpty());

		public StandardAnalyzerDescriptor StopWords(StopWords stopWords) => Assign(a => a.StopWords = stopWords);

		public StandardAnalyzerDescriptor MaxTokenLength(int? maxTokenLength) => 
			Assign(a => a.MaxTokenLength = maxTokenLength);
		
	}
}
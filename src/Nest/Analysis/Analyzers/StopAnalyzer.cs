// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// An analyzer of type stop that is built using a Lower Case Tokenizer, with Stop Token Filter.
	/// </summary>
	public interface IStopAnalyzer : IAnalyzer
	{
		/// <summary>
		/// A list of stopword to initialize the stop filter with. Defaults to the english stop words.
		/// </summary>
		[DataMember(Name ="stopwords")]
		StopWords StopWords { get; set; }

		/// <summary>
		/// A path (either relative to config location, or absolute) to a stopwords file configuration.
		/// </summary>
		[DataMember(Name ="stopwords_path")]
		string StopwordsPath { get; set; }
	}

	/// <inheritdoc />
	public class StopAnalyzer : AnalyzerBase, IStopAnalyzer
	{
		public StopAnalyzer() : base("stop") { }

		/// <inheritdoc />
		public StopWords StopWords { get; set; }

		/// <inheritdoc />
		public string StopwordsPath { get; set; }
	}

	/// <inheritdoc />
	public class StopAnalyzerDescriptor : AnalyzerDescriptorBase<StopAnalyzerDescriptor, IStopAnalyzer>, IStopAnalyzer
	{
		protected override string Type => "stop";

		StopWords IStopAnalyzer.StopWords { get; set; }
		string IStopAnalyzer.StopwordsPath { get; set; }

		public StopAnalyzerDescriptor StopWords(params string[] stopWords) => Assign(stopWords, (a, v) => a.StopWords = v);

		public StopAnalyzerDescriptor StopWords(IEnumerable<string> stopWords) =>
			Assign(stopWords.ToListOrNullIfEmpty(), (a, v) => a.StopWords = v);

		public StopAnalyzerDescriptor StopWords(StopWords stopWords) => Assign(stopWords, (a, v) => a.StopWords = v);

		public StopAnalyzerDescriptor StopwordsPath(string path) => Assign(path, (a, v) => a.StopwordsPath = v);
	}
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An analyzer of type snowball that uses the standard tokenizer, with standard filter, lowercase filter, stop filter, and snowball filter.
	/// <para> The Snowball Analyzer is a stemming analyzer from Lucene that is originally based on the snowball project from snowball.tartarus.org.</para>
	/// </summary>
	public interface ISnowballAnalyzer : IAnalyzer
	{
		[JsonProperty("language")]
		SnowballLanguage? Language { get; set; }

		[JsonProperty("stopwords")]
		StopWords StopWords { get; set; }
	}
	/// <inheritdoc/>
	public class SnowballAnalyzer : AnalyzerBase, ISnowballAnalyzer
	{
		public SnowballAnalyzer() { this.Type = "snowball"; }

		public SnowballLanguage? Language { get; set; }

		public StopWords StopWords { get; set; }
	}
	/// <inheritdoc/>
	public class SnowballAnalyzerDescriptor :
		AnalyzerDescriptorBase<SnowballAnalyzerDescriptor, ISnowballAnalyzer>, ISnowballAnalyzer
	{
		protected override string Type => "snowball";

		StopWords ISnowballAnalyzer.StopWords { get; set; }
		SnowballLanguage? ISnowballAnalyzer.Language { get; set; }

		public SnowballAnalyzerDescriptor StopWords(StopWords stopWords) => Assign(a => a.StopWords = stopWords);
		public SnowballAnalyzerDescriptor StopWords(IEnumerable<string> stopWords) => Assign(a => a.StopWords = stopWords.ToListOrNullIfEmpty());
		public SnowballAnalyzerDescriptor StopWords(params string[] stopWords) => Assign(a => a.StopWords = stopWords.ToListOrNullIfEmpty());

		public SnowballAnalyzerDescriptor Language(SnowballLanguage language) => Assign(a => a.Language = language);

	}
}
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

		//TODO validate Stopwords trhoughout, is it list, string or union of both?
		//TODO Rename all StopWords to Stopwords
		[JsonProperty("stopwords")]
		string StopWords { get; set; }
	}
	public class SnowballAnalyzer : AnalyzerBase, ISnowballAnalyzer
	{
		public SnowballAnalyzer()
		{
			this.Type = "snowball";
		}

		public SnowballLanguage? Language { get; set; }

		public string StopWords { get; set; }
	}
	public class SnowballAnalyzerDescriptor :
		AnalyzerDescriptorBase<SnowballAnalyzerDescriptor, ISnowballAnalyzer>, ISnowballAnalyzer
	{
		protected override string Type => "snowball";

		string ISnowballAnalyzer.StopWords { get; set; }
		SnowballLanguage? ISnowballAnalyzer.Language { get; set; }

		public SnowballAnalyzerDescriptor StopWords(string stopWords) =>
			Assign(a => a.StopWords = stopWords);

		public SnowballAnalyzerDescriptor Language(SnowballLanguage language) => Assign(a => a.Language = language);

	}
}
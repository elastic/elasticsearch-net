using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An analyzer of type snowball that uses the standard tokenizer, with standard filter, lowercase filter, stop filter, and snowball filter.
	/// <para> The Snowball Analyzer is a stemming analyzer from Lucene that is originally based on the snowball project from snowball.tartarus.org.</para>
	/// </summary>
	public class SnowballAnalyzer : AnalyzerBase
    {
        public SnowballAnalyzer()
        {
            this.Type = "snowball";
        }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("stopwords")]
        public string StopWords { get; set; }
    }
}
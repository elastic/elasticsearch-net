using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// The synonym token filter allows to easily handle synonyms during the analysis process. 
	/// </summary>
    public class SynonymTokenFilter : TokenFilterBase
    {
        public SynonymTokenFilter() : base("synonym")
        {
        }

        [JsonProperty("synonyms_path")]
        public string SynonymsPath { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("synonyms")]
        public IEnumerable<string> Synonyms { get; set; }

        [JsonProperty("ignore_case")]
        public bool? IgnoreCase { get; set; }

        [JsonProperty("expand")]
        public bool? Expand { get; set; }

        [JsonProperty("tokenizer")]
        public string Tokenizer { get; set; }
    }
}

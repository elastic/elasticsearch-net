using Newtonsoft.Json;

namespace Nest
{
    /// <summary>
    /// The pattern_replace char filter allows the use of a regex to manipulate the characters in a string before analysis. 
    /// </summary>
    public class PatternReplaceCharFilter : CharFilterBase
    {
        public PatternReplaceCharFilter()
            : base("pattern_replace")
        {
            
        }

        [JsonProperty("pattern")]
        public string Pattern { get; set; }

        [JsonProperty("replacement")]
        public string Replacement { get; set; }
    }
}

using Newtonsoft.Json;

namespace Nest
{
    /// <summary>
    /// Basic support for hunspell stemming. 
    ///<para> Hunspell dictionaries will be picked up from a dedicated hunspell directory on the filesystem.</para>
    /// </summary>
    public class HunspellTokenFilter : TokenFilterBase
    {
        public HunspellTokenFilter()
            : base("hunspell")
        {

        }

        /// <summary>
        /// If true, dictionary matching will be case insensitive.
        /// </summary>
        [JsonProperty("ignore_case")]
        public bool? IgnoreCase { get; set; }

        /// <summary>
        /// A locale for this filter. If this is unset, the lang or language are used instead - so one of these has to be set.
        /// </summary>
        [JsonProperty("locale")]
        public string Locale { get; set; }

        /// <summary>
        /// The name of a dictionary.
        /// </summary>
        [JsonProperty("dictionary")]
        public string Dictionary { get; set; }

        /// <summary>
        /// If only unique terms should be returned, this needs to be set to true.
        /// </summary>
        [JsonProperty("dedup")]
        public bool? Dedup { get; set; }

        /// <summary>
        /// If only the longest term should be returned, set this to true.
        /// </summary>
        [JsonProperty("longest_only")]
        public bool? LongestOnly { get; set; }


    }
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    /// <summary>
    /// The pattern_capture token filter, unlike the pattern tokenizer, emits a token for every capture group in the regular expression.
    /// </summary>
    public class PatternCaptureTokenFilter : TokenFilterBase
    {
        public PatternCaptureTokenFilter()
            : base("pattern_capture")
        {
        }

        [JsonProperty("patterns")]
        public IEnumerable<string> Patterns { get; set; }

        /// <summary>
        /// If preserve_original is set to true then it would also emit the original token
        /// </summary>
        [JsonProperty("preserve_original")]
        public bool? PreserveOriginal { get; set; }
    }
}

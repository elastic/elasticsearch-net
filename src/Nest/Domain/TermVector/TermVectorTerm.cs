using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject]
    public class TermVectorTerm
    {
        public TermVectorTerm()
        {
            Tokens = new List<Token>();
        }

        [JsonProperty("doc_freq")]
        public int DocumentFrequency { get; internal set; }

        [JsonProperty("term_freq")]
        public int TermFrequency { get; internal set; }

        [JsonProperty("tokens")]
        public IEnumerable<Token> Tokens { get; internal set; }

        [JsonProperty("ttf")]
        public int TotalTermFrequency { get; internal set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
    public interface ITermVectorResponse : IResponse
    {
        bool Found { get; }
        IDictionary<string, TermVector> TermVectors { get; }
    }

    [JsonObject]
    public class TermVectorResponse : BaseResponse, ITermVectorResponse
    {
        public TermVectorResponse()
        {
            IsValid = true;
            TermVectors = new Dictionary<string, TermVector>();
        }

        [JsonProperty("found")]
        public bool Found { get; internal set; }

        [JsonProperty("term_vectors")]
        public IDictionary<string, TermVector> TermVectors { get; internal set; }
    }
}

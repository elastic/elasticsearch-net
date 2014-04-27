using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject]
    public class TermVector
    {
        [JsonProperty("field_statistics")]
        public FieldStatistics FieldStatistics { get; internal set; }

        [JsonProperty("terms")]
        public IDictionary<string, TermVectorTerm> Terms { get; internal set; }
    }
}

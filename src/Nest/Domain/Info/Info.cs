using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest.Domain.Info
{
    public interface IInfo
    {
        string Algorithm { get; }

    }

    [JsonObject]
    public class Info : IInfo
    {
        [JsonProperty(PropertyName = "algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty(PropertyName = "search-millis")]
        public string ElapsedSearchTime { get; set; }

        [JsonProperty(PropertyName = "clustering-millis")]
        public string ElapsedClusteringTime { get; set; }

        [JsonProperty(PropertyName = "total-millis")]
        public string ElapsedTotalTime { get; set; }

        [JsonProperty(PropertyName = "include-hits")]
        public string IncludeHits { get; set; }

        [JsonProperty(PropertyName = "max-hits")]
        public string MaxHits { get; set; }

    }
}

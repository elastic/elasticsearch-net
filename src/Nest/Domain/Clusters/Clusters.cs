using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest.Domain.Clusters
{
    //[JsonConverter(typeof(ConcreteTypeConverter))]
    public interface IClusters
    {
        string Id { get; }
        string Score { get; }
        string Label { get; }
        string[] Phrases { get; }
        string[] Documents { get; }
    }

    [JsonObject]
    public class Clusters : IClusters
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "score")]
        public string Score { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "phrases")]
        public string[] Phrases { get; set; }

        [JsonProperty(PropertyName = "documents")]
        public string[] Documents { get; set; }

        [JsonProperty(PropertyName = "clusters")]
        public Clusters[] clusters { get; set; }
    }
}

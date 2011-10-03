using System.Collections.Generic;
using ElasticSearch.Client.Mapping;
using Newtonsoft.Json;

namespace ElasticSearch.Client.Settings
{
    public class IndexSettings
    {
        public IndexSettings()
        {
            this.Analysis = new AnalysisSettings();
            this.Mappings = new List<TypeMapping>();
        }

        public int? NumberOfShards { get; set; }
        public int? NumberOfReplicas { get; set; }

        [JsonIgnore]
        public AnalysisSettings Analysis { get; private set; }

        [JsonIgnore]
        public IList<TypeMapping> Mappings { get; private set; }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class HistogramFacet : Facet
    {
        [JsonProperty("entries")]
        public IList<Entry> Entries { get; internal set; }

        #region Nested type: Entry

        public class Entry : FacetItem
        {
            [JsonProperty("key")]
            public float Key { get; set; }
        }

        #endregion
    }
}
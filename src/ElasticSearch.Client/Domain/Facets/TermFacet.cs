using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class TermFacet : Facet
    {
        [JsonProperty("missing")]
        public int Missing { get; internal set; }

        [JsonProperty("other")]
        public int Other { get; internal set; }

        [JsonProperty("total")]
        public int Total { get; internal set; }

        [JsonProperty("terms")]
        public IList<TermItem> Terms { get; internal set; }

        #region Nested type: TermItem

        public class TermItem : FacetItem
        {
            [JsonProperty(PropertyName = "term")]
            public string Term { get; internal set; }
        }

        #endregion
    }
}
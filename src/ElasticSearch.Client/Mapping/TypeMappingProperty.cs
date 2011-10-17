using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client.Mapping
{
    public class TypeMappingProperty
    {
        [JsonProperty("dynamic")]
        public bool Dynamic { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("null_value")]
        public object NullValue { get; set; }

        [JsonProperty("index_name")]
        public string IndexName { get; set; }

        [JsonProperty("store")]
        public string Store { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("precision_step")]
        public int? PrecisionStep { get; set; }

        [JsonProperty("boost")]
        public double? Boost { get; set; }

        [JsonProperty("include_in_all")]
        public bool? IncludeInAll { get; set; }

        [JsonProperty("format")]
        public string DateFormat { get; set; }

        [JsonProperty("term_vector")]
        public string TermVector { get; set; }

        [JsonProperty("omit_norms")]
        public bool? OmitNorms { get; set; }

        [JsonProperty("omit_term_freq_and_positions")]
        public bool? OmitTermFreqAndPositions { get; set; }

        [JsonProperty("analyzer")]
        public string Analyzer { get; set; }

        [JsonProperty("index_analyzer")]
        public string IndexAnalyzer { get; set; }

        [JsonProperty("search_analyzer")]
        public string SearchAnalyzer { get; set; }

        [JsonProperty("properties")]
        public IDictionary<string, TypeMappingProperty> Properties { get; set; }

        [JsonProperty("fields")]
        public IDictionary<string, TypeMappingProperty> Fields { get; set; }
    }
}
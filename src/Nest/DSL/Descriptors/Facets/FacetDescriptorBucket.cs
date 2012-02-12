using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest.DSL
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class FacetDescriptorsBucket<T> where T : class
  {
    [JsonProperty(PropertyName = "terms")]
    public TermFacetDescriptor<T> Terms { get; set; }

    [JsonProperty(PropertyName = "range")]
    public IFacetDescriptor Range { get; set; }

    [JsonProperty(PropertyName = "histogram")]
    public HistogramFacetDescriptor<T> Histogram { get; set; }

    [JsonProperty(PropertyName = "date_histogram")]
    public DateHistogramFacetDescriptor<T> DateHistogram { get; set; }

    [JsonProperty(PropertyName = "query")]
    public QueryDescriptor<T> Query { get; set; }

    [JsonProperty(PropertyName = "statistical")]
    public StatisticalFacetDescriptor<T> Statistical { get; set; }

    [JsonProperty(PropertyName = "terms_stats")]
    public TermsStatsFacetDescriptor<T> TermsStats { get; set; }

    [JsonProperty(PropertyName = "geo_distance")]
    public GeoDistanceFacetDescriptor<T> GeoDistance { get; set; }
  }
}

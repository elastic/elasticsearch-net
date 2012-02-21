using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using System.Globalization;

namespace Nest.DSL
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class GeoDistanceFacetDescriptor<T> : BaseFacetDescriptor<T> where T : class
  {
    [JsonProperty(PropertyName = "pin.location")]
    internal string _PinLocation { get; set; }

    [JsonProperty(PropertyName = "value_field")]
    internal string _ValueField { get; set; }

    [JsonProperty(PropertyName = "value_script")]
    internal string _ValueScript { get; set; }

    [JsonProperty(PropertyName = "ranges")]
    internal IEnumerable<Range<int>> _Ranges { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty(PropertyName = "unit")]
    internal GeoUnit? _GeoUnit { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty(PropertyName = "distance_type")]
    internal GeoDistance? _GeoDistance { get; set; }

    [JsonProperty(PropertyName = "params")]
    internal Dictionary<string, object> _Params { get; set; }

    public GeoDistanceFacetDescriptor<T> OnValueField(string field)
    {
      field.ThrowIfNullOrEmpty("field");
      this._ValueField = field;
      return this;
    }
    public GeoDistanceFacetDescriptor<T> OnValueField(Expression<Func<T, object>> objectPath)
    {
      objectPath.ThrowIfNull("objectPath");
      var fieldName = ElasticClient.PropertyNameResolver.ResolveToSort(objectPath);
      return this.OnValueField(fieldName);
    }
    public GeoDistanceFacetDescriptor<T> OnValueScript(string script)
    {
      script.ThrowIfNullOrEmpty("script");
      this._ValueScript = script;
      return this;
    }
    public GeoDistanceFacetDescriptor<T> PinTo(string geoLocationHash)
    {
      geoLocationHash.ThrowIfNullOrEmpty("geoLocationHash");
      this._PinLocation = geoLocationHash;
      return this;
    }
    public GeoDistanceFacetDescriptor<T> PinTo(double Lat, double Lon)
    {
      var c = CultureInfo.InvariantCulture;
      Lat.ThrowIfNull("Lat");
      Lon.ThrowIfNull("Lon");
      this._PinLocation = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
      return this;
    }
    public GeoDistanceFacetDescriptor<T> Unit(GeoUnit unit)
    {
      unit.ThrowIfNull("unit");
      this._GeoUnit = unit;
      return this;
    }
    public GeoDistanceFacetDescriptor<T> DistanceType(GeoDistance distance)
    {
      distance.ThrowIfNull("unit");
      this._GeoDistance = distance;
      return this;
    }
    public GeoDistanceFacetDescriptor<T> Ranges(params Func<Range<int>, Range<int>>[] ranges)
    {
      var newRanges = new List<Range<int>>();
      foreach (var range in ranges)
      {
        var r = new Range<int>();
        newRanges.Add(range(r));
      }
      this._Ranges = newRanges;
      return this;
    }
    public GeoDistanceFacetDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
    {
      paramDictionary.ThrowIfNull("paramDictionary");
      this._Params = paramDictionary(new FluentDictionary<string, object>());
      return this;
    }

    public new GeoDistanceFacetDescriptor<T> Global()
    {
      this._IsGlobal = true;
      return this;
    }
    public GeoDistanceFacetDescriptor<T> FacetFilter(
      Func<FilterDescriptor<T>, FilterDescriptor<T>> facetFilter
    )
    {
      var filter = facetFilter(new FilterDescriptor<T>());
      this._FacetFilter = filter;
      return this;
    }
    public new GeoDistanceFacetDescriptor<T> Nested(string nested)
    {
      this._Nested = nested;
      return this;
    }
    public new GeoDistanceFacetDescriptor<T> Scope(string scope)
    {
      this._Scope = scope;
      return this;
    }

  }
}

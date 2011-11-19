using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ElasticSearch.Client.Resolvers.Converters
{
    public class FacetConverter : JsonConverter
    {
        #region Overrides of CustomCreationConverter<Facet>

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            JObject o = JObject.Load(reader);

            JToken typeToken;
            if (o.TryGetValue("_type", out typeToken))
            {
                var type = typeToken.Value<string>();

                switch (type)
                {
                    case "terms":
                        return serializer.Deserialize(o.CreateReader(), typeof (TermFacet));

                    case "range":
                        var firstRange = o.Value<JArray>("ranges")[0];
                        if (firstRange.Value<string>("from_str") != null || firstRange.Value<string>("to_str") != null)
                        {
                            return serializer.Deserialize(o.CreateReader(), typeof (DateRangeFacet));
                        }
                        else
                        {
                            return serializer.Deserialize(o.CreateReader(), typeof(RangeFacet));
                        }

                    case "histogram":
                        return serializer.Deserialize(o.CreateReader(), typeof (HistogramFacet));
                            
                    case "date_histogram":
                        return serializer.Deserialize(o.CreateReader(), typeof(DateHistogramFacet));

                    case "statistical":
                        return serializer.Deserialize(o.CreateReader(), typeof(StatisticalFacet));

                    case "terms_stats":
                        return serializer.Deserialize(o.CreateReader(), typeof(TermStatsFacet));
                    case "geo_distance":
                        return serializer.Deserialize(o.CreateReader(), typeof(GeoDistanceFacet));
                }
            }

            throw new NotSupportedException("The JObject could not be interpreted into a supported facet type.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Facet);
        }

        #endregion
    }
}
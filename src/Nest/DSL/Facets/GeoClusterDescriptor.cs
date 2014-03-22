using System;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest.DSL.Facets
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GeoClusterDescriptor<T> : BaseFacetDescriptor<T> where T : class
    {

        public GeoClusterDescriptor()
        {
            this._Factor = 0.5;
        }

        [JsonProperty(PropertyName = "factor")]
        internal double _Factor { get; set; }

        [JsonProperty(PropertyName = "field")]
        internal string _Field { get; set; }

        public GeoClusterDescriptor<T> OnField(string field)
        {
            this._Field = field;
            return this;
        }

        public GeoClusterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
        {
            var fieldName = new PropertyNameResolver().Resolve(objectPath);
            return this.OnField(fieldName);
        }

        public GeoClusterDescriptor<T> Factor(double factor)
        {
            _Factor = factor;
            return this;
        }

    }
}

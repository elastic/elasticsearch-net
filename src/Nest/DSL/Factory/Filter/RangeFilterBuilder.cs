using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Filter
{
    public class RangeFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.RangeFilterBuilder;
        private readonly string _name;
        private bool _cache;
        private string _cacheKey;
        private string _filterName;
        private object _from;
        private bool _includeLower = true;
        private bool _includeUpper = true;
        private object _to;

        /// <summary>
        /// A filter that restricts search results to values that are within the given range.
        /// </summary>
        /// <param name="name"></param>
        public RangeFilterBuilder(string name)
        {
            _name = name;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder From(object from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder From(int from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder From(long from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder From(float from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder From(double from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder Gt(object from)
        {
            _from = from;
            _includeLower = false;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder Gt(int from)
        {
            _from = from;
            _includeLower = false;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder Gt(long from)
        {
            _from = from;
            _includeLower = false;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder Gt(float from)
        {
            _from = from;
            _includeLower = false;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder Gt(double from)
        {
            _from = from;
            _includeLower = false;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder Gte(object from)
        {
            _from = from;
            _includeLower = true;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder Gte(int from)
        {
            _from = from;
            _includeLower = true;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder Gte(long from)
        {
            _from = from;
            _includeLower = true;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder Gte(float from)
        {
            _from = from;
            _includeLower = true;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeFilterBuilder Gte(double from)
        {
            _from = from;
            _includeLower = true;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder To(object to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder To(int to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder To(float to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder To(long to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder To(double to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder Lt(object to)
        {
            _to = to;
            _includeUpper = false;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder Lt(int to)
        {
            _to = to;
            _includeUpper = false;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder Lt(long to)
        {
            _to = to;
            _includeUpper = false;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder Lt(float to)
        {
            _to = to;
            _includeUpper = false;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder Lt(double to)
        {
            _to = to;
            _includeUpper = false;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder Lte(object to)
        {
            _to = to;
            _includeUpper = true;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder Lte(int to)
        {
            _to = to;
            _includeUpper = true;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder Lte(float to)
        {
            _to = to;
            _includeUpper = true;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder Lte(long to)
        {
            _to = to;
            _includeUpper = true;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeFilterBuilder Lte(double to)
        {
            _to = to;
            _includeUpper = true;
            return this;
        }

        /// <summary>
        /// Should the lower bound be included or not. Defaults to true.
        /// </summary>
        /// <param name="includeLower"></param>
        /// <returns></returns>
        public RangeFilterBuilder IncludeLower(bool includeLower)
        {
            _includeLower = includeLower;
            return this;
        }

        /// <summary>
        /// Should the upper bound be included or not. Defaults to true.
        /// </summary>
        /// <param name="includeUpper"></param>
        /// <returns></returns>
        public RangeFilterBuilder IncludeUpper(bool includeUpper)
        {
            _includeUpper = includeUpper;
            return this;
        }

        /// <summary>
        /// Sets the filter name for the filter that can be used when searching for matched_filters per hit.
        /// </summary>
        /// <param name="filterName"></param>
        /// <returns></returns>
        public RangeFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        /// <summary>
        /// Should the filter be cached or not. Defaults to true.
        /// </summary>
        /// <param name="cache"></param>
        /// <returns></returns>
        public RangeFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public RangeFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME][_name] = new JObject();
            content[NAME][_name]["from"] = new JValue(_from);
            content[NAME][_name]["to"] = new JValue(_to);
            content[NAME][_name]["include_lower"] = _includeLower;
            content[NAME][_name]["include_upper"] = _includeUpper;

            if (_filterName != null)
            {
                content[NAME]["_name"] = _filterName;
            }

            if (_cache)
            {
                content[NAME]["_cache"] = _cache;
            }

            if (_cacheKey != null)
            {
                content[NAME]["_cache_key"] = _cacheKey;
            }

            return content;
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }

        #endregion
    }
}
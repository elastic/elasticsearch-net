using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class NumericRangeFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.NumericRangeFilterBuilder;
        private readonly string _name;
        private bool _cache;
        private string _cacheKey;
        private string _filterName;
        private object _from;
        private bool _includeLower = true;
        private bool _includeUpper = true;
        private object _to;

        /// <summary>
        /// A filter that restricts search results to values that are within the given range
        /// </summary>
        /// <param name="name"></param>
        public NumericRangeFilterBuilder(string name)
        {
            _name = name;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public NumericRangeFilterBuilder From(object from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public NumericRangeFilterBuilder From(int from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public NumericRangeFilterBuilder From(long from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public NumericRangeFilterBuilder From(float from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public NumericRangeFilterBuilder From(double from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public NumericRangeFilterBuilder Gt(object from)
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
        public NumericRangeFilterBuilder Gt(int from)
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
        public NumericRangeFilterBuilder Gt(long from)
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
        public NumericRangeFilterBuilder Gt(float from)
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
        public NumericRangeFilterBuilder Gt(double from)
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
        public NumericRangeFilterBuilder Gte(object from)
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
        public NumericRangeFilterBuilder Gte(int from)
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
        public NumericRangeFilterBuilder Gte(long from)
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
        public NumericRangeFilterBuilder Gte(float from)
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
        public NumericRangeFilterBuilder Gte(double from)
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
        public NumericRangeFilterBuilder To(object to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public NumericRangeFilterBuilder To(int to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public NumericRangeFilterBuilder To(float to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public NumericRangeFilterBuilder To(long to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public NumericRangeFilterBuilder To(double to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the filter query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public NumericRangeFilterBuilder Lt(object to)
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
        public NumericRangeFilterBuilder Lt(int to)
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
        public NumericRangeFilterBuilder Lt(long to)
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
        public NumericRangeFilterBuilder Lt(float to)
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
        public NumericRangeFilterBuilder Lt(double to)
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
        public NumericRangeFilterBuilder Lte(object to)
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
        public NumericRangeFilterBuilder Lte(int to)
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
        public NumericRangeFilterBuilder Lte(float to)
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
        public NumericRangeFilterBuilder Lte(long to)
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
        public NumericRangeFilterBuilder Lte(double to)
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
        public NumericRangeFilterBuilder IncludeLower(bool includeLower)
        {
            _includeLower = includeLower;
            return this;
        }

        /// <summary>
        /// Should the upper bound be included or not. Defaults to true.
        /// </summary>
        /// <param name="includeUpper"></param>
        /// <returns></returns>
        public NumericRangeFilterBuilder IncludeUpper(bool includeUpper)
        {
            _includeUpper = includeUpper;
            return this;
        }

        public NumericRangeFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public NumericRangeFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        public NumericRangeFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
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
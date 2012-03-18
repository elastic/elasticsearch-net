using System;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    /// <summary>
    /// A Query that matches documents within an range of terms.
    /// </summary>
    public class RangeQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.RangeQueryBuilder;
        private readonly string _name;
        private object _from;
        private object _to;
        private bool _includeLower = true;
        private bool _includeUpper = true;
        private float? _boost;

        /// <summary>
        /// A Query that matches documents within an range of terms.
        /// </summary>
        /// <param name="name"></param>
        public RangeQueryBuilder(string name)
        {
            _name = name;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder From(object from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder From(string from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder From(int from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder From(long from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder From(float from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder From(double from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder Gt(object from)
        {
            _from = from;
            _includeLower = false;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder Gt(string from)
        {
            _from = from;
            _includeLower = false;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder Gt(int from)
        {
            _from = from;
            _includeLower = false;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder Gt(long from)
        {
            _from = from;
            _includeLower = false;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder Gt(float from)
        {
            _from = from;
            _includeLower = false;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder Gt(double from)
        {
            _from = from;
            _includeLower = false;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder Gte(object from)
        {
            _from = from;
            _includeLower = true;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder Gte(string from)
        {
            _from = from;
            _includeLower = true;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder Gte(int from)
        {
            _from = from;
            _includeLower = true;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder Gte(long from)
        {
            _from = from;
            _includeLower = true;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder Gte(float from)
        {
            _from = from;
            _includeLower = true;
            return this;
        }

        /// <summary>
        /// The from part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public RangeQueryBuilder Gte(double from)
        {
            _from = from;
            _includeLower = true;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder To(object to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder To(string to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder To(int to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder To(float to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder To(long to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder To(double to)
        {
            _to = to;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder Lt(object to)
        {
            _to = to;
            _includeUpper = false;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder Lt(string to)
        {
            _to = to;
            _includeUpper = false;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder Lt(int to)
        {
            _to = to;
            _includeUpper = false;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder Lt(float to)
        {
            _to = to;
            _includeUpper = false;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder Lt(long to)
        {
            _to = to;
            _includeUpper = false;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder Lt(double to)
        {
            _to = to;
            _includeUpper = false;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder Lte(object to)
        {
            _to = to;
            _includeUpper = true;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder Lte(string to)
        {
            _to = to;
            _includeUpper = true;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder Lte(int to)
        {
            _to = to;
            _includeUpper = true;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder Lte(float to)
        {
            _to = to;
            _includeUpper = true;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder Lte(long to)
        {
            _to = to;
            _includeUpper = true;
            return this;
        }

        /// <summary>
        /// The to part of the range query. Null indicates unbounded.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public RangeQueryBuilder Lte(double to)
        {
            _to = to;
            _includeUpper = true;
            return this;
        }

        /// <summary>
        /// Should the lower bound be included or not. Defaults to <tt>true</tt>.
        /// </summary>
        /// <param name="includeLower"></param>
        /// <returns></returns>
        public RangeQueryBuilder IncludeLower(bool includeLower)
        {
            _includeLower = includeLower;
            return this;
        }

        /// <summary>
        /// Should the upper bound be included or not. Defaults to <tt>true</tt>.
        /// </summary>
        /// <param name="includeUpper"></param>
        /// <returns></returns>
        public RangeQueryBuilder IncludeUpper(bool includeUpper)
        {
            _includeUpper = includeUpper;
            return this;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public RangeQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));
            
            content[NAME][_name] = new JObject();

            content[NAME][_name]["from"] = new JValue(_from);
            content[NAME][_name]["to"] = new JValue(_to);
            content[NAME][_name]["include_lower"] = _includeLower;
            content[NAME][_name]["include_upper"] = _includeUpper;

            if (_boost != null)
            {
                content[NAME][_name]["boost"] = _boost;
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
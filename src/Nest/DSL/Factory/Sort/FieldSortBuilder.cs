using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Sort
{
    public class FieldSortBuilder : ISortBuilder
    {
        private readonly string _fieldName;
        private object _missing;
        private SortOrder _order;
        private bool? _ignoreUnampped;

        public FieldSortBuilder(string fieldName)
        {
            _fieldName = fieldName;
        }

        /// <summary>
        /// Sets if the field does not exists in the index, it should be ignored and not sorted by or not. Defaults
        /// to <tt>false</tt> (not ignoring).
        /// </summary>
        /// <param name="ignoreUnmapped"></param>
        /// <returns></returns>
        public FieldSortBuilder IgnoreUnmapped(bool ignoreUnmapped)
        {
            _ignoreUnampped = ignoreUnmapped;
            return this;
        }

        #region ISortBuilder Members

        public ISortBuilder Order(SortOrder order)
        {
            _order = order;
            return this;
        }

        public ISortBuilder Missing(object missing)
        {
            _missing = missing;
            return this;
        }

        public object ToJsonObject()
        {
            var content = new JObject();
            content[_fieldName] = new JObject();

            if (_order != SortOrder.ASC)
            {
                content[_fieldName]["order"] = "desc";
            }

            if (_missing != null)
            {
                content[_fieldName]["missing"] = new JValue(_missing);
            }

            if (_ignoreUnampped != null)
            {
                content[_fieldName]["ignore_unmapped"] = _ignoreUnampped;
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
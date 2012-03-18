using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Sort
{
    /// <summary>
    /// A sort builder allowing to sort by score.
    /// </summary>
    public class ScoreSortBuilder : ISortBuilder
    {
        private const string NAME = NameRegistry.ScoreSortBuilder;
        private SortOrder _order;

        /// <summary>
        /// The order of sort scoring. By default, its DESC.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ISortBuilder Order(SortOrder order)
        {
            _order = order;
            return this;
        }

        public ISortBuilder Missing(object missing)
        {
            return this;
        }

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            if(_order == SortOrder.ASC)
            {
                content[NAME]["reverse"] = true;
            }

            return content;
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }
    }
}
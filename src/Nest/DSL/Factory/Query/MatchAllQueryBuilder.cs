using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    public class MatchAllQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.MatchAllQueryBuilder;
        private float? _boost;
        private string _normsField;

        public MatchAllQueryBuilder NormsField(string normsField)
        {
            _normsField = normsField;
            return this;
        }

        public MatchAllQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            if (_boost != null)
            {
                content[NAME]["boost"] = _boost;
            }

            if (_normsField != null)
            {
                content[NAME]["norms_field"] = _normsField;
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
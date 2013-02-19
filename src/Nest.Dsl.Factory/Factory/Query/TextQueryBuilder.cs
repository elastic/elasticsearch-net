using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class TextQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.TextQueryBuilder;
        private readonly string _name;
        private readonly object _text;
        private string _analyzer;
        private float? _boost;
        private string _fuzziness;
        private int? _maxExpansions;
        private Operator _operator;
        private int? _prefixLength;
        private int? _slop;
        private TextQueryType _type;

        public TextQueryBuilder(string fieldName, object text)
        {
            _operator = Nest.Operator.or;
            _type = TextQueryType.BOOLEAN;
            _name = fieldName;
            _text = text;
        }

        public TextQueryBuilder Type(TextQueryType type)
        {
            _type = type;
            return this;
        }

        public TextQueryBuilder Operator(Operator @operator)
        {
            _operator = @operator;
            return this;
        }

        public TextQueryBuilder Analyzer(string analyzer)
        {
            _analyzer = analyzer;
            return this;
        }

        public TextQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        public TextQueryBuilder Slop(int slop)
        {
            _slop = slop;
            return this;
        }

        public TextQueryBuilder Fuzziness(object fuzziness)
        {
            _fuzziness = fuzziness.ToString();
            return this;
        }

        public TextQueryBuilder PrefixLength(int prefixLength)
        {
            _prefixLength = prefixLength;
            return this;
        }

        public TextQueryBuilder MaxExpansions(int maxExpansions)
        {
            _maxExpansions = maxExpansions;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME][_name] = new JObject();
            content[NAME][_name]["query"] = new JValue(_text);

            if (_type != TextQueryType.BOOLEAN)
            {
                content[NAME][_name]["type"] = _type.ToString().ToLower();
            }

            if (_operator != Nest.Operator.or)
            {
                content[NAME][_name]["operator"] = "and";
            }

            if (_boost != null)
            {
                content[NAME][_name]["boost"] = _boost;
            }

            if (_analyzer != null)
            {
                content[NAME][_name]["analyzer"] = _analyzer;
            }

            if (_slop != null)
            {
                content[NAME][_name]["slop"] = _slop;
            }

            if (_fuzziness != null)
            {
                content[NAME][_name]["fuzziness"] = _fuzziness;
            }

            if (_prefixLength != null)
            {
                content[NAME][_name]["prefix_length"] = _prefixLength;
            }

            if (_maxExpansions != null)
            {
                content[NAME][_name]["max_expansions"] = _maxExpansions;
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
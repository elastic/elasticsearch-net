using System;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    /// <summary>
    /// A Query builder which allows building a query thanks to a JSON string or binary data. This is useful when you want
    /// to use the Java Builder API but still have JSON query strings at hand that you want to combine with other
    /// query builders.
    /// 
    /// Example usage in a boolean query :
    /// <pre>
    /// {@code
    ///      BoolQueryBuilder bool = new BoolQueryBuilder();
    ///      bool.must(new WrapperQueryBuilder("{\"term\": {\"field\":\"value\"}}");
    ///      bool.must(new TermQueryBuilder("field2","value2");
    /// }
    /// </pre>
    /// </summary>
    public class WrapperQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.WrapperQueryBuilder;
        private readonly byte[] _source;
        private readonly int _offset;
        private readonly int _length;

        private readonly string _jsonString;

        /// <summary>
        /// Builds a JSONQueryBuilder using the provided JSON query string.
        /// </summary>
        /// <param name="source"></param>
        public WrapperQueryBuilder(string source)
        {
            _jsonString = source;
        }

        public WrapperQueryBuilder(byte[] source, int offset, int length)
        {
            //        this.source = source;
            //        this.offset = offset;
            //        this.length = length;
            throw new NotImplementedException();
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));
            content[NAME]["query"] = _jsonString;
            return content;
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }

        #endregion
    }
}
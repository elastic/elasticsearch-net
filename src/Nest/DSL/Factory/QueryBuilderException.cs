using System;

namespace Nest.FactoryDsl
{
    public class QueryBuilderException : Exception
    {
        public QueryBuilderException(string message) : base(message) { }
    }
}

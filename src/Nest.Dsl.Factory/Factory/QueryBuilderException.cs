using System;

namespace Nest.Dsl.Factory
{
    public class QueryBuilderException : Exception
    {
        public QueryBuilderException(string message) : base(message) { }
    }
}

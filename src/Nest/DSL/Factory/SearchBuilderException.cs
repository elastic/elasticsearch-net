using System;

namespace Nest.FactoryDsl
{
    public class SearchBuilderException : Exception
    {
        public SearchBuilderException(string message) : base(message) { }
    }
}
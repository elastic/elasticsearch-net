using System;
using System.Linq.Expressions;

namespace Nest
{
    public class CompletionMappingDescriptor<T>
    {
        internal CompletionMapping _Mapping = new CompletionMapping();

        public CompletionMappingDescriptor<T> Name(string name)
        {
            this._Mapping.Name = name;
            return this;
        }

        public CompletionMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
        {
            this._Mapping.Name = objectPath;
            return this;
        }

        public CompletionMappingDescriptor<T> SearchAnalyzer(string name)
        {
            this._Mapping.SearchAnalyzer = name;
            return this;
        }

        public CompletionMappingDescriptor<T> IndexAnalyzer(string name)
        {
            this._Mapping.IndexAnalyzer = name;
            return this;
        }

        public CompletionMappingDescriptor<T> Payloads(bool payloads = true)
        {
            this._Mapping.Payloads = payloads;
            return this;
        }

        public CompletionMappingDescriptor<T> PreserveSeparators(bool preserveSeparators = true)
        {
            this._Mapping.PreserveSeparators = preserveSeparators;
            return this;
        }

        public CompletionMappingDescriptor<T> PreservePositionIncrements(bool preservePositionIncrements = true)
        {
            this._Mapping.PreservePositionIncrements = preservePositionIncrements;
            return this;
        }

        public CompletionMappingDescriptor<T> MaxInputLength(int maxInputLength)
        {
            this._Mapping.MaxInputLength = maxInputLength;
            return this;
        }
    }
}

using Nest.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
    public class CompletionMappingDescriptor<T>
    {
        internal CompletionMapping _Mapping = new CompletionMapping();

        public CompletionMappingDescriptor<T> Name(string name)
        {
            this._Mapping.TypeNameMarker = name;
            return this;
        }

        public CompletionMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
        {
            var name = new PropertyNameResolver().ResolveToLastToken(objectPath);
            this._Mapping.TypeNameMarker = name;
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

        public CompletionMappingDescriptor<T> Payloads(bool payloads)
        {
            this._Mapping.Payloads = payloads;
            return this;
        }

        public CompletionMappingDescriptor<T> PreserveSeparators(bool preserveSeparators)
        {
            this._Mapping.PreserveSeparators = preserveSeparators;
            return this;
        }

        public CompletionMappingDescriptor<T> PreservePositionIncrements(bool preservePositionIncrements)
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

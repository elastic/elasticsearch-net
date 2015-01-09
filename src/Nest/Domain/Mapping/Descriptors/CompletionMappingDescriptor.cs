using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nest
{
    public class CompletionMappingDescriptor<T>
		where T : class
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

		public CompletionMappingDescriptor<T> Context(Func<SuggestContextMappingDescriptor<T>, SuggestContextMappingDescriptor<T>> contextDescriptor)
		{
			if (this._Mapping.Context == null)
				this._Mapping.Context = new Dictionary<string, ISuggestContext>();

			var selector = contextDescriptor(new SuggestContextMappingDescriptor<T>());
			
			foreach (var context in selector._Contexts)
			{
				if (this._Mapping.Context.ContainsKey(context.Key))
					this._Mapping.Context[context.Key] = context.Value;
				else
					this._Mapping.Context.Add(context.Key, context.Value);
			}

			return this;
		}
    }
}

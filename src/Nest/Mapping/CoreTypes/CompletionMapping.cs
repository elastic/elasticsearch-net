using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class CompletionMapping : IElasticType
	{
		public FieldName Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeName Type { get { return new TypeName { Name = "completion" }; } }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("search_analyzer")]
		public string SearchAnalyzer { get; set; }

		[JsonProperty("index_analyzer")]
		public string IndexAnalyzer { get; set; }

		[JsonProperty("payloads")]
		public bool? Payloads { get; set; }

		[JsonProperty("preserve_separators")]
		public bool? PreserveSeparators { get; set; }

		[JsonProperty("preserve_position_increments")]
		public bool? PreservePositionIncrements { get; set; }

		[JsonProperty("max_input_len")]
		public int? MaxInputLength { get; set; }

		[JsonProperty("context")]
		public IDictionary<string, ISuggestContext> Context { get ;set;}
	}

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
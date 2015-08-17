using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ICompletionType : IElasticType
	{
		[JsonProperty("search_analyzer")]
		string SearchAnalyzer { get; set; }

		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("payloads")]
		bool? Payloads { get; set; }

		[JsonProperty("preserve_separators")]
		bool? PreserveSeparators { get; set; }

		[JsonProperty("preserve_position_increments")]
		bool? PreservePositionIncrements { get; set; }

		[JsonProperty("max_input_length")]
		int? MaxInputLength { get; set; }

		[JsonProperty("context")]
		IDictionary<string, ISuggestContext> Context { get ;set;}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class CompletionType : ElasticType, ICompletionType
	{
		public CompletionType() : base("completion") { }

		internal CompletionType(CompletionAttribute attribute)
			: base("completion", attribute)
		{
			SearchAnalyzer = attribute.SearchAnalyzer;
			Analyzer = attribute.Analyzer;
			Payloads = attribute.Payloads;
			PreserveSeparators = attribute.PreserveSeparators;
			MaxInputLength = attribute.MaxInputLength;
		}

		public string SearchAnalyzer { get; set; }
		public string Analyzer { get; set; }
		public bool? Payloads { get; set; }
		public bool? PreserveSeparators { get; set; }
		public bool? PreservePositionIncrements { get; set; }
		public int? MaxInputLength { get; set; }
		public IDictionary<string, ISuggestContext> Context { get ;set;}
	}

    public class CompletionTypeDescriptor<T>
		: TypeDescriptorBase<CompletionTypeDescriptor<T>, ICompletionType, T>, ICompletionType
		where T : class
    {
		string ICompletionType.SearchAnalyzer { get; set; }
		string ICompletionType.Analyzer { get; set; }
		bool? ICompletionType.Payloads { get; set; }
		bool? ICompletionType.PreserveSeparators { get; set; }
		bool? ICompletionType.PreservePositionIncrements { get; set; }
		int? ICompletionType.MaxInputLength { get; set; }
		IDictionary<string, ISuggestContext> ICompletionType.Context { get; set; }

		public CompletionTypeDescriptor<T> SearchAnalyzer(string searchAnalyzer) => 
			Assign(a => a.SearchAnalyzer = searchAnalyzer);

		public CompletionTypeDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public CompletionTypeDescriptor<T> Payloads(bool payloads = true) => Assign(a => a.Payloads = payloads);

		public CompletionTypeDescriptor<T> PreserveSeparators(bool preserveSeparators = true) => 
			Assign(a => a.PreserveSeparators = preserveSeparators);

		public CompletionTypeDescriptor<T> PreservePositionIncrements(bool preservePositionIncrements = true) => 
			Assign(a => a.PreservePositionIncrements = preservePositionIncrements);

		public CompletionTypeDescriptor<T> MaxInputLength(int maxInputLength) => Assign(a => a.MaxInputLength = maxInputLength);

		public CompletionTypeDescriptor<T> Context(Func<SuggestContextMappingDescriptor<T>, SuggestContextMappingDescriptor<T>> contextDescriptor) => Assign(a =>
		{
			a.Context = a.Context ?? new Dictionary<string, ISuggestContext>();

			var selector = contextDescriptor(new SuggestContextMappingDescriptor<T>());

			foreach (var context in selector._Contexts)
			{
				if (a.Context.ContainsKey(context.Key))
					a.Context[context.Key] = context.Value;
				else
					a.Context.Add(context.Key, context.Value);
			}
		});
    }
}
using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ICompletionProperty : IProperty
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
		ISuggestContextMapping Context { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class CompletionProperty : PropertyBase, ICompletionProperty
	{
		public CompletionProperty() : base("completion") { }

		public string SearchAnalyzer { get; set; }
		public string Analyzer { get; set; }
		public bool? Payloads { get; set; }
		public bool? PreserveSeparators { get; set; }
		public bool? PreservePositionIncrements { get; set; }
		public int? MaxInputLength { get; set; }
		public ISuggestContextMapping Context { get; set; }
	}

	public class CompletionPropertyDescriptor<T>
		: PropertyDescriptorBase<CompletionPropertyDescriptor<T>, ICompletionProperty, T>, ICompletionProperty
		where T : class
	{
		string ICompletionProperty.SearchAnalyzer { get; set; }
		string ICompletionProperty.Analyzer { get; set; }
		bool? ICompletionProperty.Payloads { get; set; }
		bool? ICompletionProperty.PreserveSeparators { get; set; }
		bool? ICompletionProperty.PreservePositionIncrements { get; set; }
		int? ICompletionProperty.MaxInputLength { get; set; }
		ISuggestContextMapping ICompletionProperty.Context { get; set; }

		public CompletionPropertyDescriptor() : base("completion") { }

		public CompletionPropertyDescriptor<T> SearchAnalyzer(string searchAnalyzer) =>
			Assign(a => a.SearchAnalyzer = searchAnalyzer);

		public CompletionPropertyDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public CompletionPropertyDescriptor<T> Payloads(bool payloads = true) => Assign(a => a.Payloads = payloads);

		public CompletionPropertyDescriptor<T> PreserveSeparators(bool preserveSeparators = true) =>
			Assign(a => a.PreserveSeparators = preserveSeparators);

		public CompletionPropertyDescriptor<T> PreservePositionIncrements(bool preservePositionIncrements = true) =>
			Assign(a => a.PreservePositionIncrements = preservePositionIncrements);

		public CompletionPropertyDescriptor<T> MaxInputLength(int maxInputLength) => Assign(a => a.MaxInputLength = maxInputLength);

		public CompletionPropertyDescriptor<T> Context(Func<SuggestContextMappingDescriptor<T>, IPromise<ISuggestContextMapping>> selector) => 
			Assign(a => a.Context = selector?.Invoke(new SuggestContextMappingDescriptor<T>())?.Value);

	}
}
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<CompletionSuggester>))]
	public interface ICompletionSuggester : ISuggester
	{
		[JsonProperty("contexts")]
		IDictionary<string, IList<ISuggestContextQuery>> Contexts { get; set; }

		[JsonProperty("fuzzy")]
		IFuzzySuggester Fuzzy { get; set; }

		[JsonIgnore]
		string Prefix { get; set; }

		[JsonIgnore]
		string Regex { get; set; }
	}

	public class CompletionSuggester : SuggesterBase, ICompletionSuggester
	{
		public IDictionary<string, IList<ISuggestContextQuery>> Contexts { get; set; }
		public IFuzzySuggester Fuzzy { get; set; }

		public string Prefix { get; set; }

		public string Regex { get; set; }
	}

	public class CompletionSuggesterDescriptor<T>
		: SuggestDescriptorBase<CompletionSuggesterDescriptor<T>, ICompletionSuggester, T>, ICompletionSuggester
		where T : class
	{
		IDictionary<string, IList<ISuggestContextQuery>> ICompletionSuggester.Contexts { get; set; }
		IFuzzySuggester ICompletionSuggester.Fuzzy { get; set; }

		string ICompletionSuggester.Prefix { get; set; }

		string ICompletionSuggester.Regex { get; set; }

		public CompletionSuggesterDescriptor<T> Prefix(string prefix) => Assign(a => a.Prefix = prefix);

		public CompletionSuggesterDescriptor<T> Regex(string regex) => Assign(a => a.Regex = regex);

		public CompletionSuggesterDescriptor<T> Fuzzy(Func<FuzzySuggestDescriptor<T>, IFuzzySuggester> selector = null) =>
			Assign(a => a.Fuzzy = selector.InvokeOrDefault(new FuzzySuggestDescriptor<T>()));

		public CompletionSuggesterDescriptor<T> Contexts(
			Func<SuggestContextQueriesDescriptor<T>, IPromise<IDictionary<string, IList<ISuggestContextQuery>>>> contexts
		) =>
			Assign(a => a.Contexts = contexts?.Invoke(new SuggestContextQueriesDescriptor<T>()).Value);
	}
}

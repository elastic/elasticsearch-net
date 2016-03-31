using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<CompletionSuggester>))]
	public interface ICompletionSuggester : ISuggester
	{
		[JsonIgnore]
		string Prefix { get; set; }

		[JsonIgnore]
		string Regex { get; set; }

		[JsonProperty("payload")]
		Fields Payload { get; set; }

		[JsonProperty("fuzzy")]
		IFuzzySuggester Fuzzy { get; set; }

		[JsonProperty("contexts")]
		IDictionary<string, IList<ISuggestContextQuery>> Contexts { get; set; }
	}

	public class CompletionSuggester : SuggesterBase, ICompletionSuggester
	{
		public IFuzzySuggester Fuzzy { get; set; }

		public IDictionary<string, IList<ISuggestContextQuery>> Contexts { get; set; }

		public string Prefix { get; set; }

		public string Regex { get; set; }

		public Fields Payload { get; set; }
	}

	public class CompletionSuggesterDescriptor<T> : SuggestDescriptorBase<CompletionSuggesterDescriptor<T>, ICompletionSuggester, T>, ICompletionSuggester
		where T : class
	{
		IFuzzySuggester ICompletionSuggester.Fuzzy { get; set; }

		IDictionary<string, IList<ISuggestContextQuery>> ICompletionSuggester.Contexts { get; set; }

		string ICompletionSuggester.Prefix { get; set; }

		string ICompletionSuggester.Regex { get; set; }

		Fields ICompletionSuggester.Payload { get; set; }

		public CompletionSuggesterDescriptor<T> Prefix(string prefix) => Assign(a => a.Prefix = prefix);

		public CompletionSuggesterDescriptor<T> Regex(string regex) => Assign(a => a.Regex = regex);

		public CompletionSuggesterDescriptor<T> Fuzzy(Func<FuzzySuggestDescriptor<T>, IFuzzySuggester> selector = null) =>
			Assign(a => a.Fuzzy = selector.InvokeOrDefault(new FuzzySuggestDescriptor<T>()));

		public CompletionSuggesterDescriptor<T> Contexts(Func<SuggestContextQueriesDescriptor<T>, IPromise<IDictionary<string, IList<ISuggestContextQuery>>>> contexts) =>
			Assign(a => a.Contexts = contexts?.Invoke(new SuggestContextQueriesDescriptor<T>()).Value);

		public CompletionSuggesterDescriptor<T> Payload(Func<FieldsDescriptor<T>, IPromise<Fields>> payload) =>
			Assign(a => a.Payload = payload?.Invoke(new FieldsDescriptor<T>())?.Value);

		public CompletionSuggesterDescriptor<T> Payload(Fields payload) => Assign(a => a.Payload = payload);
	}
}

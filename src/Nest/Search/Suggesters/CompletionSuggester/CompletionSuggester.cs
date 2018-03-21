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
		/// <summary>
		/// Prefix used to search for suggestions
		/// </summary>
		[JsonIgnore]
		string Prefix { get; set; }

		/// <summary>
		/// Prefix as a regular expression used to search for suggestions
		/// </summary>
		[JsonIgnore]
		string Regex { get; set; }

		/// <summary>
		/// Support fuzziness for the suggestions
		/// </summary>
		[JsonProperty("fuzzy")]
		IFuzzySuggester Fuzzy { get; set; }

		/// <summary>
		/// Context mappings used to filter and/or boost suggestions
		/// </summary>
		[JsonProperty("contexts")]
		IDictionary<string, IList<ISuggestContextQuery>> Contexts { get; set; }

		/// <summary>
		/// Whether duplicate suggestions should be filtered out. Defaults to <c>false</c>
		/// </summary>
		[JsonProperty("skip_duplicates")]
		bool? SkipDuplicates { get; set; }
	}

	public class CompletionSuggester : SuggesterBase, ICompletionSuggester
	{
		/// <inheritdoc />
		public IFuzzySuggester Fuzzy { get; set; }

		/// <inheritdoc />
		public IDictionary<string, IList<ISuggestContextQuery>> Contexts { get; set; }

		/// <inheritdoc />
		public string Prefix { get; set; }

		/// <inheritdoc />
		public string Regex { get; set; }

		/// <inheritdoc />
		public bool? SkipDuplicates { get; set; }
	}

	public class CompletionSuggesterDescriptor<T> : SuggestDescriptorBase<CompletionSuggesterDescriptor<T>, ICompletionSuggester, T>, ICompletionSuggester
		where T : class
	{
		IFuzzySuggester ICompletionSuggester.Fuzzy { get; set; }
		IDictionary<string, IList<ISuggestContextQuery>> ICompletionSuggester.Contexts { get; set; }
		string ICompletionSuggester.Prefix { get; set; }
		string ICompletionSuggester.Regex { get; set; }
		bool? ICompletionSuggester.SkipDuplicates { get; set; }

		/// <summary>
		/// Prefix used to search for suggestions
		/// </summary>
		public CompletionSuggesterDescriptor<T> Prefix(string prefix) => Assign(a => a.Prefix = prefix);

		/// <summary>
		/// Prefix as a regular expression used to search for suggestions
		/// </summary>
		public CompletionSuggesterDescriptor<T> Regex(string regex) => Assign(a => a.Regex = regex);

		/// <summary>
		/// Support fuzziness for the suggestions
		/// </summary>
		public CompletionSuggesterDescriptor<T> Fuzzy(Func<FuzzySuggestDescriptor<T>, IFuzzySuggester> selector = null) =>
			Assign(a => a.Fuzzy = selector.InvokeOrDefault(new FuzzySuggestDescriptor<T>()));

		/// <summary>
		/// Context mappings used to filter and/or boost suggestions
		/// </summary>
		public CompletionSuggesterDescriptor<T> Contexts(Func<SuggestContextQueriesDescriptor<T>, IPromise<IDictionary<string, IList<ISuggestContextQuery>>>> contexts) =>
			Assign(a => a.Contexts = contexts?.Invoke(new SuggestContextQueriesDescriptor<T>()).Value);

		/// <summary>
		/// Whether duplicate suggestions should be filtered out. Defaults to <c>false</c>
		/// </summary>
		public CompletionSuggesterDescriptor<T> SkipDuplicates(bool? skipDuplicates = true) => Assign(a => a.SkipDuplicates = skipDuplicates);
	}
}

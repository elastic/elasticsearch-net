// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The completion suggester provides auto-complete/search-as-you-type functionality.
	/// This is a navigational feature to guide users to relevant results as they are typing, improving search precision.
	/// It is not meant for spell correction or did-you-mean functionality like the term or phrase suggesters.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(CompletionSuggester))]
	public interface ICompletionSuggester : ISuggester
	{
		/// <summary>
		/// Context mappings used to filter and/or boost suggestions
		/// </summary>
		[DataMember(Name = "contexts")]
		IDictionary<string, IList<ISuggestContextQuery>> Contexts { get; set; }

		/// <summary>
		/// Support fuzziness for the suggestions
		/// </summary>
		[DataMember(Name = "fuzzy")]
		ISuggestFuzziness Fuzzy { get; set; }

		/// <summary>
		/// Prefix used to search for suggestions
		/// </summary>
		[IgnoreDataMember]
		string Prefix { get; set; }

		/// <summary>
		/// Prefix as a regular expression used to search for suggestions
		/// </summary>
		[IgnoreDataMember]
		string Regex { get; set; }

		/// <summary>
		/// Whether duplicate suggestions should be filtered out. Defaults to <c>false</c>
		/// </summary>
		[DataMember(Name = "skip_duplicates")]
		bool? SkipDuplicates { get; set; }
	}

	/// <inheritdoc cref="ICompletionSuggester" />
	public class CompletionSuggester : SuggesterBase, ICompletionSuggester
	{
		/// <inheritdoc />
		public IDictionary<string, IList<ISuggestContextQuery>> Contexts { get; set; }

		/// <inheritdoc />
		public ISuggestFuzziness Fuzzy { get; set; }

		/// <inheritdoc />
		public string Prefix { get; set; }

		/// <inheritdoc />
		public string Regex { get; set; }

		/// <inheritdoc />
		public bool? SkipDuplicates { get; set; }
	}

	/// <inheritdoc cref="ICompletionSuggester" />
	public class CompletionSuggesterDescriptor<T>
		: SuggestDescriptorBase<CompletionSuggesterDescriptor<T>, ICompletionSuggester, T>, ICompletionSuggester
		where T : class
	{
		IDictionary<string, IList<ISuggestContextQuery>> ICompletionSuggester.Contexts { get; set; }
		ISuggestFuzziness ICompletionSuggester.Fuzzy { get; set; }
		string ICompletionSuggester.Prefix { get; set; }
		string ICompletionSuggester.Regex { get; set; }
		bool? ICompletionSuggester.SkipDuplicates { get; set; }

		/// <inheritdoc cref="ICompletionSuggester.Prefix" />
		public CompletionSuggesterDescriptor<T> Prefix(string prefix) => Assign(prefix, (a, v) => a.Prefix = v);

		/// <inheritdoc cref="ICompletionSuggester.Regex" />
		public CompletionSuggesterDescriptor<T> Regex(string regex) => Assign(regex, (a, v) => a.Regex = v);

		/// <inheritdoc cref="ICompletionSuggester.Fuzzy" />
		public CompletionSuggesterDescriptor<T> Fuzzy(Func<SuggestFuzzinessDescriptor<T>, ISuggestFuzziness> selector = null) =>
			Assign(selector.InvokeOrDefault(new SuggestFuzzinessDescriptor<T>()), (a, v) => a.Fuzzy = v);

		/// <inheritdoc cref="ICompletionSuggester.Contexts" />
		public CompletionSuggesterDescriptor<T> Contexts(
			Func<SuggestContextQueriesDescriptor<T>, IPromise<IDictionary<string, IList<ISuggestContextQuery>>>> contexts
		) =>
			Assign(contexts, (a, v) => a.Contexts = v?.Invoke(new SuggestContextQueriesDescriptor<T>()).Value);

		/// <inheritdoc cref="ICompletionSuggester.SkipDuplicates" />
		public CompletionSuggesterDescriptor<T> SkipDuplicates(bool? skipDuplicates = true) => Assign(skipDuplicates, (a, v) => a.SkipDuplicates = v);
	}
}

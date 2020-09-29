// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<SuggestContainer, ISuggestContainer, string, ISuggestBucket>))]
	public interface ISuggestContainer : IIsADictionary<string, ISuggestBucket> { }

	public class SuggestContainer : IsADictionaryBase<string, ISuggestBucket>, ISuggestContainer
	{
		public SuggestContainer() { }

		public SuggestContainer(IDictionary<string, ISuggestBucket> container) : base(container) { }

		public SuggestContainer(Dictionary<string, ISuggestBucket> container) : base(container) { }

		public void Add(string name, ISuggestBucket script) => BackingDictionary.Add(name, script);
	}

	public class SuggestContainerDescriptor<T>
		: IsADictionaryDescriptorBase<SuggestContainerDescriptor<T>, ISuggestContainer, string, ISuggestBucket>
		where T : class
	{
		public SuggestContainerDescriptor() : base(new SuggestContainer()) { }

		private SuggestContainerDescriptor<T> AssignToBucket<TSuggester>(string name, TSuggester suggester, Action<SuggestBucket, TSuggester> assign)
			where TSuggester : ISuggester
		{
			var bucket = new SuggestBucket();
			assign(bucket, suggester);
			return Assign(name, bucket);
		}

		/// <inheritdoc cref="ITermSuggester"/>
		public SuggestContainerDescriptor<T> Term(string name, Func<TermSuggesterDescriptor<T>, ITermSuggester> suggest) =>
			AssignToBucket(name, suggest?.Invoke(new TermSuggesterDescriptor<T>()), (b, s) =>
			{
				b.Term = s;
				b.Text = s.Text;
			});

		/// <inheritdoc cref="IPhraseSuggester"/>
		public SuggestContainerDescriptor<T> Phrase(string name, Func<PhraseSuggesterDescriptor<T>, IPhraseSuggester> suggest) =>
			AssignToBucket(name, suggest?.Invoke(new PhraseSuggesterDescriptor<T>()), (b, s) =>
			{
				b.Phrase = s;
				b.Text = s.Text;
			});

		/// <summary>
		/// The completion suggester is a so-called prefix suggester.
		/// It does not do spell correction like the term or phrase suggesters but allows basic auto-complete functionality.
		/// </summary>
		public SuggestContainerDescriptor<T> Completion(string name, Func<CompletionSuggesterDescriptor<T>, ICompletionSuggester> suggest) =>
			AssignToBucket(name, suggest?.Invoke(new CompletionSuggesterDescriptor<T>()), (b, s) =>
			{
				b.Completion = s;
				b.Prefix = s.Prefix;
				b.Regex = s.Regex;
			});
	}
}

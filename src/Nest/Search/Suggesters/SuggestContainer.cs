using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<SuggestContainer, string, ISuggestBucket>))]
	public interface ISuggestContainer : IIsADictionary<string, ISuggestBucket> { }

	public class SuggestContainer : IsADictionaryBase<string, ISuggestBucket>, ISuggestContainer
	{
		public SuggestContainer() : base() { }
		public SuggestContainer(IDictionary<string, ISuggestBucket> container) : base(container) { }
		public SuggestContainer(Dictionary<string, ISuggestBucket> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(string name, ISuggestBucket script) => this.BackingDictionary.Add(name, script);
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

		/// <summary>
		/// The term suggester suggests terms based on edit distance. The provided suggest text is analyzed before terms are suggested.
		/// The suggested terms are provided per analyzed suggest text token. The term suggester doesn’t take the query into account that is part of request.
		/// </summary>
		public SuggestContainerDescriptor<T> Term(string name, Func<TermSuggesterDescriptor<T>, ITermSuggester> suggest) =>
			AssignToBucket(name, suggest?.Invoke(new TermSuggesterDescriptor<T>()), (b, s) => { b.Term = s; b.Text = s.Text; });

		/// <summary>
		/// The phrase suggester adds additional logic on top of the term suggester to select entire corrected phrases
		/// instead of individual tokens weighted based on ngram-langugage models.
		/// </summary>
		public SuggestContainerDescriptor<T> Phrase(string name, Func<PhraseSuggesterDescriptor<T>, IPhraseSuggester> suggest) =>
			AssignToBucket(name, suggest?.Invoke(new PhraseSuggesterDescriptor<T>()), (b, s) => { b.Phrase = s; b.Text = s.Text; });

		/// <summary>
		/// The completion suggester is a so-called prefix suggester.
		/// It does not do spell correction like the term or phrase suggesters but allows basic auto-complete functionality.
		/// </summary>
		public SuggestContainerDescriptor<T> Completion(string name, Func<CompletionSuggesterDescriptor<T>, ICompletionSuggester> suggest) =>
			AssignToBucket(name, suggest?.Invoke(new CompletionSuggesterDescriptor<T>()), (b, s) => { b.Completion = s; b.Prefix = s.Prefix; b.Regex = s.Regex; });

	}
}

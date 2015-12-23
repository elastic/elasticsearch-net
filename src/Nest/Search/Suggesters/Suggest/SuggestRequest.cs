using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	//TODO UNTESTED
	[JsonConverter(typeof(SuggestRequest))]
	public partial interface ISuggestRequest 
	{
		string GlobalText { get; set; }
		IDictionary<string, ISuggester> Suggest { get; set; }
	}

	public partial class SuggestRequest 
	{
		public string GlobalText { get; set; }
		public IDictionary<string, ISuggester> Suggest { get; set; }
	}

	[DescriptorFor("Suggest")]
	public partial class SuggestDescriptor<T> where T : class
	{
		string ISuggestRequest.GlobalText { get; set; }
		IDictionary<string, ISuggester> ISuggestRequest.Suggest { get; set; } = new Dictionary<string, ISuggester>();

		/// <summary>
		/// To avoid repetition of the suggest text, it is possible to define a global text.
		/// </summary>
		public SuggestDescriptor<T> GlobalText(string globalSuggestText)
		{
			Self.GlobalText = globalSuggestText;
			return this;
		}

		/// <summary>
		/// The term suggester suggests terms based on edit distance. The provided suggest text is analyzed before terms are suggested. 
		/// The suggested terms are provided per analyzed suggest text token. The term suggester doesn’t take the query into account that is part of request.
		/// </summary>
		public SuggestDescriptor<T> Term(string name, Func<TermSuggesterDescriptor<T>, TermSuggesterDescriptor<T>> suggest)
		{
			name.ThrowIfNullOrEmpty(nameof(name));
			suggest.ThrowIfNull(nameof(suggest));
			var desc = new TermSuggesterDescriptor<T>();
			var item = suggest(desc);
			Self.Suggest.Add(name, item);
			return this;
		}

		/// <summary>
		/// The phrase suggester adds additional logic on top of the term suggester to select entire corrected phrases 
		/// instead of individual tokens weighted based on ngram-langugage models. 
		/// </summary>
		public SuggestDescriptor<T> Phrase(string name, Func<PhraseSuggesterDescriptor<T>, PhraseSuggesterDescriptor<T>> suggest)
		{
			name.ThrowIfNullOrEmpty(nameof(name));
			suggest.ThrowIfNull(nameof(suggest));

			var desc = new PhraseSuggesterDescriptor<T>();
			var item = suggest(desc);
			Self.Suggest.Add(name, item);
			return this;
		}

		/// <summary>
		/// The completion suggester is a so-called prefix suggester. 
		/// It does not do spell correction like the term or phrase suggesters but allows basic auto-complete functionality.
		/// </summary>
		public SuggestDescriptor<T> Completion(string name, Func<CompletionSuggesterDescriptor<T>, CompletionSuggesterDescriptor<T>> suggest)
		{
			name.ThrowIfNullOrEmpty(nameof(name));
			suggest.ThrowIfNull(nameof(suggest));

			var desc = new CompletionSuggesterDescriptor<T>();
			var item = suggest(desc);
			Self.Suggest.Add(name, item);
			return this;
		}
	}
}

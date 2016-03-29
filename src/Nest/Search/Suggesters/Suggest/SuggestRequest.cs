using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(SuggestRequestJsonConverter))]
	public partial interface ISuggestRequest
	{
		string GlobalText { get; set; }
		ISuggestContainer Suggest { get; set; }
	}

	public partial class SuggestRequest
	{
		public string GlobalText { get; set; }
		public ISuggestContainer Suggest { get; set; }
	}

	[DescriptorFor("Suggest")]
	public partial class SuggestDescriptor<T> where T : class
	{
		string ISuggestRequest.GlobalText { get; set; }
		ISuggestContainer ISuggestRequest.Suggest { get; set; } = new SuggestContainer();

		/// <summary>
		/// To avoid repetition of the suggest text, it is possible to define a global text.
		/// </summary>
		public SuggestDescriptor<T> GlobalText(string globalText) => Assign(a => a.GlobalText = globalText);

		/// <summary>
		/// The term suggester suggests terms based on edit distance. The provided suggest text is analyzed before terms are suggested.
		/// The suggested terms are provided per analyzed suggest text token. The term suggester doesn’t take the query into account that is part of request.
		/// </summary>
		public SuggestDescriptor<T> Term(string name, Func<TermSuggesterDescriptor<T>, ITermSuggester> suggest) =>
			AssignToBucket(name, suggest?.Invoke(new TermSuggesterDescriptor<T>()), (b, s) => { b.Term = s; b.Text = s.Text; });

		/// <summary>
		/// The phrase suggester adds additional logic on top of the term suggester to select entire corrected phrases
		/// instead of individual tokens weighted based on ngram-langugage models.
		/// </summary>
		public SuggestDescriptor<T> Phrase(string name, Func<PhraseSuggesterDescriptor<T>, IPhraseSuggester> suggest) =>
			AssignToBucket(name, suggest?.Invoke(new PhraseSuggesterDescriptor<T>()), (b, s) => { b.Phrase = s; b.Text = s.Text; });

		/// <summary>
		/// The completion suggester is a so-called prefix suggester.
		/// It does not do spell correction like the term or phrase suggesters but allows basic auto-complete functionality.
		/// </summary>
		public SuggestDescriptor<T> Completion(string name, Func<CompletionSuggesterDescriptor<T>, ICompletionSuggester> suggest) =>
			AssignToBucket(name, suggest?.Invoke(new CompletionSuggesterDescriptor<T>()), (b, s) => { b.Completion = s; b.Prefix = s.Prefix; b.Regex = s.Regex; });

		private SuggestDescriptor<T> AssignToBucket<TSuggester>(string name, TSuggester suggester, Action<SuggestBucket, TSuggester> assign)
			where TSuggester : ISuggester
		{
			var bucket = new SuggestBucket();
			assign(bucket, suggester);
			return Assign(a => a.Suggest.Add(name, bucket));
		}
	}
}

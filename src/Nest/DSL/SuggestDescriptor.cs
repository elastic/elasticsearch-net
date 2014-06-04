using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("Suggest")]
	public partial class SuggestDescriptor<T> : 
		IndicesOptionalExplicitAllPathDescriptor<SuggestDescriptor<T>, SuggestRequestParameters>
		, IPathInfo<SuggestRequestParameters>
		where T : class
	{
		internal IDictionary<string, object> _Suggest = new Dictionary<string, object>();

		/// <summary>
		/// To avoid repetition of the suggest text, it is possible to define a global text.
		/// </summary>
		public SuggestDescriptor<T> GlobalText(string globalSuggestText)
		{
			this._Suggest.Add("text", globalSuggestText);
			return this;
		}

		/// <summary>
		/// The term suggester suggests terms based on edit distance. The provided suggest text is analyzed before terms are suggested. 
		/// The suggested terms are provided per analyzed suggest text token. The term suggester doesn’t take the query into account that is part of request.
		/// </summary>
		public SuggestDescriptor<T> Term(string name, Func<TermSuggestDescriptor<T>, TermSuggestDescriptor<T>> suggest)
		{
			name.ThrowIfNullOrEmpty("name");
			suggest.ThrowIfNull("suggest");
			var desc = new TermSuggestDescriptor<T>();
			var item = suggest(desc);
			ITermSuggester i = item;
			var bucket = new SuggestBucket { Text = i._Text, Term = item };
			this._Suggest.Add(name, bucket);
			return this;
		}

		/// <summary>
		/// The phrase suggester adds additional logic on top of the term suggester to select entire corrected phrases 
		/// instead of individual tokens weighted based on ngram-langugage models. 
		/// </summary>
		public SuggestDescriptor<T> Phrase(string name, Func<PhraseSuggestDescriptor<T>, PhraseSuggestDescriptor<T>> suggest)
		{
			name.ThrowIfNullOrEmpty("name");
			suggest.ThrowIfNull("suggest");
			if (this._Suggest == null)
				this._Suggest = new Dictionary<string, object>();

			var desc = new PhraseSuggestDescriptor<T>();
			var item = suggest(desc);
			IPhraseSuggester i = item;
			var bucket = new SuggestBucket { Text = i._Text, Phrase = item };
			this._Suggest.Add(name, bucket);
			return this;
		}

		/// <summary>
		/// The completion suggester is a so-called prefix suggester. 
		/// It does not do spell correction like the term or phrase suggesters but allows basic auto-complete functionality.
		/// </summary>
		public SuggestDescriptor<T> Completion(string name, Func<CompletionSuggestDescriptor<T>, CompletionSuggestDescriptor<T>> suggest)
		{
			name.ThrowIfNullOrEmpty("name");
			suggest.ThrowIfNull("suggest");
			if (this._Suggest == null)
				this._Suggest = new Dictionary<string, object>();

			var desc = new CompletionSuggestDescriptor<T>();
			var item = suggest(desc);
			ICompletionSuggester i = item;
			var bucket = new SuggestBucket { Text = i._Text, Completion = item };
			this._Suggest.Add(name, bucket);
			return this;
		}
			
		ElasticsearchPathInfo<SuggestRequestParameters> IPathInfo<SuggestRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(CustomJsonConverter))]
	public interface ISuggestRequest : IIndicesOptionalExplicitAllPath<SuggestRequestParameters>, ICustomJson
	{
		string GlobalText { get; set; }
		IDictionary<string, ISuggester> Suggest { get; set; }
	}

	internal static class SuggestPathInfo
	{
		public static void Update(ElasticsearchPathInfo<SuggestRequestParameters> pathInfo, ISuggestRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}

		public static object GetCustomJson(ISuggestRequest suggestRequest)
		{
			if (suggestRequest == null || (suggestRequest.GlobalText.IsNullOrEmpty() && suggestRequest.Suggest == null))
				return null;

			var dict = new Dictionary<string, object>();
			if (!suggestRequest.GlobalText.IsNullOrEmpty())
				dict.Add("text", suggestRequest.GlobalText);

			if (suggestRequest.Suggest != null)
			{
				foreach (var kv in suggestRequest.Suggest)
				{
					var item = kv.Value;
					var bucket = new SuggestBucket() { Text = item.Text };

					var completion = item as ICompletionSuggester;
					if (completion != null) bucket.Completion = completion;

					var phrase = item as IPhraseSuggester;
					if (phrase != null) bucket.Phrase = phrase;

					var term = item as ITermSuggester;
					if (term != null) bucket.Term = term;
					dict.Add(kv.Key, bucket);
				}
			}
			return dict;
		}
	}

	public partial class SuggestRequest : IndicesOptionalExplicitAllPathBase<SuggestRequestParameters>, ISuggestRequest
	{
		public string GlobalText { get; set; }
		public IDictionary<string, ISuggester> Suggest { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SuggestRequestParameters> pathInfo)
		{
			SuggestPathInfo.Update(pathInfo, this);
		}

		object ICustomJson.GetCustomJson() { return SuggestPathInfo.GetCustomJson(this); }

	}


	[DescriptorFor("Suggest")]
	public partial class SuggestDescriptor<T> : IndicesOptionalExplicitAllPathDescriptor<SuggestDescriptor<T>, SuggestRequestParameters>, ISuggestRequest
		where T : class
	{
		private ISuggestRequest Self { get { return this; } }

		object ICustomJson.GetCustomJson() { return SuggestPathInfo.GetCustomJson(this); }

		string ISuggestRequest.GlobalText { get; set; }
		IDictionary<string, ISuggester> ISuggestRequest.Suggest { get; set; }

		public SuggestDescriptor()
		{
			Self.Suggest = new Dictionary<string, ISuggester>();
		}

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
		public SuggestDescriptor<T> Term(string name, Func<TermSuggestDescriptor<T>, TermSuggestDescriptor<T>> suggest)
		{
			name.ThrowIfNullOrEmpty("name");
			suggest.ThrowIfNull("suggest");
			var desc = new TermSuggestDescriptor<T>();
			var item = suggest(desc);
			Self.Suggest.Add(name, item);
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

			var desc = new PhraseSuggestDescriptor<T>();
			var item = suggest(desc);
			Self.Suggest.Add(name, item);
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

			var desc = new CompletionSuggestDescriptor<T>();
			var item = suggest(desc);
			Self.Suggest.Add(name, item);
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SuggestRequestParameters> pathInfo)
		{
			SuggestPathInfo.Update(pathInfo, this);
		}

	}
}

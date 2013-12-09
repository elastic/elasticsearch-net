using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MoreLikeThisQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty(PropertyName = "fields")]
		internal IEnumerable<string> _Fields { get; set; }
		[JsonProperty(PropertyName = "like_text")]
		internal string _LikeText { get; set; }

		[JsonProperty(PropertyName = "percent_terms_to_match")]
		internal double? _TermMatchPercentage { get; set; }
		[JsonProperty(PropertyName = "stop_words")]
		internal IEnumerable<string> _StopWords { get; set; }
		[JsonProperty(PropertyName = "min_term_freq")]
		internal int? _MinTermFrequency { get; set; }
		[JsonProperty(PropertyName = "max_query_terms")]
		internal int? _MaxQueryTerms { get; set; }
		[JsonProperty(PropertyName = "min_doc_freq")]
		internal int? _MinDocumentFrequency { get; set; }
		[JsonProperty(PropertyName = "max_doc_freq")]
		internal int? _MaxDocumentFrequency { get; set; }
		[JsonProperty(PropertyName = "min_word_len")]
		internal int? _MinWordLength { get; set; }
		[JsonProperty(PropertyName = "max_word_len")]
		internal int? _MaxWordLength { get; set; }
		[JsonProperty(PropertyName = "boost_terms")]
		internal double? _BoostTerms { get; set; }
		[JsonProperty(PropertyName = "boost")]
		internal double? _Boost { get; set; }
		[JsonProperty(PropertyName = "analyzer")]
		internal string _Analyzer { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return this._LikeText.IsNullOrEmpty();
			}
		}

		public MoreLikeThisQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			this._Fields = fields;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			var fieldNames = objectPaths
				.Select(o => new PropertyNameResolver().Resolve(o));
			return this.OnFields(fieldNames);
		}
		public MoreLikeThisQueryDescriptor<T> LikeText(string likeText)
		{
			this._LikeText = likeText;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> StopWords(IEnumerable<string> stopWords)
		{
			this._StopWords = stopWords;
			return this;
		}
		
		public MoreLikeThisQueryDescriptor<T> MaxQueryTerms(int maxQueryTerms)
		{
			this._MaxQueryTerms = maxQueryTerms;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MinTermFrequency(int minTermFrequency)
		{
			this._MinTermFrequency = minTermFrequency;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MinDocumentFrequency(int minDocumentFrequency)
		{
			this._MinDocumentFrequency = minDocumentFrequency;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MaxDocumentFrequency(int maxDocumentFrequency)
		{
			this._MaxDocumentFrequency = maxDocumentFrequency;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MinWordLength(int minWordLength)
		{
			this._MinWordLength = minWordLength;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MaxWordLength(int maxWordLength)
		{
			this._MaxWordLength = maxWordLength;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> BoostTerms(double boostTerms)
		{
			this._BoostTerms = boostTerms;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> TermMatchPercentage(double termMatchPercentage)
		{
			this._TermMatchPercentage = termMatchPercentage;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> Analyzer(string analyzer)
		{
			this._Analyzer = analyzer;
			return this;
		}
	
	}
}

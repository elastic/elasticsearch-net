using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	public interface IMoreLikeThisQuery
	{
		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> _Fields { get; set; }

		[JsonProperty(PropertyName = "like_text")]
		string _LikeText { get; set; }

		[JsonProperty(PropertyName = "percent_terms_to_match")]
		double? _TermMatchPercentage { get; set; }

		[JsonProperty(PropertyName = "stop_words")]
		IEnumerable<string> _StopWords { get; set; }

		[JsonProperty(PropertyName = "min_term_freq")]
		int? _MinTermFrequency { get; set; }

		[JsonProperty(PropertyName = "max_query_terms")]
		int? _MaxQueryTerms { get; set; }

		[JsonProperty(PropertyName = "min_doc_freq")]
		int? _MinDocumentFrequency { get; set; }

		[JsonProperty(PropertyName = "max_doc_freq")]
		int? _MaxDocumentFrequency { get; set; }

		[JsonProperty(PropertyName = "min_word_len")]
		int? _MinWordLength { get; set; }

		[JsonProperty(PropertyName = "max_word_len")]
		int? _MaxWordLength { get; set; }

		[JsonProperty(PropertyName = "boost_terms")]
		double? _BoostTerms { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? _Boost { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string _Analyzer { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MoreLikeThisQueryDescriptor<T> : IQuery, IMoreLikeThisQuery where T : class
	{
		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> IMoreLikeThisQuery._Fields { get; set; }
		
		[JsonProperty(PropertyName = "like_text")]
		string IMoreLikeThisQuery._LikeText { get; set; }

		[JsonProperty(PropertyName = "percent_terms_to_match")]
		double? IMoreLikeThisQuery._TermMatchPercentage { get; set; }
	
		[JsonProperty(PropertyName = "stop_words")]
		IEnumerable<string> IMoreLikeThisQuery._StopWords { get; set; }
		
		[JsonProperty(PropertyName = "min_term_freq")]
		int? IMoreLikeThisQuery._MinTermFrequency { get; set; }
		
		[JsonProperty(PropertyName = "max_query_terms")]
		int? IMoreLikeThisQuery._MaxQueryTerms { get; set; }
		
		[JsonProperty(PropertyName = "min_doc_freq")]
		int? IMoreLikeThisQuery._MinDocumentFrequency { get; set; }
		
		[JsonProperty(PropertyName = "max_doc_freq")]
		int? IMoreLikeThisQuery._MaxDocumentFrequency { get; set; }
		
		[JsonProperty(PropertyName = "min_word_len")]
		int? IMoreLikeThisQuery._MinWordLength { get; set; }
		
		[JsonProperty(PropertyName = "max_word_len")]
		int? IMoreLikeThisQuery._MaxWordLength { get; set; }
		
		[JsonProperty(PropertyName = "boost_terms")]
		double? IMoreLikeThisQuery._BoostTerms { get; set; }
		
		[JsonProperty(PropertyName = "boost")]
		double? IMoreLikeThisQuery._Boost { get; set; }
		
		[JsonProperty(PropertyName = "analyzer")]
		string IMoreLikeThisQuery._Analyzer { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IMoreLikeThisQuery)this)._LikeText.IsNullOrEmpty();
			}
		}

		public MoreLikeThisQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			((IMoreLikeThisQuery)this)._Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			((IMoreLikeThisQuery)this)._Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> LikeText(string likeText)
		{
			((IMoreLikeThisQuery)this)._LikeText = likeText;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> StopWords(IEnumerable<string> stopWords)
		{
			((IMoreLikeThisQuery)this)._StopWords = stopWords;
			return this;
		}
		
		public MoreLikeThisQueryDescriptor<T> MaxQueryTerms(int maxQueryTerms)
		{
			((IMoreLikeThisQuery)this)._MaxQueryTerms = maxQueryTerms;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MinTermFrequency(int minTermFrequency)
		{
			((IMoreLikeThisQuery)this)._MinTermFrequency = minTermFrequency;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MinDocumentFrequency(int minDocumentFrequency)
		{
			((IMoreLikeThisQuery)this)._MinDocumentFrequency = minDocumentFrequency;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MaxDocumentFrequency(int maxDocumentFrequency)
		{
			((IMoreLikeThisQuery)this)._MaxDocumentFrequency = maxDocumentFrequency;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MinWordLength(int minWordLength)
		{
			((IMoreLikeThisQuery)this)._MinWordLength = minWordLength;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MaxWordLength(int maxWordLength)
		{
			((IMoreLikeThisQuery)this)._MaxWordLength = maxWordLength;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> BoostTerms(double boostTerms)
		{
			((IMoreLikeThisQuery)this)._BoostTerms = boostTerms;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> TermMatchPercentage(double termMatchPercentage)
		{
			((IMoreLikeThisQuery)this)._TermMatchPercentage = termMatchPercentage;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> Boost(double boost)
		{
			((IMoreLikeThisQuery)this)._Boost = boost;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> Analyzer(string analyzer)
		{
			((IMoreLikeThisQuery)this)._Analyzer = analyzer;
			return this;
		}
	
	}
}

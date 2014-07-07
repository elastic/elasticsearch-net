using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<MoreLikeThisQueryDescriptor<object>>))]
	public interface IMoreLikeThisQuery : IQuery
	{
		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> Fields { get; set; }

		[JsonProperty(PropertyName = "like_text")]
		string LikeText { get; set; }

		[JsonProperty(PropertyName = "percent_terms_to_match")]
		double? TermMatchPercentage { get; set; }

		[JsonProperty(PropertyName = "stop_words")]
		IEnumerable<string> StopWords { get; set; }

		[JsonProperty(PropertyName = "min_term_freq")]
		int? MinTermFrequency { get; set; }

		[JsonProperty(PropertyName = "max_query_terms")]
		int? MaxQueryTerms { get; set; }

		[JsonProperty(PropertyName = "min_doc_freq")]
		int? MinDocumentFrequency { get; set; }

		[JsonProperty(PropertyName = "max_doc_freq")]
		int? MaxDocumentFrequency { get; set; }

		[JsonProperty(PropertyName = "min_word_len")]
		int? MinWordLength { get; set; }

		[JsonProperty(PropertyName = "max_word_len")]
		int? MaxWordLength { get; set; }

		[JsonProperty(PropertyName = "boost_terms")]
		double? BoostTerms { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }
	}


	public class MoreLikeThisQuery : PlainQuery, IMoreLikeThisQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.MoreLikeThis = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public IEnumerable<PropertyPathMarker> Fields { get; set; }
		public string LikeText { get; set; }
		public double? TermMatchPercentage { get; set; }
		public IEnumerable<string> StopWords { get; set; }
		public int? MinTermFrequency { get; set; }
		public int? MaxQueryTerms { get; set; }
		public int? MinDocumentFrequency { get; set; }
		public int? MaxDocumentFrequency { get; set; }
		public int? MinWordLength { get; set; }
		public int? MaxWordLength { get; set; }
		public double? BoostTerms { get; set; }
		public double? Boost { get; set; }
		public string Analyzer { get; set; }
	}


	public class MoreLikeThisQueryDescriptor<T> : IMoreLikeThisQuery where T : class
	{
		IEnumerable<PropertyPathMarker> IMoreLikeThisQuery.Fields { get; set; }
		
		string IMoreLikeThisQuery.LikeText { get; set; }

		double? IMoreLikeThisQuery.TermMatchPercentage { get; set; }
	
		IEnumerable<string> IMoreLikeThisQuery.StopWords { get; set; }
		
		int? IMoreLikeThisQuery.MinTermFrequency { get; set; }
		
		int? IMoreLikeThisQuery.MaxQueryTerms { get; set; }
		
		int? IMoreLikeThisQuery.MinDocumentFrequency { get; set; }
		
		int? IMoreLikeThisQuery.MaxDocumentFrequency { get; set; }
		
		int? IMoreLikeThisQuery.MinWordLength { get; set; }
		
		int? IMoreLikeThisQuery.MaxWordLength { get; set; }
		
		double? IMoreLikeThisQuery.BoostTerms { get; set; }
		
		double? IMoreLikeThisQuery.Boost { get; set; }
		
		string IMoreLikeThisQuery.Analyzer { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IMoreLikeThisQuery)this).LikeText.IsNullOrEmpty();
			}
		}

		public MoreLikeThisQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			((IMoreLikeThisQuery)this).Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			((IMoreLikeThisQuery)this).Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> LikeText(string likeText)
		{
			((IMoreLikeThisQuery)this).LikeText = likeText;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> StopWords(IEnumerable<string> stopWords)
		{
			((IMoreLikeThisQuery)this).StopWords = stopWords;
			return this;
		}
		
		public MoreLikeThisQueryDescriptor<T> MaxQueryTerms(int maxQueryTerms)
		{
			((IMoreLikeThisQuery)this).MaxQueryTerms = maxQueryTerms;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MinTermFrequency(int minTermFrequency)
		{
			((IMoreLikeThisQuery)this).MinTermFrequency = minTermFrequency;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MinDocumentFrequency(int minDocumentFrequency)
		{
			((IMoreLikeThisQuery)this).MinDocumentFrequency = minDocumentFrequency;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MaxDocumentFrequency(int maxDocumentFrequency)
		{
			((IMoreLikeThisQuery)this).MaxDocumentFrequency = maxDocumentFrequency;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MinWordLength(int minWordLength)
		{
			((IMoreLikeThisQuery)this).MinWordLength = minWordLength;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> MaxWordLength(int maxWordLength)
		{
			((IMoreLikeThisQuery)this).MaxWordLength = maxWordLength;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> BoostTerms(double boostTerms)
		{
			((IMoreLikeThisQuery)this).BoostTerms = boostTerms;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> TermMatchPercentage(double termMatchPercentage)
		{
			((IMoreLikeThisQuery)this).TermMatchPercentage = termMatchPercentage;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> Boost(double boost)
		{
			((IMoreLikeThisQuery)this).Boost = boost;
			return this;
		}
		public MoreLikeThisQueryDescriptor<T> Analyzer(string analyzer)
		{
			((IMoreLikeThisQuery)this).Analyzer = analyzer;
			return this;
		}
	
	}
}

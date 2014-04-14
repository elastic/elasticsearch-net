using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFuzzyLikeThisQuery
	{
		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> _Fields { get; set; }

		[JsonProperty(PropertyName = "like_text")]
		string _LikeText { get; set; }

		[JsonProperty(PropertyName = "ignore_tf")]
		bool? _IgnoreTermFrequency { get; set; }

		[JsonProperty(PropertyName = "max_query_terms")]
		int? _MaxQueryTerms { get; set; }

		[JsonProperty(PropertyName = "min_similarity")]
		double? _MinSimilarity { get; set; }

		[JsonProperty(PropertyName = "prefix_length")]
		int? _PrefixLength { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? _Boost { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string _Analyzer { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FuzzyLikeThisQueryDescriptor<T> : IQuery, IFuzzyLikeThisQuery where T : class
	{
		IEnumerable<PropertyPathMarker> IFuzzyLikeThisQuery._Fields { get; set; }
		
		string IFuzzyLikeThisQuery._LikeText { get; set; }
		
		bool? IFuzzyLikeThisQuery._IgnoreTermFrequency { get; set; }
		
		int? IFuzzyLikeThisQuery._MaxQueryTerms { get; set; }
		
		double? IFuzzyLikeThisQuery._MinSimilarity { get; set; }
		
		int? IFuzzyLikeThisQuery._PrefixLength { get; set; }
		
		double? IFuzzyLikeThisQuery._Boost { get; set; }
		
		string IFuzzyLikeThisQuery._Analyzer { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IFuzzyLikeThisQuery)this)._LikeText.IsNullOrEmpty();
			}
		}

		public FuzzyLikeThisQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			((IFuzzyLikeThisQuery)this)._Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			((IFuzzyLikeThisQuery)this)._Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> LikeText(string likeText)
		{
			((IFuzzyLikeThisQuery)this)._LikeText = likeText;
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> IgnoreTermFrequency(bool ignore)
		{
			((IFuzzyLikeThisQuery)this)._IgnoreTermFrequency = ignore;
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> MinimumSimilarity(double minSimilarity)
		{
			((IFuzzyLikeThisQuery)this)._MinSimilarity = minSimilarity;
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> MaxQueryTerms(int maxQueryTerms)
		{
			((IFuzzyLikeThisQuery)this)._MaxQueryTerms = maxQueryTerms;
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			((IFuzzyLikeThisQuery)this)._PrefixLength = prefixLength;
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> Boost(double boost)
		{
			((IFuzzyLikeThisQuery)this)._Boost = boost;
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> Analyzer(string analyzer)
		{
			((IFuzzyLikeThisQuery)this)._Analyzer = analyzer;
			return this;
		}
	
	}
}

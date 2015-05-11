using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<FuzzyLikeThisQueryDescriptor<object>>))]
	public interface IFuzzyLikeThisQuery : IQuery
	{
		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> Fields { get; set; }

		[JsonProperty(PropertyName = "like_text")]
		string LikeText { get; set; }

		[JsonProperty(PropertyName = "ignore_tf")]
		bool? IgnoreTermFrequency { get; set; }

		[JsonProperty(PropertyName = "max_query_terms")]
		int? MaxQueryTerms { get; set; }

		[JsonProperty(PropertyName = "min_similarity")]
		double? MinSimilarity { get; set; }

		[JsonProperty(PropertyName = "prefix_length")]
		int? PrefixLength { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }
	}

	public class FuzzyLikeThisQuery : PlainQuery, IFuzzyLikeThisQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.FuzzyLikeThis = this;
		}
			
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public IEnumerable<PropertyPathMarker> Fields { get; set; }
		public string LikeText { get; set; }
		public bool? IgnoreTermFrequency { get; set; }
		public int? MaxQueryTerms { get; set; }
		public double? MinSimilarity { get; set; }
		public int? PrefixLength { get; set; }
		public double? Boost { get; set; }
		public string Analyzer { get; set; }
	}

	public class FuzzyLikeThisQueryDescriptor<T> : IFuzzyLikeThisQuery where T : class
	{
		private IFuzzyLikeThisQuery Self { get { return this; }}

		IEnumerable<PropertyPathMarker> IFuzzyLikeThisQuery.Fields { get; set; }
		
		string IFuzzyLikeThisQuery.LikeText { get; set; }
		
		bool? IFuzzyLikeThisQuery.IgnoreTermFrequency { get; set; }
		
		int? IFuzzyLikeThisQuery.MaxQueryTerms { get; set; }
		
		double? IFuzzyLikeThisQuery.MinSimilarity { get; set; }
		
		int? IFuzzyLikeThisQuery.PrefixLength { get; set; }
		
		double? IFuzzyLikeThisQuery.Boost { get; set; }
		
		string IFuzzyLikeThisQuery.Analyzer { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return Self.LikeText.IsNullOrEmpty();
			}
		}

		string IQuery.Name { get; set; }

		public FuzzyLikeThisQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public FuzzyLikeThisQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			Self.Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			Self.Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> LikeText(string likeText)
		{
			Self.LikeText = likeText;
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> IgnoreTermFrequency(bool ignore = true)
		{
			Self.IgnoreTermFrequency = ignore;
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> MinimumSimilarity(double minSimilarity)
		{
			Self.MinSimilarity = minSimilarity;
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> MaxQueryTerms(int maxQueryTerms)
		{
			Self.MaxQueryTerms = maxQueryTerms;
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			Self.PrefixLength = prefixLength;
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}
		public FuzzyLikeThisQueryDescriptor<T> Analyzer(string analyzer)
		{
			Self.Analyzer = analyzer;
			return this;
		}
	
	}
}

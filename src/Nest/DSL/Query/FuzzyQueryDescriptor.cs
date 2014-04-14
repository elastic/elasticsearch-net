using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{

	public interface IFuzzyQuery 
	{
		PropertyPathMarker _Field { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? _Boost { get; set; }

		[JsonProperty(PropertyName = "min_similarity")]
		double? _MinSimilarity { get; set; }

		[JsonProperty(PropertyName = "prefix_length")]
		int? _PrefixLength { get; set; }

		[JsonProperty(PropertyName = "value")]
		string Value { get; set; }

	}

	[JsonObject(MemberSerialization = MemberSerialization.OptOut)]
	public class FuzzyQueryDescriptor<T> : IQuery, IFuzzyQuery where T : class
	{
		PropertyPathMarker IFuzzyQuery._Field { get; set; }
		[JsonProperty(PropertyName = "boost")]
		double? IFuzzyQuery._Boost { get; set; }
		[JsonProperty(PropertyName = "min_similarity")]
		double? IFuzzyQuery._MinSimilarity { get; set; }
		[JsonProperty(PropertyName = "prefix_length")]
		int? IFuzzyQuery._PrefixLength { get; set; }
		[JsonProperty(PropertyName = "value")]
		string IFuzzyQuery.Value { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IFuzzyQuery)this)._Field.IsConditionless() || ((IFuzzyQuery)this).Value.IsNullOrEmpty();
			}
		}

		public FuzzyQueryDescriptor<T> OnField(string field)
		{
			((IFuzzyQuery)this)._Field = field;
			return this;
		}
		public FuzzyQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IFuzzyQuery)this)._Field = objectPath;
			return this;
		}
		public FuzzyQueryDescriptor<T> Boost(double boost)
		{
			((IFuzzyQuery)this)._Boost = boost;
			return this;
		}
		public FuzzyQueryDescriptor<T> MinSimilarity(double minSimilarity)
		{
			((IFuzzyQuery)this)._MinSimilarity = minSimilarity;
			return this;
		}
		public FuzzyQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			((IFuzzyQuery)this)._PrefixLength = prefixLength;
			return this;
		}
		public FuzzyQueryDescriptor<T> Value(string value)
		{
			((IFuzzyQuery)this).Value = value;
			return this;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;
namespace Nest
{
  public class FuzzyQueryDescriptor<T>  where T : class
	{
		internal string _Field { get; set; }
		[JsonProperty(PropertyName = "boost")]
		internal double? _Boost { get; set;}
		[JsonProperty(PropertyName = "min_similarity")]
		internal double? _MinSimilarity { get; set; }
		[JsonProperty(PropertyName = "prefix_length")]
		internal int? _PrefixLength { get; set; }
		[JsonProperty(PropertyName = "value")]
		internal string _Value { get; set; }

		public FuzzyQueryDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public FuzzyQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			var fieldName = ElasticClient.PropertyNameResolver.Resolve(objectPath);
			return this.OnField(fieldName);
		}
		public FuzzyQueryDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}
		public FuzzyQueryDescriptor<T> MinSimilarity(double minSimilarity)
		{
			this._MinSimilarity = minSimilarity;
			return this;
		}
		public FuzzyQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			this._PrefixLength = prefixLength;
			return this;
		}
		public FuzzyQueryDescriptor<T> Value(string value)
		{
			this._Value = value;
			return this;
		}
	}
}

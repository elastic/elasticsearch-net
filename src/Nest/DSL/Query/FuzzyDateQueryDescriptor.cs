using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;
namespace Nest
{
	public interface IFuzzyDateQuery
	{
		PropertyPathMarker _Field { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? _Boost { get; set; }

		[JsonProperty(PropertyName = "min_similarity")]
		string _MinSimilarity { get; set; }

		[JsonProperty(PropertyName = "value")]
		DateTime? _Value { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FuzzyDateQueryDescriptor<T> : IQuery, IFuzzyDateQuery where T : class
	{
		PropertyPathMarker IFuzzyDateQuery._Field { get; set; }
		
		[JsonProperty(PropertyName = "boost")]
		double? IFuzzyDateQuery._Boost { get; set; }
		
		[JsonProperty(PropertyName = "min_similarity")]
		string IFuzzyDateQuery._MinSimilarity { get; set; }
		
		[JsonProperty(PropertyName = "value")]
		DateTime? IFuzzyDateQuery._Value { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IFuzzyDateQuery)this)._Field.IsConditionless() || ((IFuzzyDateQuery)this)._Value == null;
			}
		}

		public FuzzyDateQueryDescriptor<T> OnField(string field)
		{
			((IFuzzyDateQuery)this)._Field = field;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IFuzzyDateQuery)this)._Field = objectPath;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Boost(double boost)
		{
			((IFuzzyDateQuery)this)._Boost = boost;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> MinSimilarity(string minSimilarity)
		{
			((IFuzzyDateQuery)this)._MinSimilarity = minSimilarity;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Value(DateTime? value)
		{
			((IFuzzyDateQuery)this)._Value = value;
			return this;
		}
	}
}

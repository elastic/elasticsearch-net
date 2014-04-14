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
	public interface IFuzzyNumericQuery
	{
		PropertyPathMarker _Field { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? _Boost { get; set; }

		[JsonProperty(PropertyName = "min_similarity")]
		double? _MinSimilarity { get; set; }

		[JsonProperty(PropertyName = "value")]
		double? _Value { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FuzzyNumericQueryDescriptor<T> : IQuery, IFuzzyNumericQuery where T : class
	{
		PropertyPathMarker IFuzzyNumericQuery._Field { get; set; }
		
		[JsonProperty(PropertyName = "boost")]
		double? IFuzzyNumericQuery._Boost { get; set; }
	
		[JsonProperty(PropertyName = "min_similarity")]
		double? IFuzzyNumericQuery._MinSimilarity { get; set; }
	
		[JsonProperty(PropertyName = "value")]
		double? IFuzzyNumericQuery._Value { get; set; }


		bool IQuery.IsConditionless
		{
			get
			{
				return ((IFuzzyNumericQuery)this)._Field.IsConditionless() || ((IFuzzyNumericQuery)this)._Value == null;
			}
		}

		public FuzzyNumericQueryDescriptor<T> OnField(string field)
		{
			((IFuzzyNumericQuery)this)._Field = field;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IFuzzyNumericQuery)this)._Field = objectPath;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> Boost(double boost)
		{
			((IFuzzyNumericQuery)this)._Boost = boost;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> MinSimilarity(double minSimilarity)
		{
			((IFuzzyNumericQuery)this)._MinSimilarity = minSimilarity;
			return this;
		}
	
		public FuzzyNumericQueryDescriptor<T> Value(int? value)
		{
			((IFuzzyNumericQuery)this)._Value = value;
			return this;
		}
	}
}

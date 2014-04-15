using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;
namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFuzzyNumericQuery : IFuzzyQuery
	{
		[JsonProperty(PropertyName = "value")]
		double? Value { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FuzzyNumericQueryDescriptor<T> : IFuzzyNumericQuery where T : class
	{
		PropertyPathMarker IFuzzyQuery.Field { get; set; }
		
		double? IFuzzyQuery.Boost { get; set; }
		
		int? IFuzzyQuery.MaxExpansions { get; set; }
		
		object IFuzzyQuery.Fuzziness { get; set; }
	
		double? IFuzzyNumericQuery.Value { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IFuzzyNumericQuery)this).Field.IsConditionless() || ((IFuzzyNumericQuery)this).Value == null;
			}
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((IFuzzyQuery)this).Field;
		}
		
		public FuzzyNumericQueryDescriptor<T> OnField(string field)
		{
			((IFuzzyNumericQuery)this).Field = field;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IFuzzyNumericQuery)this).Field = objectPath;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> Boost(double boost)
		{
			((IFuzzyNumericQuery)this).Boost = boost;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			((IFuzzyQuery)this).Fuzziness = fuzziness;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> Fuzziness(string fuzziness)
		{
			((IFuzzyQuery)this).Fuzziness = fuzziness;
			return this;
		}
		
		public FuzzyNumericQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			((IFuzzyQuery)this).MaxExpansions = maxExpansions;
			return this;
		}
	
		public FuzzyNumericQueryDescriptor<T> Value(int? value)
		{
			((IFuzzyNumericQuery)this).Value = value;
			return this;
		}
	}
}

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
	public interface IFuzzyDateQuery : IFuzzyQuery
	{
		[JsonProperty(PropertyName = "value")]
		DateTime? Value { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FuzzyDateQueryDescriptor<T> : IFuzzyDateQuery where T : class
	{
		PropertyPathMarker IFuzzyQuery.Field { get; set; }
		
		double? IFuzzyQuery.Boost { get; set; }
		
		int? IFuzzyQuery.MaxExpansions { get; set; }
		
		object IFuzzyQuery.Fuzziness { get; set; }
		
		DateTime? IFuzzyDateQuery.Value { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IFuzzyDateQuery)this).Field.IsConditionless() || ((IFuzzyDateQuery)this).Value == null;
			}
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((IFuzzyQuery)this).Field;
		}
		
		public FuzzyDateQueryDescriptor<T> OnField(string field)
		{
			((IFuzzyDateQuery)this).Field = field;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IFuzzyDateQuery)this).Field = objectPath;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Boost(double boost)
		{
			((IFuzzyDateQuery)this).Boost = boost;
			return this;
		}
			
		public FuzzyDateQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			((IFuzzyQuery)this).Fuzziness = fuzziness;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Fuzziness(string fuzziness)
		{
			((IFuzzyQuery)this).Fuzziness = fuzziness;
			return this;
		}
		
		public FuzzyDateQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			((IFuzzyQuery)this).MaxExpansions = maxExpansions;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Value(DateTime? value)
		{
			((IFuzzyDateQuery)this).Value = value;
			return this;
		}
	}
}

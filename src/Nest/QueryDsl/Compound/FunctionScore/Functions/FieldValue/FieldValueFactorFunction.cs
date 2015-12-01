using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFieldValueFactorFunction : IFunctionScoreFunction
	{
		[JsonProperty(PropertyName = "field_value_factor")]
		IFieldValueFactor FieldValueFactor { get; set; }
	}

	public class FieldValueFactorFunction : FunctionScoreFunctionBase
	{
		public IFieldValueFactor FieldValueFactor { get; set; }
	}

	public class FieldValueFactorFunctionDescriptor<T> 
		: FunctionScoreFunctionBaseDescriptor<FieldValueFactorFunctionDescriptor<T>, IFieldValueFactorFunction, T >, IFieldValueFactorFunction
		where T : class
	{
		IFieldValueFactor IFieldValueFactorFunction.FieldValueFactor { get; set; }

		public FieldValueFactorFunctionDescriptor<T> FieldValueFactor(Func<FieldValueFactorDescriptor<T>, IFieldValueFactor> selector) =>
			Assign(a => a.FieldValueFactor = selector?.Invoke(new FieldValueFactorDescriptor<T>()));
	}
}

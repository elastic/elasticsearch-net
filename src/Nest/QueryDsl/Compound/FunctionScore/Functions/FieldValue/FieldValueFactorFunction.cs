using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFieldValueFactorFunction : IScoreFunction
	{

		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("factor")]
		double? Factor { get; set; }

		[JsonProperty("modifier")]
		[JsonConverter(typeof(StringEnumConverter))]
		FieldValueFactorModifier? Modifier { get; set; }

		[JsonProperty("missing")]
		double? Missing { get; set; }

	}

	public class FieldValueFactorFunction : FunctionScoreFunctionBase,IFieldValueFactorFunction
	{
		public Field Field { get; set; }

		public double? Factor { get; set; }

		public FieldValueFactorModifier? Modifier { get; set; }

		public double? Missing { get; set; }

	}

	public class FieldValueFactorFunctionDescriptor<T> 
		: FunctionScoreFunctionDescriptorBase<FieldValueFactorFunctionDescriptor<T>, IFieldValueFactorFunction, T >, IFieldValueFactorFunction
		where T : class
	{
		Field IFieldValueFactorFunction.Field { get; set; }
		double? IFieldValueFactorFunction.Factor { get; set; }
		FieldValueFactorModifier? IFieldValueFactorFunction.Modifier { get; set; }
		double? IFieldValueFactorFunction.Missing { get; set; }

		public FieldValueFactorFunctionDescriptor<T> Field(Field field) => Assign(a => a.Field = field);
		public FieldValueFactorFunctionDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public FieldValueFactorFunctionDescriptor<T> Factor(double? factor) => Assign(a => a.Factor = factor);

		public FieldValueFactorFunctionDescriptor<T> Modifier(FieldValueFactorModifier? modifier) => Assign(a => a.Modifier = modifier);

		public FieldValueFactorFunctionDescriptor<T> Missing(double? missing) => Assign(a => a.Missing = missing);
	}
}

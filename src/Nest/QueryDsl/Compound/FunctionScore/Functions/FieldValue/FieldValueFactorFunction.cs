using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFieldValueFactorFunction : IScoreFunction
	{
		[JsonProperty("factor")]
		double? Factor { get; set; }

		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("missing")]
		double? Missing { get; set; }

		[JsonProperty("modifier")]
		[JsonConverter(typeof(StringEnumConverter))]
		FieldValueFactorModifier? Modifier { get; set; }
	}

	public class FieldValueFactorFunction : FunctionScoreFunctionBase, IFieldValueFactorFunction
	{
		public double? Factor { get; set; }
		public Field Field { get; set; }

		public double? Missing { get; set; }

		public FieldValueFactorModifier? Modifier { get; set; }
	}

	public class FieldValueFactorFunctionDescriptor<T>
		: FunctionScoreFunctionDescriptorBase<FieldValueFactorFunctionDescriptor<T>, IFieldValueFactorFunction, T>, IFieldValueFactorFunction
		where T : class
	{
		double? IFieldValueFactorFunction.Factor { get; set; }
		Field IFieldValueFactorFunction.Field { get; set; }
		double? IFieldValueFactorFunction.Missing { get; set; }
		FieldValueFactorModifier? IFieldValueFactorFunction.Modifier { get; set; }

		public FieldValueFactorFunctionDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public FieldValueFactorFunctionDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(field, (a, v) => a.Field = v);

		public FieldValueFactorFunctionDescriptor<T> Factor(double? factor) => Assign(factor, (a, v) => a.Factor = v);

		public FieldValueFactorFunctionDescriptor<T> Modifier(FieldValueFactorModifier? modifier) => Assign(modifier, (a, v) => a.Modifier = v);

		public FieldValueFactorFunctionDescriptor<T> Missing(double? missing) => Assign(missing, (a, v) => a.Missing = v);
	}
}

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[DataContract]
	public interface IFieldValueFactorFunction : IScoreFunction
	{
		[DataMember(Name ="factor")]
		double? Factor { get; set; }

		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="missing")]
		double? Missing { get; set; }

		[DataMember(Name ="modifier")]

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

		public FieldValueFactorFunctionDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public FieldValueFactorFunctionDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public FieldValueFactorFunctionDescriptor<T> Factor(double? factor) => Assign(a => a.Factor = factor);

		public FieldValueFactorFunctionDescriptor<T> Modifier(FieldValueFactorModifier? modifier) => Assign(a => a.Modifier = modifier);

		public FieldValueFactorFunctionDescriptor<T> Missing(double? missing) => Assign(a => a.Missing = missing);
	}
}

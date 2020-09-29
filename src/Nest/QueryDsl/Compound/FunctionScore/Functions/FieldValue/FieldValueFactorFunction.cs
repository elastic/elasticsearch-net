// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;


namespace Nest
{
	[InterfaceDataContract]
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

		public FieldValueFactorFunctionDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public FieldValueFactorFunctionDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		public FieldValueFactorFunctionDescriptor<T> Factor(double? factor) => Assign(factor, (a, v) => a.Factor = v);

		public FieldValueFactorFunctionDescriptor<T> Modifier(FieldValueFactorModifier? modifier) => Assign(modifier, (a, v) => a.Modifier = v);

		public FieldValueFactorFunctionDescriptor<T> Missing(double? missing) => Assign(missing, (a, v) => a.Missing = v);
	}
}

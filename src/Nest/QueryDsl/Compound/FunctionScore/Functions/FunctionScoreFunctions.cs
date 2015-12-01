using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public class FunctionScoreFunctionsDescriptor<T> : DescriptorPromiseBase<FunctionScoreFunctionsDescriptor<T>, IList<IFunctionScoreFunction>>
		where T : class
	{
		public FunctionScoreFunctionsDescriptor() : base(new List<IFunctionScoreFunction>()) { }

		public FunctionScoreFunctionsDescriptor<T> Gauss(Func<GaussFunctionDescriptor<T>, IGaussFunction> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new GaussFunctionDescriptor<T>())));

		public FunctionScoreFunctionsDescriptor<T> Linear(Func<LinearFunctionDescriptor<T>, ILinearFunction> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new LinearFunctionDescriptor<T>())));

		public FunctionScoreFunctionsDescriptor<T> Exp(Func<ExpFunctionDescriptor<T>, IExpFunction> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ExpFunctionDescriptor<T>())));

		public FunctionScoreFunctionsDescriptor<T> BoostFactor(Func<BoostFactorFunctionDescriptor<T>, IBoostFactorFunction> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new BoostFactorFunctionDescriptor<T>())));

		public FunctionScoreFunctionsDescriptor<T> ScriptScoreFactor(Func<ScriptScoreFunctionDescriptor<T>, IScriptScoreFunction> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ScriptScoreFunctionDescriptor<T>())));

		public FunctionScoreFunctionsDescriptor<T> FieldValueFactor(Func<FieldValueFactorFunctionDescriptor<T>, IFieldValueFactorFunction> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new FieldValueFactorFunctionDescriptor<T>())));

		public FunctionScoreFunctionsDescriptor<T> Weight(Func<WeightFunctionDescriptor<T>, IWeightFunction> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new WeightFunctionDescriptor<T>())));

		public FunctionScoreFunctionsDescriptor<T> Weight(double weight) => Assign(a => a.AddIfNotNull(new WeightFunction { Weight = weight }));

	}
}
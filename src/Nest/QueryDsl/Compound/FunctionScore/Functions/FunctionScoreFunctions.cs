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

		public FunctionScoreFunctionsDescriptor<T> Gauss(Func<GaussDecayFunctionDescriptor<double?, double?, T>, IDecayFunction<double?, double?>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new GaussDecayFunctionDescriptor<double?, double?, T>())));

		public FunctionScoreFunctionsDescriptor<T> GaussDate(Func<GaussDecayFunctionDescriptor<DateMath, TimeUnitExpression, T>, IDecayFunction<DateMath, TimeUnitExpression>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new GaussDecayFunctionDescriptor<DateMath, TimeUnitExpression, T>())));

		public FunctionScoreFunctionsDescriptor<T> GaussGeoLocation(Func<GaussDecayFunctionDescriptor<GeoLocation, GeoDistance, T>, IDecayFunction<GeoLocation, GeoDistance>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new GaussDecayFunctionDescriptor<GeoLocation, GeoDistance, T>())));

		public FunctionScoreFunctionsDescriptor<T> Linear(Func<LinearDecayFunctionDescriptor<double?, double?, T>, IDecayFunction<double?, double?>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new LinearDecayFunctionDescriptor<double?, double?, T>())));

		public FunctionScoreFunctionsDescriptor<T> LinearDate(Func<LinearDecayFunctionDescriptor<DateMath, TimeUnitExpression, T>, IDecayFunction<DateMath, TimeUnitExpression>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new LinearDecayFunctionDescriptor<DateMath, TimeUnitExpression, T>())));

		public FunctionScoreFunctionsDescriptor<T> LinearGeoLocation(Func<LinearDecayFunctionDescriptor<GeoLocation, GeoDistance, T>, IDecayFunction<GeoLocation, GeoDistance>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new LinearDecayFunctionDescriptor<GeoLocation, GeoDistance, T>())));

		public FunctionScoreFunctionsDescriptor<T> Exponential(Func<ExponentialDecayFunctionDescriptor<double?, double?, T>, IDecayFunction<double?, double?>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ExponentialDecayFunctionDescriptor<double?, double?, T>())));

		public FunctionScoreFunctionsDescriptor<T> ExponentialDate(Func<ExponentialDecayFunctionDescriptor<DateMath, TimeUnitExpression, T>, IDecayFunction<DateMath, TimeUnitExpression>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ExponentialDecayFunctionDescriptor<DateMath, TimeUnitExpression, T>())));

		public FunctionScoreFunctionsDescriptor<T> ExponentialGeoLocation(Func<ExponentialDecayFunctionDescriptor<GeoLocation, GeoDistance, T>, IDecayFunction<GeoLocation, GeoDistance>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ExponentialDecayFunctionDescriptor<GeoLocation, GeoDistance, T>())));

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
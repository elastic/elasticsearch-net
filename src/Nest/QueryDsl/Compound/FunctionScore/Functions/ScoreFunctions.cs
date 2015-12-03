using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public class ScoreFunctionsDescriptor<T> : DescriptorPromiseBase<ScoreFunctionsDescriptor<T>, IList<IScoreFunction>>
		where T : class
	{
		public ScoreFunctionsDescriptor() : base(new List<IScoreFunction>()) { }

		public ScoreFunctionsDescriptor<T> Gauss(Func<GaussDecayFunctionDescriptor<double?, double?, T>, IDecayFunction<double?, double?>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new GaussDecayFunctionDescriptor<double?, double?, T>())));

		public ScoreFunctionsDescriptor<T> GaussDate(Func<GaussDecayFunctionDescriptor<DateMath, TimeUnitExpression, T>, IDecayFunction<DateMath, TimeUnitExpression>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new GaussDecayFunctionDescriptor<DateMath, TimeUnitExpression, T>())));

		public ScoreFunctionsDescriptor<T> GaussGeoLocation(Func<GaussDecayFunctionDescriptor<GeoLocation, GeoDistance, T>, IDecayFunction<GeoLocation, GeoDistance>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new GaussDecayFunctionDescriptor<GeoLocation, GeoDistance, T>())));

		public ScoreFunctionsDescriptor<T> Linear(Func<LinearDecayFunctionDescriptor<double?, double?, T>, IDecayFunction<double?, double?>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new LinearDecayFunctionDescriptor<double?, double?, T>())));

		public ScoreFunctionsDescriptor<T> LinearDate(Func<LinearDecayFunctionDescriptor<DateMath, TimeUnitExpression, T>, IDecayFunction<DateMath, TimeUnitExpression>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new LinearDecayFunctionDescriptor<DateMath, TimeUnitExpression, T>())));

		public ScoreFunctionsDescriptor<T> LinearGeoLocation(Func<LinearDecayFunctionDescriptor<GeoLocation, GeoDistance, T>, IDecayFunction<GeoLocation, GeoDistance>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new LinearDecayFunctionDescriptor<GeoLocation, GeoDistance, T>())));

		public ScoreFunctionsDescriptor<T> Exponential(Func<ExponentialDecayFunctionDescriptor<double?, double?, T>, IDecayFunction<double?, double?>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ExponentialDecayFunctionDescriptor<double?, double?, T>())));

		public ScoreFunctionsDescriptor<T> ExponentialDate(Func<ExponentialDecayFunctionDescriptor<DateMath, TimeUnitExpression, T>, IDecayFunction<DateMath, TimeUnitExpression>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ExponentialDecayFunctionDescriptor<DateMath, TimeUnitExpression, T>())));

		public ScoreFunctionsDescriptor<T> ExponentialGeoLocation(Func<ExponentialDecayFunctionDescriptor<GeoLocation, GeoDistance, T>, IDecayFunction<GeoLocation, GeoDistance>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ExponentialDecayFunctionDescriptor<GeoLocation, GeoDistance, T>())));

		public ScoreFunctionsDescriptor<T> ScriptScore(Func<ScriptScoreFunctionDescriptor<T>, IScriptScoreFunction> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ScriptScoreFunctionDescriptor<T>())));

		public ScoreFunctionsDescriptor<T> FieldValueFactor(Func<FieldValueFactorFunctionDescriptor<T>, IFieldValueFactorFunction> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new FieldValueFactorFunctionDescriptor<T>())));

		public ScoreFunctionsDescriptor<T> RandomScore(Func<RandomScoreFunctionDescriptor<T>, IRandomScoreFunction> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new RandomScoreFunctionDescriptor<T>())));

		public ScoreFunctionsDescriptor<T> RandomScore(int? seed) => Assign(a => a.AddIfNotNull(new RandomScoreFunction { Seed = seed }));

		public ScoreFunctionsDescriptor<T> Weight(Func<WeightFunctionDescriptor<T>, IWeightFunction> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new WeightFunctionDescriptor<T>())));

		public ScoreFunctionsDescriptor<T> Weight(double weight) => Assign(a => a.AddIfNotNull(new WeightFunction { Weight = weight }));

	}
}
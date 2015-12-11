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

		public ScoreFunctionsDescriptor<T> GaussDate(Func<GaussDecayFunctionDescriptor<DateMath, TimeUnit, T>, IDecayFunction<DateMath, TimeUnit>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new GaussDecayFunctionDescriptor<DateMath, TimeUnit, T>())));

		public ScoreFunctionsDescriptor<T> GaussGeoLocation(Func<GaussDecayFunctionDescriptor<GeoLocation, DistanceUnit, T>, IDecayFunction<GeoLocation, DistanceUnit>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new GaussDecayFunctionDescriptor<GeoLocation, DistanceUnit, T>())));

		public ScoreFunctionsDescriptor<T> Linear(Func<LinearDecayFunctionDescriptor<double?, double?, T>, IDecayFunction<double?, double?>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new LinearDecayFunctionDescriptor<double?, double?, T>())));

		public ScoreFunctionsDescriptor<T> LinearDate(Func<LinearDecayFunctionDescriptor<DateMath, TimeUnit, T>, IDecayFunction<DateMath, TimeUnit>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new LinearDecayFunctionDescriptor<DateMath, TimeUnit, T>())));

		public ScoreFunctionsDescriptor<T> LinearGeoLocation(Func<LinearDecayFunctionDescriptor<GeoLocation, DistanceUnit, T>, IDecayFunction<GeoLocation, DistanceUnit>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new LinearDecayFunctionDescriptor<GeoLocation, DistanceUnit, T>())));

		public ScoreFunctionsDescriptor<T> Exponential(Func<ExponentialDecayFunctionDescriptor<double?, double?, T>, IDecayFunction<double?, double?>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ExponentialDecayFunctionDescriptor<double?, double?, T>())));

		public ScoreFunctionsDescriptor<T> ExponentialDate(Func<ExponentialDecayFunctionDescriptor<DateMath, TimeUnit, T>, IDecayFunction<DateMath, TimeUnit>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ExponentialDecayFunctionDescriptor<DateMath, TimeUnit, T>())));

		public ScoreFunctionsDescriptor<T> ExponentialGeoLocation(Func<ExponentialDecayFunctionDescriptor<GeoLocation, DistanceUnit, T>, IDecayFunction<GeoLocation, DistanceUnit>> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new ExponentialDecayFunctionDescriptor<GeoLocation, DistanceUnit, T>())));

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
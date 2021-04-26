/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;

namespace Nest
{
	public class ScoreFunctionsDescriptor<T> : DescriptorPromiseBase<ScoreFunctionsDescriptor<T>, IList<IScoreFunction>>
		where T : class
	{
		public ScoreFunctionsDescriptor() : base(new List<IScoreFunction>()) { }

		public ScoreFunctionsDescriptor<T> Gauss(Func<GaussDecayFunctionDescriptor<double?, double?, T>, IDecayFunction<double?, double?>> selector
		) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new GaussDecayFunctionDescriptor<double?, double?, T>())));

		public ScoreFunctionsDescriptor<T> GaussDate(Func<GaussDecayFunctionDescriptor<DateMath, Time, T>, IDecayFunction<DateMath, Time>> selector
		) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new GaussDecayFunctionDescriptor<DateMath, Time, T>())));

		public ScoreFunctionsDescriptor<T> GaussGeoLocation(
			Func<GaussDecayFunctionDescriptor<GeoLocation, Distance, T>, IDecayFunction<GeoLocation, Distance>> selector
		) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new GaussDecayFunctionDescriptor<GeoLocation, Distance, T>())));

		public ScoreFunctionsDescriptor<T> Linear(Func<LinearDecayFunctionDescriptor<double?, double?, T>, IDecayFunction<double?, double?>> selector
		) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new LinearDecayFunctionDescriptor<double?, double?, T>())));

		public ScoreFunctionsDescriptor<T> LinearDate(Func<LinearDecayFunctionDescriptor<DateMath, Time, T>, IDecayFunction<DateMath, Time>> selector
		) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new LinearDecayFunctionDescriptor<DateMath, Time, T>())));

		public ScoreFunctionsDescriptor<T> LinearGeoLocation(
			Func<LinearDecayFunctionDescriptor<GeoLocation, Distance, T>, IDecayFunction<GeoLocation, Distance>> selector
		) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new LinearDecayFunctionDescriptor<GeoLocation, Distance, T>())));

		public ScoreFunctionsDescriptor<T> Exponential(
			Func<ExponentialDecayFunctionDescriptor<double?, double?, T>, IDecayFunction<double?, double?>> selector
		) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new ExponentialDecayFunctionDescriptor<double?, double?, T>())));

		public ScoreFunctionsDescriptor<T> ExponentialDate(
			Func<ExponentialDecayFunctionDescriptor<DateMath, Time, T>, IDecayFunction<DateMath, Time>> selector
		) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new ExponentialDecayFunctionDescriptor<DateMath, Time, T>())));

		public ScoreFunctionsDescriptor<T> ExponentialGeoLocation(
			Func<ExponentialDecayFunctionDescriptor<GeoLocation, Distance, T>, IDecayFunction<GeoLocation, Distance>> selector
		) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new ExponentialDecayFunctionDescriptor<GeoLocation, Distance, T>())));

		public ScoreFunctionsDescriptor<T> ScriptScore(Func<ScriptScoreFunctionDescriptor<T>, IScriptScoreFunction> selector) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new ScriptScoreFunctionDescriptor<T>())));

		public ScoreFunctionsDescriptor<T> FieldValueFactor(Func<FieldValueFactorFunctionDescriptor<T>, IFieldValueFactorFunction> selector) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new FieldValueFactorFunctionDescriptor<T>())));

		public ScoreFunctionsDescriptor<T> RandomScore(Func<RandomScoreFunctionDescriptor<T>, IRandomScoreFunction> selector = null) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v.InvokeOrDefault(new RandomScoreFunctionDescriptor<T>())));

		public ScoreFunctionsDescriptor<T> Weight(Func<WeightFunctionDescriptor<T>, IWeightFunction> selector) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new WeightFunctionDescriptor<T>())));

		public ScoreFunctionsDescriptor<T> Weight(double weight) =>
			Assign(weight, (a, v) => a.AddIfNotNull(new WeightFunction { Weight = v }));
	}
}

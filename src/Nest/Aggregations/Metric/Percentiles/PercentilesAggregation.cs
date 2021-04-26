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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(PercentilesAggregationFormatter))]
	public interface IPercentilesAggregation : IFormattableMetricAggregation
	{
		IPercentilesMethod Method { get; set; }
		IEnumerable<double> Percents { get; set; }
		bool? Keyed { get; set; }
	}

	public class PercentilesAggregation : FormattableMetricAggregationBase, IPercentilesAggregation
	{
		internal PercentilesAggregation() { }

		public PercentilesAggregation(string name, Field field) : base(name, field) { }

		public IPercentilesMethod Method { get; set; }
		public IEnumerable<double> Percents { get; set; }
		public bool? Keyed { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Percentiles = this;
	}

	public class PercentilesAggregationDescriptor<T>
		: FormattableMetricAggregationDescriptorBase<PercentilesAggregationDescriptor<T>, IPercentilesAggregation, T>
			, IPercentilesAggregation
		where T : class
	{
		IPercentilesMethod IPercentilesAggregation.Method { get; set; }
		IEnumerable<double> IPercentilesAggregation.Percents { get; set; }
		bool? IPercentilesAggregation.Keyed { get; set; }

		public PercentilesAggregationDescriptor<T> Percents(IEnumerable<double> percentages) =>
			Assign(percentages, (a, v) => a.Percents = v);

		public PercentilesAggregationDescriptor<T> Percents(params double[] percentages) =>
			Assign(percentages, (a, v) => a.Percents = v);

		public PercentilesAggregationDescriptor<T> Method(Func<PercentilesMethodDescriptor, IPercentilesMethod> methodSelector) =>
			Assign(methodSelector, (a, v) => a.Method = v?.Invoke(new PercentilesMethodDescriptor()));

		public PercentilesAggregationDescriptor<T> Keyed(bool? keyed = true) =>
			Assign(keyed, (a, v) => a.Keyed = v);
	}
}

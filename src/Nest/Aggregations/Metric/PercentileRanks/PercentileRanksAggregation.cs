// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(PercentileRanksAggregationFormatter))]
	public interface IPercentileRanksAggregation : IFormattableMetricAggregation
	{
		IPercentilesMethod Method { get; set; }
		IEnumerable<double> Values { get; set; }
		bool? Keyed { get; set; }
	}

	public class PercentileRanksAggregation : FormattableMetricAggregationBase, IPercentileRanksAggregation
	{
		internal PercentileRanksAggregation() { }

		public PercentileRanksAggregation(string name, Field field) : base(name, field) { }

		public IPercentilesMethod Method { get; set; }
		public IEnumerable<double> Values { get; set; }
		public bool? Keyed { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.PercentileRanks = this;
	}

	public class PercentileRanksAggregationDescriptor<T>
		: FormattableMetricAggregationDescriptorBase<PercentileRanksAggregationDescriptor<T>, IPercentileRanksAggregation, T>, IPercentileRanksAggregation
		where T : class
	{
		IPercentilesMethod IPercentileRanksAggregation.Method { get; set; }
		IEnumerable<double> IPercentileRanksAggregation.Values { get; set; }
		bool? IPercentileRanksAggregation.Keyed { get; set; }

		public PercentileRanksAggregationDescriptor<T> Values(IEnumerable<double> values) =>
			Assign(values, (a, v) => a.Values = v);

		public PercentileRanksAggregationDescriptor<T> Values(params double[] values) =>
			Assign(values, (a, v) => a.Values = v);

		public PercentileRanksAggregationDescriptor<T> Method(Func<PercentilesMethodDescriptor, IPercentilesMethod> methodSelctor) =>
			Assign(methodSelctor, (a, v) => a.Method = v?.Invoke(new PercentilesMethodDescriptor()));

		public PercentileRanksAggregationDescriptor<T> Keyed(bool? keyed = true) =>
			Assign(keyed, (a, v) => a.Keyed = v);
	}
}

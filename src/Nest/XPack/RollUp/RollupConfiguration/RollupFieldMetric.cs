// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(RollupFieldMetric))]
	public interface IRollupFieldMetric
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="metrics")]
		IEnumerable<RollupMetric> Metrics { get; set; }
	}

	public class RollupFieldMetric : IRollupFieldMetric
	{
		public Field Field { get; set; }
		public IEnumerable<RollupMetric> Metrics { get; set; }
	}

	public class RollupFieldMetricsDescriptor<T> : DescriptorPromiseBase<RollupFieldMetricsDescriptor<T>, IList<IRollupFieldMetric>>
		where T : class
	{
		public RollupFieldMetricsDescriptor() : base(new List<IRollupFieldMetric>()) { }

		public RollupFieldMetricsDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field, params RollupMetric[] metrics) =>
			Assign(new RollupFieldMetric { Field = field, Metrics = metrics }, (a, v) => a.Add(v));

		public RollupFieldMetricsDescriptor<T> Field(Field field, params RollupMetric[] metrics) =>
			Assign(new RollupFieldMetric { Field = field, Metrics = metrics }, (a, v) => a.Add(v));

		public RollupFieldMetricsDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field, IEnumerable<RollupMetric> metrics) =>
			Assign(new RollupFieldMetric { Field = field, Metrics = metrics }, (a, v) => a.Add(v));

		public RollupFieldMetricsDescriptor<T> Field(Field field, IEnumerable<RollupMetric> metrics) =>
			Assign(new RollupFieldMetric { Field = field, Metrics = metrics }, (a, v) => a.Add(v));
	}
}

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

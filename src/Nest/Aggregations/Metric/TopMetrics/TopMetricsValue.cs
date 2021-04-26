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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The configuration for a field or script that provides a value or weight
	/// for <see cref="TopMetricsAggregation" />
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(TopMetricsValue))]
	public interface ITopMetricsValue
	{
		/// <summary>
		/// The field that values should be extracted from
		/// </summary>
		[DataMember(Name = "field")]
		Field Field { get; set; }
	}

	/// <inheritdoc />
	public class TopMetricsValue : ITopMetricsValue
	{
		internal TopMetricsValue() { }

		public TopMetricsValue(Field field) => Field = field;

		/// <inheritdoc />
		public Field Field { get; set; }
	}

	/// <inheritdoc cref="ITopMetricsAggregation" />
	public class TopMetricsValuesDescriptor<T> : DescriptorPromiseBase<TopMetricsValuesDescriptor<T>, IList<ITopMetricsValue>>
		where T : class
	{
		public TopMetricsValuesDescriptor() : base(new List<ITopMetricsValue>()) { }

		public TopMetricsValuesDescriptor<T> Field(Field field) => AddTopMetrics(new TopMetricsValue { Field = field });

		public TopMetricsValuesDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) =>
			AddTopMetrics(new TopMetricsValue { Field = field});

		private TopMetricsValuesDescriptor<T> AddTopMetrics(ITopMetricsValue TopMetrics) => TopMetrics == null ? this : Assign(TopMetrics, (a, v) => a.Add(v));
	}

}

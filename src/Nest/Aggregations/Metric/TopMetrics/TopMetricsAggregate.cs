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

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class TopMetricsAggregate : MetricAggregateBase
	{
		public IReadOnlyCollection<TopMetric> Top { get; internal set; } = EmptyReadOnly<TopMetric>.Collection;
	}

	public class TopMetric
	{
		/// <summary>
		/// The sort values used in sorting the hit relative to other hits
		/// </summary>
		[DataMember(Name = "sort")]
		public IReadOnlyCollection<object> Sort { get; internal set; }

		/// <summary>
		/// The metrics.
		/// </summary>
		[DataMember(Name = "metrics")]
		public IReadOnlyDictionary<string, object> Metrics { get; internal set; }
	}
}

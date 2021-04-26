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

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class MetricsEqualityTests
	{
		[U] public void Eq()
		{
			Metrics metrics = IndicesStatsMetric.All;
			Metrics[] equal = { IndicesStatsMetric.All };
			foreach (var t in equal)
			{
				(t == metrics).ShouldBeTrue(t);
				t.Should().Be(metrics);
			}
			metrics.Should().Be(IndicesStatsMetric.All);
		}

		[U] public void NotEq()
		{
			Metrics metrics = IndicesStatsMetric.All;
			Metrics[] notEqual = { IndicesStatsMetric.Completion, ClusterStateMetric.All };
			foreach (var t in notEqual)
			{
				(t != metrics).ShouldBeTrue(t);
				t.Should().NotBe(metrics);
			}
			metrics.Should().NotBe(ClusterStateMetric.All);
		}

		[U] public void Null()
		{
			Metrics value = IndicesStatsMetric.All;
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}

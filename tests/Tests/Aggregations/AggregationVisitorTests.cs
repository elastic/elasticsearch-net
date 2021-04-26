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

using System.Linq;
using System.Reflection;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.Aggregations
{
	public class AggregationVisitorTests
	{
		[U] public void VisitMethodForEachTypeOfAggregation()
		{
			// exclude intermediate aggregations
			var exclude = new[]
			{
				typeof(IMetricAggregation), typeof(IBucketAggregation), typeof(IPipelineAggregation), typeof(IMatrixAggregation),
				typeof(IFormattableMetricAggregation)
			};

			var aggregationTypes =
				(from t in typeof(IAggregation).Assembly.Types()
				where typeof(IAggregation).IsAssignableFrom(t)
				where t.IsInterface && !exclude.Contains(t)
				select t).ToList();

			var visitorMethodParameters =
				(from m in typeof(IAggregationVisitor).GetMethods()
				where m.Name == "Visit"
				let aggregationInterface = m.GetParameters().First().ParameterType
				where aggregationInterface != typeof(IAggregationContainer)
				select aggregationInterface).ToList();

			if (aggregationTypes.Count < visitorMethodParameters.Count)
				visitorMethodParameters.Except(aggregationTypes).Should().BeEmpty();
			else
				aggregationTypes.Except(visitorMethodParameters).Should().BeEmpty();
		}
	}
}

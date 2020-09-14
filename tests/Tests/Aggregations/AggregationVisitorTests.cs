// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

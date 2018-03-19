using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class IndexMetricsEqualityTests
	{
		[U] public void Eq()
		{
			IndexMetrics metrics = NodesStatsIndexMetric.All;
			IndexMetrics[] equal = {NodesStatsIndexMetric.All};
			foreach (var t in equal)
			{
				(t == metrics).ShouldBeTrue(t);
				t.Should().Be(metrics);
			}
			metrics.Should().Be(NodesStatsIndexMetric.All);
		}

		[U] public void NotEq()
		{
			IndexMetrics metrics = NodesStatsIndexMetric.All;
			IndexMetrics[] notEqual = {NodesStatsIndexMetric.Flush};
			foreach (var t in notEqual)
			{
				(t != metrics).ShouldBeTrue(t);
				t.Should().NotBe(metrics);
			}
			metrics.Should().NotBe(NodesStatsMetric.All);
		}

		[U] public void Null()
		{
			IndexMetrics value = NodesStatsIndexMetric.All;
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}

using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class MetricsEqualityTests
	{
		[U] public void Eq()
		{
			Metrics metrics = IndicesStatsMetric.All;
			Metrics[] equal = {IndicesStatsMetric.All};
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
			Metrics[] notEqual = {IndicesStatsMetric.Completion, ClusterStateMetric.All};
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

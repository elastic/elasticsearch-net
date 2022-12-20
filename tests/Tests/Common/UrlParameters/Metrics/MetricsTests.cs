// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Tests.Core.Extensions;

using M = Elastic.Clients.Elasticsearch.Metrics;

namespace Tests.Common.UrlParameters.Metrics;

public class MetricsTests
{
	[U]
	public void Equal()
	{
		var metrics = M.All;

		M[] equal = { M.All };

		foreach (var value in equal)
		{
			(value == metrics).ShouldBeTrue(value);
			value.Should().Be(metrics);
		}

		metrics.Should().Be(M.All);
	}

	[U]
	public void SequenceEqual()
	{
		M metricsOne = new[] { "completion", "merge" };
		M metricsTwo = new[] { "merge", "completion" };

		(metricsOne == metricsTwo).Should().BeTrue();
		metricsOne.Should().Be(metricsTwo);
	}

	[U]
	public void ToStringOverride()
	{
		M metrics = new[] { "completion", "merge" };
		metrics.ToString().Should().Be("completion,merge");
	}

	[U]
	public void NotEqual()
	{
		var metrics = M.All;

		M[] notEqual = { "completion", "merge" };

		foreach (var value in notEqual)
		{
			(value != metrics).ShouldBeTrue(value);
			value.Should().NotBe(metrics);
		}
	}

	[U]
	public void Null()
	{
		var value = M.All;
		(value == null).Should().BeFalse();
		(null == value).Should().BeFalse();
	}
}

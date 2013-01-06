using System;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Cluster
{
	[TestFixture]
	public class HealthTests : BaseJsonTests
	{
		[Test]
		public void ClusterHealth()
		{
			var r = this._client.Health(HealthLevel.Cluster);
			var u = new Uri(r.ConnectionStatus.RequestUrl);
			u.AbsolutePath.Should().StartWith("/_cluster/health");
			u.Query.Should().Contain("level=cluster");
		}
		[Test]
		public void ClusterHealthPerIndex()
		{
			var r = this._client.Health(new[] { Test.Default.DefaultIndex }, HealthLevel.Cluster);
			var u = new Uri(r.ConnectionStatus.RequestUrl);
			u.AbsolutePath.Should().StartWith("/_cluster/health/nest_test_data");
			u.Query.Should().Contain("level=cluster");
		}
		[Test]
		public void IndexHealth()
		{
			var r = this._client.Health(HealthLevel.Indices);
			var u = new Uri(r.ConnectionStatus.RequestUrl);
			u.AbsolutePath.Should().StartWith("/_cluster/health");
			u.Query.Should().Contain("level=indices");
		}
		[Test]
		public void ShardHealth()
		{
			var r = this._client.Health(HealthLevel.Shards);
			var u = new Uri(r.ConnectionStatus.RequestUrl);
			u.AbsolutePath.Should().StartWith("/_cluster/health");
			u.Query.Should().Contain("level=shards");
		}
		[Test]
		public void DetailedHealth()
		{
			var r = this._client.Health(new HealthParams
				{
					CheckLevel = HealthLevel.Shards,
					Timeout = "30s",
					WaitForMinNodes = 1,
					WaitForRelocatingShards = 0,
					WaitForStatus = HealthStatus.Green
				});
			var u = new Uri(r.ConnectionStatus.RequestUrl);
			u.AbsolutePath.Should().StartWith("/_cluster/health");
			u.Query.Should().Contain("level=shards");
			u.Query.Should().Contain("timeout=30s");
			u.Query.Should().Contain("wait_for_nodes=1");
			u.Query.Should().Contain("wait_for_relocating_shards=0");
			u.Query.Should().Contain("wait_for_status=green");
		}
	}
}
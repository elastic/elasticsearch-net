using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class GatewaySnapshotTests : IntegrationTests
	{
		[Test]
		public void GatewaySnapshotAll()
		{
			var r = this._client.GatewaySnapshot();
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void GatewaySnapshotIndex()
		{
			var r = this._client.GatewaySnapshot(s=>s.Index(ElasticsearchConfiguration.DefaultIndex));
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void GatewaySnapshotIndices()
		{
			var r = this._client.GatewaySnapshot(s=>s.Indices(
				ElasticsearchConfiguration.DefaultIndex, 
				ElasticsearchConfiguration.DefaultIndex + "_clone")
			);
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void GatewaySnapshotTyped()
		{
			var r = this._client.GatewaySnapshot(s=>s.Index<ElasticsearchProject>());
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
	}
}
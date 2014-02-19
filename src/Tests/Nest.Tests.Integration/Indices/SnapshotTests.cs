using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class SnapshotTests : IntegrationTests
	{
		[Test]
		public void SnapshotAll()
		{
			var r = this._client.Snapshot();
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void SnapshotIndex()
		{
			var r = this._client.Snapshot(s=>s.Index(ElasticsearchConfiguration.DefaultIndex));
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void SnapshotIndices()
		{
			var r = this._client.Snapshot(s=>s.Indices(
				ElasticsearchConfiguration.DefaultIndex, 
				ElasticsearchConfiguration.DefaultIndex + "_clone")
			);
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void SnapshotTyped()
		{
			var r = this._client.Snapshot(s=>s.Index<ElasticsearchProject>());
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
	}
}
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
			Assert.True(r.OK);
		}
		[Test]
		public void SnapshotIndex()
		{
			var r = this._client.Snapshot(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.OK);
		}
		[Test]
		public void SnapshotIndeces()
		{
			var r = this._client.Snapshot(
				new []{ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "_clone" });
			Assert.True(r.OK);
		}
		[Test]
		public void SnapshotTyped()
		{
			var r = this._client.Snapshot<ElasticSearchProject>();
			Assert.True(r.OK);
		}
	}
}
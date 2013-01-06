using NUnit.Framework;

namespace Nest.Tests.Integration.Cluster
{
	[TestFixture]
	public class HealthTests : IntegrationTests
	{
		[Test]
		public void ClusterHealth()
		{
			var r = this._client.Health(HealthLevel.Cluster);
			Assert.True(r.IsValid);
		}
		[Test]
		public void ClusterHealthPerIndex()
		{
			var r = this._client.Health(new[] { Test.Default.DefaultIndex }, HealthLevel.Cluster);
			Assert.True(r.IsValid);
		}
		[Test]
		public void IndexHealth()
		{
			var r = this._client.Health(HealthLevel.Indices);
			Assert.True(r.IsValid);
		}
		[Test]
		public void ShardHealth()
		{
			var r = this._client.Health(HealthLevel.Shards);
			Assert.True(r.IsValid);
		}
		[Test]
		public void DetailedHealth()
		{
			var r = this._client.Health(new HealthParams
				{
					CheckLevel = HealthLevel.Shards,
					Timeout = "30s",
					WaitForMinNodes = 1,
					WaitForRelocatingShards = 0
				});
			Assert.True(r.IsValid);
		}
		[Test]
		public void DetailedHealthPerIndex()
		{
			var r = this._client.Health(new[] { Test.Default.DefaultIndex },
												new HealthParams
													{
														CheckLevel = HealthLevel.Shards,
														Timeout = "30s",
														WaitForMinNodes = 1,
														WaitForRelocatingShards = 0
													});
			Assert.True(r.IsValid);
		}
	}
}
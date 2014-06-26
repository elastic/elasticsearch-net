using Elasticsearch.Net;
using NUnit.Framework;

namespace Nest.Tests.Integration.Cluster
{
	[TestFixture]
	public class PutSettingsTests : IntegrationTests
	{
		[Test]
		public void PutSettings()
		{
			var r = this.Client.ClusterSettings(p=>p
				.FlatSettings()
				.Transient(t=>t
					.Add("discovery.zen.minimum_master_nodes", 1)
				)
			);
			Assert.True(r.IsValid);
			Assert.True(r.Acknowledged);
			Assert.AreEqual(r.Transient["discovery.zen.minimum_master_nodes"], "1");
		}
		
	}
}
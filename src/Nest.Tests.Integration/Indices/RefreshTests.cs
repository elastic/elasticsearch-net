using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class RefreshTests : IntegrationTests
	{
		[Test]
		public void RefreshAll()
		{
			var r = this._client.Refresh();
			Assert.True(r.OK);
		}
		[Test]
		public void RefreshIndex()
		{
			var r = this._client.Refresh(e=>e.Index(ElasticsearchConfiguration.DefaultIndex));
			Assert.True(r.OK);
		}
		[Test]
		public void RefreshIndeces()
		{
			var r = this._client.Refresh(rr=>rr
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "_clone" )
			);
			Assert.True(r.OK);
		}
		[Test]
		public void RefreshTyped()
		{
			var r = this._client.Refresh(rr => rr.Index<ElasticsearchProject>());
			Assert.True(r.OK);
		}
	}
}
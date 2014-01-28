using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class ExistsTest : IntegrationTests
	{
		[Test]
		public void ShouldNotExist()
		{
			var r = this._client.IndexExists(f=>f.Index("yadadadadadaadada"));
			Assert.False(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}
		[Test]
		public void ShouldExist()
		{
			var r = this._client.IndexExists(f=>f.Index(ElasticsearchConfiguration.DefaultIndex));
			Assert.True(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}
	}
}
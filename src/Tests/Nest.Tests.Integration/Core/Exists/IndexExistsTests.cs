using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Exists
{
	[TestFixture]
	public class IndexExistsTest : IntegrationTests
	{
		[Test]
		public void ShouldNotExist()
		{
			var r = this.Client.IndexExists(f=>f.Index("yadadadadadaadada"));
			Assert.False(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}
		[Test]
		public void ShouldExist()
		{
			var r = this.Client.IndexExists(f=>f.Index(ElasticsearchConfiguration.DefaultIndex));
			Assert.True(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}
	}
}
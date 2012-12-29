using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class ExistsTest : BaseElasticSearchTests
	{
		[Test]
		public void ShouldNotExist()
		{
			var r = this._client.IndexExists("yadadadadadaadada");
			Assert.False(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}
		[Test]
		public void ShouldExist()
		{
			var r = this._client.IndexExists(this.Settings.DefaultIndex);
			Assert.True(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}
	}
}
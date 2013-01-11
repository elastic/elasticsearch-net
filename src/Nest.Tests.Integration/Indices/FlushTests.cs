using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class FlushTests : IntegrationTests
	{
		[Test]
		public void FlushAll()
		{
			var r = this._client.Flush();
			Assert.True(r.OK, r.ConnectionStatus.ToString());
		}
		[Test]
		public void FlushIndex()
		{
			var r = this._client.Flush(Test.Default.DefaultIndex);
			Assert.True(r.OK);
		}
		[Test]
		public void FlushIndeces()
		{
			var r = this._client.Flush(
				new[] { Test.Default.DefaultIndex, Test.Default.DefaultIndex + "_clone" });
			Assert.True(r.OK);
		}
		[Test]
		public void FlushTyped()
		{
			var r = this._client.Flush<ElasticSearchProject>();
			Assert.True(r.OK);
		}
		[Test]
		public void FlushAllRefresh()
		{
			var r = this._client.Flush(true);
			Assert.True(r.OK);
		}
		[Test]
		public void FlushIndexRefresh()
		{
			var r = this._client.Flush(Test.Default.DefaultIndex, true);
			Assert.True(r.OK);
		}
		[Test]
		public void FlushIndecesRefresh()
		{
			var r = this._client.Flush(
				new[] { Test.Default.DefaultIndex, Test.Default.DefaultIndex + "_clone" }, true);
			Assert.True(r.OK);
		}
		[Test]
		public void FlushTypedRefresh()
		{
			var r = this._client.Flush<ElasticSearchProject>(true);
			Assert.True(r.OK);
		}
	}
}
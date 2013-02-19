using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class OptimizeTests : IntegrationTests
	{
		[Test]
		public void OptimizeAll()
		{
			var r = this._client.Optimize();
			Assert.True(r.OK, r.ConnectionStatus.ToString());
		}
		[Test]
		public void OptimizeIndex()
		{
			var r = this._client.Optimize(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeIndeces()
		{
			var r = this._client.Optimize(new []{ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "_clone" });
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeTyped()
		{
			var r = this._client.Optimize<ElasticSearchProject>();
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeAllWithParameters()
		{
			var r = this._client.Optimize(new OptimizeParams());
			Assert.True(r.OK, r.ConnectionStatus.ToString());
		}
		[Test]
		public void OptimizeIndexWithParameters()
		{
			var r = this._client.Optimize(ElasticsearchConfiguration.DefaultIndex, new OptimizeParams());
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeIndecesWithParameters()
		{
			var r = this._client.Optimize(new[] { ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "_clone" }, new OptimizeParams());
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeTypedWithParameters()
		{
			var r = this._client.Optimize<ElasticSearchProject>(new OptimizeParams());
			Assert.True(r.OK);
		}

	}
}
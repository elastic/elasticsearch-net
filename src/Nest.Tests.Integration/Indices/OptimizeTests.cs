using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class OptimizeTests : BaseElasticSearchTests
	{
		[Test]
		public void OptimizeAll()
		{
			var r = this._client.Optimize();
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeIndex()
		{
			var r = this._client.Optimize(Test.Default.DefaultIndex);
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeIndeces()
		{
			var r = this._client.Optimize(new []{Test.Default.DefaultIndex, Test.Default.DefaultIndex + "_clone" });
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
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeIndexWithParameters()
		{
			var r = this._client.Optimize(Test.Default.DefaultIndex, new OptimizeParams());
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeIndecesWithParameters()
		{
			var r = this._client.Optimize(new[] { Test.Default.DefaultIndex, Test.Default.DefaultIndex + "_clone" }, new OptimizeParams());
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
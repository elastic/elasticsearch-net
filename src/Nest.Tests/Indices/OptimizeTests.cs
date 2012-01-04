using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;
using Nest.Mapping;
using Nest.Domain;

namespace Nest.Tests.Search
{
	[TestFixture]
	public class OptimizeTests : BaseElasticSearchTests
	{
		[Test]
		public void OptimizeAll()
		{
			var r = this.ConnectedClient.Optimize();
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeIndex()
		{
			var r = this.ConnectedClient.Optimize(Test.Default.DefaultIndex);
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeIndeces()
		{
			var r = this.ConnectedClient.Optimize(new []{Test.Default.DefaultIndex, Test.Default.DefaultIndex + "_clone" });
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeTyped()
		{
			var r = this.ConnectedClient.Optimize<ElasticSearchProject>();
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeAllWithParameters()
		{
			var r = this.ConnectedClient.Optimize(new OptimizeParams());
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeIndexWithParameters()
		{
			var r = this.ConnectedClient.Optimize(Test.Default.DefaultIndex, new OptimizeParams());
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeIndecesWithParameters()
		{
			var r = this.ConnectedClient.Optimize(new[] { Test.Default.DefaultIndex, Test.Default.DefaultIndex + "_clone" }, new OptimizeParams());
			Assert.True(r.OK);
		}
		[Test]
		public void OptimizeTypedWithParameters()
		{
			var r = this.ConnectedClient.Optimize<ElasticSearchProject>(new OptimizeParams());
			Assert.True(r.OK);
		}

	}
}
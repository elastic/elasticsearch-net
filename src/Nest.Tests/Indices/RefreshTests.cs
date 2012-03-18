using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;


namespace Nest.Tests.Search
{
	[TestFixture]
	public class RefreshTests : BaseElasticSearchTests
	{
		[Test]
		public void RefreshAll()
		{
			var r = this.ConnectedClient.Refresh();
			Assert.True(r.OK);
		}
		[Test]
		public void RefreshIndex()
		{
			var r = this.ConnectedClient.Refresh(Test.Default.DefaultIndex);
			Assert.True(r.OK);
		}
		[Test]
		public void RefreshIndeces()
		{
			var r = this.ConnectedClient.Refresh(
				new []{Test.Default.DefaultIndex, Test.Default.DefaultIndex + "_clone" });
			Assert.True(r.OK);
		}
		[Test]
		public void RefreshTyped()
		{
			var r = this.ConnectedClient.Refresh<ElasticSearchProject>();
			Assert.True(r.OK);
		}
	}
}
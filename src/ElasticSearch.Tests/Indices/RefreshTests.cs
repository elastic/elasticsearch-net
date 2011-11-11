using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;
using ElasticSearch.Client.Mapping;

namespace ElasticSearch.Tests.Search
{
	[TestFixture]
	public class RefreshTests : BaseElasticSearchTests
	{
		[Test]
		public void RefreshAll()
		{
			var countResults = this.ConnectedClient.Refresh();
			Assert.True(countResults.OK);
		}
		[Test]
		public void RefreshIndex()
		{
			var countResults = this.ConnectedClient.Refresh(Test.Default.DefaultIndex);
			Assert.True(countResults.OK);
		}
		[Test]
		public void RefreshIndeces()
		{
			var countResults = this.ConnectedClient.Refresh(
				new []{Test.Default.DefaultIndex, Test.Default.DefaultIndex + "_clone" });
			Assert.True(countResults.OK);
		}
		[Test]
		public void RefreshTyped()
		{
			var countResults = this.ConnectedClient.Refresh<ElasticSearchProject>();
			Assert.True(countResults.OK);
		}
	}
}
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
	public class OpenCloseTests : BaseElasticSearchTests
	{
		[Test]
		public void CloseAndOpenIndex()
		{
			var r = this.ConnectedClient.CloseIndex(Test.Default.DefaultIndex);
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
			r = this.ConnectedClient.OpenIndex(Test.Default.DefaultIndex);
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
		}
		[Test]
		public void CloseAndOpenIndexTyped()
		{
			var r = this.ConnectedClient.CloseIndex<ElasticSearchProject>();
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
			r = this.ConnectedClient.OpenIndex<ElasticSearchProject>();
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
		}
	}
}
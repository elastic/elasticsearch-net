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
	public class PercolateTests : BaseElasticSearchTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void RegisterPercolateTest()
		{
			var name = "mypercolator";
			var c = this.ConnectedClient;
			var r = c.RegisterPercolate<ElasticSearchProject>(
				name, 
				@"{
					""query"" : {
						""term"" : {
							""field1"" : ""value1""
						}
					}
				}"
			);
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			Assert.AreEqual(r.Type, this.Settings.DefaultIndex);
			Assert.AreEqual(r.Id, name);
			Assert.Greater(r.Version, 0);
		}
		[Test]
		public void UnregisterPercolateTest()
		{
			var name = "mypercolator";
			var c = this.ConnectedClient;
			var r = c.RegisterPercolate<ElasticSearchProject>(
				name,
				@"{
					""query"" : {
						""term"" : {
							""field1"" : ""value1""
						}
					}
				}"
			);
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			Assert.AreEqual(r.Type, this.Settings.DefaultIndex);
			Assert.AreEqual(r.Id, name);
			Assert.Greater(r.Version, 0);

			var re = c.UnregisterPercolate<ElasticSearchProject>(name);
			Assert.True(re.IsValid);
			Assert.True(re.OK);
			Assert.True(re.Found);
			Assert.AreEqual(re.Type, this.Settings.DefaultIndex);
			Assert.AreEqual(re.Id, name);
			Assert.Greater(re.Version, 0);
			re = c.UnregisterPercolate<ElasticSearchProject>(name);
			Assert.True(re.IsValid);
			Assert.True(re.OK);
			Assert.False(re.Found);
		}
	}
}
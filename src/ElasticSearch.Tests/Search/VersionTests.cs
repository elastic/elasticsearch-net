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
	public class VersionTests : BaseElasticSearchTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void SimpleVersion()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
					@" {
						""version"": true,
						""query"" : {
							""match_all"" : { }
					} }"
				);

			Assert.True(queryResults.DocumentsWithMetaData.All(h=>!h.Version.IsNullOrEmpty()));
		}
	
	}
}
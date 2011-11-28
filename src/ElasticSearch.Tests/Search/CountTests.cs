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
	public class CountTests : BaseElasticSearchTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;


		[Test]
		public void SimpleCount()
		{
			//does a match_all on the default specified index
			var countResults = this.ConnectedClient.Count();

			Assert.True(countResults.Count > 0);
		}

		[Test]
		public void SimpleQueryCount()
		{
			//does a match_all on the default specified index
			var countResults = this.ConnectedClient.Count(@"{ ""fuzzy"" : {
							""followers.firstName"" : """ + this._LookFor.ToLower() + @"x""
					}
				}");

			Assert.True(countResults.Count > 0);
		}

		[Test]
		public void SimpleQueryWithIndexAndTypeCount()
		{
			//does a match_all on the default specified index
			var countResults = this.ConnectedClient.Count(@"{ ""fuzzy"" : {
							""followers.firstName"" : """ + this._LookFor.ToLower() + @"x""
					}
				}", "nest_test_data", "elasticsearchprojects");


			Assert.True(countResults.Count > 0);
		}


		[Test]
		public void SimpleTypedCount()
		{
			//does a count over the default index/whatever T resolves to as type name
			var countResults = this.ConnectedClient.Count<ElasticSearchProject>();

			Assert.True(countResults.Count > 0);
		}
		[Test]
		public void QueryTypedCount()
		{
			var countResults = this.ConnectedClient.Count<ElasticSearchProject>(
				@"{ ""fuzzy"" : {
							""followers.firstName"" : """ + this._LookFor.ToLower() + @"x""
					}
				}"
			);

			Assert.True(countResults.Count > 0);
		}
	
	}
}
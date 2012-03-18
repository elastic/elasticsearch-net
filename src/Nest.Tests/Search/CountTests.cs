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
	public class CountTests : BaseElasticSearchTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;


		[Test]
		public void SimpleCount()
		{
			//does a match_all on the default specified index
			var countResults = this.ConnectedClient.CountAll(q=>q.MatchAll());

			Assert.True(countResults.Count > 0);
		}

		[Test]
		public void SimpleQueryCount()
		{
			//does a match_all on the default specified index
			var countResults = this.ConnectedClient.CountAll(@"{ ""fuzzy"" : {
							""followers.firstName"" : """ + this._LookFor.ToLower() + @"x""
					}
				}");

			Assert.True(countResults.Count > 0);
		}

		[Test]
		public void SimpleQueryWithIndexAndTypeCount()
		{
			//does a match_all on the default specified index
			var countResults = this.ConnectedClient.Count<ElasticSearchProject>(q=>q
				.Fuzzy(fq=>fq
					.OnField(f=>f.Followers.First().FirstName)
					.Value(this._LookFor.ToLower())
				)
			);
			Assert.True(countResults.Count > 0);
		}


		[Test]
		public void SimpleTypedCount()
		{
			//does a count over the default index/whatever T resolves to as type name
			var countResults = this.ConnectedClient.Count<ElasticSearchProject>(q=>q.MatchAll());

			Assert.True(countResults.Count > 0);
		}
	
	}
}
using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search
{
	[TestFixture]
	public class CountTests : BaseElasticSearchTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;


		[Test]
		public void SimpleCount()
		{
			//does a match_all on the default specified index
			var countResults = this._client.CountAll(q=>q.MatchAll());

			Assert.True(countResults.Count > 0);
		}

		[Test]
		public void SimpleQueryCount()
		{
			var countResults = this._client.CountAll<ElasticSearchProject>(q=>q
				.Fuzzy(fq=>fq
					.Value(this._LookFor.ToLower())
					.OnField(f=>f.Followers.First().FirstName)			
				)
			);
			Assert.True(countResults.Count > 0);
		}

		[Test]
		public void SimpleQueryWithIndexAndTypeCount()
		{
			//does a match_all on the default specified index
			var countResults = this._client.Count<ElasticSearchProject>(q=>q
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
			var countResults = this._client.Count<ElasticSearchProject>(q=>q.MatchAll());

			Assert.True(countResults.Count > 0);
		}
	
	}
}
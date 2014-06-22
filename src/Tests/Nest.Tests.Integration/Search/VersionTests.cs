using Elasticsearch.Net;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using Shared.Extensions;
using System.Linq;

namespace Nest.Tests.Integration.Search
{
	[TestFixture]
	public class VersionTests : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void WithVersion()
		{
			var queryResults = this._client.Search<ElasticsearchProject>(s=>s
				.Version()
				.MatchAll() //not explicitly needed.
			);
			Assert.True(queryResults.IsValid);
			Assert.Greater(queryResults.Total, 0);
			Assert.True(queryResults.Hits.All(h => !h.Version.IsNullOrEmpty()));
		}
		[Test]
		public void NoVersion()
		{
			var queryResults = this._client.Search<ElasticsearchProject>(s => s
				   .Version(false)
				   .MatchAll() //not explicitly needed.
			   );

			Assert.True(queryResults.IsValid);
			Assert.Greater(queryResults.Total, 0);
			Assert.True(queryResults.Hits.All(h => h.Version.IsNullOrEmpty()));
		}
	}
}
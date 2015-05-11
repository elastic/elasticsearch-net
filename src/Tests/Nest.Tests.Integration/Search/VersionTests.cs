using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.Search
{
	[TestFixture]
	public class VersionTests : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void WithVersion()
		{
			var queryResults = this.Client.Search<ElasticsearchProject>(s=>s
				.Version()
				.MatchAll() //not explicitly needed.
			);
			Assert.True(queryResults.IsValid);
			Assert.Greater(queryResults.Total, 0);
			Assert.True(queryResults.Hits.All(h => h.Version > 0));
		}
		[Test]
		public void NoVersion()
		{
			var queryResults = this.Client.Search<ElasticsearchProject>(s => s
				   .Version(false)
				   .MatchAll() //not explicitly needed.
			   );

			Assert.True(queryResults.IsValid);
			Assert.Greater(queryResults.Total, 0);
			Assert.True(queryResults.Hits.All(h => h.Version == null));
		}
	}
}
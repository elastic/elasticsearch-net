using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search
{
	[TestFixture]
	public class VersionTests : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void SimpleVersion()
		{
			var queryResults = this._client.SearchRaw<ElasticSearchProject>(
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
using System.Linq;
using ElasticSearch.Client;
using NUnit.Framework;
using Nest.TestData;
using Nest.TestData.Domain;

namespace ElasticSearch.Tests.FacetResponses
{
    public class BaseFacetTestFixture : BaseElasticSearchTests
    {
        protected string _LookFor = NestTestData.Data.First().Followers.First().LastName.ToLower();

        protected void TestDefaultAssertions(QueryResponse<ElasticSearchProject> queryResponse)
        {
            Assert.True(queryResponse.IsValid);
            Assert.NotNull(queryResponse.ConnectionStatus);
            Assert.Null(queryResponse.ConnectionStatus.Error);
            Assert.True(queryResponse.Total > 0, "No hits");
            Assert.True(queryResponse.Documents.Any());
            Assert.True(queryResponse.Documents.Count() > 0);
            Assert.True(queryResponse.Shards.Total > 0);
            Assert.True(queryResponse.Shards.Successful == queryResponse.Shards.Total);
            Assert.True(queryResponse.Shards.Failed == 0);
        }
    }
}
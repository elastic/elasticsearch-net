using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using ElasticSearch.Client;
using HackerNews.Indexer.Domain;
using Nest.TestData;
using Nest.TestData.Domain;

namespace ElasticSearch.Tests
{
	/// <summary>
	///  Tests that test whether the query response can be successfully mapped or not
	/// </summary>
	public class QueryResponseMapperTests : BaseElasticSearchTests
	{
		[Fact]
		public void BogusQuery()
		{
			var client = this.ConnectedClient;
			QueryResponse<Post> queryResults = client.Search<Post>(
				@"here be dragons"
			);
			Assert.False(queryResults.IsValid);
			Assert.True(queryResults.ConnectionError.HttpStatusCode == System.Net.HttpStatusCode.InternalServerError);
		}
		[Fact]
		public void SimpleFuzzyQuery()
		{
			var client = this.ConnectedClient;
			var data = NestTestData.Data;
			var lookFor = data.First().Followers.First().FirstName;

			var queryResults = client.Search<ElasticSearchProject>(
				@" { ""query"" : {  ""fuzzy"" : {  ""followers.firstName"" : """+ lookFor +@"""  } } }"
			);
			Assert.True(queryResults.IsValid);
			Assert.Null(queryResults.ConnectionError);
			Assert.True(queryResults.Total > 0);
			Assert.True(queryResults.Documents.Any());
			Assert.True(queryResults.Documents.Count() > 0);
			Assert.True(queryResults.Documents.First().Followers
				.Any(f=>f.FirstName.Equals(lookFor,StringComparison.InvariantCultureIgnoreCase)));
		}
		[Fact]
		public void ExtendedFuzzyQuery()
		{
			var client = this.ConnectedClient;
			var data = NestTestData.Data;
			var lookFor = data.First().Followers.First().FirstName;

			var queryResults = client.Search<ElasticSearchProject>(
				@" { ""query"" : {
						""fuzzy"" : { 
						   ""followers.firstName"" : {
								""value"" : """ + lookFor + @""",
								""boost"" : 1.0,
								""min_similarity"" : 0.5,
								""prefix_length"" : 0
							}
						}
					} }"
			);
			Assert.True(queryResults.IsValid);
			Assert.Null(queryResults.ConnectionError);
			Assert.True(queryResults.Total > 0);
			Assert.True(queryResults.Documents.Any());
			Assert.True(queryResults.Documents.Count() > 0);
			Assert.True(queryResults.Documents.First().Followers
				.Any(f => f.FirstName.Equals(lookFor, StringComparison.InvariantCultureIgnoreCase)));
		}


	}
}

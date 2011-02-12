using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using ElasticSearch.Client;
using HackerNews.Indexer.Domain;

namespace ElasticSearch.Tests
{
	/// <summary>
	///  Tests that test wheter the query response can be succesfully mapped
	/// </summary>
	public class QueryResponseMapperTests : BaseElasticSearchTests
	{
		[Fact]
		public void BogusQuery()
		{
			var client = this.ConnectedClient;
			QueryResponse<Post> queryResults = client.Search<Post>(
				@"asdadsadads"
			);
			Assert.False(queryResults.IsValid);
			Assert.True(queryResults.ConnectionError.HttpStatusCode == System.Net.HttpStatusCode.InternalServerError);
		}
		[Fact]
		public void SimpleFuzzyQuery()
		{
			var client = this.ConnectedClient;
			QueryResponse<Post> queryResults = client.Search<Post>(
				@"query : {  ""fuzzy"" : {  ""meta.username"" : ""ki""  } }"
			);
			Assert.True(queryResults.IsValid);
			Assert.Null(queryResults.ConnectionError);
		}


	}
}

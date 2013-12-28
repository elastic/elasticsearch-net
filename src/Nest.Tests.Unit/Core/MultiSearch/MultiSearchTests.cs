using System;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.MultiSearch
{
	[TestFixture]
	public class MultiSearchTests : BaseJsonTests
	{
		[Test]
		public void MultiSearchNonFixed()
		{
			var result = this._client.MultiSearch(m=>m
				.Search<ElasticSearchProject>(s=>s.MatchAll())	
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/_msearch");
		}
		[Test]
		public void MultiSearchFixedIndex()
		{
			var result = this._client.MultiSearch(b => b
				.FixedPath("myindex")
				.Search<ElasticSearchProject>(s => s.MatchAll())
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/_msearch");
		}
		[Test]
		public void MultiSearchFixedIndexAndType()
		{
			var result = this._client.MultiSearch(b => b
				.FixedPath("myindex", "mytype")
				.Search<ElasticSearchProject>(s => s.MatchAll())	
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/mytype/_msearch");
		}
		[Test]
		public void MultiSearchRespectsSearchTypePreferenceAndRouting()
		{
			var result = this._client.MultiSearch(b => b
				.FixedPath("myindex", "mytype") 
				.Search<ElasticSearchProject>(s => s.MatchAll().Preference("_primary").Routing("customvalue1").SearchType(SearchType.DfsQueryAndFetch))
				.Search<Person>(s => s.MatchAll().Preference("_primary_first").Routing("customvalue2").SearchType(SearchType.Count))
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/mytype/_msearch");
			const string first = @"{""index"":""myindex"",""type"":""mytype"",""search_type"":""dfs_query_and_fetch"",""preference"":""_primary"",""routing"":""customvalue1""}";
			const string second = @"{""index"":""myindex"",""type"":""mytype"",""search_type"":""count"",""preference"":""_primary_first"",""routing"":""customvalue2""}";

            StringAssert.Contains(first, status.Request);
            StringAssert.Contains(second, status.Request); 
			

		}
		
	}
}

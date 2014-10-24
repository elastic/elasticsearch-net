using System;
using Elasticsearch.Net;
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
			var result = this._client.MultiSearch(m => m
				.Search<ElasticsearchProject>(s => s.MatchAll())
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
				.Search<ElasticsearchProject>(s => s.MatchAll())
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/_msearch");
		}
		[Test]
		public void ShouldNotThrowWhenTypeIsSpecified()
		{
			// see: https://github.com/elasticsearch/elasticsearch-net/issues/523
			var indexName = "multisearch-with-type-name-error";
			var typeName = "product";
			Assert.DoesNotThrow(() =>
			{
				var result = this._client.MultiSearch(b => b
					.Search<ElasticsearchProject>("name", s=>s.Index(indexName).Type(typeName).MatchAll())
				);
			});
		}
		[Test]
		public void MultiSearchFixedIndexAndType()
		{
			var result = this._client.MultiSearch(b => b
				.FixedPath("myindex", "mytype")
				.Search<ElasticsearchProject>(s => s.MatchAll())
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/mytype/_msearch");
		}
		[Test]
		public void MultiSearchHeaderParts()
		{
			var result = this._client.MultiSearch(b => b
				.FixedPath("myindex", "mytype")
				.Search<ElasticsearchProject>(s => s
					.IgnoreUnavailable()
					.Index("myindex2")
					.Type("mytype2")
					.MatchAll()
					.Preference("_primary")
					.Routing("customvalue1")
					.SearchType(SearchType.DfsQueryAndFetch))
				.Search<Person>(s => s.MatchAll()
					.Index("myindex2")
					.Type("mytype2")
					.Preference("_primary_first")
					.Routing("customvalue2")
					.SearchType(SearchType.Count))
				.Search<ElasticsearchProject>(s => s
					.IgnoreUnavailable()
					.MatchAll()
					.Preference("_primary")
					.Routing("customvalue1")
					.SearchType(SearchType.DfsQueryAndFetch))
				.Search<Person>(s => s.MatchAll()
					.Preference("_primary_first")
					.Routing("customvalue2")
					.SearchType(SearchType.Count))
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/mytype/_msearch");
			var results = new[]
			{
				@"{""index"":""myindex2"",""type"":""mytype2"",""search_type"":""dfs_query_and_fetch"",""preference"":""_primary"",""routing"":""customvalue1"",""ignore_unavailable"":true}",
				@"{""index"":""myindex2"",""type"":""mytype2"",""search_type"":""count"",""preference"":""_primary_first"",""routing"":""customvalue2""}",
				@"{""type"":""mytype"",""search_type"":""dfs_query_and_fetch"",""preference"":""_primary"",""routing"":""customvalue1"",""ignore_unavailable"":true}",
				@"{""type"":""mytype"",""search_type"":""count"",""preference"":""_primary_first"",""routing"":""customvalue2""}"
			};

			foreach (var resultItem in results)
			{
				StringAssert.Contains(resultItem, status.Request.Utf8String());
			}
		}

	}
}

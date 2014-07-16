using Elasticsearch.Net;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.MoreLikeThis
{
	[TestFixture]
	public class MltUrlTests : BaseJsonTests
	{
		[Test]
		public void MltSimpleById()
		{
			var result = this._client.MoreLikeThis<ElasticsearchProject>(mlt => mlt.Id(1));
			var status = result.ConnectionStatus;
			StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.ResponseRaw.Utf8String());
			StringAssert.EndsWith("/nest_test_data/elasticsearchprojects/1/_mlt", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("GET", status.RequestMethod);
		}
		[Test]
		public void MltSimpleByObject()
		{
			var result = this._client.MoreLikeThis<ElasticsearchProject>(mlt => mlt.Id(new ElasticsearchProject { Id = 1 }));
			var status = result.ConnectionStatus;
			StringAssert.EndsWith("/nest_test_data/elasticsearchprojects/1/_mlt", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("GET", status.RequestMethod);
		}
		[Test]
		public void MltSimpleByObjectAlternativeIndexAndType()
		{
			var result = this._client.MoreLikeThis<ElasticsearchProject>(mlt => mlt
				.Index("someotherindex")
				.Type("sillytypename")
				.Id(new ElasticsearchProject { Id = 1 })
			);
			var status = result.ConnectionStatus;
			StringAssert.EndsWith("/someotherindex/sillytypename/1/_mlt", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("GET", status.RequestMethod);
		}
		[Test]
		public void MltOptions()
		{
			var result = this._client.MoreLikeThis<ElasticsearchProject>(mlt => mlt
				.Id(1)
				.MltFields(p => p.Country, p => p.Content)
				.PercentTermsToMatch(0.3)
				.MinTermFreq(2)
				.MaxQueryTerms(25)
				.StopWords(new[] { "c#", "es" })
				.MinDocFreq(5)
				.MaxDocFreq(200)
				.MinWordLength(2)
				.MaxWordLength(200)
				.BoostTerms(1.4)
			);
			var status = result.ConnectionStatus;
			StringAssert.Contains("mlt_fields=country%2Ccontent", status.RequestUrl);
			StringAssert.Contains("stop_words=c%23%2Ces", status.RequestUrl);
			StringAssert.Contains("percent_terms_to_match=0.3", status.RequestUrl);
			StringAssert.Contains("min_term_freq=2", status.RequestUrl);
			StringAssert.Contains("max_query_terms=25", status.RequestUrl);
			StringAssert.Contains("min_doc_freq=5", status.RequestUrl);
			StringAssert.Contains("max_doc_freq=200", status.RequestUrl);
			StringAssert.Contains("min_word_length=2", status.RequestUrl);
			StringAssert.Contains("boost_terms=1.4", status.RequestUrl);

		}
		[Test]
		public void SetSearchUrlOptions()
		{
			var result = this._client.MoreLikeThis<ElasticsearchProject>(mlt => mlt
				.Id(1)
				.MltFields(p => p.Country, p => p.Content)
				.Search(s => s
					.SearchType(SearchType.DfsQueryAndFetch)
					.Scroll("5m")
				)
			);
			var status = result.ConnectionStatus;
			StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
			StringAssert.Contains("mlt_fields=country%2Ccontent", status.RequestUrl);
			StringAssert.Contains("search_type=dfs_query_and_fetch", status.RequestUrl);
			StringAssert.Contains("scroll=5m", status.RequestUrl);
			//We are using the search descriptor so this should trigger the POST
		}
	}
}

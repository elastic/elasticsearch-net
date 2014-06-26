using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.Integration
{
	[TestFixture]
	public class HighlightIntegrationTests : IntegrationTests
	{
		[Test]
		public void TestHighlight()
		{
			var result = this.Client.Search<ElasticsearchProject>(s => s
			  .From(0)
			  .Size(10)
			  .Query(q => q
				.QueryString(qs => qs
				  .DefaultField(e => e.Content)
				  .Query("null or null*")
				)
			  )
			  .Highlight(h => h
				.PreTags("<b>")
				.PostTags("</b>")
				.OnFields(
				  f => f
					.OnField(e => e.Content)
					.PreTags("<em>")
					.PostTags("</em>")
					
				)
			  )
			);
			Assert.IsTrue(result.IsValid);
			Assert.DoesNotThrow(() => result.Highlights.Count());
			Assert.IsNotNull(result.Highlights);
			Assert.Greater(result.Highlights.Count(), 0);
			Assert.True(result.Highlights.All(h => h.Value != null));

			Assert.True(result.Highlights.All(h => h.Value.All(hh => !hh.Value.DocumentId.IsNullOrEmpty())));

			var id = result.Documents.First().Id.ToString();
			var highlights = result.Highlights[id];	
			Assert.NotNull(highlights);
			Assert.NotNull(highlights["content"]);
			Assert.Greater(highlights["content"].Highlights.Count(), 0);

			highlights = result.Hits.First().Highlights;
			Assert.NotNull(highlights);
			Assert.NotNull(highlights["content"]);
			Assert.Greater(highlights["content"].Highlights.Count(), 0);
		}

		[Test]
		public void TestHighlightNoNullRef()
		{
			var result = this.Client.Search<ElasticsearchProject>(s => s
			  .From(0)
			  .Size(10)
			  .Query(q => q
				.QueryString(qs => qs
				  .Query("elasticsearch.pm")
				)
			  )
			  .Highlight(h => h
				.PreTags("<b>")
				.PostTags("</b>")
				.OnFields(
				  f => f
					.OnField(e => e.Content)
					.PreTags("<em>")
					.PostTags("</em>")
				)
			  )
			);
			Assert.IsTrue(result.IsValid);
			Assert.DoesNotThrow(() => result.Highlights.Count());
			Assert.IsNotNull(result.Highlights);
			Assert.GreaterOrEqual(result.Total, 1);
			Assert.AreEqual(result.Highlights.Count(), 0);
		}
	}
}

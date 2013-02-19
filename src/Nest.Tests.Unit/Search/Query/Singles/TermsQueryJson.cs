using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class TermsQueryJson
	{
		[Test]
		public void TermsQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff
					.Terms(f => f.Name, new[] { "elasticsearch.pm" })
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						terms: {
							""name"": [""elasticsearch.pm""]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TermsQueryParams()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff.Terms(f => f.Name, "elasticsearch.pm", "nest")
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						terms: {
							""name"": [""elasticsearch.pm"", ""nest""],
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TermsQueryDescriptor()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff.
					TermsDescriptor(tq => tq
						.OnField(f=>f.Name).Terms("elasticsearch.pm", "nest")
						.MinimumMatch(2)
						.DisableCoord()
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						terms: {
							""name"": [""elasticsearch.pm"", ""nest""],
							minimum_match: 2,
							disable_coord : true
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

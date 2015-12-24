using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class SpanFirstQueryJson
	{
		[Test]
		public void SpanFirstQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.SpanFirst(sf=>sf
						.Name("named_query")
						.MatchTerm(f => f.Name, "elasticsearch.pm", 1.1)
						.End(3)
                        .Boost(2.2)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{
				span_first: { 
					_name: ""named_query"",
					match: { 
						span_term: { 
							name: {
								value: ""elasticsearch.pm"",
								boost: 1.1
							}
						}
					},
					end:3,
                    boost:2.2
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

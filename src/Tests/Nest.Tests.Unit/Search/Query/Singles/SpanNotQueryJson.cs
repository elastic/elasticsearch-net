using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class SpanNotQueryJson
	{
		[Test]
		public void SpanNotQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.SpanNot(sf => sf
						.Pre(1)
						.Post(2)
						.Dist(3)
						.Include(e => e.SpanTerm(f => f.Name, "elasticsearch.pm", 1.1))
						.Exclude(e => e.SpanTerm(f => f.Name, "elasticsearch.pm", 1.1))
						.Boost(2.2)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{
				span_not: { 
					include: { 
						span_term: { 
							name: {
								value: ""elasticsearch.pm"",
								boost: 1.1
							}
						}
					},
					exclude: { 
						span_term: { 
							name: {
								value: ""elasticsearch.pm"",
								boost: 1.1
							}
						}
					},
					boost: 2.2,
					pre: 1,
					post: 2,
					dist: 3
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

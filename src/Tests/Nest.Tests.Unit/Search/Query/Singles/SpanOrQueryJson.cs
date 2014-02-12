using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class SpanOrQueryJson
	{
		[Test]
		public void SpanOrQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.SpanOr(sn => sn
						.Clauses(
							c => c.SpanTerm(f => f.Name, "elasticsearch.pm", 1.1),
							c => c.SpanFirst(sf => sf
								.MatchTerm(f => f.Name, "elasticsearch.pm", 1.1)
								.End(3)
							)
						)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{
				span_or: { 
					clauses: 
					[{
						span_term: { 
								name: {
									value: ""elasticsearch.pm"",
									boost: 1.1
								}
							}
						},
						{ 
							span_first: {
								match: {
									span_term: {
										name: {
											value: ""elasticsearch.pm"",
											boost: 1.1
										}
									}
								},
								end: 3
							}
						}
					]
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

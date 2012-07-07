using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.QueryTests
{
	[TestFixture]
	public class SpanNearQueryJson
	{
		[Test]
		public void SpanNearQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.SpanNear(sn => sn
						.Clauses(
							c => c.SpanTerm(f => f.Name, "elasticsearch.pm", 1.1),
							c => c.SpanFirst(sf => sf
								.MatchTerm(f => f.Name, "elasticsearch.pm", 1.1)
								.End(3)
							)
						)
						.Slop(3)
						.CollectPayloads(false)
						.InOrder(false)
					)
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{
				span_near: { 
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
					],
					slop: 3,
					in_order: false,
					collect_payloads: false
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.QueryTests
{
	[TestFixture]
	public class SpanFirstQueryJson
	{
		[Test]
		public void SpanFirstQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.SpanFirst(sf=>sf
						.MatchTerm(f => f.Name, "elasticsearch.pm", 1.1)
						.End(3)
					)
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
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
					end:3
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

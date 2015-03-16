using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class SpanTermQueryJson
	{
		[Test]
		public void SpanTermQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.SpanTerm(f=>f.Name, "elasticsearch.pm", 1.1)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ 
				span_term: { 
					name: {
						value: ""elasticsearch.pm"",
						boost: 1.1
					}
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void SpanTermQueryNamed()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.SpanTerm(sp=>sp.OnField(p=>p.Name).Value("elasticsearch.pm").Name("named_query"))
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ 
				span_term: { 
					_name: ""named_query"",
					name: {
						value: ""elasticsearch.pm""
					}
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}

	}
}

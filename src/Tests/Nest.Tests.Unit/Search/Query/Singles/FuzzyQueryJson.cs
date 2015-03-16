using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class FuzzyQueryJson
	{
		[Test]
		public void TestFuzzyQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Fuzzy(fz => fz
						.Name("named_query")
						.OnField(f=>f.Name)
						.Value("elasticsearcc")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ fuzzy: { name : { value : ""elasticsearcc"", _name: ""named_query"" } }}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestFuzzyWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Fuzzy(fz => fz
						.OnField(f => f.Name)
						.Value("elasticsearcc")
						.Boost(2.0)
						.Fuzziness(0.6)
						.PrefixLength(2)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ fuzzy: { name : { 
				prefix_length: 2,
				value : ""elasticsearcc"", 
				boost: 2.0,
				fuzziness: ""0.6""
			}}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

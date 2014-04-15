using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class FuzzyNumericQueryJson
	{
		[Test]
		public void TestFuzzyNumericQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.FuzzyNumeric(fz => fz
						.OnField(f=>f.LOC)
						.Value(200)
						.Fuzziness(12)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ fuzzy: { loc : { value : 200.0, fuzziness: 12.0 } }}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestFuzzyNumericWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.FuzzyNumeric(fz => fz
						.OnField(f => f.LOC)
						.Value(200)
						.Boost(2.0)
						.Fuzziness(0.6)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ fuzzy: { loc : { 
				value : 200.0, 
				boost: 2.0,
				fuzziness: 0.6
			}}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

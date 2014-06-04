using System;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class FuzzyDateQueryJson
	{
		[Test]
		public void TestFuzzDateQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.FuzzyDate(fz => fz
						.OnField(f=>f.StartedOn)
						.Value(new DateTime(1999,12,31))
						.Fuzziness("1d")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ fuzzy: { startedOn : { 
				value: ""1999-12-31T00:00:00"",
				fuzziness: ""1d""
		  } }}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestFuzzyDateWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.FuzzyDate(fz => fz
						.OnField(f => f.StartedOn)
						.Value(new DateTime(1999, 12, 31))
						.Boost(2.0)
						.Fuzziness("1d")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ fuzzy: { startedOn : { 
				value: ""1999-12-31T00:00:00"",
				boost: 2.0,
				fuzziness: ""1d""
			}}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

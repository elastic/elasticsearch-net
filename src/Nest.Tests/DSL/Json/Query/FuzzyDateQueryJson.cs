using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.FilterTests
{
	[TestFixture]
	public class FuzzyDateQueryJson
	{
		[Test]
		public void TestFuzzDateQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.FuzzyDate(fz => fz
						.OnField(f=>f.StartedOn)
						.Value(new DateTime(1999,12,31))
						.MinSimilarity("1d")
					)
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ fuzzy: { startedOn : { 
				min_similarity: ""1d"",
        value: ""1999-12-31T00:00:00""
		  } }}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestFuzzyDateWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.FuzzyDate(fz => fz
						.OnField(f => f.StartedOn)
						.Value(new DateTime(1999, 12, 31))
						.Boost(2.0)
						.MinSimilarity("1d")
					)
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ fuzzy: { startedOn : { 
				boost: 2.0,
				min_similarity: ""1d"",
        value: ""1999-12-31T00:00:00""
			}}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

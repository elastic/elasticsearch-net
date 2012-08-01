using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.QueryTests
{
	[TestFixture]
	public class FuzzyNumericQueryJson
	{
		[Test]
		public void TestFuzzyNumericQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.FuzzyNumeric(fz => fz
						.OnField(f=>f.LOC)
						.Value(200)
						.MinSimilarity(12)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ fuzzy: { loc : { min_similarity: 12.0, value : 200.0 } }}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestFuzzyNumericWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.FuzzyNumeric(fz => fz
						.OnField(f => f.LOC)
						.Value(200)
						.Boost(2.0)
						.MinSimilarity(0.6)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ fuzzy: { loc : { 
				boost: 2.0,
				min_similarity: 0.6,
				value : 200.0, 
			}}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.FilterTests
{
	[TestFixture]
	public class FuzzyQueryJson
	{
		[Test]
		public void TestFuzzyQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Fuzzy(fz => fz
						.OnField(f=>f.Name)
						.Value("elasticsearcc")
					)
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ fuzzy: { name : { value : ""elasticsearcc"" } }}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestFuzzyWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Fuzzy(fz => fz
						.OnField(f => f.Name)
						.Value("elasticsearcc")
						.Boost(2.0)
						.MinSimilarity(0.6)
						.PrefixLength(2)
					)
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ fuzzy: { name : { 
				boost: 2.0,
				min_similarity: 0.6,
				prefix_length: 2,
				value : ""elasticsearcc"", 
			}}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

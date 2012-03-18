using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Filter
{
	[TestFixture]
	public class WildcardQueryJson
	{
		[Test]
		public void TestWildcardQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Wildcard(f => f.Name, "elasticsearch.*")
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ wildcard: { name : { value : ""elasticsearch.*"" } }}}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestWildcardWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Wildcard(f => f.Name, "elasticsearch.*", Boost: 1.2)
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ wildcard: { name : { value : ""elasticsearch.*"", boost: 1.2 } }}}";
			Assert.True(json.JsonEquals(expected));
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.QueryTests
{
	[TestFixture]
	public class PrefixQueryJson
	{
		[Test]
		public void TestPrefixQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Prefix(f => f.Name, "elasticsearch.*")
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ prefix: { name : { value : ""elasticsearch.*"" } }}}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestPrefixWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Prefix(f => f.Name, "el", Boost: 1.2)
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ prefix: { name : { value : ""el"", boost: 1.2 } }}}";
			Assert.True(json.JsonEquals(expected), json);
		}

	}
}

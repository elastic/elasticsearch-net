using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Filter
{
	[TestFixture]
	public class TermQueryJson
	{
		[Test]
		public void TestTermQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Term(f => f.Name, "elasticsearch.pm")
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ term: { name : { value : ""elasticsearch.pm"" } }}}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestTermWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Term(f => f.Name, "elasticsearch.pm", Boost: 1.2)
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ term: { name : { value : ""elasticsearch.pm"", boost: 1.2 } }}}";
			Assert.True(json.JsonEquals(expected));
		}
	}
}

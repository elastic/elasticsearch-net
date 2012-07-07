using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.QueryTests
{
	[TestFixture]
	public class MatchAllQueryJson
	{
		[Test]
		public void TestMatchAllQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q.MatchAll());
			var json = ElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10, query : { match_all: {}}}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestMatchAllShortcut()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.MatchAll();
			var json = ElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10, query : { match_all: {}}}";
			Assert.True(json.JsonEquals(expected));
		}

		[Test]
		public void TestMatchAllWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.MatchAll(Boost: 1.2)
				);
			var json = ElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10, query : { match_all: { boost: 1.2 }}}";
			Assert.True(json.JsonEquals(expected));
		}

		[Test]
		public void TestMatchAllWithNormFieldQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.MatchAll(NormField: "name")
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : { match_all: { norm_field: ""name"" }}}";
			Assert.True(json.JsonEquals(expected));
		}
	}
}

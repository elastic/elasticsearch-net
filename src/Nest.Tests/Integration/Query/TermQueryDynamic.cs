using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Integration.Query
{
	[TestFixture]
	public class TermQueryDynamic : BaseElasticSearchTests
	{
		[Test]
		public void TestTermQuery()
		{
			var results = this.ConnectedClient.Search(s=>s
				.Index("nest_test_data")
				.Type("elasticsearchprojects")
				.From(0)
				.Size(10)
				.Query(q => q
					.Term("name", "elasticsearch.pm")
				)
			);
			Assert.True(results.IsValid);
			Assert.Greater(results.Documents.Count(), 0);
			var first = results.Documents.First();
			Assert.IsNotNullOrEmpty((string)first.followers[0].firstName);
		}
	}
}

using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1513Tests : IntegrationTests
	{
		[Test]
		public void DeserializingTermsAggregationThrowsException()
		{
			var result = Client.Search<ElasticsearchProject>(s => s
				.SearchType(Elasticsearch.Net.SearchType.Count)
				.Aggregations(aggs => aggs
					.Terms("type", t => t
						.Field(p => p.Name)
					)
				)
			);
			result.IsValid.Should().BeTrue();
		}
	}
}

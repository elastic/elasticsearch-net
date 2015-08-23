using Elasticsearch.Net;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1515Tests : IntegrationTests
	{
		[Test]
		public void Test()
		{
			var result = Client.Search<ElasticsearchProject>(s => s
				.SearchType(SearchType.Count)
				.Aggregations(ag => ag
					.Terms("countries", st => st
						.Field("country")
						.Size(10)
						.Aggregations(aa => aa
							.Terms("names", t => t
								.Field(f => f.Name)
							)
						)
					)
				)
			);

			result.IsValid.Should().BeTrue();
		}
	}
}

using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1079Tests : IntegrationTests
	{
		[Test]
		public void InnerSumAggregationTest()
		{
			var response = this.Client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Histogram("hist", h => h
						.Field(p => p.LongValue)
						.Interval(3000)
						.Aggregations(aa => aa
							.Sum("sizes", sa => sa
								.Field(p => p.LongValue)
							)
						)
					)
				)
			);

			var hist = response.Aggs.Histogram("hist");
			foreach (var interval in hist.Items)
			{
				var sizes = interval.Sum("sizes");
				sizes.Should().NotBeNull();
			}
		}
	}
}

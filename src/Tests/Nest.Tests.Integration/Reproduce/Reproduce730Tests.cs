using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce730Tests : IntegrationTests
	{
		[Test]
		public void TermAggIntegerAsKeyProducesNullKeyItem()
		{
			var result = this.Client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Terms("my_term_agg", t => t
						.Field(o => o.DoubleValue))));

			result.IsValid.Should().BeTrue();
			var myTermAgg = result.Aggs.Terms("my_term_agg");
			myTermAgg.Items.Count.Should().BeGreaterThan(0);
			myTermAgg.Items[0].Key.Should().NotBeNull();
			myTermAgg.Items[0].DocCount.Should().BeGreaterThan(0);
		}
	}
}

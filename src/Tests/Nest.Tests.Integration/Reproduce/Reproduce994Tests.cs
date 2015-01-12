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
	public class Reproduce994Tests : IntegrationTests
	{
		[Test]
		public void DateTimeRangeItemsAreReturnedProperly()
		{
			var fixedDateString = "2014-03-01T00:00:00.000Z";

			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.DateRange("my_geod", dh => dh
						.Field(p => p.StartedOn)
						.Ranges(
							r => r.To("now-10M/M").Key(fixedDateString),
							r => r.From("now-10M/M").Key(fixedDateString + "-*")
						)
					)
				)
			);

			var aggs = results.Aggs.DateRange("my_geod").Items;
			aggs.Should().NotBeEmpty().And.HaveCount(2);
			aggs[0].ToAsString.Should().NotBeNullOrEmpty().And.Be(fixedDateString);
			//key should not be intepretted as date either
			aggs[0].Key.Should().NotBeNullOrEmpty().And.Be(fixedDateString);
			aggs[1].FromAsString.Should().NotBeNullOrEmpty().And.Be(fixedDateString);
			aggs[1].Key.Should().NotBeNullOrEmpty().And.Be(fixedDateString + "-*");
		}
	}
}

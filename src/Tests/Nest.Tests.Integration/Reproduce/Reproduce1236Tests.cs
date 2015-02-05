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
	public class Reproduce1236Tests : IntegrationTests
	{
		[Test]
		public void AggregationsMissingWithZeroHits()
		{
			var result = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Term(p => p.Country, "doesnotexist")
				)
				.Aggregations(a => a
					.Terms("new", t => t
						.Field(p => p.Name)
					)
					.Min("first_occurence", min => min
						.Field(p => p.StartedOn)
					)
					.Max("last_occurence", max => max
						.Field(p => p.StartedOn)
					)
				)
			);

			result.IsValid.Should().BeTrue();
			result.Aggregations.Count.Should().Be(3);
		}
	}
}

using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1474Tests : IntegrationTests
	{
		[Test]
		public void GeoBoundsAggregationNotReadToCompletion()
		{
			var response = this.Client.Search<ElasticsearchProject>(s => s
				.Aggregations(aggs => aggs
					.Terms("names", ct => ct
						.Field(p => p.Name)
						.Size(0)
					)
					.Terms("countries", at => at
						.Field(p => p.Country)
						.Size(0)
					)
					.GeoHash("locations", l => l
						.Field(f => f.Origin)
						.GeoHashPrecision(GeoHashPrecision.Precision6)
					)
					.GeoBounds("bounds", b => b
						.Field(f => f.Origin)
					)
				)
			);
			response.IsValid.Should().BeTrue();
			var names = response.Aggs.Terms("names");
			names.Should().NotBeNull();
			var countries = response.Aggs.Terms("countries");
			countries.Should().NotBeNull();
			var locations = response.Aggs.GeoHash("locations");
			locations.Should().NotBeNull();
			var bounds = response.Aggs.GeoBounds("bounds");
			bounds.Should().NotBeNull();
		}
	}
}

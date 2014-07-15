using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class GeoShapeQueryTests : ParseQueryTestsBase
	{

		[Test]
		public void GeoShape_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f => f.GeoShape,
				f => f.GeoShapeEnvelope(gq => gq
					.OnField(p => p.MyGeoShape)
					.Coordinates(new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } })
					)
				);

			q.Field.Should().Be("myGeoShape");
			var envelopeQuery = q as IGeoShapeEnvelopeQuery;
			envelopeQuery.Should().NotBeNull();
			envelopeQuery.Shape.Type.Should().Be("envelope");
			envelopeQuery.Shape.Coordinates.SelectMany(c=>c).Should()
				.BeEquivalentTo(new [] {13.0, 53.0, 14.0, 52.0 });
		}
	}
}
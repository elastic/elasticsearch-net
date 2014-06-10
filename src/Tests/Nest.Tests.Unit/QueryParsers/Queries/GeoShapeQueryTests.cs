using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class GeoShapeQueryTests : ParseQueryTestsBase
	{

		[Test]
		public void GeoShape_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.GeoShape,
				f=>f.GeoShape(gq=>gq
					.OnField(p=>p.MyGeoShape)
					.Coordinates(new [] { new [] {13.0, 53.0}, new [] { 14.0, 52.0} })
					.Type("enveloppe")
					)
				);

			q.Field.Should().Be("myGeoShape");
			q.Shape.Should().NotBeNull();
			q.Shape.Type.Should().Be("enveloppe");
			q.Shape.Coordinates.SelectMany(c=>c).Should()
				.BeEquivalentTo(new [] {13.0, 53.0, 14.0, 52.0 });
		}
	}
}
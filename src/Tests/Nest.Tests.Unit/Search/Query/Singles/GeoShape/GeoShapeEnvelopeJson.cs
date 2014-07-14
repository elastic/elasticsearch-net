using System.Reflection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Collections.Generic;

namespace Nest.Tests.Unit.Search.Query.Singles.GeoShape
{
	[TestFixture]
	public class GeoShapeEnvelopeJson : BaseJsonTests
	{
		[Test]
		public void GeoShapeEnvelope()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.GeoShape(qs => qs
						.OnField(p => p.MyGeoShape)
						.Shape(new Envelope { Coordinates = new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } } })
					)
			);

			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}

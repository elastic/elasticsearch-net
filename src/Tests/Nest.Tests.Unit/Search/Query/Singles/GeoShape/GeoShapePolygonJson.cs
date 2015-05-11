using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Search.Query.Singles.GeoShape
{
	[TestFixture]
	public class GeoShapePolygonJson : BaseJsonTests
	{
		[Test]
		public void GeoShapePolygon()
		{
			var polygon = new PolygonGeoShape
				{
					Coordinates = new[] { 
							new[] { new[] { 100.0, 0.0 }, new[] { 101.0, 0.0 }, new[] { 101.0, 1.0 }, new[] { 100.0, 1.0 }, new [] { 100.0, 0.0 } },
							new[] { new[] { 100.2, 0.2}, new[] { 100.8, 0.2 }, new[] { 100.8, 0.8}, new[] { 100.2, 0.8 }, new [] { 100.2, 0.2} }
						}
				};

			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.GeoShapePolygon(qs => qs
						.Name("named_query")
						.OnField(p => p.MyGeoShape)
						.Coordinates(polygon.Coordinates)
					)
				);

			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}

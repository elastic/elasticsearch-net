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
	public class GeoShapeLineStringJson : BaseJsonTests
	{
		[Test]
		public void GeoShapeLineString()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.GeoShape(qs => qs
						.OnField(p => p.MyGeoShape)
						.Shape(new LineString { Coordinates = new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } } })
					)
			);

			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}

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
	public class GeoShapePointJson : BaseJsonTests
	{
		[Test]
		public void GeoShapePoint()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.GeoShape(qs => qs
						.OnField(p => p.MyGeoShape)
						.Shape(new Point { Coordinates = new[] { -45.0, 45.0 } })
					)
			);

			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}

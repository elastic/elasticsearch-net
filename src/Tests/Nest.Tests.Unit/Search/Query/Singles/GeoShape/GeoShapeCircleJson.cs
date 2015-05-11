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
	public class GeoShapeCircleJson : BaseJsonTests
	{
		[Test]
		public void GeoShapeCircle()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.GeoShapeCircle(qs => qs
						.OnField(p => p.MyGeoShape)
						.Coordinates(new[] { -45.0, 45.0 })
						.Radius("100m")
						.Name("named_query")
					)
			);

			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}

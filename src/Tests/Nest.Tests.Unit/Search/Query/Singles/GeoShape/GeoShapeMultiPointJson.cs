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
	public class GeoShapeMultiPointJson : BaseJsonTests
	{
		[Test]
		public void GeoShapeMultiPoint()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.GeoShapeMultiPoint(qs => qs
						.OnField(p => p.MyGeoShape)
						.Coordinates(new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } })
					)
			);

			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}

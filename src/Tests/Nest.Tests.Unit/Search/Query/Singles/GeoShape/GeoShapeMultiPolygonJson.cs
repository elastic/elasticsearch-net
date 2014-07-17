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
	public class GeoShapeMultiPolygonJson : BaseJsonTests
	{
		[Test]
		public void GeoShapeMultiPolygon()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.GeoShapeMultiPolygon(qs => qs
						.OnField(p => p.MyGeoShape)
						.Coordinates(
								new[] {
									new [] {
										new [] {
											new [] { 102.0, 2.0 }, new [] { 103.0, 2.0 }, new [] { 103.0, 3.0 }, new [] { 102.0, 3.0 }, new [] { 102.0, 2.0 }
										}
									},
									new [] {
										new [] {
											new [] { 100.0, 0.0 }, new [] { 101.0, 0.0 }, new [] { 101.0, 1.0 }, new [] {100.0, 1.0 }, new [] { 100.0, 0.0 }
										},
										new [] {
											new [] { 100.2, 0.2 }, new [] { 100.8, 0.2 }, new [] { 100.8, 0.8 }, new [] { 100.2, 0.8 }, new [] { 100.2, 0.2 }
										}
									}
								}
							)
						)
					);

			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}

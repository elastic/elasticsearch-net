using System.Reflection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Query.Singles.GeoShape
{
	[TestFixture]
	public class GeoShapeQueryJson : BaseJsonTests
	{
		[Test]
		public void GeoShapeFull()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q=>q
					.GeoShape(qs=>qs
						.OnField(p=>p.MyGeoShape)
						.Coordinates(new [] { new [] {13.0, 53.0}, new [] { 14.0, 52.0} })
						.Type("enveloppe")
					)
			);
				
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}

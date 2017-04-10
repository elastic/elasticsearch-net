using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Geo.Polygon
{
	public class GeoPolygonQueryUsageTests : QueryDslUsageTestsBase
	{
		public GeoPolygonQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_polygon = new
			{
				_name = "named_query",
				boost = 1.1,
				validation_method = "strict",
				location = new
				{
					points = new[] {
						new { lat = 45.0, lon = -45.0 },
						new { lat = -34.0, lon = 34.0 }
					}
				}
			}

		};

		protected override QueryContainer QueryInitializer => new GeoPolygonQuery
		{
			Boost = 1.1,
			Name = "named_query",
			ValidationMethod =	GeoValidationMethod.Strict,
			Points = new [] { new GeoLocation(45,-45), new GeoLocation(-34,34),  },
			Field = Infer.Field<Project>(p=>p.Location)
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoPolygon(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.ValidationMethod(GeoValidationMethod.Strict)
				.Points( new GeoLocation(45,-45), new GeoLocation(-34,34))
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoPolygonQuery>(a => a.GeoPolygon)
		{
			q =>  q.Field = null,
			q =>  q.Points = null,
			q =>  q.Points = Enumerable.Empty<GeoLocation>()
		};

	}
}

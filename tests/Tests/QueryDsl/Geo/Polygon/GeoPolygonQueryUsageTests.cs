// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Geo.Polygon
{
	public class GeoPolygonQueryUsageTests : QueryDslUsageTestsBase
	{
		public GeoPolygonQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoPolygonQuery>(a => a.GeoPolygon)
		{
			q => q.Field = null,
			q => q.Points = null,
			q => q.Points = Enumerable.Empty<GeoLocation>()
		};

		protected override QueryContainer QueryInitializer => new GeoPolygonQuery
		{
			Boost = 1.1,
			Name = "named_query",
			ValidationMethod = GeoValidationMethod.Strict,
			Points = new[] { new GeoLocation(45, -45), new GeoLocation(-34, 34), new GeoLocation(70, -70) },
			Field = Infer.Field<Project>(p => p.LocationPoint)
		};

		protected override object QueryJson => new
		{
			geo_polygon = new
			{
				_name = "named_query",
				boost = 1.1,
				validation_method = "strict",
				locationPoint = new
				{
					points = new[]
					{
						new { lat = 45.0, lon = -45.0 },
						new { lat = -34.0, lon = 34.0 },
						new { lat = 70.0, lon = -70.0 },
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoPolygon(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LocationPoint)
				.ValidationMethod(GeoValidationMethod.Strict)
				.Points(new GeoLocation(45, -45), new GeoLocation(-34, 34), new GeoLocation(70, -70))
			);
	}
}

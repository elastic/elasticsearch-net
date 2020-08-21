// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Geo.BoundingBox
{
	public class GeoBoundingBoxQueryUsageTests : QueryDslUsageTestsBase
	{
		public GeoBoundingBoxQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoBoundingBoxQuery>(a => a.GeoBoundingBox)
		{
			q => q.BoundingBox = null,
			q => q.BoundingBox = new Nest.BoundingBox(),
			q => q.Field = null
		};

		protected override QueryContainer QueryInitializer => new GeoBoundingBoxQuery
		{
			Boost = 1.1,
			Name = "named_query",
			Field = Infer.Field<Project>(p => p.LocationPoint),
			BoundingBox = new Nest.BoundingBox
			{
				TopLeft = new GeoLocation(34, -34),
				BottomRight = new GeoLocation(-34, 34),
			},
			Type = GeoExecution.Indexed,
			ValidationMethod = GeoValidationMethod.Strict
		};

		protected override object QueryJson => new
		{
			geo_bounding_box = new
			{
				type = "indexed",
				validation_method = "strict",
				_name = "named_query",
				boost = 1.1,
				locationPoint = new
				{
					top_left = new
					{
						lat = 34.0,
						lon = -34.0
					},
					bottom_right = new
					{
						lat = -34.0,
						lon = 34.0
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoBoundingBox(g => g
				.Boost(1.1)
				.Name("named_query")
				.Field(p => p.LocationPoint)
				.BoundingBox(b => b
					.TopLeft(34, -34)
					.BottomRight(-34, 34)
				)
				.ValidationMethod(GeoValidationMethod.Strict)
				.Type(GeoExecution.Indexed)
			);
	}

	public class GeoBoundingBoxWKTQueryUsageTests : QueryDslUsageTestsBase
	{
		public GeoBoundingBoxWKTQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoBoundingBoxQuery>(a => a.GeoBoundingBox)
		{
			q => q.BoundingBox = null,
			q => q.BoundingBox = new Nest.BoundingBox(),
			q => q.Field = null
		};

		protected override QueryContainer QueryInitializer => new GeoBoundingBoxQuery
		{
			Boost = 1.1,
			Name = "named_query",
			Field = Infer.Field<Project>(p => p.LocationPoint),
			BoundingBox = new Nest.BoundingBox
			{
				WellKnownText = "BBOX (-34, 34, 34, -34)"
			},
			Type = GeoExecution.Indexed,
			ValidationMethod = GeoValidationMethod.Strict
		};

		protected override object QueryJson => new
		{
			geo_bounding_box = new
			{
				type = "indexed",
				validation_method = "strict",
				_name = "named_query",
				boost = 1.1,
				locationPoint = new
				{
					wkt = "BBOX (-34, 34, 34, -34)"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoBoundingBox(g => g
				.Boost(1.1)
				.Name("named_query")
				.Field(p => p.LocationPoint)
				.BoundingBox(b => b
					.WellKnownText("BBOX (-34, 34, 34, -34)")
				)
				.ValidationMethod(GeoValidationMethod.Strict)
				.Type(GeoExecution.Indexed)
			);
	}
}

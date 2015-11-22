using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.QueryDsl.Geo.Shape.IndexedShape
{
	public class GeoIndexedShapeUsageTests : QueryDslUsageTestsBase
	{
		public GeoIndexedShapeUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private readonly IEnumerable<double> _coordinates = new[] { -77.03653, 38.897676 };

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				location = new
				{
					_name="named_query",
					boost = 1.1,
					indexed_shape = new 
					{
						id = 2,
						type = "project",
						index = "project",
						path = "location"
					}
				}
			}
		};

		protected override QueryContainer QueryInitializer => new GeoIndexedShapeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p=>p.Location),
			IndexedShape = new IndexedGeoShape
			{
				Id = 2,
				Index = Index<Project>(),
				Type = Type<Project>(),
				Path = Field<Project>(p=>p.Location)
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoIndexedShape(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.IndexedShape(p=>p
					.Id(2)
					.Path(pp=>pp.Location)
				)
			);
	}
}

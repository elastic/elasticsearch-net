using System.Collections.Generic;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.MultiLineString
{
	public class GeoMultiLineStringUsageTests : ShapeQueryUsageTestsBase
	{
		public GeoMultiLineStringUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private readonly IEnumerable<IEnumerable<GeoCoordinate>> _coordinates = new[]
		{
			new GeoCoordinate[] { new [] { 12.0, 2.0 }, new [] { 13.0, 2.0},new [] { 13.0, 3.0 }, new []{ 12.0, 3.0 } },
			new GeoCoordinate[] { new [] { 10.0, 0.0 }, new [] { 11.0, 0.0},new [] { 11.0, 1.0 }, new []{ 10.0, 1.0 } },
			new GeoCoordinate[] { new [] { 10.2, 0.2 }, new [] { 10.8, 0.2},new [] { 10.8, 0.8 }, new []{ 12.0, 0.8 } },
		};

		protected override object ShapeJson => new
		{
			type ="multilinestring",
			coordinates = this._coordinates
		};

		protected override QueryContainer QueryInitializer => new GeoShapeMultiLineStringQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p=>p.Location),
			Shape = new MultiLineStringGeoShape(this._coordinates)
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeMultiLineString(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.Coordinates(this._coordinates)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoShapeMultiLineStringQuery>(a => a.GeoShape as IGeoShapeMultiLineStringQuery)
		{
			q =>  q.Field = null,
			q =>  q.Shape = null,
			q =>  q.Shape.Coordinates = null,
		};
	}
}

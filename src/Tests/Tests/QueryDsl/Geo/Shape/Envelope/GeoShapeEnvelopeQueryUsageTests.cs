using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.QueryDsl.Geo.Shape.Envelope
{
	public class GeoShapeEnvelopeQueryUsageTests : GeoShapeQueryUsageTestsBase
	{
		private readonly IEnumerable<GeoCoordinate> _coordinates = new GeoCoordinate[]
		{
			new[] { -45.0, 45.0 },
			new[] { 45.0, -45.0 }
		};

		public GeoShapeEnvelopeQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen =>
			new ConditionlessWhen<IGeoShapeEnvelopeQuery>(a => a.GeoShape as IGeoShapeEnvelopeQuery)
			{
				q => q.Field = null,
				q => q.Shape = null,
				q => q.Shape.Coordinates = null,
			};

		protected override QueryContainer QueryInitializer => new GeoShapeEnvelopeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Field<Project>(p => p.Location),
			Shape = new EnvelopeGeoShape(_coordinates),
			Relation = GeoShapeRelation.Intersects,
		};

		protected override object ShapeJson => new
		{
			type = "envelope",
			coordinates = _coordinates
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoShapeEnvelope(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Location)
				.Coordinates(_coordinates)
				.Relation(GeoShapeRelation.Intersects)
			);
	}
}

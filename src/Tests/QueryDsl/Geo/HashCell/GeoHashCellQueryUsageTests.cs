using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Geo.HashCell
{
	public class GeoHashCellUsageTests : QueryDslUsageTestsBase
	{
		public GeoHashCellUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geohash_cell = new
			{
				_name = "named_query",
				boost = 1.1,
				precision = "3.0m",
				neighbors = true,
				location = new
				{
					lat = 13.408,
					lon = 52.5186
				}
			}
		};

		protected override QueryContainer QueryInitializer => new GeoHashCellQuery
		{
			Boost = 1.1,
			Name = "named_query",
			Field = Infer.Field<Project>(p=>p.Location),
			Location = new GeoLocation(13.4080, 52.5186),
			Neighbors = true,
			Precision = Nest.Distance.Meters(3)
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.GeoHashCell(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p=>p.Location)
				.Location(new GeoLocation(13.4080, 52.5186))
				.Neighbors()
				.Precision(Nest.Distance.Meters(3))
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IGeoHashCellQuery>(a => a.GeoHashCell)
		{
			q =>  q.Field = null,
			q =>  q.Location = null
		};
	}
}

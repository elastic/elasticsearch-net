using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Geo.DistanceRange
{
	public class GeoDistanceRangeUsageTests : QueryDslUsageTestsBase
	{
		public GeoDistanceRangeUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			query_string = new
			{
				_name = "named_query",
				boost = 1.1,
			}
		};

		protected override QueryContainer QueryInitializer => new GeoDistanceRangeQuery
		{
			Boost = 1.1,
			Name = "named_query",
			Field = Static.Field<Project>(p=>p.Location),
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q;
	}
}

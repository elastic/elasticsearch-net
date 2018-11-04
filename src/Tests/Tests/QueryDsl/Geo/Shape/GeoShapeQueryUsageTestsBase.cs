using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;

namespace Tests.QueryDsl
{
	public abstract class GeoShapeQueryUsageTestsBase : QueryDslUsageTestsBase
	{
		protected GeoShapeQueryUsageTestsBase(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name = "named_query",
				boost = 1.1,
				ignore_unmapped = false,
				location = new
				{
					relation = "intersects",
					shape = ShapeJson
				}
			}
		};

		protected abstract object ShapeJson { get; }
	}
}

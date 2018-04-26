using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.QueryDsl
{
	public abstract class GeoShapeQueryUsageTestsBase : QueryDslUsageTestsBase
	{
		protected GeoShapeQueryUsageTestsBase(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				_name="named_query",
				boost = 1.1,
				location = new
				{
					relation = "intersects",
					shape = this.ShapeJson
				}
			}
		};

		protected abstract object ShapeJson { get; }
	}
}

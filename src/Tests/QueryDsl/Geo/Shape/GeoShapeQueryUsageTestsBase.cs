using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.QueryDsl.Geo.Shape
{
	public abstract class GeoShapeQueryUsageTestsBase : QueryDslUsageTestsBase
	{
		protected GeoShapeQueryUsageTestsBase(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				location = new
				{
					_name="named_query",
					boost = 1.1,
					shape = this.ShapeJson,
					relation = "intersects"
				}
			}
		};

		protected abstract object ShapeJson { get; }
	}
}

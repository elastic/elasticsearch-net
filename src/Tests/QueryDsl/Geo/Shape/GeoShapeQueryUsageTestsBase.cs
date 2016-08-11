using Tests.Framework.Integration;
using Xunit;

namespace Tests.QueryDsl
{
	public abstract class ShapeQueryUsageTestsBase : QueryDslUsageTestsBase
	{
		public GeoShapeQueryUsageTestsBase(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				location = new
				{
					_name="named_query",
					boost = 1.1,
					ignore_unmapped = false,
					relation = "intersects",
					shape = this.ShapeJson
				}
			}
		};

		protected abstract object ShapeJson { get; }
	}
}

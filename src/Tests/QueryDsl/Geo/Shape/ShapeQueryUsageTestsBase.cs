using Tests.Framework.Integration;
using Xunit;

namespace Tests.QueryDsl
{
	[Collection(IntegrationContext.ReadOnly)]
	public abstract class ShapeQueryUsageTestsBase : QueryDslUsageTestsBase
	{
		public ShapeQueryUsageTestsBase(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			geo_shape = new
			{
				location = new { _name="named_query", boost = 1.1, shape = this.ShapeJson }
			}
		};

		protected abstract object ShapeJson { get; }

	}
}

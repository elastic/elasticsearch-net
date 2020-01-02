using Nest;

namespace Tests.Mapping.Types.Core.Percolator
{
	public class PercolatorTest
	{
		[Percolator]
		public QueryContainer Query { get; set; }
	}

	public class PercolatorAttributeTests : AttributeTestsBase<PercolatorTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				query = new
				{
					type = "percolator"
				}
			}
		};
	}
}

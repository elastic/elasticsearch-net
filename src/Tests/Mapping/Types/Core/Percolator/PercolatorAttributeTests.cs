using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest_5_2_0;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Tests.Mapping.Types.Core.Percolator
{
	public class PercolatorTest
	{
		[Percolator]
		public QueryContainer Query { get; set; }
	}

	public class PercolatorMappingTests : TypeMappingTestBase<PercolatorTest>
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

		protected override Func<PropertiesDescriptor<PercolatorTest>, IPromise<IProperties>> FluentProperties => p => p
			.Percolator(s => s
				.Name(o => o.Query)
			);
	}
}

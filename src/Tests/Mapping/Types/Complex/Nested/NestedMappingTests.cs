using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework.MockData;

namespace Tests.Mapping.Types.Complex.Nested
{
	public class NestedTest
	{
		[Nested(
			IncludeInParent = true,
			IncludeInRoot = false,
			Dynamic = DynamicMapping.Strict,
			Enabled = true,
			IncludeInAll = true,
			Path = "mypath")]
		public Project Full { get; set; }

		[Nested]
		public Project Minimal { get; set; }
	}

	public class NestedMappingTests : TypeMappingTestBase<NestedTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "nested",
					include_in_parent = true,
					include_in_root = false,
					dynamic = "strict",
					enabled = true,
					include_in_all = true,
					path = "mypath"
				},
				minimal = new
				{
					type = "nested"
				}
			}
		};

		protected override Func<PropertiesDescriptor<NestedTest>, IPromise<IProperties>> FluentProperties => p => p
			.Nested<Project>(s => s
				.Name(o => o.Full)
				.IncludeInParent()
				.IncludeInRoot(false)
				.Dynamic(DynamicMapping.Strict)
				.Enabled()
				.IncludeInAll()
				.Path("mypath")
			)
			.Nested<Project>(b => b
				.Name(o => o.Minimal)
			);
	}
}

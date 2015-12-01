using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework.MockData;

namespace Tests.Mapping.Types.Complex.Object
{
	public class ObjectTest
	{
		[Object(
			Dynamic = DynamicMapping.Strict,
			Enabled = true,
			IncludeInAll = true,
			Path = "mypath")]
		public Project Full { get; set; }

		[Object]
		public Project Minimal { get; set; }
	}

	public class ObjectMappingTests : TypeMappingTestBase<ObjectTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "object",
					dynamic = "strict",
					enabled = true,
					include_in_all = true,
					path = "mypath"
				},
				minimal = new
				{
					type = "object"
				}
			}
		};

		protected override Func<PropertiesDescriptor<ObjectTest>, IPromise<IProperties>> FluentProperties => p => p
			.Object<Project>(s => s
				.Name(o => o.Full)
				.Dynamic(DynamicMapping.Strict)
				.Enabled()
				.IncludeInAll()
				.Path("mypath")
			)
			.Object<Project>(b => b
				.Name(o => o.Minimal)
			);
	}
}

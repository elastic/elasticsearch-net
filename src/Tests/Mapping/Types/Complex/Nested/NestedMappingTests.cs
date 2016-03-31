using System;
using Nest;

namespace Tests.Mapping.Types.Complex.Nested
{
	public class NestedTest
	{
		public class InnerObject
		{
			public string Name { get; set; }
		}

		[Nested(
			IncludeInParent = true,
			IncludeInRoot = false,
			Dynamic = DynamicMapping.Strict,
			Enabled = true,
			IncludeInAll = true,
			Path = "mypath")]
		public InnerObject Full { get; set; }

		[Nested]
		public InnerObject Minimal { get; set; }
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
					path = "mypath",
					properties = new
					{
						name = new
						{
							type = "text",
							fields = new
							{
								keyword = new
								{
									type = "keyword"
								}
							}
						}
					}
				},
				minimal = new
				{
					type = "nested",
					properties = new
					{
						name = new
						{
							type = "text",
							fields = new
							{
								keyword = new
								{
									type = "keyword"
								}
							}
						}
					}
				}
			}
		};

		protected override Func<PropertiesDescriptor<NestedTest>, IPromise<IProperties>> FluentProperties => p => p
			.Nested<NestedTest.InnerObject>(s => s
				.AutoMap()
				.Name(o => o.Full)
				.IncludeInParent()
				.IncludeInRoot(false)
				.Dynamic(DynamicMapping.Strict)
				.Enabled()
				.IncludeInAll()
				.Path("mypath")
			)
			.Nested<NestedTest.InnerObject>(b => b
				.AutoMap()
				.Name(o => o.Minimal)
			);
	}
}

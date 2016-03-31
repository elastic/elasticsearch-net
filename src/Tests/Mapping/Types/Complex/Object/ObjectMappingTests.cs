using System;
using Nest;

namespace Tests.Mapping.Types.Complex.Object
{
	public class ObjectTest
	{
		public class InnerObject
		{
			public string Name { get; set; }
		}

		[Object(
			Dynamic = DynamicMapping.Strict,
			Enabled = true,
			IncludeInAll = true,
			Path = "mypath")]
		public InnerObject Full { get; set; }

		[Object]
		public InnerObject Minimal { get; set; }
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
					type = "object",
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

		protected override Func<PropertiesDescriptor<ObjectTest>, IPromise<IProperties>> FluentProperties => p => p
			.Object<ObjectTest.InnerObject>(s => s
				.AutoMap()
				.Name(o => o.Full)
				.Dynamic(DynamicMapping.Strict)
				.Enabled()
				.IncludeInAll()
				.Path("mypath")
			)
			.Object<ObjectTest.InnerObject>(b => b
				.AutoMap()
				.Name(o => o.Minimal)
			);
	}
}

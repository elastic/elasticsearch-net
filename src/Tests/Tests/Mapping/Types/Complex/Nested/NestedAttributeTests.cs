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
			Enabled = true)]
		public InnerObject Full { get; set; }

		[Nested]
		public InnerObject Minimal { get; set; }
	}

	public class NestedAttributeTests : AttributeTestsBase<NestedTest>
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
					enabled = true,
					properties = new
					{
						name = new
						{
							type = "text",
							fields = new
							{
								keyword = new
								{
									type = "keyword",
									ignore_above = 256
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
									type = "keyword",
									ignore_above = 256
								}
							}
						}
					}
				}
			}
		};
	}
}

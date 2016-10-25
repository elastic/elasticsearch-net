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
			Enabled = true,
			IncludeInAll = true)]
		public InnerObject Full { get; set; }

		[Object]
		public InnerObject Minimal { get; set; }
	}

	public class ObjectAttributeTests : AttributeTestsBase<ObjectTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "object",
					enabled = true,
					include_in_all = true,
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
	}
}

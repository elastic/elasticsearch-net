// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Mapping.Types.Complex.Nested
{
	public class NestedTest
	{
		[Nested(
			IncludeInParent = true,
			IncludeInRoot = false,
			Enabled = true)]
		public InnerObject Full { get; set; }

		[Nested]
		public InnerObject Minimal { get; set; }

		public class InnerObject
		{
			public string Name { get; set; }
		}
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

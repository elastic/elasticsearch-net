using System;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Mapping.Types.Specialized.Murmur3Hash
{
	public class Murmur3HashTest
	{
		[Murmur3Hash]
		public string Full { get; set; }

		[Murmur3Hash]
		public string Minimal { get; set; }
	}

	public class Murmur3HashAttributeTests : AttributeTestsBase<Murmur3HashTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "murmur3",
				},
				minimal = new
				{
					type = "murmur3"
				}
			}
		};
	}
}

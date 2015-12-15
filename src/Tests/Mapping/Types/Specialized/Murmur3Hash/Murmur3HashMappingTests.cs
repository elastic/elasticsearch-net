using System;
using Nest;

namespace Tests.Mapping.Types.Specialized.Murmur3Hash
{
	public class Murmur3HashTest
	{
		[Murmur3Hash]
		public string Full { get; set; }

		[Murmur3Hash]
		public string Minimal { get; set; }
	}

	public class Murmur3HashMappingTests : TypeMappingTestBase<Murmur3HashTest>
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

		protected override Func<PropertiesDescriptor<Murmur3HashTest>, IPromise<IProperties>> FluentProperties => p => p
			.Murmur3Hash(s => s
				.Name(o => o.Full)
			)
			.Murmur3Hash(b => b
				.Name(o => o.Minimal)
			);
	}
}

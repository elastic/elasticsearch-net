using System;
using Nest;

namespace Tests.Mapping.Types.Specialized.Ip
{
	public class IpTest
	{
		[Ip(
			Index = NonStringIndexOption.No,
			PrecisionStep = 4,
			Boost = 1.3,
			NullValue = "127.0.0.1",
			IncludeInAll = true)]
		public string Full { get; set; }

		[Ip]
		public string Minimal { get; set; }
	}

	public class IpMappingTests : TypeMappingTestBase<IpTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "ip",
					index = "no",
					precision_step = 4,
					boost = 1.3,
					null_value = "127.0.0.1",
					include_in_all = true
				},
				minimal = new
				{
					type = "ip"
				}
			}
		};

		protected override Func<PropertiesDescriptor<IpTest>, IPromise<IProperties>> FluentProperties => p => p
			.Ip(s => s
				.Name(o => o.Full)
				.Index(NonStringIndexOption.No)
				.PrecisionStep(4)
				.Boost(1.3)
				.NullValue("127.0.0.1")
				.IncludeInAll()
			)
			.Ip(b => b
				.Name(o => o.Minimal)
			);
	}
}

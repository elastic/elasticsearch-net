// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Mapping.Types.Specialized.Ip
{
	public class IpTest
	{
		[Ip(
			Index = false,
			NullValue = "127.0.0.1")]
		public string Full { get; set; }

		[Ip]
		public string Minimal { get; set; }
	}

	public class IpAttributeTests : AttributeTestsBase<IpTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "ip",
					index = false,
					null_value = "127.0.0.1"
				},
				minimal = new
				{
					type = "ip"
				}
			}
		};
	}
}

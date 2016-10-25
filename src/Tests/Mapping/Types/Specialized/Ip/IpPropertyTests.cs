using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using Tests.Framework.Integration;

namespace Tests.Mapping.Types.Core.Ip
{
	public class IpPropertyTests : PropertyTestsBase
	{
		public IpPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "ip",
					index = false,
					boost = 1.3,
					null_value = "127.0.0.1",
					include_in_all = true,
					doc_values = true,
					store = true,
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
				.Ip(s => s
					.Name(p => p.Name)
					.Index(false)
					.Boost(1.3)
					.NullValue("127.0.0.1")
					.IncludeInAll()
					.DocValues()
					.Store()
				);


		protected override IProperties InitializerProperties => new Properties
		{
			{ "name", new IpProperty
				{
					Index = false,
					Boost = 1.3,
					NullValue = "127.0.0.1",
					IncludeInAll = true,
					DocValues = true,
					Store = true,
				}
			}
		};
	}
}

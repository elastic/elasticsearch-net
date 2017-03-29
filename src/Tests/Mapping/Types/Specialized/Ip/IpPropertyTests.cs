using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.Mapping.Types.Specialized.Ip
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

#pragma warning disable 618 // Usage of IncludeInAll
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
#pragma warning restore 618

		protected override IProperties InitializerProperties => new Properties
		{
			{ "name", new IpProperty
				{
					Index = false,
					Boost = 1.3,
					NullValue = "127.0.0.1",
#pragma warning disable 618
					IncludeInAll = true,
#pragma warning restore 618
					DocValues = true,
					Store = true,
				}
			}
		};
	}
}

using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Mapping.Types.Core.Nested
{
	public class NestedPropertyTests : PropertyTestsBase
	{
		public NestedPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				leadDeveloper = new
				{
					type = "nested",
					include_in_parent = true,
					include_in_root = false,
					dynamic = "strict",
					enabled = true,
					include_in_all = true,
					properties = new
					{
						ipAddress = new
						{
							type = "ip"
						}
					}
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
				.Nested<Developer>(n => n
					.Name(p => p.LeadDeveloper)
					.IncludeInParent()
					.IncludeInRoot(false)
					.Dynamic(DynamicMapping.Strict)
					.Enabled()
					.IncludeInAll()
					.Properties(pps => pps
						.Ip(i => i
							.Name(p => p.IpAddress)
						)
					)
				);

		protected override IProperties InitializerProperties => new Properties
		{
			{ "leadDeveloper", new NestedProperty
				{
					IncludeInParent = true,
					IncludeInRoot = false,
					Dynamic = DynamicMapping.Strict,
					Enabled = true,
					IncludeInAll = true,
					Properties = new Properties
					{
						{ "ipAddress", new IpProperty () }
					}
				}
			}
		};
	}
}

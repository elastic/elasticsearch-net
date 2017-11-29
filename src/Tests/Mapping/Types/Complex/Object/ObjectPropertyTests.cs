using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Mapping.Types.Core.Object
{
	public class ObjectPropertyTests : PropertyTestsBase
	{
		public ObjectPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				leadDeveloper = new
				{
					type = "object",
					dynamic = true,
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
			.Object<Developer>(n => n
				.Name(p => p.LeadDeveloper)
				.Dynamic(true)
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
				{ "leadDeveloper", new ObjectProperty
					{
						Dynamic = true,
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

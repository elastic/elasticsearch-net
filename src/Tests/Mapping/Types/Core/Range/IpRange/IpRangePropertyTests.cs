using System;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.Mapping.Types.Core.Range.IpRange
{
	[SkipVersion("<5.5.0", "ip range type is a new 5.5.0 feature")]
	public class IpRangePropertyTests : PropertyTestsBase
	{
		public IpRangePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				ranges = new
				{
					type = "object",
					properties = new
					{
						ips = new
						{
							type = "ip_range",
							store = true,
							index = false,
							boost = 1.5,
							coerce = true
						}
					}
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Object<Ranges>(m => m
				.Name(p => p.Ranges)
				.Properties(props => props
					.IpRange(n => n
						.Name(p => p.Ips)
						.Store()
						.Index(false)
						.Boost(1.5)
						.Coerce()
					)
				)
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"ranges", new ObjectProperty
				{
					Properties = new Properties
					{
						{
							"ips", new IpRangeProperty
							{
								Store = true,
								Index = false,
								Boost = 1.5,
								Coerce = true
							}
						}
					}
				}
			}
		};
	}
}

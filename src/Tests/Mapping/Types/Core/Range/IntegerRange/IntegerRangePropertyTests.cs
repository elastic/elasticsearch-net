using System;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.Mapping.Types.Core.Range.IntegerRange
{
	[SkipVersion("<5.2.0", "dedicated range types is a new 5.2.0 feature")]
	public class IntegerRangePropertyTests : PropertyTestsBase
	{
		public IntegerRangePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				ranges = new
				{
					type = "object",
					properties = new
					{
						integers = new
						{
							type = "integer_range",
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
					.IntegerRange(n => n
						.Name(p => p.Integers)
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
							"integers", new IntegerRangeProperty
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

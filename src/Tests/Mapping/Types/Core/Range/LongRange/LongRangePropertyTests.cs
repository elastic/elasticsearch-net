using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Mapping.Types.Core.Range.LongRange
{
	public class LongRangePropertyTests : PropertyTestsBase
	{
		public LongRangePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				ranges = new
				{
					type = "object",
					properties = new
					{
						longs = new
						{
							type = "long_range",
							store = true,
							index = false,
							boost = 1.5,
							include_in_all = false,
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
					.LongRange(n => n
						.Name(p => p.Longs)
						.Store()
						.Index(false)
						.Boost(1.5)
						.IncludeInAll(false)
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
							"longs", new LongRangeProperty
							{
								Store = true,
								Index = false,
								Boost = 1.5,
								IncludeInAll = false,
								Coerce = true
							}
						}
					}
				}
			}
		};
	}
}

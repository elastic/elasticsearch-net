using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Mapping.Types.Core.Range.DoubleRange
{
	public class DoubleRangePropertyTests : PropertyTestsBase
	{
		public DoubleRangePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				ranges = new
				{
					type = "object",
					properties = new
					{
						doubles = new
						{
							type = "double_range",
							store = true,
							index = false,
							include_in_all = false,
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
					.DoubleRange(n => n
						.Name(p => p.Doubles)
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
							"doubles", new DoubleRangeProperty
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

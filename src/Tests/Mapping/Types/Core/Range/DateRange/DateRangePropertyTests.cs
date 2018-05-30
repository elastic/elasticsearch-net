using System;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.Mapping.Types.Core.Range.DateRange
{
	[SkipVersion("<5.2.0", "dedicated range types is a new 5.2.0 feature")]
	public class DateRangePropertyTests : PropertyTestsBase
	{
		private DateTime _nullValue = new DateTime(2000, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc);

		public DateRangePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				ranges = new
				{
					type = "object",
					properties = new
					{
						dates = new
						{
							type = "date_range",
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
					.DateRange(n => n
						.Name(p => p.Dates)
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
							"dates", new DateRangeProperty
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

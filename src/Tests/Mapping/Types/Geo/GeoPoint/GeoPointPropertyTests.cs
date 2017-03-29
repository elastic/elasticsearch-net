using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.Mapping.Types.Geo.GeoPoint
{
	public class GeoPointPropertyTests : PropertyTestsBase
	{
		public GeoPointPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				location = new
				{
					type = "geo_point",
					ignore_malformed = true
				},
			}
		};


		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
				.GeoPoint(s => s
					.Name(p => p.Location)
					.IgnoreMalformed()
				);


		protected override IProperties InitializerProperties => new Properties
		{
			{ "location", new GeoPointProperty
				{
					IgnoreMalformed = true
				}
			}
		};
	}
}

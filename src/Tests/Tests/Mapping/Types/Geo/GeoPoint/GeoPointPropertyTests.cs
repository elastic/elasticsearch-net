using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;

namespace Tests.Mapping.Types.Core.GeoPoint
{
	public class GeoPointPropertyTests : PropertyTestsBase
	{
		public GeoPointPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				locationPoint = new
				{
					type = "geo_point",
					ignore_malformed = true
				},
			}
		};


		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.GeoPoint(s => s
				.Name(p => p.LocationPoint)
				.IgnoreMalformed()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"locationPoint", new GeoPointProperty
				{
					IgnoreMalformed = true
				}
			}
		};
	}
}

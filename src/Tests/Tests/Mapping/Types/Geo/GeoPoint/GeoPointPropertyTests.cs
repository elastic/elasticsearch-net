using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Mapping.Types.Core.GeoPoint
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

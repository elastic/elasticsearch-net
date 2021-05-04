// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Geo.GeoPoint
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

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Geo.GeoShape
{
	public class GeoShapePropertyTests : PropertyTestsBase
	{
		public GeoShapePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				locationShape = new
				{
					type = "geo_shape",
					orientation = "cw",
					strategy = "recursive",
					coerce = true
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.GeoShape(s => s
				.Name(p => p.LocationShape)
				.Orientation(GeoOrientation.ClockWise)
				.Strategy(GeoStrategy.Recursive)
				.Coerce()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"locationShape", new GeoShapeProperty
				{
					Orientation = GeoOrientation.ClockWise,
					Strategy = GeoStrategy.Recursive,
					Coerce = true
				}
			}
		};
	}
}

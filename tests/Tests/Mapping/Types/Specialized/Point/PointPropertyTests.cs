// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Specialized.Point
{
	[SkipVersion("<7.8.0", "Points introduced in 7.8.0+")]
	public class PointPropertyTests : PropertyTestsBase
	{
		public PointPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				arbitraryPoint = new
				{
					type = "point",
					ignore_z_value = true
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Point(p => p
				.Name("arbitraryPoint")
				.IgnoreZValue()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"arbitraryPoint", new PointProperty
				{
					IgnoreZValue = true
				}
			}
		};
	}
}

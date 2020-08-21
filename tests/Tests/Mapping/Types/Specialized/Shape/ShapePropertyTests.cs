// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Specialized.Shape
{
	[SkipVersion("<7.4.0", "Shape queries introduced in 7.4.0+")]
	public class ShapePropertyTests : PropertyTestsBase
	{
		public ShapePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				arbitraryShape = new
				{
					type = "shape",
					orientation = "clockwise",
					coerce = true
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Shape(s => s
				.Name(p => p.ArbitraryShape)
				.Orientation(ShapeOrientation.ClockWise)
				.Coerce()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"arbitraryShape", new ShapeProperty
				{
					Orientation = ShapeOrientation.ClockWise,
					Coerce = true
				}
			}
		};
	}
}

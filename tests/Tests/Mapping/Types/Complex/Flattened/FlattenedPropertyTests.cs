// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Complex.Flattened
{
	[SkipVersion("<7.3.0", "introduced in 7.3.0")]
	public class FlattenedPropertyTests : PropertyTestsBase
	{
		public FlattenedPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				labels = new
				{
					type = "flattened",
					depth_limit = 5
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Flattened(n => n
				.Name(p => p.Labels)
				.DepthLimit(5)
			);

		protected override IProperties InitializerProperties => new Properties
		{
			{
				"labels", new FlattenedProperty
				{
					DepthLimit = 5
				}
			}
		};
	}
}

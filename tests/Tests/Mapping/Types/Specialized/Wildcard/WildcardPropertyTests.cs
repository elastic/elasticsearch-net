// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Specialized.Wildcard
{
	[SkipVersion("<7.9.0", "introduced in 7.9.0")]
	public class WildcardPropertyTests : PropertyTestsBase
	{
		public WildcardPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				description = new
				{
					type = "wildcard"
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Wildcard(s => s
				.Name(n => n.Description)
			);

		protected override IProperties InitializerProperties => new Properties
		{
			{
				"description", new WildcardProperty()
			}
		};
	}
}

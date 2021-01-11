// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Specialized.Version
{
	[SkipVersion("<7.10.0", "Version property introduced in 7.10.0")]
	public class VersionPropertyTests : PropertyTestsBase
	{
		public VersionPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "version"
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Version(s => s
				.Name(p => p.Name)
			);

		protected override IProperties InitializerProperties => new Properties
		{
			{
				"name", new VersionProperty
				{
				}
			}
		};
	}
}

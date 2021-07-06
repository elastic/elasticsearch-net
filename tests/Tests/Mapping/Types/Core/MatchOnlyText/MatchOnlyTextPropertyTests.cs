// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Core.MatchOnlyText
{
	[SkipVersion("<7.14.0", "Match only text property added in 7.14.0")]
	public class MatchOnlyTextPropertyTests : PropertyTestsBase
	{
		public MatchOnlyTextPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new { properties = new { name = new { type = "match_only_text" } } };

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.MatchOnlyText(s => s
				.Name(p => p.Name)
			);

		protected override IProperties InitializerProperties => new Properties { { "name", new MatchOnlyTextProperty() } };
	}
}

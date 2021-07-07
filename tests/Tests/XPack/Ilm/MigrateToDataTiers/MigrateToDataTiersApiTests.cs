// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Ilm.MigrateToDataTiers
{
	[SkipVersion("<7.14.0", "Migrate to data tiers added in 7.14.0")]
	public class MigrateToDataTiersApiTests
		: ApiTestBase<XPackCluster, MigrateToDataTiersResponse, IMigrateToDataTiersRequest, MigrateToDataTiersDescriptor, MigrateToDataTiersRequest>
	{
		public MigrateToDataTiersApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new { legacy_template_to_delete = "template-name", node_attribute = "test-attribute" };

		protected override Func<MigrateToDataTiersDescriptor, IMigrateToDataTiersRequest> Fluent => d => d
			.DryRun()
			.LegacyTemplateToDelete("template-name")
			.NodeAttribute("test-attribute");

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override MigrateToDataTiersRequest Initializer => new()
		{
			DryRun = true, LegacyTemplateToDelete = "template-name", NodeAttribute = "test-attribute"
		};

		protected override string UrlPath => "/_ilm/migrate_to_data_tiers?dry_run=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.IndexLifecycleManagement.MigrateToDataTiers(f),
			(client, f) => client.IndexLifecycleManagement.MigrateToDataTiersAsync(f),
			(client, r) => client.IndexLifecycleManagement.MigrateToDataTiers(r),
			(client, r) => client.IndexLifecycleManagement.MigrateToDataTiersAsync(r)
		);

		protected override MigrateToDataTiersDescriptor NewDescriptor() => new();
	}
}

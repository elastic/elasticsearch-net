// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Ilm.MigrateToDataTiers
{
	public class MigrateToDataTiersUrlRequests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await POST("/_ilm/migrate_to_data_tiers")
				.Fluent(c => c.IndexLifecycleManagement.MigrateToDataTiers())
				.Request(c => c.IndexLifecycleManagement.MigrateToDataTiers())
				.FluentAsync(c => c.IndexLifecycleManagement.MigrateToDataTiersAsync())
				.RequestAsync(c => c.IndexLifecycleManagement.MigrateToDataTiersAsync());
	}
}

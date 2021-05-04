// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Slm.StartLifecycle
{
	public class StartLifecycleUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await POST("/_slm/start")
				.Fluent(c => c.SnapshotLifecycleManagement.Start())
				.Request(c => c.SnapshotLifecycleManagement.Start(new StartSnapshotLifecycleManagementRequest()))
				.FluentAsync(c => c.SnapshotLifecycleManagement.StartAsync())
				.RequestAsync(c => c.SnapshotLifecycleManagement.StartAsync(new StartSnapshotLifecycleManagementRequest()));
	}
}

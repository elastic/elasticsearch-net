// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Migration.DeprecationInfo
{
	public class DeprecationInfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_migration/deprecations")
					.Fluent(c => c.Migration.DeprecationInfo())
					.Request(c => c.Migration.DeprecationInfo(new DeprecationInfoRequest()))
					.FluentAsync(c => c.Migration.DeprecationInfoAsync())
					.RequestAsync(c => c.Migration.DeprecationInfoAsync(new DeprecationInfoRequest()))
				;

			var index = "another-index";

			await GET($"/{index}/_migration/deprecations")
					.Fluent(c => c.Migration.DeprecationInfo(d => d.Index(index)))
					.Request(c => c.Migration.DeprecationInfo(new DeprecationInfoRequest(index)))
					.FluentAsync(c => c.Migration.DeprecationInfoAsync(d => d.Index(index)))
					.RequestAsync(c => c.Migration.DeprecationInfoAsync(new DeprecationInfoRequest(index)))
				;
		}
	}
}

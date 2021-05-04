// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Info
{
	public class XPackInfoUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_xpack")
					.Fluent(c => c.XPack.Info())
					.Request(c => c.XPack.Info())
					.FluentAsync(c => c.XPack.InfoAsync())
					.RequestAsync(c => c.XPack.InfoAsync())
				;

			await GET("/_xpack/usage")
					.Fluent(c => c.XPack.Usage())
					.Request(c => c.XPack.Usage())
					.FluentAsync(c => c.XPack.UsageAsync())
					.RequestAsync(c => c.XPack.UsageAsync())
				;
		}
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.License.StartBasicLicense
{
	public class StartBasicLicenseUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_license/start_basic")
			.Fluent(c => c.License.StartBasic())
			.Request(c => c.License.StartBasic(new StartBasicLicenseRequest()))
			.FluentAsync(c => c.License.StartBasicAsync())
			.RequestAsync(c => c.License.StartBasicAsync(new StartBasicLicenseRequest()));
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.License.GetLicense
{
	public class GetLicenseUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_license")
			.Fluent(c => c.License.Get())
			.Request(c => c.License.Get(new GetLicenseRequest()))
			.FluentAsync(c => c.License.GetAsync())
			.RequestAsync(c => c.License.GetAsync(new GetLicenseRequest()));
	}
}

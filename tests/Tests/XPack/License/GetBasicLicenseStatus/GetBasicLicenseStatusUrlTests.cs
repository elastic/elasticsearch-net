// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.License.GetBasicLicenseStatus
{
	public class GetBasicLicenseStatusUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_license/basic_status")
			.Fluent(c => c.License.GetBasicStatus())
			.Request(c => c.License.GetBasicStatus(new GetBasicLicenseStatusRequest()))
			.FluentAsync(c => c.License.GetBasicStatusAsync())
			.RequestAsync(c => c.License.GetBasicStatusAsync(new GetBasicLicenseStatusRequest()));
	}
}

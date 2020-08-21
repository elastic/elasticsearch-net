// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.License.GetTrialLicenseStatus
{
	public class GetTrialLicenseStatusUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_license/trial_status")
			.Fluent(c => c.License.GetTrialStatus())
			.Request(c => c.License.GetTrialStatus(new GetTrialLicenseStatusRequest()))
			.FluentAsync(c => c.License.GetTrialStatusAsync())
			.RequestAsync(c => c.License.GetTrialStatusAsync(new GetTrialLicenseStatusRequest()));
	}
}

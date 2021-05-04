// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.License.StartTrialLicense
{
	public class StartTrialLicenseUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_license/start_trial")
			.Fluent(c => c.License.StartTrial())
			.Request(c => c.License.StartTrial(new StartTrialLicenseRequest()))
			.FluentAsync(c => c.License.StartTrialAsync())
			.RequestAsync(c => c.License.StartTrialAsync(new StartTrialLicenseRequest()));
	}
}

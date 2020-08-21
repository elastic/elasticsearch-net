// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.License.PostLicense
{
	public class PostLicenseUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_license")
			.Fluent(c => c.License.Post())
			.Request(c => c.License.Post(new PostLicenseRequest()))
			.FluentAsync(c => c.License.PostAsync())
			.RequestAsync(c => c.License.PostAsync(new PostLicenseRequest()));
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.Ssl.GetCertificates
{
	public class GetCertificatesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.GET("/_ssl/certificates")
			.Fluent(c => c.Security.GetCertificates())
			.Request(c => c.Security.GetCertificates(new GetCertificatesRequest()))
			.FluentAsync(c => c.Security.GetCertificatesAsync())
			.RequestAsync(c => c.Security.GetCertificatesAsync(new GetCertificatesRequest()));
	}
}

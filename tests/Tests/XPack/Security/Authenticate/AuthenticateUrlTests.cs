// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.Authenticate
{
	public class AuthenticateUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_security/_authenticate")
			.Fluent(c => c.Security.Authenticate())
			.Request(c => c.Security.Authenticate(new AuthenticateRequest()))
			.FluentAsync(c => c.Security.AuthenticateAsync())
			.RequestAsync(c => c.Security.AuthenticateAsync(new AuthenticateRequest()));
	}
}

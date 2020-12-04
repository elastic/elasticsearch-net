// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.Security.ApiKey.GrantApiKey
{
	public class GrantApiKeyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.POST("/_security/api_key/grant")
			.Fluent(c => c.Security.GrantApiKey(p => p))
			.Request(c => c.Security.GrantApiKey(new GrantApiKeyRequest()))
			.FluentAsync(c => c.Security.GrantApiKeyAsync(p => p))
			.RequestAsync(c => c.Security.GrantApiKeyAsync(new GrantApiKeyRequest()));
	}
}

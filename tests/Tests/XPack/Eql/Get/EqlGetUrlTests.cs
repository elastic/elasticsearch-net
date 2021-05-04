// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Eql.Get
{
	public class EqlGetUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_eql/search/search_id")
			.Fluent(c => c.Eql.Get<Log>("search_id", f => f))
			.Request(c => c.Eql.Get<Log>(new EqlGetRequest("search_id")))
			.FluentAsync(c => c.Eql.GetAsync<Log>("search_id", f => f))
			.RequestAsync(c => c.Eql.GetAsync<Log>(new EqlGetRequest("search_id")));
	}
}

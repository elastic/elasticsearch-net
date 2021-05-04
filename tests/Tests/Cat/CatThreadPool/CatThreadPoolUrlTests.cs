// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatThreadPool
{
	public class CatThreadPoolUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/thread_pool")
			.Fluent(c => c.Cat.ThreadPool())
			.Request(c => c.Cat.ThreadPool(new CatThreadPoolRequest()))
			.FluentAsync(c => c.Cat.ThreadPoolAsync())
			.RequestAsync(c => c.Cat.ThreadPoolAsync(new CatThreadPoolRequest()));
	}
}

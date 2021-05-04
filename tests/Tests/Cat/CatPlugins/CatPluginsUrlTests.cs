// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatPlugins
{
	public class CatPluginsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/plugins")
			.Fluent(c => c.Cat.Plugins())
			.Request(c => c.Cat.Plugins(new CatPluginsRequest()))
			.FluentAsync(c => c.Cat.PluginsAsync())
			.RequestAsync(c => c.Cat.PluginsAsync(new CatPluginsRequest()));
	}
}

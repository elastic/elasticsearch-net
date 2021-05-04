// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatMaster
{
	public class CatMasterUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/master")
			.Fluent(c => c.Cat.Master())
			.Request(c => c.Cat.Master(new CatMasterRequest()))
			.FluentAsync(c => c.Cat.MasterAsync())
			.RequestAsync(c => c.Cat.MasterAsync(new CatMasterRequest()));
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Rollup.GetRollupIndexCapabilities
{
	public class GetRollupIndexCapabilitiesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string index = "rollup-index";
			await GET($"{index}/_rollup/data")
				.Fluent(c => c.Rollup.GetIndexCapabilities(index))
				.Request(c => c.Rollup.GetIndexCapabilities(new GetRollupIndexCapabilitiesRequest(index)))
				.FluentAsync(c => c.Rollup.GetIndexCapabilitiesAsync(index))
				.RequestAsync(c => c.Rollup.GetIndexCapabilitiesAsync(new GetRollupIndexCapabilitiesRequest(index)));
		}
	}
}

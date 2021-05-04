// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.Scripting.ExecutePainlessScript
{
	public class ExecutePainlessScriptUrlTests
	{
		[U] public async Task Urls()
		{
			var painless = "1 + 1";
			var request = new ExecutePainlessScriptRequest
			{
				Script = new InlineScript(painless)
			};

			await POST("/_scripts/painless/_execute")
					.Fluent(c => c.ExecutePainlessScript<string>(f => f.Script(s => s.Source(painless))))
					.Request(c => c.ExecutePainlessScript<string>(request))
					.FluentAsync(c => c.ExecutePainlessScriptAsync<string>(f => f.Script(s => s.Source(painless))))
					.RequestAsync(c => c.ExecutePainlessScriptAsync<string>(request))
				;
		}
	}
}

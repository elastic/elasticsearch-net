// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.Scripting.DeleteScript
{
	public class DeleteScriptUrlTests
	{
		[U] public async Task Urls()
		{
			var id = "id";

			await DELETE($"/_scripts/{id}")
					.Fluent(c => c.DeleteScript(id))
					.Request(c => c.DeleteScript(new DeleteScriptRequest(id)))
					.FluentAsync(c => c.DeleteScriptAsync(id))
					.RequestAsync(c => c.DeleteScriptAsync(new DeleteScriptRequest(id)))
				;
		}
	}
}

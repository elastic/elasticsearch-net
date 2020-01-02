using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.Scripting.GetScript
{
	public class GetScriptUrlTests
	{
		[U] public async Task Urls()
		{
			var id = "id";

			await GET($"/_scripts/{id}")
					.Fluent(c => c.GetScript(id))
					.Request(c => c.GetScript(new GetScriptRequest(id)))
					.FluentAsync(c => c.GetScriptAsync(id))
					.RequestAsync(c => c.GetScriptAsync(new GetScriptRequest(id)))
				;
		}
	}
}

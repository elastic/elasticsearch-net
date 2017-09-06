using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.Scripting.PutScript
{
	public class PutScriptUrlTests
	{
		[U] public async Task Urls()
		{
			var id = "id";

			await PUT($"/_scripts/{id}")
				.Fluent(c => c.PutScript(id, s=>s.Script("")))
				.Request(c => c.PutScript(new PutScriptRequest(id)))
				.FluentAsync(c => c.PutScriptAsync(id, s=>s.Script("")))
				.RequestAsync(c => c.PutScriptAsync(new PutScriptRequest(id)))
				;
		}
	}
}

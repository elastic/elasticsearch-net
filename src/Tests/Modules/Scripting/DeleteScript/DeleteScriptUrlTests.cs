using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

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

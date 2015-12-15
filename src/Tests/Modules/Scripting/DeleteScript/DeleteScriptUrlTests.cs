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
			var lang = "lang";
			var id = "id";

			await DELETE($"/_scripts/{lang}/{id}")
				.Fluent(c => c.DeleteScript(lang, id))
				.Request(c => c.DeleteScript(new DeleteScriptRequest(lang, id)))
				.FluentAsync(c => c.DeleteScriptAsync(lang, id))
				.RequestAsync(c => c.DeleteScriptAsync(new DeleteScriptRequest(lang, id)))
				;
		}
	}
}

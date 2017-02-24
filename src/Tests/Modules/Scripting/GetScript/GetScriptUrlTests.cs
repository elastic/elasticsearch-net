using System.Threading.Tasks;
using Nest_5_2_0;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.Scripting.GetScript
{
	public class GetScriptUrlTests
	{
		[U] public async Task Urls()
		{
			var lang = "lang";
			var id = "id";

			await GET($"/_scripts/{lang}/{id}")
				.Fluent(c => c.GetScript(lang, id))
				.Request(c => c.GetScript(new GetScriptRequest(lang, id)))
				.FluentAsync(c => c.GetScriptAsync(lang, id))
				.RequestAsync(c => c.GetScriptAsync(new GetScriptRequest(lang, id)))
				;
		}
	}
}

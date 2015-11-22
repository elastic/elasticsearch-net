using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.Scripting.PutScript
{
	public class PutScriptUrlTests
	{
		[U] public async Task Urls()
		{
			var lang = "lang";
			var id = "id";

			await PUT($"/_scripts/{lang}/{id}")
				.Fluent(c => c.PutScript(lang, id, s=>s.Script("")))
				.Request(c => c.PutScript(new PutScriptRequest(lang, id)))
				.FluentAsync(c => c.PutScriptAsync(lang, id, s=>s.Script("")))
				.RequestAsync(c => c.PutScriptAsync(new PutScriptRequest(lang, id)))
				;
		}
	}
}

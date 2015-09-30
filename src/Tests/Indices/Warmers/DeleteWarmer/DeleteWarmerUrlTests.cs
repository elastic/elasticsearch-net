using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.Warmers.DeleteWarmer
{
	public class DeleteWarmerUrlTests
	{
		[U] public async Task Urls()
		{
			var name = "id";

			await DELETE($"/_all/_warmer/{name}")
				.Fluent(c => c.DeleteWarmer(Nest.Indices.All, name))
				.Request(c => c.DeleteWarmer(new DeleteWarmerRequest("_all", name)))
				.FluentAsync(c => c.DeleteWarmerAsync(Nest.Indices.All, name))
				.RequestAsync(c => c.DeleteWarmerAsync(new DeleteWarmerRequest(Nest.Indices.All, name)))
				;
		}
	}
}

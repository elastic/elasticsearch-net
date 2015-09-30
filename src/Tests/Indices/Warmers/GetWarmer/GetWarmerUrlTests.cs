using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.Warmers.GetWarmer
{
	public class GetWarmerUrlTests
	{
		[U] public async Task Urls()
		{
			var name = "id";

			await GET($"/_warmer")
				.Fluent(c => c.GetWarmer())
				.Request(c => c.GetWarmer(new GetWarmerRequest()))
				.FluentAsync(c => c.GetWarmerAsync())
				.RequestAsync(c => c.GetWarmerAsync(new GetWarmerRequest()))
				;

			var index = "indexx";
			await GET($"/{index}/_warmer")
				.Fluent(c => c.GetWarmer(w=>w.Index(Nest.Indices.Single(index))))
				.Request(c => c.GetWarmer(new GetWarmerRequest(Nest.Indices.Single(index))))
				.FluentAsync(c => c.GetWarmerAsync(w=>w.Index(index)))
				.RequestAsync(c => c.GetWarmerAsync(new GetWarmerRequest(Nest.Indices.Single(index))))
				;

			await GET($"/{index}/_warmer/{name}")
				.Fluent(c => c.GetWarmer(w=>w.Index(index).Name(name)))
				.Request(c => c.GetWarmer(new GetWarmerRequest(index, name)))
				.FluentAsync(c => c.GetWarmerAsync(w=>w.Index(index).Name(name)))
				.RequestAsync(c => c.GetWarmerAsync(new GetWarmerRequest(index, name)))
				;

			await GET($"/_warmer/{name}")
				.Fluent(c => c.GetWarmer(w=>w.Name(name)))
				.Request(c => c.GetWarmer(new GetWarmerRequest((Name)name)))
				.FluentAsync(c => c.GetWarmerAsync(w=>w.Name(name)))
				.RequestAsync(c => c.GetWarmerAsync(new GetWarmerRequest((Name)name)))
				;
		}
	}
}

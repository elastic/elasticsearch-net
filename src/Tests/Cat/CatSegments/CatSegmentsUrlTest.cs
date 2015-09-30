using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatAliases
{
	public class CatSegmentsUrlTest : IUrlTest
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/segments")
				.Fluent(c => c.CatSegments())
				.Request(c => c.CatSegments(new CatSegmentsRequest()))
				.FluentAsync(c => c.CatSegmentsAsync())
				.RequestAsync(c => c.CatSegmentsAsync(new CatSegmentsRequest()))
				;

			await GET("/_cat/segments/project")
				.Fluent(c => c.CatSegments(r => r.Index<Project>()))
				.Request(c => c.CatSegments(new CatSegmentsRequest(Nest.Indices.Single<Project>())))
				.FluentAsync(c => c.CatSegmentsAsync(r => r.Index<Project>()))
				.RequestAsync(c => c.CatSegmentsAsync(new CatSegmentsRequest(Nest.Indices.Single<Project>())));
		}
	}
}

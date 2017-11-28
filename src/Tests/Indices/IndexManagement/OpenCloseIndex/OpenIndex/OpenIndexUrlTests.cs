﻿using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.OpenIndex
{
	public class OpenIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Nest.Indices.Index<Project>().And<Developer>();
			var index = "project%2Cdevs";
			await UrlTester.POST($"/{index}/_open")
				.Fluent(c => c.OpenIndex(indices, s=>s))
				.Request(c => c.OpenIndex(new OpenIndexRequest(indices)))
				.FluentAsync(c => c.OpenIndexAsync(indices))
				.RequestAsync(c => c.OpenIndexAsync(new OpenIndexRequest(indices)))
				;

		}
	}
}

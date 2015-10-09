using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest.Indices;
using static Nest.Types;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.CloseIndex
{
	public class CloseIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Index<Project>().And<CommitActivity>();
			var index = "project";
			await POST($"/{index}/_close")
				.Fluent(c => c.CloseIndex(indices, s=>s))
				.Request(c => c.CloseIndex(new CloseIndexRequest(indices)))
				.FluentAsync(c => c.CloseIndexAsync(indices))
				.RequestAsync(c => c.CloseIndexAsync(new CloseIndexRequest(indices)))
				;

		}
	}
}

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

namespace Tests.Modules.Indices.IndexManagement.IndexExists
{
	public class IndexExistsUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Index<Project>().And<CommitActivity>();
			var index = "project";
			await HEAD($"/{index}")
				.Fluent(c => c.IndexExists(index, s=>s))
				.Request(c => c.IndexExists(new IndexExistsRequest(index)))
				.FluentAsync(c => c.IndexExistsAsync(index))
				.RequestAsync(c => c.IndexExistsAsync(new IndexExistsRequest(index)))
				;

		}
	}
}

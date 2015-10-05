using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.StatusManagement.Refresh
{
	public class RefreshUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_refresh")
				.Fluent(c => c.Refresh(All))
				.Request(c => c.Refresh(new RefreshRequest()))
				.FluentAsync(c => c.RefreshAsync(All))
				.RequestAsync(c => c.RefreshAsync(new RefreshRequest()))
				;

			var index = "index1,index2";
			await POST($"/{index}/_refresh")
				.Fluent(c => c.Refresh(index))
				.Request(c => c.Refresh(new RefreshRequest(index)))
				.FluentAsync(c => c.RefreshAsync(index))
				.RequestAsync(c => c.RefreshAsync(new RefreshRequest(index)))
				;
		}
	}
}

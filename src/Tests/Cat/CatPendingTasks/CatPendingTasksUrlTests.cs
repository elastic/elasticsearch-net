using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatPendingTasks
{
	public class CatPendingTasksUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/pending_tasks")
				.Fluent(c => c.CatPendingTasks())
				.Request(c => c.CatPendingTasks(new CatPendingTasksRequest()))
				.FluentAsync(c => c.CatPendingTasksAsync())
				.RequestAsync(c => c.CatPendingTasksAsync(new CatPendingTasksRequest()))
				;
		}
	}
}

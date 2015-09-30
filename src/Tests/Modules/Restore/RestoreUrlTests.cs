using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.Restore.Restore
{
	public class RestoreUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";
			var snapshot = "snap";

			await POST($"/_snapshot/{repository}/{snapshot}/_restore")
				.Fluent(c => c.Restore(repository, snapshot))
				.Request(c => c.Restore(new RestoreRequest(repository, snapshot)))
				.FluentAsync(c => c.RestoreAsync(repository, snapshot))
				.RequestAsync(c => c.RestoreAsync(new RestoreRequest(repository, snapshot)))
				;
		}
	}
}

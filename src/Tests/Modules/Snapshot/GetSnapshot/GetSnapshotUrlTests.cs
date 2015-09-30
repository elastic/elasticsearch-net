using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.GetSnapshot.GetSnapshot
{
	public class GetSnapshotUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";
			var snapshot = "snap";

			await GET($"/_snapshot/{repository}/{snapshot}")
				.Fluent(c => c.GetSnapshot(repository, snapshot))
				.Request(c => c.GetSnapshot(new GetSnapshotRequest(repository, snapshot)))
				.FluentAsync(c => c.GetSnapshotAsync(repository, snapshot))
				.RequestAsync(c => c.GetSnapshotAsync(new GetSnapshotRequest(repository, snapshot)))
				;
		}
	}
}

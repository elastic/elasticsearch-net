using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatSnapshots
{
	public class CatSnapshotsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/snapshots/foo")
				.Fluent(c => c.CatSnapshots("foo"))
				.Request(c => c.CatSnapshots(new CatSnapshotsRequest("foo")))
				.FluentAsync(c => c.CatSnapshotsAsync("foo"))
				.RequestAsync(c => c.CatSnapshotsAsync(new CatSnapshotsRequest("foo")))
				;

			await GET("/_cat/snapshots/foo?v=true")
				.Fluent(c => c.CatSnapshots("foo", s=>s.Verbose()))
				.Request(c => c.CatSnapshots(new CatSnapshotsRequest("foo") { Verbose = true }))
				.FluentAsync(c => c.CatSnapshotsAsync("foo", s=>s.Verbose()))
				.RequestAsync(c => c.CatSnapshotsAsync(new CatSnapshotsRequest("foo") { Verbose = true }))
				;
		}
	}
}

using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Percolator.MultiPercolate
{
	public class MultiPercolateUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "indexx";
			await POST($"/_mpercolate")
				.Fluent(c=>c.MultiPercolate(s=>s))
				.Request(c=>c.MultiPercolate(new MultiPercolateRequest()))
				.FluentAsync(c=>c.MultiPercolateAsync(s=> s))
				.RequestAsync(c=>c.MultiPercolateAsync(new MultiPercolateRequest()))
				;

			await POST($"/{index}/_mpercolate")
				.Fluent(c=>c.MultiPercolate(s=>s.Index(index)))
				.Request(c=>c.MultiPercolate(new MultiPercolateRequest(index)))
				.FluentAsync(c=>c.MultiPercolateAsync(s=> s.Index(index)))
				.RequestAsync(c=>c.MultiPercolateAsync(new MultiPercolateRequest(index)))
				;

			await POST($"/{index}/commits/_mpercolate")
				.Fluent(c=>c.MultiPercolate(s=>s.Index(index).Type<CommitActivity>()))
				.Request(c=>c.MultiPercolate(new MultiPercolateRequest(index, TypeName.From<CommitActivity>())))
				.FluentAsync(c=>c.MultiPercolateAsync(s=> s.Index(index).Type(typeof(CommitActivity))))
				.RequestAsync(c=>c.MultiPercolateAsync(new MultiPercolateRequest(index, typeof(CommitActivity))))
				;
		}
	}
}

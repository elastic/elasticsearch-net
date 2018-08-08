using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

#pragma warning disable 618 // testing deprecated percolate APIs

namespace Tests.Search.Percolator.MultiPercolate
{
	[SkipVersion("5.0.0-alpha2,5.0.0-alpha3", "deprecated")]
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

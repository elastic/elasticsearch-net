using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Search.MultiSearch.MultiSearchTemplate
{
	public class MultiSearchTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "indexx";

			await POST($"/_msearch/template")
				.Fluent(c=>c.MultiSearchTemplate(s=>s))
				.Request(c=>c.MultiSearchTemplate(new MultiSearchTemplateRequest()))
				.FluentAsync(c=>c.MultiSearchTemplateAsync(s=> s))
				.RequestAsync(c=>c.MultiSearchTemplateAsync(new MultiSearchTemplateRequest()))
				;

			await POST($"/{index}/_msearch/template")
				.Fluent(c=>c.MultiSearchTemplate(s=>s.Index(index)))
				.Request(c=>c.MultiSearchTemplate(new MultiSearchTemplateRequest(index)))
				.FluentAsync(c=>c.MultiSearchTemplateAsync(s=> s.Index(index)))
				.RequestAsync(c=>c.MultiSearchTemplateAsync(new MultiSearchTemplateRequest(index)))
				;

			await POST($"/{index}/commits/_msearch/template")
				.Fluent(c=>c.MultiSearchTemplate(s=>s.Index(index).Type<CommitActivity>()))
				.Request(c=>c.MultiSearchTemplate(new MultiSearchTemplateRequest(index, TypeName.From<CommitActivity>())))
				.FluentAsync(c=>c.MultiSearchTemplateAsync(s=> s.Index(index).Type(typeof(CommitActivity))))
				.RequestAsync(c=>c.MultiSearchTemplateAsync(new MultiSearchTemplateRequest(index, typeof(CommitActivity))))
				;
		}
	}
}

using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Nest.Infer;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.OpenOpenIndex.OpenIndex
{
	public class OpenIndexUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var indices = Index<Project>().And<CommitActivity>();
			var index = "project";
			await POST($"/{index}/_open")
					.Fluent(c => c.OpenIndex(indices, s => s))
					.Request(c => c.OpenIndex(new OpenIndexRequest(indices)))
					.FluentAsync(c => c.OpenIndexAsync(indices))
					.RequestAsync(c => c.OpenIndexAsync(new OpenIndexRequest(indices)))
				;
		}
	}
}

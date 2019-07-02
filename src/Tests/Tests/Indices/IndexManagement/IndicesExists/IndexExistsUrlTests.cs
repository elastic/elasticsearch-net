using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;

namespace Tests.Indices.IndexManagement.IndicesExists
{
	public class IndexExistsUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Nest.Indices.Index<Project>().And<CommitActivity>();
			var index = "project";
			await UrlTester.HEAD($"/{index}")
					.Fluent(c => c.Indices.Exists(index, s => s))
					.Request(c => c.Indices.Exists(new IndexExistsRequest(index)))
					.FluentAsync(c => c.Indices.ExistsAsync(index))
					.RequestAsync(c => c.Indices.ExistsAsync(new IndexExistsRequest(index)))
				;
		}
	}
}

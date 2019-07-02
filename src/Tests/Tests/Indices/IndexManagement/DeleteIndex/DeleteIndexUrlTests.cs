using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Nest.Indices;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexManagement.DeleteIndex
{
	public class DeleteIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Index<Project>().And<Developer>();
			var index = "project%2Cdevs";
			await DELETE($"/{index}")
					.Fluent(c => c.Indices.Delete(indices, s => s))
					.Request(c => c.Indices.Delete(new DeleteIndexRequest(indices)))
					.FluentAsync(c => c.Indices.DeleteAsync(indices))
					.RequestAsync(c => c.Indices.DeleteAsync(new DeleteIndexRequest(indices)))
				;
		}
	}
}

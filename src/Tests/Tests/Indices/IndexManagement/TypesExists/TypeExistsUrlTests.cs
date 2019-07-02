using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;

namespace Tests.Indices.IndexManagement.TypesExists
{
	public class TypeExistsUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Nest.Indices.Index<Project>().And<Developer>();
			var index = "project%2Cdevs";
			var types = "_doc";
			var type = "_doc";
			await UrlTester.HEAD($"/{index}/_mapping/{type}")
					.Fluent(c => c.Indices.TypeExists(indices, types))
					.Request(c => c.Indices.TypeExists(new TypeExistsRequest(indices, types)))
					.FluentAsync(c => c.Indices.TypeExistsAsync(indices, types))
					.RequestAsync(c => c.Indices.TypeExistsAsync(new TypeExistsRequest(indices, types)))
				;
		}
	}
}

using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Nest.Indices;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.TypeExists
{
	public class TypeExistsUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Index<Project>().And<Developer>();
			var index = "project%2Cdevs";
			var types = "_doc";
			var type = "_doc";
			await HEAD($"/{index}/_mapping/{type}")
					.Fluent(c => c.Indices.TypeExists(indices, types))
					.Request(c => c.Indices.TypeExists(new TypeExistsRequest(indices, types)))
					.FluentAsync(c => c.Indices.TypeExistsAsync(indices, types))
					.RequestAsync(c => c.Indices.TypeExistsAsync(new TypeExistsRequest(indices, types)))
				;
		}
	}
}

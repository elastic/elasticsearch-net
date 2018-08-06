using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest.Types;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.MappingManagement.GetMapping
{
	public class GetMappingUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1,index2";
			var types = Type<Project>().And<Developer>();
			await GET($"/index1%2Cindex2/_mapping/doc%2Cdeveloper")
				.Fluent(c => c.GetMapping<Project>(m=>m.Index(index).Type(types)))
				.Request(c => c.GetMapping(new GetMappingRequest(index, types)))
				.FluentAsync(c => c.GetMappingAsync<Project>(m=>m.Index(index).Type(types)))
				.RequestAsync(c => c.GetMappingAsync(new GetMappingRequest(index, types)))
				;

		}
	}
}

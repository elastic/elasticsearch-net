using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.MappingManagement.PutMapping
{
	public class PutMappingUrlTests
	{
		[U] public async Task Urls()
		{
			await PUT($"/project/project/_mapping")
				.Fluent(c => c.Map<Project>(m=>m))
				.Request(c => c.Map(new PutMappingRequest("project", TypeName.From<Project>())))
				.Request(c => c.Map(new PutMappingRequest<Project>()))
				.FluentAsync(c => c.MapAsync<Project>(m=>m))
				.RequestAsync(c => c.MapAsync(new PutMappingRequest("project", "project")))
				.RequestAsync(c => c.MapAsync(new PutMappingRequest<Project>()))
				;

		}
	}
}

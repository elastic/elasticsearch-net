using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest.Infer;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.MappingManagement.GetFieldMapping
{
	public class GetFieldMappingUrlTests
	{
		[U]
		public async Task Urls()
		{
			var index = "index1,index2";
			Nest.Indices indices = index;
			var types = Type<Project>().And<CommitActivity>();
			var fields = Field<Project>(p => p.Name).And("field");
			await GET($"/_mapping/field/name%2Cfield")
				.Fluent(c => c.GetFieldMapping<Project>(fields))
				.Request(c => c.GetFieldMapping(new GetFieldMappingRequest(fields)))
				.FluentAsync(c => c.GetFieldMappingAsync<Project>(fields))
				.RequestAsync(c => c.GetFieldMappingAsync(new GetFieldMappingRequest(fields)))
				;

			await GET($"/index1%2Cindex2/_mapping/field/name%2Cfield")
				.Fluent(c => c.GetFieldMapping<Project>(fields, g => g.Index(index)))
				.Request(c => c.GetFieldMapping(new GetFieldMappingRequest(indices, fields)))
				.FluentAsync(c => c.GetFieldMappingAsync<Project>(fields, g => g.Index(index)))
				.RequestAsync(c => c.GetFieldMappingAsync(new GetFieldMappingRequest(indices, fields)))
				;

			await GET($"/_mapping/project%2Ccommits/field/name%2Cfield")
				.Fluent(c => c.GetFieldMapping<Project>(fields, g => g.Type(types)))
				.Request(c => c.GetFieldMapping(new GetFieldMappingRequest(types, fields)))
				.FluentAsync(c => c.GetFieldMappingAsync<Project>(fields, g => g.Type(types)))
				.RequestAsync(c => c.GetFieldMappingAsync(new GetFieldMappingRequest(types, fields)))
				;

			await GET($"/index1%2Cindex2/_mapping/project%2Ccommits/field/name%2Cfield")
				.Fluent(c => c.GetFieldMapping<Project>(fields, g => g.Index(indices).Type(types)))
				.Request(c => c.GetFieldMapping(new GetFieldMappingRequest(indices, types, fields)))
				.FluentAsync(c => c.GetFieldMappingAsync<Project>(fields, g => g.Index(indices).Type(types)))
				.RequestAsync(c => c.GetFieldMappingAsync(new GetFieldMappingRequest(indices, types, fields)))
				;
		}
	}
}

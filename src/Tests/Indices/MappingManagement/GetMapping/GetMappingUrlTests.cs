using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest.Indices;
using static Nest.Types;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.Indices.MappingManagement.GetMapping
{
	public class GetMappingUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1,index2";
			var types = Type<Project>().And<CommitActivity>();
			await GET($"/{index}/_mapping/project,commits")
				.Fluent(c => c.GetMapping<Project>(m=>m.Index(index).Type(types)))
				.Request(c => c.GetMapping(new GetMappingRequest(index, types)))
				.FluentAsync(c => c.GetMappingAsync<Project>(m=>m.Index(index).Type(types)))
				.RequestAsync(c => c.GetMappingAsync(new GetMappingRequest(index, types)))
				;

		}
	}
}

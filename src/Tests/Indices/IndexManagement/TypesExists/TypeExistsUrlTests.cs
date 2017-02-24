using System.Threading.Tasks;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest_5_2_0.Indices;
using static Nest_5_2_0.Types;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.TypeExists
{
	public class TypeExistsUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Index<Project>().And<CommitActivity>();
			var index = "project";
			var types = Type<Project>().And<CommitActivity>();
			var type = "project%2Ccommits";
			await HEAD($"/{index}/_mapping/{type}")
				.Fluent(c => c.TypeExists(indices, types))
				.Request(c => c.TypeExists(new TypeExistsRequest(indices, types)))
				.FluentAsync(c => c.TypeExistsAsync(indices, types))
				.RequestAsync(c => c.TypeExistsAsync(new TypeExistsRequest(indices, types)))
				;

		}
	}
}

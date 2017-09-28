using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest.Indices;
using static Nest.Types;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.TypeExists
{
	public class TypeExistsUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Index<Project>().And<CommitActivity>();
			var index = "project%2Ccommits";
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

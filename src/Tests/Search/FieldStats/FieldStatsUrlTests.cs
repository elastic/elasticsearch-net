using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Search.FieldStats
{
	public class FieldStatsUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/project/_field_stats")
				.Fluent(c => c.FieldStats(Nest.Indices.Index<Project>()))
				.Request(c => c.FieldStats(new FieldStatsRequest("project") {}))
				.FluentAsync(c => c.FieldStatsAsync(typeof(Project)))
				.RequestAsync(c => c.FieldStatsAsync(new FieldStatsRequest("project")))
				;
		}
	}
}

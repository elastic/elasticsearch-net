using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Document.Multiple.Bulk
{
	public class BulkUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_bulk")
				.Fluent(c => c.Bulk(b=>b))
				.Request(c => c.Bulk(new BulkRequest()))
				.FluentAsync(c => c.BulkAsync(b=>b))
				.RequestAsync(c => c.BulkAsync(new BulkRequest()))
				;

			await POST("/project/_bulk")
				.Fluent(c => c.Bulk(b => b.Index<Project>()))
				.Request(c => c.Bulk(new BulkRequest(typeof(Project))))
				.FluentAsync(c => c.BulkAsync(b => b.Index<Project>()))
				.RequestAsync(c => c.BulkAsync(new BulkRequest(typeof(Project))))
				;

			await POST("/project/project/_bulk")
				.Fluent(c => c.Bulk(b => b.Index(typeof(Project)).Type(typeof(Project))))
				.Request(c => c.Bulk(new BulkRequest(typeof(Project), typeof(Project))))
				.FluentAsync(c => c.BulkAsync(b => b.Index(typeof(Project)).Type(typeof(Project))))
				.RequestAsync(c => c.BulkAsync(new BulkRequest(typeof(Project), typeof(Project))))
				;
		}
	}
}

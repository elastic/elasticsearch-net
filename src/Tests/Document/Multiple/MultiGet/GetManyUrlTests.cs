using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.CapturingUrlTester;

namespace Tests.Document.Multiple.MultiGet
{
	public class GetManyUrlTests : IUrlTests
	{
		private static readonly string[] StringIds = { "1" };
		private static readonly long[] LongIds = { 1 };

		[U]
		public async Task Urls()
		{
			await POST("/_mget")
				.Request(c => c.GetMany<Project>(StringIds))
				.Request(c => c.GetMany<Project>(LongIds))
				.RequestAsync(c => c.GetManyAsync<Project>(StringIds))
				.RequestAsync(c => c.GetManyAsync<Project>(LongIds))
				;

			await POST("/project/_mget")
				.Request(c => c.GetMany<Project>(StringIds, "project"))
				.Request(c => c.GetMany<Project>(LongIds, "project"))
				.RequestAsync(c => c.GetManyAsync<Project>(StringIds, "project"))
				.RequestAsync(c => c.GetManyAsync<Project>(LongIds, "project"))
				;

			await POST("/project/sometype/_mget")
				.Request(c => c.GetMany<Project>(StringIds, "project", "sometype"))
				.Request(c => c.GetMany<Project>(LongIds, "project", "sometype"))
				.RequestAsync(c => c.GetManyAsync<Project>(StringIds, "project", "sometype"))
				.RequestAsync(c => c.GetManyAsync<Project>(LongIds, "project", "sometype"))
				;
		}
	}
}

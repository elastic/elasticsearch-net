using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexManagement.RolloverIndex
{
	public class RolloverIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var alias = "alias1";
			await POST($"/{alias}/_rollover")
				.Fluent(c => c.RolloverIndex(alias))
				.Request(c => c.RolloverIndex(new RolloverIndexRequest(alias)))
				.FluentAsync(c => c.RolloverIndexAsync(alias))
				.RequestAsync(C => C.RolloverIndexAsync(new RolloverIndexRequest(alias)));

			var index = "newindex";

			await POST($"/{alias}/_rollover/{index}")
				.Fluent(c => c.RolloverIndex(alias, r => r.NewIndex(index)))
				.Request(c => c.RolloverIndex(new RolloverIndexRequest(alias, index)))
				.FluentAsync(c => c.RolloverIndexAsync(alias, r => r.NewIndex(index)))
				.RequestAsync(C => C.RolloverIndexAsync(new RolloverIndexRequest(alias, index)));
		}
	}
}

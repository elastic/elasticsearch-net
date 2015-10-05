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

namespace Tests.Indices.IndexManagement.GetIndex
{
	public class GetIndexUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1";
			await GET($"/{index}")
				.Fluent(c => c.GetIndex(index, s=>s))
				.Request(c => c.GetIndex(new GetIndexRequest(index)))
				.FluentAsync(c => c.GetIndexAsync(index))
				.RequestAsync(c => c.GetIndexAsync(new GetIndexRequest(index)))
				;

			var features = GetIndexFeature.Mappings | GetIndexFeature.Settings;
			await GET($"/{index}/mappings,settings")
				.Fluent(c => c.GetIndex(index, s=>s.Feature(features)))
				.Request(c => c.GetIndex(new GetIndexRequest(index, features)))
				.FluentAsync(c => c.GetIndexAsync(index, s=>s.Feature(features)))
				.RequestAsync(c => c.GetIndexAsync(new GetIndexRequest(index, features)))
				;
		}
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.Document.Multiple.UpdateByQueryRethrottle
{
	public class UpdateByQueryRethrottleUrlTests : UrlTestsBase
	{
		private readonly TaskId _taskId = "rhtoNesNR4aXVIY2bRR4GQ:13056";

		[U] public override async Task Urls() =>
			await UrlTester.POST($"/_update_by_query/{UrlTester.EscapeUriString(_taskId.ToString())}/_rethrottle")
				.Fluent(c => c.UpdateByQueryRethrottle(_taskId))
				.Request(c => c.UpdateByQueryRethrottle(new UpdateByQueryRethrottleRequest(_taskId)))
				.FluentAsync(c => c.UpdateByQueryRethrottleAsync(_taskId))
				.RequestAsync(c => c.UpdateByQueryRethrottleAsync(new UpdateByQueryRethrottleRequest(_taskId)));
	}
}

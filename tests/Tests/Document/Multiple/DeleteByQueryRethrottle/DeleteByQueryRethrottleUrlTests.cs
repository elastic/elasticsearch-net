// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Document.Multiple.DeleteByQueryRethrottle
{
	public class DeleteByQueryRethrottleUrlTests : UrlTestsBase
	{
		private readonly TaskId _taskId = "rhtoNesNR4aXVIY2bRR4GQ:13056";

		[U] public override async Task Urls() =>
			await POST($"/_delete_by_query/{EscapeUriString(_taskId.ToString())}/_rethrottle")
				.Fluent(c => c.DeleteByQueryRethrottle(_taskId))
				.Request(c => c.DeleteByQueryRethrottle(new DeleteByQueryRethrottleRequest(_taskId)))
				.FluentAsync(c => c.DeleteByQueryRethrottleAsync(_taskId))
				.RequestAsync(c => c.DeleteByQueryRethrottleAsync(new DeleteByQueryRethrottleRequest(_taskId)));
	}
}

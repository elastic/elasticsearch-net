/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

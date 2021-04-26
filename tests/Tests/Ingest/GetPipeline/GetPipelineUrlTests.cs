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
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Ingest.GetPipeline
{
	public class GetPipelineUrlTests
	{
		[U] public async Task Urls()
		{
			await GET($"/_ingest/pipeline")
					.Fluent(c => c.Ingest.GetPipeline())
					.Request(c => c.Ingest.GetPipeline(new GetPipelineRequest()))
					.FluentAsync(c => c.Ingest.GetPipelineAsync())
					.RequestAsync(c => c.Ingest.GetPipelineAsync())
				;

			var id = "id";

			await GET($"/_ingest/pipeline/{id}")
					.Fluent(c => c.Ingest.GetPipeline(g => g.Id(id)))
					.Request(c => c.Ingest.GetPipeline(new GetPipelineRequest(id)))
					.FluentAsync(c => c.Ingest.GetPipelineAsync(g => g.Id(id)))
					.RequestAsync(c => c.Ingest.GetPipelineAsync(new GetPipelineRequest(id)))
				;
		}
	}
}

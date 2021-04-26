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

namespace Tests.Ingest.SimulatePipeline
{
	public class SimulatePipelineUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_ingest/pipeline/_simulate")
					.Fluent(c => c.Ingest.SimulatePipeline(s => s))
					.Request(c => c.Ingest.SimulatePipeline(new SimulatePipelineRequest()))
					.FluentAsync(c => c.Ingest.SimulatePipelineAsync(s => s))
					.RequestAsync(c => c.Ingest.SimulatePipelineAsync(new SimulatePipelineRequest()))
				;

			var id = "id";
			await POST($"/_ingest/pipeline/{id}/_simulate")
					.Fluent(c => c.Ingest.SimulatePipeline(s => s.Id(id)))
					.Request(c => c.Ingest.SimulatePipeline(new SimulatePipelineRequest(id)))
					.FluentAsync(c => c.Ingest.SimulatePipelineAsync(s => s.Id(id)))
					.RequestAsync(c => c.Ingest.SimulatePipelineAsync(new SimulatePipelineRequest(id)))
				;
		}
	}
}

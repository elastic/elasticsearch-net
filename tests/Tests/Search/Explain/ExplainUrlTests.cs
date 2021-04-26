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
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Search.Explain
{
	public class ExplainUrlTests
	{
		[U] public async Task Urls()
		{
			var project = new Project { Name = "NEST" };
			var routing = Infer.Route(project);

			await POST("/project/_explain/NEST?routing=NEST")
					.Fluent(c => c.Explain<Project>(project, e => e.Routing(routing).Query(q => q.MatchAll())))
					.Request(c => c.Explain<Project>(new ExplainRequest<Project>(project) { Routing = routing }))
					.FluentAsync(c => c.ExplainAsync<Project>(project, e => e.Routing(routing).Query(q => q.MatchAll())))
					.RequestAsync(c => c.ExplainAsync<Project>(new ExplainRequest<Project>(project) { Routing = routing }))
				;

			await POST("/project/_explain/NEST")
					.Fluent(c => c.Explain<Project>("NEST", e => e.Query(q => q.MatchAll())))
					.Request(c => c.Explain<Project>(new ExplainRequest<Project>("project", "NEST")))
					.FluentAsync(c => c.ExplainAsync<Project>("NEST", e => e.Query(q => q.MatchAll())))
					.RequestAsync(c => c.ExplainAsync<Project>(new ExplainRequest<Project>("NEST")))
				;
		}
	}
}

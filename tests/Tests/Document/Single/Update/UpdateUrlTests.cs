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

namespace Tests.Document.Single.Update
{
	public class UpdateUrlTests
	{
		[U] public async Task Urls()
		{
			var document = new Project { Name = "foo" };

			await POST($"/project/_update/foo")
					.Fluent(c => c.Update<Project>(document, u => u))
					.Request(c => c.Update(new UpdateRequest<Project, object>(document)))
					.FluentAsync(c => c.UpdateAsync<Project>(document, u => u))
					.RequestAsync(c => c.UpdateAsync(new UpdateRequest<Project, object>(document)))
				;

			var otherId = "other-id";

			await POST($"/project/_update/{otherId}")
					.Fluent(c => c.Update<Project>(otherId, u => u))
					.Request(c => c.Update(new UpdateRequest<Project, object>(typeof(Project), otherId)))
					.FluentAsync(c => c.UpdateAsync<Project>(otherId, u => u))
					.RequestAsync(c => c.UpdateAsync(new UpdateRequest<Project, object>(typeof(Project), otherId)))
				;

			var otherIndex = "other-index";

			await POST($"/{otherIndex}/_update/{otherId}")
					.Fluent(c => c.Update<Project>(otherId, u => u.Index(otherIndex)))
					.Request(c => c.Update(new UpdateRequest<Project, object>(otherIndex, otherId)))
					.FluentAsync(c => c.UpdateAsync<Project>(otherId, u => u.Index(otherIndex)))
					.RequestAsync(c => c.UpdateAsync(new UpdateRequest<Project, object>(otherIndex, otherId)))
				;
		}
	}
}

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
using Elasticsearch.Net;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.Snapshot
{
	public class SnapshotUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";
			var snapshot = "snap";

			await PUT($"/_snapshot/{repository}/{snapshot}")
					.Fluent(c => c.Snapshot.Snapshot(repository, snapshot))
					.Request(c => c.Snapshot.Snapshot(new SnapshotRequest(repository, snapshot)))
					.FluentAsync(c => c.Snapshot.SnapshotAsync(repository, snapshot))
					.RequestAsync(c => c.Snapshot.SnapshotAsync(new SnapshotRequest(repository, snapshot)))
				;


			await ExpectUrl(HttpMethod.PUT, $"/_snapshot/{repository}/{snapshot}?pretty=true", s => s.PrettyJson())
				.Fluent(c => c.Snapshot.Snapshot(repository, snapshot))
				.Request(c => c.Snapshot.Snapshot(new SnapshotRequest(repository, snapshot)))
				.FluentAsync(c => c.Snapshot.SnapshotAsync(repository, snapshot))
				.RequestAsync(c => c.Snapshot.SnapshotAsync(new SnapshotRequest(repository, snapshot)));

			await ExpectUrl(HttpMethod.PUT, $"/_snapshot/{repository}/{snapshot}?pretty=true", s => s.PrettyJson())
				.Fluent(c => c.Snapshot.Snapshot(repository, snapshot, s => s.Pretty()))
				.Request(c => c.Snapshot.Snapshot(new SnapshotRequest(repository, snapshot) { Pretty = true }))
				.FluentAsync(c => c.Snapshot.SnapshotAsync(repository, snapshot, s => s.Pretty()))
				.RequestAsync(c => c.Snapshot.SnapshotAsync(new SnapshotRequest(repository, snapshot) { Pretty = true }));
		}
	}
}

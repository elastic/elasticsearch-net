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

namespace Tests.Modules.SnapshotAndRestore.Repositories.VerifyRepository
{
	public class VerifyRepositoryUrlTests
	{
		[U] public async Task Urls()
		{
			var repos = "repos1";

			await POST($"/_snapshot/repos1/_verify")
					.Fluent(c => c.Snapshot.VerifyRepository(repos))
					.Request(c => c.Snapshot.VerifyRepository(new VerifyRepositoryRequest(repos)))
					.FluentAsync(c => c.Snapshot.VerifyRepositoryAsync(repos))
					.RequestAsync(c => c.Snapshot.VerifyRepositoryAsync(new VerifyRepositoryRequest(repos)))
				;
		}
	}
}

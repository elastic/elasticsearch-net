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

namespace Tests.Indices.IndexManagement.IndicesExists
{
	public class IndexExistsUrlTests
	{
		[U] public async Task Urls()
		{
			var indices = Nest.Indices.Index<Project>().And<CommitActivity>();
			var index = "project";
			await UrlTester.HEAD($"/{index}")
					.Fluent(c => c.Indices.Exists(index, s => s))
					.Request(c => c.Indices.Exists(new IndexExistsRequest(index)))
					.FluentAsync(c => c.Indices.ExistsAsync(index))
					.RequestAsync(c => c.Indices.ExistsAsync(new IndexExistsRequest(index)))
				;
		}
	}
}

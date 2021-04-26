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

namespace Tests.Indices.MappingManagement.GetMapping
{
	public class GetMappingUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1,index2";
			await GET($"/index1%2Cindex2/_mapping")
					.Fluent(c => c.Indices.GetMapping<Project>(m => m.Index(index)))
					.Request(c => c.Indices.GetMapping(new GetMappingRequest(index)))
					.FluentAsync(c => c.Indices.GetMappingAsync<Project>(m => m.Index(index)))
					.RequestAsync(c => c.Indices.GetMappingAsync(new GetMappingRequest(index)))
				;
		}
	}
}

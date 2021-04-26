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
using static Nest.Infer;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.AliasManagement.GetAlias
{
	public class GetAliasUrlTests
	{
		[U] public async Task Urls()
		{
			Name name = "hardcoded";
			IndexName index = "index";
			await GET($"/_alias")
					.Fluent(c => c.Indices.GetAlias())
					.Request(c => c.Indices.GetAlias(new GetAliasRequest()))
					.FluentAsync(c => c.Indices.GetAliasAsync())
					.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest()))
				;
			await GET($"/_all/_alias/hardcoded")
				.Fluent(c => c.Indices.GetAlias(AllIndices, b => b.Name(name)))
				.Request(c => c.Indices.GetAlias(new GetAliasRequest(AllIndices, name)))
				.FluentAsync(c => c.Indices.GetAliasAsync(AllIndices, b => b.Name(name)))
				.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest(AllIndices, name)));

			await GET($"/_alias/hardcoded")
					.Request(c => c.Indices.GetAlias(new GetAliasRequest(name)))
					.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest(name)))
				;
			await GET($"/index/_alias")
					.Fluent(c => c.Indices.GetAlias(index))
					.Request(c => c.Indices.GetAlias(new GetAliasRequest(index)))
					.FluentAsync(c => c.Indices.GetAliasAsync(index))
					.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest(index)))
				;

			await GET($"/index/_alias/hardcoded")
					.Fluent(c => c.Indices.GetAlias(index, b => b.Name(name)))
					.Request(c => c.Indices.GetAlias(new GetAliasRequest(index, name)))
					.FluentAsync(c => c.Indices.GetAliasAsync(index, b => b.Name(name)))
					.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest(index, name)))
				;
		}
	}
}

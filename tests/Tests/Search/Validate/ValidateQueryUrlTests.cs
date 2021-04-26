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

namespace Tests.Search.Validate
{
	public class ValidateQueryUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			await POST("/devs/_validate/query")
					.Fluent(c => c.Indices.ValidateQuery<Developer>(s => s))
					.Request(c => c.Indices.ValidateQuery(new ValidateQueryRequest<Developer>()))
					.FluentAsync(c => c.Indices.ValidateQueryAsync<Developer>(s => s))
					.RequestAsync(c => c.Indices.ValidateQueryAsync(new ValidateQueryRequest<Developer>()))
				;

			await POST("/devs/_validate/query")
					.Fluent(c => c.Indices.ValidateQuery<Developer>(s => s))
					.Request(c => c.Indices.ValidateQuery(new ValidateQueryRequest<Project>(typeof(Developer))))
					.Request(c => c.Indices.ValidateQuery(new ValidateQueryRequest(typeof(Developer))))
					.FluentAsync(c => c.Indices.ValidateQueryAsync<Developer>(s => s))
					.RequestAsync(c => c.Indices.ValidateQueryAsync(new ValidateQueryRequest<Project>(typeof(Developer))))
					.RequestAsync(c => c.Indices.ValidateQueryAsync(new ValidateQueryRequest(typeof(Developer))))
				;

			await POST("/project/_validate/query")
					.Fluent(c => c.Indices.ValidateQuery<Project>(s => s))
					.Fluent(c => c.Indices.ValidateQuery<Project>(s => s))
					.Request(c => c.Indices.ValidateQuery(new ValidateQueryRequest("project")))
					.Request(c => c.Indices.ValidateQuery(new ValidateQueryRequest<Project>("project")))
					.FluentAsync(c => c.Indices.ValidateQueryAsync<Project>(s => s))
					.RequestAsync(c => c.Indices.ValidateQueryAsync(new ValidateQueryRequest<Project>(typeof(Project))))
					.FluentAsync(c => c.Indices.ValidateQueryAsync<Project>(s => s))
				;

			await POST("/hardcoded/_validate/query")
					.Fluent(c => c.Indices.ValidateQuery<Project>(s => s.Index(hardcoded)))
					.Fluent(c => c.Indices.ValidateQuery<Project>(s => s.Index(hardcoded)))
					.Request(c => c.Indices.ValidateQuery(new ValidateQueryRequest(hardcoded)))
					.Request(c => c.Indices.ValidateQuery(new ValidateQueryRequest<Project>(hardcoded)))
					.FluentAsync(c => c.Indices.ValidateQueryAsync<Project>(s => s.Index(hardcoded)))
					.RequestAsync(c => c.Indices.ValidateQueryAsync(new ValidateQueryRequest<Project>(hardcoded)))
					.FluentAsync(c => c.Indices.ValidateQueryAsync<Project>(s => s.Index(hardcoded)))
				;

			await POST("/_all/_validate/query")
					.Fluent(c => c.Indices.ValidateQuery<Project>(s => s.AllIndices()))
					.Request(c => c.Indices.ValidateQuery(new ValidateQueryRequest<Project>(Nest.Indices.All)))
					.FluentAsync(c => c.Indices.ValidateQueryAsync<Project>(s => s.AllIndices()))
					.RequestAsync(c => c.Indices.ValidateQueryAsync(new ValidateQueryRequest<Project>(Nest.Indices.All)))
				;

			await POST("/_validate/query")
					.Request(c => c.Indices.ValidateQuery(new ValidateQueryRequest()))
					.RequestAsync(c => c.Indices.ValidateQueryAsync(new ValidateQueryRequest()))
				;
		}
	}
}

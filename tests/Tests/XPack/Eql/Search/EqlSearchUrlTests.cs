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

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Eql.Search
{
	public class EqlSearchUrlTests
	{
		[U] public async Task Urls() =>
			//var hardcoded = "hardcoded";
			await POST("/logs/_eql/search")
				.Fluent(c => c.Eql.Search<Log>("logs"))
				.Request(c => c.Eql.Search<Log>(new EqlSearchRequest("logs")))
				.FluentAsync(c => c.Eql.SearchAsync<Log>("logs"))
				.RequestAsync(c => c.Eql.SearchAsync<Log>(new EqlSearchRequest("logs")));

		//await POST("/devs/_search")
		//		.Fluent(c => c.Eql.Search<Log>(s => s))
		//		.Request(c => c.Eql.Search<Project>(new EqlSearchRequest<Log>(typeof(Log))))
		//		.FluentAsync(c => c.Eql.SearchAsync<Log>(s => s))
		//		.RequestAsync(c => c.Eql.SearchAsync<Project>(new EqlSearchRequest<Log>(typeof(Log))));
		//await POST("/project/_search")
		//		.Fluent(c => c.Eql.Search<Project>(s => s))
		//		.Fluent(c => c.Eql.Search<Project>(s => s))
		//		.Request(c => c.Eql.Search<Project>(new EqlSearchRequest("project")))
		//		.Request(c => c.Eql.Search<Project>(new EqlSearchRequest<Project>("project")))
		//		.FluentAsync(c => c.Eql.SearchAsync<Project>(s => s))
		//		.RequestAsync(c => c.Eql.SearchAsync<Project>(new EqlSearchRequest<Project>(typeof(Project))))
		//		.FluentAsync(c => c.Eql.SearchAsync<Project>(s => s));
		//await POST("/hardcoded/_search")
		//		.Fluent(c => c.Eql.Search<Project>(s => s.Index(hardcoded)))
		//		.Fluent(c => c.Eql.Search<Project>(s => s.Index(hardcoded)))
		//		.Request(c => c.Eql.Search<Project>(new EqlSearchRequest(hardcoded)))
		//		.Request(c => c.Eql.Search<Project>(new EqlSearchRequest<Project>(hardcoded)))
		//		.FluentAsync(c => c.Eql.SearchAsync<Project>(s => s.Index(hardcoded)))
		//		.RequestAsync(c => c.Eql.SearchAsync<Project>(new EqlSearchRequest<Project>(hardcoded)))
		//		.FluentAsync(c => c.Eql.SearchAsync<Project>(s => s.Index(hardcoded)));
		//await POST("/_all/_search")
		//		.Fluent(c => c.Eql.Search<Project>(s => s.AllIndices()))
		//		.Request(c => c.Eql.Search<Project>(new EqlSearchRequest<Project>(Nest.Indices.All)))
		//		.FluentAsync(c => c.Eql.SearchAsync<Project>(s => s.AllIndices()))
		//		.RequestAsync(c => c.Eql.SearchAsync<Project>(new EqlSearchRequest<Project>(Nest.Indices.All)));
		//await POST("/_search?scroll=1m")
		//		.Request(c => c.Eql.Search<Project>(new EqlSearchRequest { Scroll = TimeSpan.FromMinutes(1) }))
		//		.RequestAsync(c => c.Eql.SearchAsync<Project>(new EqlSearchRequest { Scroll = 60000 }));
	}
}

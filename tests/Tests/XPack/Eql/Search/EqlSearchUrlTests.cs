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

namespace Tests.XPack.Eql.Search
{
	public class EqlSearchUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";

			await POST("/customlogs-%2A/_eql/search")
				.Fluent(c => c.Eql.Search<Log>())
				.Request(c => c.Eql.Search<Log>(new EqlSearchRequest("customlogs-*")))
				.FluentAsync(c => c.Eql.SearchAsync<Log>())
				.RequestAsync(c => c.Eql.SearchAsync<Log>(new EqlSearchRequest("customlogs-*")));

			await POST("/customlogs-%2A/_eql/search")
					.Fluent(c => c.Eql.Search<Log>(s => s))
					.Request(c => c.Eql.Search<Log>(new EqlSearchRequest<Log>(typeof(Log))))
					.FluentAsync(c => c.Eql.SearchAsync<Log>(s => s))
					.RequestAsync(c => c.Eql.SearchAsync<Log>(new EqlSearchRequest<Log>(typeof(Log))));

			await POST("/hardcoded/_eql/search")
					.Fluent(c => c.Eql.Search<Log>(s => s.Index(hardcoded)))
					.Request(c => c.Eql.Search<Log>(new EqlSearchRequest(hardcoded)))
					.Request(c => c.Eql.Search<Log>(new EqlSearchRequest<Log>(hardcoded)))
					.FluentAsync(c => c.Eql.SearchAsync<Log>(s => s.Index(hardcoded)))
					.RequestAsync(c => c.Eql.SearchAsync<Log>(new EqlSearchRequest(hardcoded)))
					.RequestAsync(c => c.Eql.SearchAsync<Log>(new EqlSearchRequest<Log>(hardcoded)));

			await POST("/_all/_eql/search")
					.Fluent(c => c.Eql.Search<Log>(s => s.AllIndices()))
					.Request(c => c.Eql.Search<Log>(new EqlSearchRequest<Project>(Nest.Indices.All)))
					.FluentAsync(c => c.Eql.SearchAsync<Log>(s => s.AllIndices()))
					.RequestAsync(c => c.Eql.SearchAsync<Log>(new EqlSearchRequest<Project>(Nest.Indices.All)));
		}
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

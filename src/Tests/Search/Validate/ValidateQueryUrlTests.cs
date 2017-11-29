﻿using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Validate
{
	public class ValidateQueryUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			await POST("/devs/developer/_validate/query")
				.Fluent(c=>c.ValidateQuery<Developer>(s=>s))
				.Request(c=>c.ValidateQuery(new ValidateQueryRequest<Developer>()))
				.FluentAsync(c=>c.ValidateQueryAsync<Developer>(s=>s))
				.RequestAsync(c=>c.ValidateQueryAsync(new ValidateQueryRequest<Developer>()))
				;

			await POST("/devs/hardcoded/_validate/query")
				.Fluent(c=>c.ValidateQuery<Developer>(s=>s.Type(hardcoded)))
				.Request(c=>c.ValidateQuery(new ValidateQueryRequest<Project>(typeof(Developer), hardcoded)))
				.Request(c=>c.ValidateQuery(new ValidateQueryRequest(typeof(Developer), hardcoded)))
				.FluentAsync(c=>c.ValidateQueryAsync<Developer>(s=>s.Type(hardcoded)))
				.RequestAsync(c=>c.ValidateQueryAsync(new ValidateQueryRequest<Project>(typeof(Developer), hardcoded)))
				.RequestAsync(c=>c.ValidateQueryAsync(new ValidateQueryRequest(typeof(Developer), hardcoded)))
				;

			await POST("/project/_validate/query")
				.Fluent(c=>c.ValidateQuery<Project>(s=>s.Type(Types.All)))
				.Fluent(c=>c.ValidateQuery<Project>(s=>s.AllTypes()))
				.Request(c=>c.ValidateQuery(new ValidateQueryRequest("project")))
				.Request(c=>c.ValidateQuery(new ValidateQueryRequest<Project>("project", Types.All)))
				.FluentAsync(c=>c.ValidateQueryAsync<Project>(s=>s.Type(Types.All)))
				.RequestAsync(c=>c.ValidateQueryAsync(new ValidateQueryRequest<Project>(typeof(Project), Types.All)))
				.FluentAsync(c=>c.ValidateQueryAsync<Project>(s=>s.AllTypes()))
				;

			await POST("/hardcoded/_validate/query")
				.Fluent(c=>c.ValidateQuery<Project>(s=>s.Index(hardcoded).Type(Types.All)))
				.Fluent(c=>c.ValidateQuery<Project>(s=>s.Index(hardcoded).AllTypes()))
				.Request(c=>c.ValidateQuery(new ValidateQueryRequest(hardcoded)))
				.Request(c=>c.ValidateQuery(new ValidateQueryRequest<Project>(hardcoded, Types.All)))
				.FluentAsync(c=>c.ValidateQueryAsync<Project>(s=>s.Index(hardcoded).Type(Types.All)))
				.RequestAsync(c=>c.ValidateQueryAsync(new ValidateQueryRequest<Project>(hardcoded, Types.All)))
				.FluentAsync(c=>c.ValidateQueryAsync<Project>(s=>s.Index(hardcoded).AllTypes()))
				;

			await POST("/_validate/query")
				.Fluent(c=>c.ValidateQuery<Project>(s=>s.AllTypes().AllIndices()))
				.Request(c=>c.ValidateQuery(new ValidateQueryRequest()))
				.Request(c=>c.ValidateQuery(new ValidateQueryRequest<Project>(Nest.Indices.All, Types.All)))
				.FluentAsync(c=>c.ValidateQueryAsync<Project>(s=>s.AllIndices().Type(Types.All)))
				.RequestAsync(c=>c.ValidateQueryAsync(new ValidateQueryRequest<Project>(Nest.Indices.All, Types.All)))
				.RequestAsync(c=>c.ValidateQueryAsync(new ValidateQueryRequest()))
				;
		}
	}
}

using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Validate
{
	public class ValidateQueryUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			await POST("/devs/_validate/query")
					.Fluent(c => c.ValidateQuery<Developer>(s => s))
					.Request(c => c.ValidateQuery(new ValidateQueryRequest<Developer>()))
					.FluentAsync(c => c.ValidateQueryAsync<Developer>(s => s))
					.RequestAsync(c => c.ValidateQueryAsync(new ValidateQueryRequest<Developer>()))
				;

			await POST("/devs/_validate/query")
					.Fluent(c => c.ValidateQuery<Developer>(s => s))
					.Request(c => c.ValidateQuery(new ValidateQueryRequest<Project>(typeof(Developer))))
					.Request(c => c.ValidateQuery(new ValidateQueryRequest(typeof(Developer))))
					.FluentAsync(c => c.ValidateQueryAsync<Developer>(s => s))
					.RequestAsync(c => c.ValidateQueryAsync(new ValidateQueryRequest<Project>(typeof(Developer))))
					.RequestAsync(c => c.ValidateQueryAsync(new ValidateQueryRequest(typeof(Developer))))
				;

			await POST("/project/_validate/query")
					.Fluent(c => c.ValidateQuery<Project>(s => s))
					.Fluent(c => c.ValidateQuery<Project>(s => s))
					.Request(c => c.ValidateQuery(new ValidateQueryRequest("project")))
					.Request(c => c.ValidateQuery(new ValidateQueryRequest<Project>("project")))
					.FluentAsync(c => c.ValidateQueryAsync<Project>(s => s))
					.RequestAsync(c => c.ValidateQueryAsync(new ValidateQueryRequest<Project>(typeof(Project))))
					.FluentAsync(c => c.ValidateQueryAsync<Project>(s => s))
				;

			await POST("/hardcoded/_validate/query")
					.Fluent(c => c.ValidateQuery<Project>(s => s.Index(hardcoded)))
					.Fluent(c => c.ValidateQuery<Project>(s => s.Index(hardcoded)))
					.Request(c => c.ValidateQuery(new ValidateQueryRequest(hardcoded)))
					.Request(c => c.ValidateQuery(new ValidateQueryRequest<Project>(hardcoded)))
					.FluentAsync(c => c.ValidateQueryAsync<Project>(s => s.Index(hardcoded)))
					.RequestAsync(c => c.ValidateQueryAsync(new ValidateQueryRequest<Project>(hardcoded)))
					.FluentAsync(c => c.ValidateQueryAsync<Project>(s => s.Index(hardcoded)))
				;

			await POST("/_validate/query")
					.Fluent(c => c.ValidateQuery<Project>(s => s.AllIndices()))
					.Request(c => c.ValidateQuery(new ValidateQueryRequest()))
					.Request(c => c.ValidateQuery(new ValidateQueryRequest<Project>(Nest.Indices.All)))
					.FluentAsync(c => c.ValidateQueryAsync<Project>(s => s.AllIndices()))
					.RequestAsync(c => c.ValidateQueryAsync(new ValidateQueryRequest<Project>(Nest.Indices.All)))
					.RequestAsync(c => c.ValidateQueryAsync(new ValidateQueryRequest()))
				;
		}
	}
}

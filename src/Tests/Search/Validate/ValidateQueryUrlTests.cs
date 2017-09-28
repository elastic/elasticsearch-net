using System.Threading.Tasks;
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
			await POST("/commits/commits/_validate/query")
				.Fluent(c=>c.ValidateQuery<CommitActivity>(s=>s))
				.Request(c=>c.ValidateQuery(new ValidateQueryRequest<CommitActivity>()))
				.FluentAsync(c=>c.ValidateQueryAsync<CommitActivity>(s=>s))
				.RequestAsync(c=>c.ValidateQueryAsync(new ValidateQueryRequest<CommitActivity>()))
				;

			await POST("/commits/hardcoded/_validate/query")
				.Fluent(c=>c.ValidateQuery<CommitActivity>(s=>s.Type(hardcoded)))
				.Request(c=>c.ValidateQuery(new ValidateQueryRequest<Project>(typeof(CommitActivity), hardcoded)))
				.Request(c=>c.ValidateQuery(new ValidateQueryRequest(typeof(CommitActivity), hardcoded)))
				.FluentAsync(c=>c.ValidateQueryAsync<CommitActivity>(s=>s.Type(hardcoded)))
				.RequestAsync(c=>c.ValidateQueryAsync(new ValidateQueryRequest<Project>(typeof(CommitActivity), hardcoded)))
				.RequestAsync(c=>c.ValidateQueryAsync(new ValidateQueryRequest(typeof(CommitActivity), hardcoded)))
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

using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Domain.Helpers;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Sql.QuerySql
{
	//[SkipVersion("<6.4.0", "")]
	public class QuerySqlApiTests : ApiIntegrationTestBase<XPackCluster, IQuerySqlResponse, IQuerySqlRequest, QuerySqlDescriptor, QuerySqlRequest>
	{
		public QuerySqlApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => this.Calls(
			fluent: (client, f) => client.QuerySql(f),
			fluentAsync: (client, f) => client.QuerySqlAsync(f),
			request: (client, r) => client.QuerySql(r),
			requestAsync: (client, r) => client.QuerySqlAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/_xpack/sql";

		private static readonly string SqlQuery =
			$@"SELECT type, name, startedOn, numberOfCommits
FROM {TestValueHelper.ProjectsIndex}
WHERE type = '{Project.TypeName}'
ORDER BY numberOfContributors DESC";

		protected override object ExpectJson { get; } = new {
			query = SqlQuery,
			fetch_size = 5
		};

		protected override Func<QuerySqlDescriptor, IQuerySqlRequest> Fluent => d => d
			.Query(SqlQuery)
			.FetchSize(5)
		;

		protected override QuerySqlRequest Initializer => new QuerySqlRequest()
		{
			Query = SqlQuery,
			FetchSize = 5
		};

		protected override void ExpectResponse(IQuerySqlResponse response)
		{
			response.Cursor.Should().NotBeNullOrWhiteSpace("response cursor");
			response.Rows.Should().NotBeNullOrEmpty();
			response.Columns.Should().NotBeNullOrEmpty().And.HaveCount(4);
			foreach (var c in response.Columns)
			{
				c.Name.Should().NotBeNullOrWhiteSpace("column name");
				c.Type.Should().NotBeNullOrWhiteSpace("column type");
			}
			foreach (var r in response.Rows)
			{
				r.Should().NotBeNull().And.HaveCount(4);
				var type = r[0].As<string>().Should().NotBeNullOrWhiteSpace("a type returned null");
				var name = r[1].As<string>().Should().NotBeNullOrWhiteSpace("a name returned null");
				var date = r[2].As<DateTime>().Should().BeAfter(default(DateTime));
				var numberOfCommits = r[3].As<int?>();
			}
		}
	}
}

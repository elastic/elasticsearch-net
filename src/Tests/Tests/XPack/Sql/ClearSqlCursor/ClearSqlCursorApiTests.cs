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

namespace Tests.XPack.Sql.ClearSqlCursor
{
	//[SkipVersion("<6.4.0", "")]
	public class ClearSqlCursorApiTests : ApiIntegrationTestBase<XPackCluster, IClearSqlCursorResponse, IClearSqlCursorRequest, ClearSqlCursorDescriptor, ClearSqlCursorRequest>
	{
		public ClearSqlCursorApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => this.Calls(
			fluent: (client, f) => client.ClearSqlCursor(f),
			fluentAsync: (client, f) => client.ClearSqlCursorAsync(f),
			request: (client, r) => client.ClearSqlCursor(r),
			requestAsync: (client, r) => client.ClearSqlCursorAsync(r)
		);

		private string _currentCursor = "default-for-unit-tests";

		private static readonly string SqlQuery =
			$@"SELECT type, name, startedOn, numberOfCommits
FROM {TestValueHelper.ProjectsIndex}
WHERE type = '{Project.TypeName}'
ORDER BY numberOfContributors DESC";

		protected override void OnBeforeCall(IElasticClient client)
		{
			var sqlQueryResponse = this.Client.QuerySql(q => q.Query(SqlQuery).FetchSize(5));
			if (!sqlQueryResponse.IsValid)
				throw new Exception("Setup: Initial scroll failed.");
			_currentCursor = sqlQueryResponse.Cursor ?? _currentCursor;
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_xpack/sql/close";

		protected override object ExpectJson => new { cursor = this._currentCursor };

		protected override Func<ClearSqlCursorDescriptor, IClearSqlCursorRequest> Fluent => d => d
			.Cursor(_currentCursor)
		;

		protected override ClearSqlCursorRequest Initializer => new ClearSqlCursorRequest()
		{
			Cursor = _currentCursor,
		};

		protected override void ExpectResponse(IClearSqlCursorResponse response) =>
			response.Succeeded.Should().BeTrue("succeeded property on response");
	}
}

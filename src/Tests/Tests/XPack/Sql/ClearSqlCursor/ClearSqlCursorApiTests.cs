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
	[SkipVersion("<6.4.0", "")]
	public class ClearSqlCursorApiTests
		: ApiIntegrationTestBase<XPackCluster, IClearSqlCursorResponse, IClearSqlCursorRequest, ClearSqlCursorDescriptor, ClearSqlCursorRequest>
	{
		private static readonly string SqlQuery =
			$@"SELECT type, name, startedOn, numberOfCommits
FROM {TestValueHelper.ProjectsIndex}
WHERE type = '{Project.TypeName}'
ORDER BY numberOfContributors DESC";

		private string _currentCursor = "default-for-unit-tests";

		public ClearSqlCursorApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new { cursor = _currentCursor };
		protected override int ExpectStatusCode => 200;

		protected override Func<ClearSqlCursorDescriptor, IClearSqlCursorRequest> Fluent => d => d
			.Cursor(_currentCursor);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ClearSqlCursorRequest Initializer => new ClearSqlCursorRequest()
		{
			Cursor = _currentCursor,
		};

		protected override string UrlPath => $"/_sql/close";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ClearSqlCursor(f),
			(client, f) => client.ClearSqlCursorAsync(f),
			(client, r) => client.ClearSqlCursor(r),
			(client, r) => client.ClearSqlCursorAsync(r)
		);

		protected override void OnBeforeCall(IElasticClient client)
		{
			var sqlQueryResponse = Client.QuerySql(q => q.Query(SqlQuery).FetchSize(5));
			if (!sqlQueryResponse.IsValid)
				throw new Exception("Setup: Initial scroll failed.");

			_currentCursor = sqlQueryResponse.Cursor ?? _currentCursor;
		}

		protected override void ExpectResponse(IClearSqlCursorResponse response) =>
			response.Succeeded.Should().BeTrue("succeeded property on response");
	}
}

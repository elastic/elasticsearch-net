// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Domain.Helpers;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Sql
{
	[SkipVersion("<7.14.0", "All endpoints GA in 7.14.0")]
	public class SqlSearchApiCoordinatedTests : CoordinatedIntegrationTestBase<XPackCluster>
	{
		private const string DeleteStep = nameof(DeleteStep);
		private const string GetStep = nameof(GetStep);
		private const string StatusStep = nameof(StatusStep);
		private const string SubmitStep = nameof(SubmitStep);
		private const string WaitStep = nameof(WaitStep);

		private static readonly string SqlQuery =
			$@"SELECT type, name, startedOn, numberOfCommits
FROM {TestValueHelper.ProjectsIndex}
WHERE type = '{Project.TypeName}'
ORDER BY numberOfContributors DESC";

		public SqlSearchApiCoordinatedTests(XPackCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage, testOnlyOne: true)
		{
			{
				SubmitStep, u =>
					u.Calls<QuerySqlDescriptor, QuerySqlRequest, IQuerySqlRequest, QuerySqlResponse>(
						_ => new QuerySqlRequest { Query = SqlQuery, FetchSize = 5, WaitForCompletionTimeout = "0s" },
						(_, d) => d
							.Query(SqlQuery)
							.FetchSize(5)
							.WaitForCompletionTimeout("0s"),
						(_, c, f) => c.Sql.Query(f),
						(_, c, f) => c.Sql.QueryAsync(f),
						(_, c, r) => c.Sql.Query(r),
						(_, c, r) => c.Sql.QueryAsync(r),
						(r, values) => values.ExtendedValue("id", r.Id)
					)
			},
			{
				StatusStep, u =>
					u.Calls<SqlSearchStatusDescriptor, SqlSearchStatusRequest, ISqlSearchStatusRequest, SqlSearchStatusResponse>(
						v => new SqlSearchStatusRequest(v),
						(v, d) => d,
						(v, c, f) => c.Sql.SearchStatus(v, f),
						(v, c, f) => c.Sql.SearchStatusAsync(v, f),
						(v, c, r) => c.Sql.SearchStatus(r),
						(v, c, r) => c.Sql.SearchStatusAsync(r),
						uniqueValueSelector: values => values.ExtendedValue<string>("id")
					)
			},
			{
				// allows the search to complete
				WaitStep, u => u.Call(async (_, c) =>
				{
					// wait for the search to complete
					var complete = false;
					var count = 0;

					while (!complete && count++ < 10)
					{
						await Task.Delay(100);
						var status = await c.Sql.SearchStatusAsync(u.Usage.CallUniqueValues.ExtendedValue<string>("id"));
						complete = !status.IsRunning && status.CompletionStatus.HasValue;
					}
				})
			},
			{
				GetStep, u =>
					u.Calls<SqlGetDescriptor, SqlGetRequest, ISqlGetRequest, SqlGetResponse>(
						v => new SqlGetRequest(v),
						(_, d) => d,
						(v, c, f) => c.Sql.Get(v, f),
						(v, c, f) => c.Sql.GetAsync(v, f),
						(_, c, r) => c.Sql.Get(r),
						(_, c, r) => c.Sql.GetAsync(r),
						uniqueValueSelector: values => values.ExtendedValue<string>("id")
					)
			},
			{
				DeleteStep, u =>
					u.Calls<SqlDeleteDescriptor, SqlDeleteRequest, ISqlDeleteRequest, SqlDeleteResponse>(
						v => new SqlDeleteRequest(v),
						(_, d) => d,
						(v, c, f) => c.Sql.Delete(v, f),
						(v, c, f) => c.Sql.DeleteAsync(v, f),
						(_, c, r) => c.Sql.Delete(r),
						(_, c, r) => c.Sql.DeleteAsync(r),
						uniqueValueSelector: values => values.ExtendedValue<string>("id")
					)
			}
		}) { }

		[I] public async Task SqlSearchResponse() => await Assert<QuerySqlResponse>(SubmitStep, r =>
		{
			r.ShouldBeValid();
			r.Id.Should().NotBeNullOrEmpty();
			r.IsPartial.Should().BeTrue();
			r.IsRunning.Should().BeTrue();
		});

		[I] public async Task SqlSearchStatusResponse() => await Assert<SqlSearchStatusResponse>(StatusStep, r =>
		{
			r.ShouldBeValid();
			r.Id.Should().NotBeNullOrEmpty();
			r.IsPartial.Should().BeTrue();
			r.IsRunning.Should().BeTrue();
			r.ExpirationTimeInMillis.Should().BeGreaterThan(0);
			r.StartTimeInMillis.Should().BeGreaterThan(0);
		});

		[I] public async Task SqlGetResponse() => await Assert<SqlGetResponse>(GetStep, r =>
		{
			r.ShouldBeValid();
			r.IsPartial.Should().BeFalse();
			r.IsRunning.Should().BeFalse();

			r.Cursor.Should().NotBeNullOrWhiteSpace("response cursor");
			r.Rows.Should().NotBeNullOrEmpty();
			r.Columns.Should().NotBeNullOrEmpty().And.HaveCount(4);
			foreach (var c in r.Columns)
			{
				c.Name.Should().NotBeNullOrWhiteSpace("column name");
				c.Type.Should().NotBeNullOrWhiteSpace("column type");
			}
			foreach (var row in r.Rows)
			{
				row.Should().NotBeNull().And.HaveCount(4);
				var type = row[0].As<string>().Should().NotBeNullOrWhiteSpace("a type returned null");
				var name = row[1].As<string>().Should().NotBeNullOrWhiteSpace("a name returned null");
				var date = row[2].As<DateTime>().Should().BeAfter(default);
				var numberOfCommits = row[3].As<int?>();
			}
		});

		[I] public async Task SqlDeleteResponse() => await Assert<SqlDeleteResponse>(DeleteStep, r =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});
	}
}

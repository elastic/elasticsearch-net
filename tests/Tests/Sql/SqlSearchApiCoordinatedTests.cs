// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using Elastic.Clients.Elasticsearch.Sql;
using System;

namespace Tests.Sql;

public class SqlSearchApiCoordinatedTests : CoordinatedIntegrationTestBase<ReadOnlyCluster>
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

	public SqlSearchApiCoordinatedTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage, testOnlyOne: true)
	{
		{
			SubmitStep, u =>
				u.Calls<SqlQueryRequestDescriptor, SqlQueryRequest, SqlQueryResponse>(
					_ => new SqlQueryRequest { Query = SqlQuery, FetchSize = 5, WaitForCompletionTimeout = "0s" },
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
				u.Calls<SqlGetAsyncStatusRequestDescriptor, SqlGetAsyncStatusRequest, SqlGetAsyncStatusResponse>(
					v => new SqlGetAsyncStatusRequest(v),
					(v, d) => d,
					(v, c, f) => c.Sql.GetAsyncSearchStatus(v, f),
					(v, c, f) => c.Sql.GetAsyncSearchStatusAsync(v, f),
					(v, c, r) => c.Sql.GetAsyncSearchStatus(r),
					(v, c, r) => c.Sql.GetAsyncSearchStatusAsync(r),
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
					var status = await c.Sql.GetAsyncSearchStatusAsync(u.Usage.CallUniqueValues.ExtendedValue<string>("id"));
					complete = !status.IsRunning && status.CompletionStatus.HasValue;
				}
			})
		},
		{
			GetStep, u =>
				u.Calls<SqlGetAsyncRequestDescriptor, SqlGetAsyncRequest, SqlGetAsyncResponse>(
					v => new SqlGetAsyncRequest(v),
					(_, d) => d,
					(v, c, f) => c.Sql.GetAsyncSearch(v, f),
					(v, c, f) => c.Sql.GetAsyncSearchAsync(v, f),
					(_, c, r) => c.Sql.GetAsyncSearch(r),
					(_, c, r) => c.Sql.GetAsyncSearchAsync(r),
					uniqueValueSelector: values => values.ExtendedValue<string>("id")
				)
		},
		{
			DeleteStep, u =>
				u.Calls<SqlDeleteAsyncRequestDescriptor, SqlDeleteAsyncRequest, SqlDeleteAsyncResponse>(
					v => new SqlDeleteAsyncRequest(v),
					(_, d) => d,
					(v, c, f) => c.Sql.DeleteAsyncSearch(v, f),
					(v, c, f) => c.Sql.DeleteAsyncSearchAsync(v, f),
					(_, c, r) => c.Sql.DeleteAsyncSearch(r),
					(_, c, r) => c.Sql.DeleteAsyncSearchAsync(r),
					uniqueValueSelector: values => values.ExtendedValue<string>("id")
				)
		}
	}) { }

	[I] public async Task SqlSearchResponse() => await Assert<SqlQueryResponse>(SubmitStep, r =>
	{
		r.ShouldBeValid();
		r.Id.Should().NotBeNullOrEmpty();
		r.IsPartial.Should().BeTrue();
		r.IsRunning.Should().BeTrue();
	});

	[I] public async Task SqlSearchStatusResponse() => await Assert<SqlGetAsyncStatusResponse>(StatusStep, r =>
	{
		r.ShouldBeValid();
		r.Id.Should().NotBeNullOrEmpty();

		if (r.CompletionStatus.HasValue)
		{
			r.IsPartial.Should().BeFalse();
			r.IsRunning.Should().BeFalse();
		}
		else
		{
			r.IsPartial.Should().BeTrue();
			r.IsRunning.Should().BeTrue();
			r.StartTimeInMillis.Should().BeGreaterThan(0);
		}
		
		r.ExpirationTimeInMillis.Should().BeGreaterThan(0);
	});

	[I] public async Task SqlGetResponse() => await Assert<SqlGetAsyncResponse>(GetStep, r =>
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
			var numberOfCommits = row[3].As<int?>().Should().BePositive();
		}
	});

	[I] public async Task SqlDeleteResponse() => await Assert<SqlDeleteAsyncResponse>(DeleteStep, r =>
	{
		r.ShouldBeValid();
		r.Acknowledged.Should().BeTrue();
	});
}

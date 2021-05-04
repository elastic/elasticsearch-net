// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Domain.Helpers;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Sql.QuerySql
{
	[SkipVersion("<6.4.0", "Added in 6.4.0")]
	public class QuerySqlApiTests : ApiIntegrationTestBase<XPackCluster, QuerySqlResponse, IQuerySqlRequest, QuerySqlDescriptor, QuerySqlRequest>
	{
		private static readonly string SqlQuery =
			$@"SELECT type, name, startedOn, numberOfCommits
FROM {TestValueHelper.ProjectsIndex}
WHERE type = '{Project.TypeName}'
ORDER BY numberOfContributors DESC";

		public QuerySqlApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			query = SqlQuery,
			fetch_size = 5
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<QuerySqlDescriptor, IQuerySqlRequest> Fluent => d => d
			.Query(SqlQuery)
			.FetchSize(5);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override QuerySqlRequest Initializer => new QuerySqlRequest()
		{
			Query = SqlQuery,
			FetchSize = 5
		};

		protected override string UrlPath => $"/_sql";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Sql.Query(f),
			(client, f) => client.Sql.QueryAsync(f),
			(client, r) => client.Sql.Query(r),
			(client, r) => client.Sql.QueryAsync(r)
		);

		protected override void ExpectResponse(QuerySqlResponse response)
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

	[SkipVersion("<7.13.0", "Runtime mappings added in 7.13.0")]
	public class QuerySqlApiRuntimeMappingsTests : ApiIntegrationTestBase<XPackCluster, QuerySqlResponse, IQuerySqlRequest, QuerySqlDescriptor, QuerySqlRequest>
	{
		private static readonly string SqlQuery =
			$@"SELECT numberOfCommits, doublesCommits FROM {TestValueHelper.ProjectsIndex}";

		public QuerySqlApiRuntimeMappingsTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			query = SqlQuery,
			fetch_size = 5,
			runtime_mappings = new
			{
				doublesCommits = new
				{
					script = new
					{
						source = "emit((long)(doc['numberOfCommits'].value * 2))"
					},
					type = "long"
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<QuerySqlDescriptor, IQuerySqlRequest> Fluent => d => d
			.Query(SqlQuery)
			.FetchSize(5)
			.RuntimeFields(rtf => rtf.RuntimeField("doublesCommits", FieldType.Long, s => s.Script("emit((long)(doc['numberOfCommits'].value * 2))")));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override QuerySqlRequest Initializer => new()
		{
			Query = SqlQuery,
			FetchSize = 5,
			RuntimeFields = new RuntimeFields
			{
				{ "doublesCommits", new RuntimeField
					{
						Type = FieldType.Long,
						Script = new InlineScript("emit((long)(doc['numberOfCommits'].value * 2))")
					}
				}
			}
		};

		protected override string UrlPath => "/_sql";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Sql.Query(f),
			(client, f) => client.Sql.QueryAsync(f),
			(client, r) => client.Sql.Query(r),
			(client, r) => client.Sql.QueryAsync(r)
		);

		protected override void ExpectResponse(QuerySqlResponse response)
		{
			response.Cursor.Should().NotBeNullOrWhiteSpace("response cursor");
			response.Rows.Should().NotBeNullOrEmpty();

			response.Columns.Single(x => x.Name == "numberOfCommits").Type.Should().Be("integer");
			response.Columns.Single(x => x.Name == "doublesCommits").Type.Should().Be("long");

			foreach (var r in response.Rows)
			{
				r.Should().NotBeNull().And.HaveCount(2);
				var numberOfCommits = r[0].As<int?>();
				var doublesCommits = r[1].As<long?>();

				if (!numberOfCommits.HasValue) continue;

				doublesCommits.HasValue.Should().BeTrue();
				doublesCommits!.Value.Should().Be(numberOfCommits.Value * 2);
			}
		}
	}
}

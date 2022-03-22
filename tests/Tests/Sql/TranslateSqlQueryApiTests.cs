//// Licensed to Elasticsearch B.V under one or more agreements.
//// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
//// See the LICENSE file in the project root for more information.

//using System;
//using Tests.Core.ManagedElasticsearch.Clusters;
//using Tests.Domain;
//using Tests.Framework.EndpointTests;
//using Tests.Framework.EndpointTests.TestState;
//using Elastic.Clients.Elasticsearch.Sql;

//namespace Tests.XPack.Sql.TranslateSql;

//public class TranslateSqlApiTests
//	: ApiIntegrationTestBase<ReadOnlyCluster, SqlTranslateResponse, SqlTranslateRequestDescriptor, SqlTranslateRequest>
//{
//	private static readonly string SqlQuery =
//		$@"SELECT type, name, startedOn, numberOfCommits
//FROM {TestValueHelper.ProjectsIndex}
//WHERE type = '{Project.TypeName}'
//ORDER BY numberOfContributors DESC";

//	public TranslateSqlApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

//	protected override bool ExpectIsValid => true;

//	protected override object ExpectJson { get; } = new
//	{
//		query = SqlQuery,
//		fetch_size = 5
//	};

//	protected override int ExpectStatusCode => 200;

//	protected override Action<SqlTranslateRequestDescriptor> Fluent => d => d
//		.Query(SqlQuery)
//		.FetchSize(5);

//	protected override HttpMethod HttpMethod => HttpMethod.POST;

//	protected override SqlTranslateRequest Initializer => new()
//	{
//		Query = SqlQuery,
//		FetchSize = 5
//	};

//	protected override string ExpectedUrlPathAndQuery => $"/_sql/translate";

//	protected override LazyResponses ClientUsage() => Calls(
//		(client, f) => client.Sql.Translate(f),
//		(client, f) => client.Sql.TranslateAsync(f),
//		(client, r) => client.Sql.Translate(r),
//		(client, r) => client.Sql.TranslateAsync(r)
//	);

//	protected override void ExpectResponse(SqlTranslateResponse response)
//	{
//		response.Size.Should().Be(5);
//		response.Source.Should().NotBeNull();
//		response.Source.HasBoolValue.Should().Be(true);
//		response.Source.TryGetBool(out var value).Should().Be(true);
//		value.Should().Be(false);

//		// TODO assert on response.Fields

//		var q = response.Query;
//		q.Should().NotBeNull();
//		q.Term.Should().NotBeNull();
//		q.Term.Value.Should().Be(TestValueHelper.ProjectsIndex);
//		q.Term.Boost.Should().Be(1.0f);
//		q.Term.Field.Name.Should().Be("type.keyword");
//	}
//}

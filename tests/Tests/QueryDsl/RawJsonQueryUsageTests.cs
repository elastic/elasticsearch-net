// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl;

public class RawJsonQueryUsageTests : QueryDslUsageTestsBase
{
	private static readonly string RawTermQuery = @"{""term"": { ""fieldname"":""value"" } }";

	public RawJsonQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage)
	{
	}

	protected override Query QueryInitializer => new RawJsonQuery(RawTermQuery);

	protected override QueryDescriptor<Project> QueryFluent(QueryDescriptor<Project> queryDescriptor) =>
		queryDescriptor
			.RawJson(RawTermQuery);
}

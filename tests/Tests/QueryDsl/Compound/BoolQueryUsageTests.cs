// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Compound;

public class BoolQueryUsageTests : QueryDslUsageTestsBase
{
	public BoolQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage)
	{
	}

	protected override bool VerifyJson => true;

	protected override Query QueryInitializer => new BoolQuery
	{
		Must = new Query[]
		{
			new MatchAllQuery()
		},
		Should = new Query[]
		{
			new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "Steve" },
			new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "David" }
		}
	};

	protected override QueryDescriptor<Project> QueryFluent(QueryDescriptor<Project> queryDescriptor) =>
		queryDescriptor
			.Bool(b => b
				.Must(m => m.MatchAll())
				.Should(
					s => s.Term(f => f.Name, "Steve"),
					s => s.Term(f => f.Name, "David")));
}

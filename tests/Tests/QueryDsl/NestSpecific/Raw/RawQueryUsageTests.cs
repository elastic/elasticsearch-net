// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.NestSpecific.Raw
{
	/**
	 * Allows a query represented as a string of JSON to be passed to NEST's Fluent API or Object Initializer syntax.
	 * This can be useful when porting over a query expressed in the query DSL over to NEST.
	 */
	public class RawUsageTests : QueryDslUsageTestsBase
	{
		private static readonly string RawTermQuery = @"{""term"": { ""fieldname"":""value"" } }";

		public RawUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override QueryContainer QueryInitializer => new RawQuery(RawTermQuery);

		protected override object QueryJson => new
		{
			term = new { fieldname = "value" }
		};

		protected override bool SupportsDeserialization => false;

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Raw(RawTermQuery);
	}
}

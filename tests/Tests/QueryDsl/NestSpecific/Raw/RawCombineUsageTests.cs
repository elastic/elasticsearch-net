// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.NestSpecific.Raw
{
	// ReSharper disable InvalidXmlDocComment
	/**
	 * NEST's <<raw-query-usage, raw query>> can be combined with other queries using a <<compound-queries, compound query>>
	 * such as a `bool` query.
	 */
	// ReSharper restore InvalidXmlDocComment
	public class RawCombineUsageTests : QueryDslUsageTestsBase
	{
		private static readonly string RawTermQuery = @"{""term"": { ""fieldname"":""value"" } }";

		public RawCombineUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override QueryContainer QueryInitializer =>
			new RawQuery(RawTermQuery)
			&& new TermQuery { Field = "x", Value = "y" };

		protected override object QueryJson => new
		{
			@bool = new
			{
				must = new object[]
				{
					new { term = new { fieldname = "value" } },
					new { term = new { x = new { value = "y" } } }
				}
			}
		};

		protected override bool SupportsDeserialization => false;

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) =>
			q.Raw(RawTermQuery) && q.Term("x", "y");
	}
}

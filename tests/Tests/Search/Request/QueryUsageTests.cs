// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Request
{
	// ReSharper disable InvalidXmlDocComment
	/**
	 * The query element within the search request body allows to define a query using the <<query-dsl,Query DSL>>.
	 */
	// ReSharper restore InvalidXmlDocComment
	public class QueryUsageTests : SearchUsageTestBase
	{
		public QueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new { query = new { term = new { name = new { value = "elasticsearch" } } } };

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(q => q
				.Term(p => p.Name, "elasticsearch")
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Query = new TermQuery
				{
					Field = "name",
					Value = "elasticsearch"
				}
			};
	}
}

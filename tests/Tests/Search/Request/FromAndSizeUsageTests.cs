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
	public class FromAndSizeUsageTests : SearchUsageTestBase
	{
		/**
		 * Pagination of results can be done by using the `from` and `size` parameters.
		 *
		 * `from` parameter:: defines the offset from the first result you want to fetch.
		 * `size` parameter:: allows you to configure the maximum amount of hits to be returned.
		 */

		public FromAndSizeUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new { from = 10, size = 12 };

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.From(10)
			.Size(12);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>()
			{
				From = 10,
				Size = 12
			};
	}
}

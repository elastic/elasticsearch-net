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
	public class SlicedScrollSearchUsageTests : SearchUsageTestBase
	{
		public SlicedScrollSearchUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new { slice = new { id = 0, max = 5 } };

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Scroll("1m")
			.Slice(ss => ss.Id(0).Max(5));

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>()
			{
				Scroll = "1m",
				Slice = new SlicedScroll { Id = 0, Max = 5 }
			};

		protected override string UrlPath => "/project/_search?scroll=1m";
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.Sdk;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.DocumentationTests;
using Xunit;
using static Nest.Infer;

namespace Tests.ClientConcepts.Troubleshooting
{
	/**
	 * === Deprecation logging
	 *
	 * Elasticsearch will send back `Warn` HTTP Headers when you are using an API feature that is
	 * deprecated and will be removed or rewritten in a future release.
	 *
	 * Elasticsearch.NET and NEST report these back to you so you can log and watch out for them.
	 */
	public class DeprecationLogging : IntegrationDocumentationTestBase, IClusterFixture<ReadOnlyCluster>
	{
		public DeprecationLogging(ReadOnlyCluster cluster) : base(cluster) { }

		[I] public void RequestWithMultipleWarning()
		{
			var request = new SearchRequest<Project>
			{
				Size = 0,
				Aggregations = new CompositeAggregation("test")
				{
					Sources = new []
					{
						new DateHistogramCompositeAggregationSource("date")
						{
							Field = Field<Project>(f => f.LastActivity),
#pragma warning disable 618
							Interval = new Time("7d"),
#pragma warning restore 618
							Format = "yyyy-MM-dd"
						}
					}
				}
			};
			var response = this.Client.Search<Project>(request);

			response.ApiCall.DeprecationWarnings.Should().NotBeNullOrEmpty();
			response.ApiCall.DeprecationWarnings.Should().HaveCountGreaterOrEqualTo(1);
			response.DebugInformation.Should().Contain("deprecated"); // <1> `DebugInformation` also contains the deprecation warnings
		}
	}
}

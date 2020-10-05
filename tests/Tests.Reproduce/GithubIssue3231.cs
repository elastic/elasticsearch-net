// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue3231 : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public GithubIssue3231(ReadOnlyCluster cluster) => _cluster = cluster;

		[I]
		public void CatIndicesUsesThrowExceptionsFromConnectionSettingsWhenRequestSettingsAreNotSetTest()
		{
			var client = new ElasticClient(_cluster.CreateConnectionSettings().ThrowExceptions());
			Action catIndicesRequest = () => client.LowLevel.Cat.Indices<StringResponse>("non-existing-index");
			catIndicesRequest.Should().Throw<TransportException>();
		}
	}
}

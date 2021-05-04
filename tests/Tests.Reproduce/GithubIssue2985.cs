// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue2985 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue2985(WritableCluster cluster) => _cluster = cluster;

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		[I]
		public void BadRequestErrorShouldBeWrappedInTransportException()
		{
			var client = _cluster.Client;
			var index = $"gh2985-{RandomString()}";
			var response = client.Indices.Create(index, i => i
				.Settings(s => s
					.Analysis(a => a
						.Analyzers(an => an
							.Custom("custom", c => c.Filters("ascii_folding").Tokenizer("standard"))
						)
					)
				)
			);
			response.OriginalException.Should().NotBeNull().And.BeOfType<TransportException>();
			response.OriginalException.Message.Should()
				.Contain(
					"Type: illegal_argument_exception Reason: \"Custom Analyzer [custom] failed to find filter under name [ascii_folding]\""
				);

			client.Indices.Delete(index);
		}
	}
}

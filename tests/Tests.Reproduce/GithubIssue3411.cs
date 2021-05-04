// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GithubIssue3411
	{
		[U]
		public void IndexNameParsesClusterAndIndexWithMultipleColons()
		{
			var name = "write::some-name";
			IndexName indexName = name;
			indexName.Cluster.Should().Be("write");
			indexName.Name.Should().Be(":some-name");

			var inferrer = new Inferrer(new ConnectionSettings());
			inferrer.IndexName(indexName).Should().Be(name);
		}
	}
}

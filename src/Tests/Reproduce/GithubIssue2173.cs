using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Reproduce
{
	public class GithubIssue2173 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;
		public GithubIssue2173(WritableCluster cluster)
		{
			_cluster = cluster;
		}

		[I] public void UpdateByQueryWithInvalidScript()
		{
			var client = _cluster.Client;
			var response = client.UpdateByQuery<Project>(typeof(Project), typeof(Project), u => u
				.Script("invalid groovy")
			);
			response.ShouldNotBeValid();
		}
	}
}

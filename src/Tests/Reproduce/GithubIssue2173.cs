using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Reproduce
{
	[Collection(TypeOfCluster.Indexing)]
	public class GithubIssue2173
	{
		private readonly IndexingCluster _cluster;
		public GithubIssue2173(IndexingCluster cluster)
		{
			_cluster = cluster;
		}

		[I] public void UpdateByQueryWithInvalidScript()
		{
			var client = _cluster.Client();
			var response = client.UpdateByQuery<Project>(u => u
				.Script("invalid groovy")
			);
			response.IsValid.Should().BeFalse();
		}
	}
}

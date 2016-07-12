using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.ClientConcepts.Connection
{
	[Collection(IntegrationContext.ReadOnly)]
	public class HttpConnectionTests
	{
		ReadOnlyCluster _cluster;

		public HttpConnectionTests(ReadOnlyCluster cluster, EndpointUsage usage)
		{
			_cluster = cluster;
		}

		[I]
		public void HttpCompression()
		{
			var client = _cluster.Client(s => s.EnableHttpCompression());
			var response = client.Search<Project>(s => s
				.Index("project")
				.Query(q => q
					.Term("foo", "bar")
				)
			);
			response.IsValid.Should().BeTrue();
		}
	}
}

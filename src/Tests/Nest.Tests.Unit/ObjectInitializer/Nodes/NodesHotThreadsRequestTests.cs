using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.ObjectInitializer.Nodes
{
	[TestFixture]
	public class NodesHotThreadsRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public NodesHotThreadsRequestTests()
		{
			var request = new NodesHotThreadsRequest
			{
				NodeId = "my-node-id",
				ThreadType = ThreadType.Block,
				Threads = 10,
				Snapshots = 2,
				Interval = "5s"
			};

			var response = _client.NodesHotThreads(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/nodes/my-node-id/hotthreads?type=block&threads=10&snapshots=2&interval=5s");
			this._status.RequestMethod.Should().Be("GET");
		}
	}
}

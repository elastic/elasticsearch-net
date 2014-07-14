using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.ClusterState
{
	[TestFixture]
	public class ClusterStateRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public ClusterStateRequestTests()
		{
			var request = new ClusterStateRequest()
			{
				Metrics = new [] {ClusterStateMetric.Blocks, ClusterStateMetric.MasterNode },
				FlatSettings = true
			};
			var response = this._client.ClusterState(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_cluster/state/blocks%2Cmaster_node?flat_settings=true");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}

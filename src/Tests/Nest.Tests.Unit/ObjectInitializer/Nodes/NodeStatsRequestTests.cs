using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Nodes
{
	[TestFixture]
	public class NodeStatsRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public NodeStatsRequestTests()
		{
			var request = new NodesStatsRequest
			{
				NodeId = "my-node-id",
				Human = true,
				CompletionFields = new List<PropertyPathMarker>
				{
					Property.Path<ElasticsearchProject>(p=>p.Name)
				}
			};
			var response = this._client.NodesStats(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_nodes/my-node-id/stats?human=true&completion_fields=name");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}

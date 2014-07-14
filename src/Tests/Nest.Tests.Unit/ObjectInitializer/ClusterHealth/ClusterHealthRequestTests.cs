using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.ClusterHealth
{
	[TestFixture]
	public class ClusterHealthRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public ClusterHealthRequestTests()
		{
			var request = new ClusterHealthRequest()
			{
				Level = Level.Cluster,
				WaitForStatus = WaitForStatus.Yellow
			};
			var response = this._client.ClusterHealth(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_cluster/health?level=cluster&wait_for_status=yellow");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}

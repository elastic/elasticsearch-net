using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Snapshot
{
	[TestFixture]
	public class GetSnapshotRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public GetSnapshotRequestTests()
		{
			var request = new GetSnapshotRequest("my-repository", "my-snap");
			var response = this._client.GetSnapshot(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_snapshot/my-repository/my-snap");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}

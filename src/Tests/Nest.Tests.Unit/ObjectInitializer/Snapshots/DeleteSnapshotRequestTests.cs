using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Snapshots
{
	[TestFixture]
	public class DeleteSnapshotRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public DeleteSnapshotRequestTests()
		{
			var request = new DeleteSnapshotRequest("repos", "snap");
			var response = this._client.DeleteSnapshot(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_snapshot/repos/snap");
			this._status.RequestMethod.Should().Be("DELETE");
		}
	}
}

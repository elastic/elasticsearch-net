using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Snapshot
{
	[TestFixture]
	public class SnapshotStatusRequestTests : BaseJsonTests
	{

		[Test]
		public void DefaultPath()
		{
			var result = this._client.SnapshotStatus(new SnapshotStatusRequest());
			var status = result.ConnectionStatus;

			status.RequestUrl.Should().EndWith("/_snapshot/_status");
		}
		
		[Test]
		public void RepositoryPath()
		{
			var result = this._client.SnapshotStatus(new SnapshotStatusRequest("my-repos"));
			var status = result.ConnectionStatus;

			status.RequestUrl.Should().EndWith("/_snapshot/my-repos/_status");
		}
		
		[Test]
		public void RepositoryWithSnapshots()
		{
			var result = this._client.SnapshotStatus(new SnapshotStatusRequest("my-repos", "snap1", "snap2"));
			var status = result.ConnectionStatus;

			status.RequestUrl.Should().EndWith("/_snapshot/my-repos/snap1%2Csnap2/_status");
		}
		
	}
}

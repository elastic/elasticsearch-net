using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Snapshots
{
	[TestFixture]
	public class SnapshotRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public SnapshotRequestTests()
		{
			var request = new SnapshotRequest("repos", "snap")
			{
				IgnoreUnavailable = true,
				IncludeGlobalState = true,
				WaitForCompletion = true,
				Partial = true,
			};
			var response = this._client.Snapshot(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should()
				.EndWith("/_snapshot/repos/snap?wait_for_completion=true");
			this._status.RequestMethod.Should().Be("PUT");
		}
		
		[Test]
		public void SnapshotBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}
}

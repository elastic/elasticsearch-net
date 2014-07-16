using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Snapshots
{
	[TestFixture]
	public class DeleteRepositoryRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public DeleteRepositoryRequestTests()
		{
			var request = new DeleteRepositoryRequest("repos");
			var response = this._client.DeleteRepository(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_snapshot/repos");
			this._status.RequestMethod.Should().Be("DELETE");
		}
	}
}

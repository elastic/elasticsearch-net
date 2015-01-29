using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Repository
{
	[TestFixture]
	public class VerifyRepositoryRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public VerifyRepositoryRequestTests()
		{
			var request = new VerifyRepositoryRequest("my-repository");
			var response = this._client.VerifyRepository(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_snapshot/my-repository/_verify");
			this._status.RequestMethod.Should().Be("POST");
		}
	}

}

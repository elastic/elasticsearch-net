using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Delete
{
	[TestFixture]
	public class DeleteRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public DeleteRequestTests()
		{
			var request = new DeleteRequest("my-index","my-type","my-doc-id")
			{
				Refresh = true,
				Consistency = Consistency.All,
				Version = 3,
				VersionType = VersionType.Force
			};
			var response = this._client.Delete(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/my-index/my-type/my-doc-id?refresh=true&consistency=all&version=3&version_type=force");
			this._status.RequestMethod.Should().Be("DELETE");
		}
	}

}

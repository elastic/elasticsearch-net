using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Snapshots
{
	[TestFixture]
	public class RestoreRequestTests : BaseJsonTests
	{
		private IRestoreRequest _request;
		private IRestoreResponse _response;
		private IElasticsearchResponse _status;

		public RestoreRequestTests()
		{
			this._request = new RestoreRequest("repos", "snap")
			{
				IgnoreUnavailable = true,
				IncludeGlobalState = true,
				WaitForCompletion = true,
				RenamePattern = "index_(.+)",
				RenameReplacement = "index_restored_$1"
			};
			this._response = this._client.Restore(this._request);
			this._status = this._response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			var url = this._status.RequestUrl;
			url.Should().EndWith("/_snapshot/repos/snap/_restore?wait_for_completion=true");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void RestoreBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}
}

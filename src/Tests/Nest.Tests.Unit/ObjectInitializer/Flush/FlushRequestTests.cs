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
	public class FlushRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public FlushRequestTests()
		{
			var request = new FlushRequest()
			{
				AllowNoIndices = false
			};
			var response = this._client.Flush(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/nest_test_data/_flush?allow_no_indices=false");
			this._status.RequestMethod.Should().Be("POST");
		}
	}

}

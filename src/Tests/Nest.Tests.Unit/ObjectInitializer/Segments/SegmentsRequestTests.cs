using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.ClusterHealth
{
	[TestFixture]
	public class SegmentsRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public SegmentsRequestTests()
		{
			var request = new SegmentsRequest()
			{
				AllowNoIndices = true
			};
			var response = this._client.Segments(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_segments?allow_no_indices=true");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}

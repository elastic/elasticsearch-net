using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Percolate
{
	[TestFixture]
	public class PercolateRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public PercolateRequestTests()
		{
			var doc = new ElasticsearchProject() { Id = 19, Name = "Hello" };
			var request = new PercolateRequest<ElasticsearchProject>(doc)
			{
				Query = Query<ElasticsearchProject>.Term("color", "blue")
			};
			var response = this._client.Percolate<ElasticsearchProject>(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/nest_test_data/elasticsearchprojects/_percolate");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void PercolateBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}

}

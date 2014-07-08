using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Index
{
	[TestFixture]
	public class IndexRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public IndexRequestTests()
		{
			var newProject = new ElasticsearchProject
			{
				Id = 15,
				Name = "Some awesome new elasticsearch project"
			};
			var request = new IndexRequest<ElasticsearchProject>(newProject)
			{
				Replication = Replication.Async
			};
			//TODO Index(request) does not work as expected
			var response = this._client.Index<ElasticsearchProject>(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/nest_test_data/elasticsearchprojects/15?replication=async");
			this._status.RequestMethod.Should().Be("PUT");
		}
		
		[Test]
		public void IndexBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}

}

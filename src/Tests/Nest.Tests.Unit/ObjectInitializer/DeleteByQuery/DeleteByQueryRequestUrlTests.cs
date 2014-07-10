using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.DeleteByQuery
{
	[TestFixture]
	public class DeleteByQueryRequestUrlTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		[Test]
		public void Untyped_Defaults()
		{
			var status = this._client.DeleteByQuery(new DeleteByQueryRequest())
				.ConnectionStatus;

			status.RequestUrl.Should().EndWith("/_all/_query");
			status.RequestMethod.Should().Be("DELETE");
		}
	
		[Test]
		public void Untyped_Overrides()
		{
			var status = this._client.DeleteByQuery(new DeleteByQueryRequest
			{
				Indices = new IndexNameMarker[] { typeof(ElasticsearchProject), "my-other-index"},
				Types = new TypeNameMarker[] {"sometype"}
			}).ConnectionStatus;

			status.RequestUrl.Should().EndWith("/nest_test_data%2Cmy-other-index/sometype/_query");
			status.RequestMethod.Should().Be("DELETE");
		}
	
		[Test]
		public void Untyped_IndexTypeConstructor()
		{
			var status = this._client.DeleteByQuery(new DeleteByQueryRequest("myindex", "mytype")
			{
			})
				.ConnectionStatus;

			status.RequestUrl.Should().EndWith("/myindex/mytype/_query");
			status.RequestMethod.Should().Be("DELETE");
		}
	
		[Test]
		public void Untyped_ConstructorBeatsAllSetting()
		{
			var status = this._client.DeleteByQuery(new DeleteByQueryRequest("my-other-index")
			{
				AllIndices = true,
				AllTypes = true
			})
				.ConnectionStatus;

			status.RequestUrl.Should().EndWith("/my-other-index/_query");
			status.RequestMethod.Should().Be("DELETE");
		}
	
		[Test]
		public void Untyped_AllIndicesAllTypes()
		{
			var status = this._client.DeleteByQuery(new DeleteByQueryRequest
			{
				AllTypes = true,
				AllIndices = true
			})
				.ConnectionStatus;

			status.RequestUrl.Should().EndWith("/_all/_query");
			status.RequestMethod.Should().Be("DELETE");
		}
	
	}
}

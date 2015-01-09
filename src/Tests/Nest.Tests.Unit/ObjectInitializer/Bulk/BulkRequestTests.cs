using System.Collections.Generic;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Bulk
{
	[TestFixture]
	public class BulkRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public BulkRequestTests()
		{

			var newProject = new ElasticsearchProject { Id = 4, Name = "new-project" };

			var request = new BulkRequest()
			{
				Refresh = true,
				Consistency = Consistency.One,
				Operations = new List<IBulkOperation>
				{
					{ new BulkIndexOperation<ElasticsearchProject>(newProject)  { Id= "2"}},
					{ new BulkDeleteOperation<ElasticsearchProject>(6) },
					{ new BulkCreateOperation<ElasticsearchProject>(newProject) { Id = "6" } },
					{ new BulkUpdateOperation<ElasticsearchProject, object>(newProject, new { name = "new-project2"}) { Id = "3" } },
				}
			};
			var response = this._client.Bulk(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_bulk?refresh=true&consistency=one");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void BulkBody()
		{
			this.BulkJsonEquals(this._status.Request.Utf8String(), MethodBase.GetCurrentMethod());
		}
	}
}

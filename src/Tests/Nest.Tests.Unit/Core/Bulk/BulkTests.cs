using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Bulk
{
	[TestFixture]
	public class BulkTests : BaseJsonTests
	{
		[Test]
		public void BulkOperations()
		{
			var result = this._client.Bulk(b => b
				.Index<ElasticsearchProject>(i => i
					.Document(new ElasticsearchProject {Id = 2})
					.VersionType(VersionType.Force))
				.Create<ElasticsearchProject>(i => i
					.Document(new ElasticsearchProject { Id = 3 })
					.VersionType(VersionType.Internal))
				.Delete<ElasticsearchProject>(i => i
					.Document(new ElasticsearchProject { Id = 4 })
					.VersionType(VersionType.ExternalGte))
				.Update<ElasticsearchProject, object>(i => i
					.InferFrom(new ElasticsearchProject { Id = 3 })
					.VersionType(VersionType.External)
					.PartialDocument(new { name = "NEST"})
				)
			);
			var status = result.ConnectionStatus;
			this.BulkJsonEquals(status.Request.Utf8String(), MethodInfo.GetCurrentMethod());
		}
		
		[Test]
		public void BulkOperations_ObjectInitializer()
		{
			var result = this._client.Bulk(new BulkRequest
			{
				Operations = new List<IBulkOperation>
				{
					{ new BulkIndexOperation<ElasticsearchProject>(new ElasticsearchProject { Id = 2 })
					{
						VersionType = VersionType.Force
					}},
					{ new BulkCreateOperation<ElasticsearchProject>(new ElasticsearchProject { Id = 3 })
					{
						VersionType = VersionType.Internal
					}},
					{ new BulkDeleteOperation<ElasticsearchProject>(4)
					{
						VersionType = VersionType.ExternalGte
					}},
					{ new BulkUpdateOperation<ElasticsearchProject, object>(new ElasticsearchProject { Id = 3})
					{
						VersionType = VersionType.External,
						PartialDocument = new { name = "NEST"}
					}},
				}
			});
			var status = result.ConnectionStatus;
			this.BulkJsonEquals(status.Request.Utf8String(), MethodBase.GetCurrentMethod(), "BulkOperations");
		}

		[Test]
		public void BulkUpdateDetails()
		{
			var result = this._client.Bulk(b => b
				.Update<ElasticsearchProject, object>(i => i
					.InferFrom(new ElasticsearchProject { Id = 3 })
					.PartialDocument(new { name = "NEST" })
					.RetriesOnConflict(4)
				)
				.Index<ElasticsearchProject>(i=>i
					.Document(new ElasticsearchProject { Name = "yodawg", Id = 90})
					.Percolate("percolateme")
				)
			);
			var status = result.ConnectionStatus;
			//Assert.Fail(status.Request.Utf8String());
			this.BulkJsonEquals(status.Request.Utf8String(), MethodInfo.GetCurrentMethod());
		}
	}
}

using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
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
					.IdFrom(new ElasticsearchProject { Id = 3 })
					.VersionType(VersionType.External)
					.Doc(new { name = "NEST"})
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
						Doc = new { name = "NEST"}
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
					.IdFrom(new ElasticsearchProject { Id = 3 })
					.Doc(new { name = "NEST" })
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

		[Test]
		public void BulkIndexDetailsFixedPath()
		{
			var result = this._client.Bulk(b => b
				.FixedPath("myindex", "mytype")
				.Index<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 5 }))
				.Index<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 6 }).Index("myindex2"))
				.Index<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 7 }).Index("myindex2").Type("mytype2"))
				.Create<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 5 }))
				.Create<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 6 }).Index("myindex2"))
				.Create<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 7 }).Index("myindex2").Type("mytype2"))
				.Delete<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 5 }))
				.Delete<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 6 }).Index("myindex2"))
				.Delete<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 7 }).Index("myindex2").Type("mytype2"))
			);
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/mytype/_bulk");

			var status = result.ConnectionStatus;
			//Assert.Fail(status.Request.Utf8String());
			this.BulkJsonEquals(status.Request.Utf8String(), MethodInfo.GetCurrentMethod());
		}

	}
}

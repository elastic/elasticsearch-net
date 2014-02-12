using System;
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
				.Index<ElasticsearchProject>(i => i.Object(new ElasticsearchProject {Id = 2}))
				.Create<ElasticsearchProject>(i => i.Object(new ElasticsearchProject { Id = 3 }))
				.Delete<ElasticsearchProject>(i => i.Object(new ElasticsearchProject { Id = 4 }))
				.Update<ElasticsearchProject, object>(i => i
					.Object(new ElasticsearchProject { Id = 3 })
					.Document(new { name = "NEST"})
				)
			);
			var status = result.ConnectionStatus;
			this.BulkJsonEquals(status.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void BulkUpdateDetails()
		{
			var result = this._client.Bulk(b => b
				.Update<ElasticsearchProject, object>(i => i
					.Object(new ElasticsearchProject { Id = 3 })
					.Document(new { name = "NEST" })
					.RetriesOnConflict(4)
				)
				.Index<ElasticsearchProject>(i=>i
					.Object(new ElasticsearchProject { Name = "yodawg", Id = 90})
					.Percolate("percolateme")
				)
			);
			var status = result.ConnectionStatus;
			this.BulkJsonEquals(status.Request, MethodInfo.GetCurrentMethod());
		}
	}
}

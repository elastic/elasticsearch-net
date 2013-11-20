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
				.Index<ElasticSearchProject>(i => i.Object(new ElasticSearchProject {Id = 2}))
				.Create<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 3 }))
				.Delete<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 4 }))
				.Update<ElasticSearchProject, object>(i => i
					.Object(new ElasticSearchProject { Id = 3 })
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
				.Update<ElasticSearchProject, object>(i => i
					.Object(new ElasticSearchProject { Id = 3 })
					.Document(new { name = "NEST" })
					.RetriesOnConflict(4)
				)
				.Index<ElasticSearchProject>(i=>i
					.Object(new ElasticSearchProject { Name = "yodawg", Id = 90})
					.Percolate("percolateme")
				)
			);
			var status = result.ConnectionStatus;
			this.BulkJsonEquals(status.Request, MethodInfo.GetCurrentMethod());
		}
	}
}

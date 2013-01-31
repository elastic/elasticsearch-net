using System;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.Bulk
{
	[TestFixture]
	public class BulkTests : BaseJsonTests
	{
		[Test]
		public void BulkNonFixed()
		{
			var result = this._client.Bulk(b => b
				.Index<ElasticSearchProject>(i => i.Object(new ElasticSearchProject {Id = 2}))
				.Create<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 3 }))
				.Delete<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 4 }))
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/_bulk");
		}
		[Test]
		public void BulkFixedIndex()
		{
			var result = this._client.Bulk(b => b
				.FixedPath("myindex")
				.Index<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 2 }))
				.Create<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 3 }))
				.Delete<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 4 }))
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/_bulk");
		}
		[Test]
		public void BulkFixedIndexAndType()
		{
			var result = this._client.Bulk(b => b
				.FixedPath("myindex", "mytype")
				.Index<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 2 }))
				.Create<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 3 }))
				.Delete<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 4 }))
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/mytype/_bulk");
		}
	}
}

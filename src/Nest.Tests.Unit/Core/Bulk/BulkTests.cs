using System;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.Bulk
{
	[TestFixture]
	public class BulkTests : BaseJsonTests
	{
		[Test]
		public void Bulk()
		{
			var result = this._client.Bulk(b => b
				.Index<ElasticSearchProject>(i => i.Object(new ElasticSearchProject {Id = 2}))
				.Create<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 3 }))
				.Delete<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 4 }))
			);
			var status = result.ConnectionStatus;
			throw new Exception(status.Request);
			//StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.Result);
			//StringAssert.EndsWith("/nest_test_data/elasticsearchprojects/1", status.RequestUrl);
		}
	}
}

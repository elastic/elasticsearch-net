using Elasticsearch.Net;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Search.Filter
{
	[TestFixture]
	public class BoolFilterTests : IntegrationTests
	{
		//this test started to fail on 0.90.0
		//see https://github.com/elasticsearch/elasticsearch/issues/2996
		[Test]
		public void BoolFilter()
		{
			var results = this._client.Search<ElasticsearchProject>(s=>s
				.From(0)
				.Size(20)
				.Fields(p=>p.Id, p=>p.Name, p=>p.LOC)
				.Filter(filter=>filter
					.Bool(b=>b
						.Must(
							f => f.MatchAll()
						)
						.MustNot(
							f => f.Term(e => e.Name, "elasticflume")
						)
						.Should(
							f=> f.Exists(p => p.LOC)
						)
					)
				)
			);
			Assert.True(results.IsValid, results.ConnectionStatus.ResponseRaw.Utf8String());
			Assert.Greater(results.Total, 0);
			Assert.Greater(results.Total, 10);
			
			// assert we actually filtered on something
			// A known bug exisits in 0.90.0.0 causing this test to fail
			//https://github.com/elasticsearch/elasticsearch/issues/2996

			var totalInIndex = this._client.Count<ElasticsearchProject>(c=>c.Query(q=>q.MatchAll())).Count;
			Assert.Less(results.Total, totalInIndex);
		}
	}
}

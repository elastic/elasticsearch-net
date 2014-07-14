using System;
using Elasticsearch.Net;
using NUnit.Framework;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Search.Query
{
	[TestFixture]
	public class BoolQueryResults : IntegrationTests
	{

		/// <summary>
		/// Document used in test.
		/// </summary>
		private ElasticsearchProject _LookFor;

		[TestFixtureSetUp]
		public void Initialize()
		{
			_LookFor = NestTestData.Session.Single<ElasticsearchProject>().Get();
			_LookFor.Name = "one two three four";
			var status = this._client.Index(_LookFor, i=>i.Refresh()).ConnectionStatus;
			Assert.True(status.Success, status.ResponseRaw.Utf8String());
		}

		[Test]
		public void SingleTerm()
		{
			var results = _client.Search<BoolTerm>(s => s
				.Query(q => 
					q.Term(p => p.Name1, "a1")
				)
			);
			this.AssertBoolQueryResults(results, expectedCount: 1);
		}

		[Test]
		public void TwoTermsOfSingleDocument()
		{
			var results = _client.Search<BoolTerm>(s => s
				.Query(q =>
					q.Term(p => p.Name1, "a1") && q.Term(p=>p.Name2, "b1")
				)
			);
			this.AssertBoolQueryResults(results, expectedCount: 1);
		}

		[Test]
		public void TwoTermsOfNoDocument()
		{
			var results = _client.Search<BoolTerm>(s => s
				.Query(q =>
					q.Term(p => p.Name1, "a1") && q.Term(p=>p.Name2, "b2")
				)
			);
			this.AssertBoolQueryResults(results, expectedCount: 0);
		}

		[Test]
		public void ThreeTermsOfOneDocument()
		{
			var results = _client.Search<BoolTerm>(s => s
				.Query(q =>
					q.Term(p => p.Name1, "a1") && (q.Term(p => p.Name2, "b2") || q.Term(p => p.Name2, "b1"))
				)
			);
			this.AssertBoolQueryResults(results, expectedCount: 1);
		}

		[Test]
		public void TwoTermsOfTwoDocuments()
		{
			var results = _client.Search<BoolTerm>(s => s
				.Query(q =>
					q.Term(p => p.Name1, "a1") || q.Term(p => p.Name2, "b2")
				)
			);
			this.AssertBoolQueryResults(results, expectedCount: 2);
		}

		[Test]
		public void FourTermsOfTwoDocuments()
		{
			var results = _client.Search<BoolTerm>(s => s
				.Query(q =>
					(q.Term(p => p.Name1, "a1") && q.Term(p => p.Name2, "b1"))
					|| (q.Term(p => p.Name1, "a2") && q.Term(p => p.Name2, "b2"))
				)
			);
			this.AssertBoolQueryResults(results, expectedCount: 2);
		}

		private void AssertBoolQueryResults(ISearchResponse<BoolTerm> results, int expectedCount)
		{
			Assert.True(results.IsValid, results.ConnectionStatus.ResponseRaw.Utf8String());
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.ResponseRaw.Utf8String());
			Assert.GreaterOrEqual(results.Total, expectedCount);
		}
	}
}

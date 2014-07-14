using System;
using Elasticsearch.Net;
using NUnit.Framework;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Search.Query
{
	[TestFixture]
	public class TermToStringJson : IntegrationTests
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
			var status = this.Client.Index(_LookFor, i=>i.Refresh()).ConnectionStatus;
			Assert.True(status.Success, status.ResponseRaw.Utf8String());
		}

		[Test]
		public void IntToStringTest()
		{
			var results = Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Term(p => p.LOC, _LookFor.LOC)
				)
			);
			this.AssertTermResults(results);
		}

		[Test]
		public void DoubleToStringTest()
		{
			var results = Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Term(p => p.DoubleValue, _LookFor.DoubleValue)
				)
			);
			this.AssertTermResults(results);
		}

		[Test]
		public void FloatToStringTest()
		{
			var results = Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Term(p => p.FloatValue, _LookFor.FloatValue)
					)
			);
			this.AssertTermResults(results);
		}

		[Test]
		public void LongToStringTest()
		{
			var results = Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Term(p => p.LongValue, _LookFor.LongValue)
					)
			);
			this.AssertTermResults(results);
		}
		[Test]
		public void DateTimeToStringTest()
		{
			//this should serialize to ISO NOT simply datetime.tostring()!
			var results = Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Term(p => p.StartedOn, _LookFor.StartedOn)
					)
			);
			this.AssertTermResults(results);
		}

		[Test]
		public void BoolToStringTests()
		{
			//this should serialize to ISO NOT simply datetime.tostring()!
			var results = Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Term(p => p.BoolValue, _LookFor.BoolValue)
					)
			);
			this.AssertTermResults(results);
		}
		

		private void AssertTermResults(ISearchResponse<ElasticsearchProject> results)
		{
			Assert.True(results.IsValid, results.ConnectionStatus.ResponseRaw.Utf8String());
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.ResponseRaw.Utf8String());
			Assert.GreaterOrEqual(results.Total, 1);
		}
	}
}

using System;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.Integration;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Integration.Query
{
	[TestFixture]
	public class TermToStringJson : IntegrationTests
	{

		/// <summary>
		/// Document used in test.
		/// </summary>
		private ElasticSearchProject _LookFor;

		[TestFixtureSetUp]
		public void Initialize()
		{
			_client.IsValid.Should().BeTrue();

			_LookFor = NestTestData.Session.Single<ElasticSearchProject>().Get();
			_LookFor.Name = "one two three four";
			var status = this._client.Index(_LookFor, new IndexParameters { Refresh = true }).ConnectionStatus;
			Assert.True(status.Success, status.Result);
		}

		[Test]
		public void IntToStringTest()
		{
			var results = _client.Search<ElasticSearchProject>(s => s
				.Query(q => q
					.Term(p => p.LOC, _LookFor.LOC)
				)
			);
			this.AssertTermResults(results);
		}

		[Test]
		public void DoubleToStringTest()
		{
			var results = _client.Search<ElasticSearchProject>(s => s
				.Query(q => q
					.Term(p => p.DoubleValue, _LookFor.DoubleValue)
				)
			);
			this.AssertTermResults(results);
		}

		[Test]
		public void FloatToStringTest()
		{
			var results = _client.Search<ElasticSearchProject>(s => s
				.Query(q => q
					.Term(p => p.FloatValue, _LookFor.FloatValue)
					)
			);
			this.AssertTermResults(results);
		}

		[Test]
		public void LongToStringTest()
		{
			var results = _client.Search<ElasticSearchProject>(s => s
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
			var results = _client.Search<ElasticSearchProject>(s => s
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
			var results = _client.Search<ElasticSearchProject>(s => s
				.Query(q => q
					.Term(p => p.BoolValue, _LookFor.BoolValue)
					)
			);
			this.AssertTermResults(results, 7);
		}
		

		private void AssertTermResults(IQueryResponse<ElasticSearchProject> results, int expected = 1)
		{
			Assert.True(results.IsValid, results.ConnectionStatus.Result);
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.Result);
			Assert.AreEqual(expected, results.Total);
		}
	}
}

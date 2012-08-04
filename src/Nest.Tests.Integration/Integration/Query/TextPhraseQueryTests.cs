using System.Linq;
using System.Threading;
using NUnit.Framework;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Integration.Query
{
	/// <summary>
	/// Integrated tests of NumericRangeFilter with elasticsearch.
	/// </summary>
	[TestFixture]
	public class TextPhraseQueryTests : BaseElasticSearchTests
	{
		/// <summary>
		/// Document used in test.
		/// </summary>
		private ElasticSearchProject _LookFor;

		/// <summary>
		/// Create document for test.
		/// </summary>
		protected override void ResetIndexes()
		{
			base.ResetIndexes();
			var client = this.ConnectedClient;
			if (client.IsValid)
			{
				_LookFor = NestTestData.Session.Single<ElasticSearchProject>().Get();
				_LookFor.Name = "one two three four";
				var status = this.ConnectedClient.Index(_LookFor);
				Assert.True(status.Success, status.Result);

				Assert.True(this.ConnectedClient.Flush<ElasticSearchProject>().OK, "Flush");
			}
		}

		/// <summary>
		/// Test control. If this test fail, the problem not in TextPhraseQuery.
		/// </summary>
		[Test]
		public void TestControl()
		{
			var id = _LookFor.Id;
			var filterId = Filter<ElasticSearchProject>.Term(e => e.Id, id.ToString());

			var results = this.ConnectedClient.Search<ElasticSearchProject>(
				s => s.Filter(filterId)
				);

			Assert.True(results.IsValid, results.ConnectionStatus.Result);
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.Result);
			Assert.AreEqual(1, results.Total);
		}

		/// <summary>
		/// Set of filters that should not filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestNotFiltered()
		{
			var id = _LookFor.Id;
			var filterId = Filter<ElasticSearchProject>.Term(e => e.Id, id.ToString());
			var querySlop0 = Query<ElasticSearchProject>.TextPhrase(
				textPhrase => textPhrase
					.OnField(p => p.Name)
					.QueryString("one two")
					.Slop(0)
					.Operator(Operator.and));
			var querySlop1 = Query<ElasticSearchProject>.TextPhrase(
				textPhrase => textPhrase
					.OnField(p => p.Name)
					.QueryString("one three")
					.Slop(1)
					.Operator(Operator.and));

			var results = this.ConnectedClient.Search<ElasticSearchProject>(
				s => s.Filter(filterId)
					.Query(querySlop0 && querySlop1)
				);

			Assert.True(results.IsValid, results.ConnectionStatus.Result);
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.Result);
			Assert.AreEqual(1, results.Total);
		}

		/// <summary>
		/// Set of filters that should filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestFiltered()
		{
			var id = _LookFor.Id;
			var filterId = Filter<ElasticSearchProject>.Term(e => e.Id, id.ToString());
			var querySlop0 = Query<ElasticSearchProject>.TextPhrase(
				textPhrase => textPhrase
					.OnField(p => p.Name)
					.QueryString("one three")
					.Slop(0));
			var querySlop1 = Query<ElasticSearchProject>.TextPhrase(
				textPhrase => textPhrase
					.OnField(p => p.Name)
					.QueryString("one four")
					.Slop(1));
			var queryFail = Query<ElasticSearchProject>.TextPhrase(
				textPhrase => textPhrase
					.OnField(p => p.Name)
					.QueryString("one fail"));
			var queryFailOr = Query<ElasticSearchProject>.TextPhrase(
				textPhrase => textPhrase
					.OnField(p => p.Name)
					.QueryString("fail fail"));

			var results = this.ConnectedClient.Search<ElasticSearchProject>(
				s => s.Filter(filterId)
					.Query(querySlop0 || querySlop1 || queryFail || queryFailOr)
				);

			Assert.True(results.IsValid, results.ConnectionStatus.Result);
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.Result);
			Assert.AreEqual(0, results.Total);
		}
	}
}

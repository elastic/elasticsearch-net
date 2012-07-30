using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Integration.Filter
{
	/// <summary>
	/// Integrated tests of NumericRangeFilter with elasticsearch.
	/// </summary>
	[TestFixture]
	public class NumericRangeFilterTests : BaseElasticSearchTests
	{
		/// <summary>
		/// Document used in test.
		/// </summary>
		private ElasticSearchProject _LookFor = NestTestData.Data.First();

		/// <summary>
		/// Test control. If this test fail, the problem not in NumericRangeFilter.
		/// </summary>
		[Test]
		public void TestControl()
		{
			var id = _LookFor.Id;
			var filterId = Filter<ElasticSearchProject>.Term(e => e.Id, id.ToString());

			var results = this.ConnectedClient.Search<ElasticSearchProject>(
				s => s.Filter(filterId && filterId)
				);

			Assert.True(results.IsValid, results.ConnectionStatus.Result);
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.Result);
			Assert.AreEqual(results.Total, 1);
		}

		/// <summary>
		/// Set of filters that should not filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestNotFiltered()
		{
			var id = _LookFor.Id;
			var filterId = Filter<ElasticSearchProject>.Term(e => e.Id, id.ToString());
			var filterInclusive = Filter<ElasticSearchProject>.NumericRange(range => range.OnField(e => e.Id).From(id).To(id));
			var filterExclusive = Filter<ElasticSearchProject>.NumericRange(range => range.OnField(e => e.Id).From(id - 1).To(id + 1).FromExclusive().ToExclusive());
			var filterGreaterOrEquals = Filter<ElasticSearchProject>.NumericRange(range => range.OnField(e => e.Id).GreaterOrEquals(id));
			var filterLowerOrEquals = Filter<ElasticSearchProject>.NumericRange(range => range.OnField(e => e.Id).LowerOrEquals(id));

			var results = this.ConnectedClient.Search<ElasticSearchProject>(
				s => s.Filter(filterId && filterInclusive && filterExclusive && filterGreaterOrEquals && filterLowerOrEquals)
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
			var filterFromInclusive = Filter<ElasticSearchProject>.NumericRange(range => range.OnField(e => e.Id).From(id + 1));
			var filterToInclusive = Filter<ElasticSearchProject>.NumericRange(range => range.OnField(e => e.Id).To(id - 1));
			var filterFromExclusive = Filter<ElasticSearchProject>.NumericRange(range => range.OnField(e => e.Id).From(id).FromExclusive());
			var filterToExclusive = Filter<ElasticSearchProject>.NumericRange(range => range.OnField(e => e.Id).To(id).ToExclusive());

			var results = this.ConnectedClient.Search<ElasticSearchProject>(
				s => s.Filter(filterId && (filterFromInclusive || filterToInclusive || filterFromExclusive || filterToExclusive))
				);

			Assert.True(results.IsValid, results.ConnectionStatus.Result);
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.Result);
			Assert.AreEqual(0, results.Total);
		}
	}
}

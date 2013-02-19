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
	public class NumericRangeFilterTests : IntegrationTests
	{
		/// <summary>
		/// Document used in test.
		/// </summary>
		private ElasticSearchProject _LookFor = NestTestData.Data.First();

		/// <summary>
		/// Set of filters that should not filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestNotFiltered()
		{
			var id = _LookFor.Id;

			this.DoFilterTest(f => f.NumericRange(range => range.OnField(e => e.Id).From(id).To(id)), _LookFor, true);

			this.DoFilterTest(f => f.NumericRange(range => range.OnField(e => e.Id).From(id - 1).To(id + 1).FromExclusive().ToExclusive()), _LookFor, true);

			this.DoFilterTest(f => f.NumericRange(range => range.OnField(e => e.Id).GreaterOrEquals(id)), _LookFor, true);

			this.DoFilterTest(f => f.NumericRange(range => range.OnField(e => e.Id).LowerOrEquals(id)), _LookFor, true);
		}

		/// <summary>
		/// Set of filters that should filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestFiltered()
		{
			var id = _LookFor.Id;

			this.DoFilterTest(f => f.NumericRange(range => range.OnField(e => e.Id).From(id + 1)), _LookFor, false);

			this.DoFilterTest(f => f.NumericRange(range => range.OnField(e => e.Id).To(id - 1)), _LookFor, false);

			this.DoFilterTest(f => f.NumericRange(range => range.OnField(e => e.Id).From(id).FromExclusive()), _LookFor, false);

			this.DoFilterTest(f => f.NumericRange(range => range.OnField(e => e.Id).To(id).ToExclusive()), _LookFor, false);
		}
	}
}

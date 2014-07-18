using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Search.Filter
{
	/// <summary>
	/// Integrated tests of AndFilter, OrFilter and NotFilter with elasticsearch.
	/// </summary>
	[TestFixture]
	public class AndOrNotFilterTests : IntegrationTests
	{
		/// <summary>
		/// Document used in test.
		/// </summary>
		private ElasticsearchProject _LookFor = NestTestData.Data.First();

		/// <summary>
		/// Test the AndFilter.
		/// </summary>
		[Test]
		public void TestAndFilter()
		{
			var country = _LookFor.Country;
			var countryFail = _LookFor.Country + "_fail";

			this.DoFilterTest(
				ft => ft.And(
					f => f.Term(e => e.Country, country),
					f => f.Term(e => e.Country, country),
					f => f.Term(e => e.Country, country)
				      	),
				_LookFor,
				true
				);

			this.DoFilterTest(
				ft => ft.And(
					f => f.Term(e => e.Country, country),
					f => f.Term(e => e.Country, country),
					f => f.Term(e => e.Country, countryFail)
				      	),
				_LookFor,
				false
				);
		}

		/// <summary>
		/// Test the OrFilter.
		/// </summary>
		[Test]
		public void TestOrFilter()
		{
			var country = _LookFor.Country;
			var countryFail = _LookFor.Country + "_fail";

			this.DoFilterTest(
				ft => ft.Or(
					f => f.Term(e => e.Country, country),
					f => f.Term(e => e.Country, countryFail),
					f => f.Term(e => e.Country, countryFail)
						),
				_LookFor,
				true
				);

			this.DoFilterTest(
				ft => ft.Or(
					f => f.Term(e => e.Country, countryFail),
					f => f.Term(e => e.Country, countryFail),
					f => f.Term(e => e.Country, countryFail)
						),
				_LookFor,
				false
				);
		}

		/// <summary>
		/// Set of filters that should not filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestNotFilter()
		{
			this.DoFilterTest(
				f => f.Not(n => n.Term(o => o.Id, (_LookFor.Id + 1))),
				_LookFor,
				true);

			this.DoFilterTest(
				f => f.Not(n => n.Term(o => o.Id, _LookFor.Id)),
				_LookFor,
				false);
		}
	}
}

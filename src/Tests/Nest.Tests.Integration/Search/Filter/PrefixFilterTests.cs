using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Search.Filter
{
	/// <summary>
	/// Integrated tests of PrefixFilter with elasticsearch.
	/// </summary>
	[TestFixture]
	public class PrefixFilterTests : IntegrationTests
	{
		/// <summary>
		/// Document used in test.
		/// </summary>
		private ElasticsearchProject _LookFor = NestTestData.Data.First();

		/// <summary>
		/// Set of filters that should not filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestNotFiltered()
		{
			var country = _LookFor.Country;

			this.DoFilterTest(f => f.Prefix(p => p.Country, country.Substring(0, 2)), _LookFor, true);

			this.DoFilterTest(f => f.Prefix(e => e.Country, country.Substring(0, 1)), _LookFor, true);
	
			this.DoFilterTest(f => f.Prefix(e => e.Country, country), _LookFor, true);

		}

		/// <summary>
		/// Set of filters that should filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestFiltered()
		{
			var countryFail = "_" + _LookFor.Country;

			this.DoFilterTest(f => f.Prefix(p => p.Country, countryFail.Substring(0, 2)), _LookFor, false);

			this.DoFilterTest(f => f.Prefix(e => e.Country, countryFail.Substring(0, 3)), _LookFor, false);
	
			this.DoFilterTest(f => f.Prefix(e => e.Country, countryFail), _LookFor, false);

		}
	}
}

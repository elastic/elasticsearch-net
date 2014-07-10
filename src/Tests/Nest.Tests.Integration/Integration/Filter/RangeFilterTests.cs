using System.Linq;
using Elasticsearch.Net;
using NUnit.Framework;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Integration.Filter
{
	/// <summary>
	/// Integrated tests of RangeFilter with elasticsearch.
	/// </summary>
	[TestFixture]
	public class RangeFilterTests : IntegrationTests
	{
		/// <summary>
		/// Document used in test.
		/// </summary>
		private ElasticsearchProject _LookFor;

		[TestFixtureSetUp]
		public void Initialize()
		{
			_LookFor = NestTestData.Session.Single<ElasticsearchProject>().Get();
			_LookFor.Name = "mmm";
			var status = this._client.Index(_LookFor, i=>i.Refresh()).ConnectionStatus;
			Assert.True(status.Success, status.ResponseRaw.Utf8String());
		}



		/// <summary>
		/// Set of filters that should not filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestNotFiltered()
		{
			var name = _LookFor.Name;

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).GreaterOrEquals(name).LowerOrEquals(name)), _LookFor, true);

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).GreaterOrEquals("aaa").LowerOrEquals("zzz")), _LookFor, true);

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).GreaterOrEquals(name)), _LookFor, true);

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).LowerOrEquals(name)), _LookFor, true);

		}

		/// <summary>
		/// Set of filters that should filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestFiltered()
		{
			var name = _LookFor.Name;

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).GreaterOrEquals("zzz")), _LookFor, false);

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).LowerOrEquals("aaa")), _LookFor, false);

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).GreaterOrEquals(name)), _LookFor, true);

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).LowerOrEquals(name)), _LookFor, true);

		}
	}
}

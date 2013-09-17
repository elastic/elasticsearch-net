using System.Linq;
using FluentAssertions;
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
		private ElasticSearchProject _LookFor;

		[TestFixtureSetUp]
		public void Initialize()
		{
			_LookFor = NestTestData.Session.Single<ElasticSearchProject>().Get();
			_LookFor.Name = "mmm";
			var status = this._client.Index(_LookFor, new IndexParameters { Refresh = true }).ConnectionStatus;
			Assert.True(status.Success, status.Result);
		}



		/// <summary>
		/// Set of filters that should not filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestNotFiltered()
		{
			var name = _LookFor.Name;

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).From(name).To(name)), _LookFor, true);

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).From("aaa").To("zzz").FromExclusive().ToExclusive()), _LookFor, true);

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

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).From("zzz")), _LookFor, false);

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).To("aaa")), _LookFor, false);

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).From(name).FromExclusive()), _LookFor, false);

			this.DoFilterTest(f => f.Range(range => range.OnField(e => e.Name).To(name).ToExclusive()), _LookFor, false);

		}
	}
}

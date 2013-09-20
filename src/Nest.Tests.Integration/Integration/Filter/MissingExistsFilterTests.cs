using System;
using System.Linq;
using System.Linq.Expressions;
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
	public class MissingExistsFilterTests : IntegrationTests
	{
		/// <summary>
		/// Document used in test.
		/// </summary>
		private ElasticSearchProject _LookFor;

		/// <summary>
		/// Field missing on document.
		/// </summary>
		private Expression<Func<ElasticSearchProject, object>> _MissingField;

		/// <summary>
		/// Field exists on document.
		/// </summary>
		private Expression<Func<ElasticSearchProject, object>> _ExistsField;


		[TestFixtureSetUp]
		public void Initialize()
		{
			_LookFor = NestTestData.Session.Single<ElasticSearchProject>().Get();
			_MissingField = f => f.Name;
			_ExistsField = f => f.Id;

			// missing
			_LookFor.Name = null;

			var status = this._client.Index(_LookFor, new IndexParameters { Refresh = true }).ConnectionStatus;
			Assert.True(status.Success, status.Result);
		}





		/// <summary>
		/// Set of filters that should not filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestMissingFilter()
		{
			this.DoFilterTest(
				f => f.Missing(_MissingField),
				_LookFor,
				true);

			this.DoFilterTest(
				f => f.Missing(_ExistsField),
				_LookFor,
				false);
		}

		/// <summary>
		/// Set of filters that should not filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestExistsFilter()
		{
			this.DoFilterTest(
				f => f.Exists(_ExistsField),
				_LookFor,
				true);

			this.DoFilterTest(
				f => f.Exists(_MissingField),
				_LookFor,
				false);
		}

	}
}

using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using Shared.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

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
		private ElasticsearchProject _LookFor;

		/// <summary>
		/// Field missing on document.
		/// </summary>
		private Expression<Func<ElasticsearchProject, object>> _MissingField;

		/// <summary>
		/// Field exists on document.
		/// </summary>
		private Expression<Func<ElasticsearchProject, object>> _ExistsField;


		[TestFixtureSetUp]
		public void Initialize()
		{
			_LookFor = NestTestData.Session.Single<ElasticsearchProject>().Get();
			_MissingField = f => f.Name;
			_ExistsField = f => f.Id;

			// missing
			_LookFor.Name = null;

			var status = this._client.Index(_LookFor, i=>i.Refresh()).ConnectionStatus;
			Assert.True(status.Success, status.ResponseRaw.Utf8String());
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

using System;
using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Search.Filter
{
	/// <summary>
	/// Integrated tests of ScriptFilter with elasticsearch.
	/// </summary>
	[TestFixture]
	public class ScriptFilterTests : IntegrationTests
	{
		/// <summary>
		/// Document used in test.
		/// </summary>
		private ElasticsearchProject _LookFor = NestTestData.Data.First();

		/// <summary>
		/// Set of filters that should not filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestScriptFilter()
		{
			this.DoFilterTest(
				f => f.Script(s => s.Script("doc['id'].value == " + _LookFor.Id)),
				_LookFor,
				true);

			this.DoFilterTest(
				f => f.Script(s => s.Script("doc['id'].value == param1").Params(p => p.Add("param1", _LookFor.Id))),
				_LookFor,
				true);

			this.DoFilterTest(
				f => f.Script(s => s.Script("doc['id'].value == param1").Params(p => p.Add("param1", _LookFor.Id)).Lang("mvel")),
				_LookFor,
				true);

			this.DoFilterTest(
				f => f.Script(s => s.Script("doc['id'].value == " + (_LookFor.Id + 1))),
				_LookFor,
				false);

			this.DoFilterTest(
				f => f.Script(s => s.Script("doc['id'].value == param1").Params(p => p.Add("param1", (_LookFor.Id + 1)))),
				_LookFor,
				false);
		}
	}
}

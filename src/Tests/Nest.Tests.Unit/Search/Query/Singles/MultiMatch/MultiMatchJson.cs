using System.Reflection;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles.MultiMatch
{
	[TestFixture]
	public class MultiMatchJson : BaseJsonTests
	{
		[Test]
		public void TestMultiMatchJson()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.MultiMatch(m=>m
						.Name("named_query")
						.OnFields(p=>p.Name, p=>p.Country)
						.Query("this is a query")
						//disabling obsolete message in this test
						#pragma warning disable 0618
						.UseDisMax(true)
						#pragma warning restore 0618
						.TieBreaker(0.7)
					)
				);
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void TestMultiMatchOnFieldsWithBoost()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.MultiMatch(m => m
						.Name("named_query")
						.OnFieldsWithBoost(f=>f.Add("field1", 1.2).Add("field2",.9))
						.Query("this is a query")
					)
				);
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}

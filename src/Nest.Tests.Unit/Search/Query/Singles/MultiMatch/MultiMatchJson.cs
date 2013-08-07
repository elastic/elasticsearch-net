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
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.MultiMatch(m=>m
						.OnFields(p=>p.Name, p=>p.Country)
						.QueryString("this is a query")
						.UseDisMax(true)
						.TieBreaker(0.7)
					)
				);
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}

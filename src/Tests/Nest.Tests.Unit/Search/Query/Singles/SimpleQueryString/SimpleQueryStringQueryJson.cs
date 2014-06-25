using System.Reflection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Query.Singles.SimpleQueryString
{
	[TestFixture]
	public class QueryStringQueryJson : BaseJsonTests
	{
		[Test]
		public void SimpleQueryStringFull()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q=>q
					.SimpleQueryString(qs=>qs
						.Query("\"fried eggs\" +(eggplant | potato) -frittata")
						.OnFieldsWithBoost(d=>d
							.Add(f=>f.Name, 2.0)
							.Add(f=>f.Country, 5.0)
						)
						.Flags("ALL")
						.DefaultOperator(Operator.And)
						.LowercaseExpendedTerms(true)
						.Locale("ROOT")
					)
			);
				
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}

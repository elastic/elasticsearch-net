using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Properties
{
	[TestFixture]
	public class PropertiesTests : BaseJsonTests
	{
		[Test]
		public void TopChildrenQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.TopChildren<Person>(tcq => tcq
					.Query(qq =>
					qq.Term(f => f.FirstName, "foo") || qq.Term(f => f.FirstName, "bar")
					)
				)
			);

			

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

	}
}

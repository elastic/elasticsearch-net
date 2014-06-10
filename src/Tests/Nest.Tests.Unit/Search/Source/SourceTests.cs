using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Source
{
	[TestFixture]
	public class SourceTests : BaseJsonTests
	{
		[Test]
		public void SourceSerializes()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			  .From(0)
			  .Size(10)
			  .Source(source=>source
				.Include(p=>p.Name, p=>p.NestedFollowers)
				.Exclude(p=>p.NestedFollowers.First().DateOfBirth)
			  );
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}
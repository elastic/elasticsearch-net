using System.Reflection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Query.Modes
{
	[TestFixture]
	public class QueryModesTests : BaseJsonTests
	{
		[Test]
		public void StrictThrowsOnEmptyTerm()
		{
			Assert.Throws<DslException>(() =>
			{
				var s = new SearchDescriptor<ElasticsearchProject>()
					.From(0)
					.Size(10)
					.Query(ff => ff.Strict().Term(p => p.Name, ""));
			});
			//this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void VerbatimDoesNotThrowOnEmptyTerm()
		{
			var mi = MethodInfo.GetCurrentMethod();
			Assert.DoesNotThrow(() =>
			{
				var s = new SearchDescriptor<ElasticsearchProject>()
					.From(0)
					.Size(10)
					.Query(ff => ff.Verbatim().Term(p => p.Name, ""));
				
				//make sure empty term is not lost on s
				this.JsonEquals(s, mi);
			});
			
		}
	}
}

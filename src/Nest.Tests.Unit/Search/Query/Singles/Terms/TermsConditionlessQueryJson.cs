using System.Reflection;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles.Terms
{
	[TestFixture]
	public class TermsConditionlessQueryJson : BaseJsonTests
	{
		
		
		[Test]
		public void StrictThrowsOnEmptyTerm()
		{
			Assert.Throws<DslException>(() =>
			{
				var s = new SearchDescriptor<ElasticSearchProject>()
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
				var s = new SearchDescriptor<ElasticSearchProject>()
					.From(0)
					.Size(10)
					.Query(ff => ff.Verbatim().Term(p => p.Name, ""));
				
				this.JsonEquals(s, mi);
			});
			
		}
	}
}

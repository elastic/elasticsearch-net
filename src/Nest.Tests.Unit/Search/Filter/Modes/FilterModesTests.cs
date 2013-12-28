using System.Reflection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Filter.Modes
{
	[TestFixture]
	public class FilterModesTests : BaseJsonTests
	{
		[Test]
		public void StrictThrowsOnEmptyTerm()
		{
			Assert.Throws<DslException>(() =>
			{
				var s = new SearchDescriptor<ElasticSearchProject>()
					.From(0)
					.Size(10)
					.Filter(ff => ff.Strict().Term(p => p.Name, ""));
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
					.Filter(ff => ff.Verbatim().Term(p => p.Name, ""));
				Assert.False(s._Filter.IsConditionless);
				//make sure empty term is not lost on s
				this.JsonEquals(s, mi);
			});

		}

		[Test]
		public void ConditionlessIsStillConditionless()
		{
			var mi = MethodInfo.GetCurrentMethod();
			var s = new SearchDescriptor<ElasticSearchProject>()
					.From(0)
					.Size(10)
					.Filter(ff => ff.Term(p => p.Name, ""));
			Assert.Null(s._Filter);
			
		}

	}
}

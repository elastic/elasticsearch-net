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
				var s = new SearchDescriptor<ElasticsearchProject>()
					.From(0)
					.Size(10)
					.PostFilter(ff => ff.Strict().Term(p => p.Name, ""));
			});
			//this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void VerbatimDoesNotThrowOnEmptyTerm()
		{
			var mi = MethodInfo.GetCurrentMethod();
			Assert.DoesNotThrow(() =>
			{
				var f = Filter<ElasticsearchProject>.Verbatim().Term(p => p.Name, "");
				Assert.False(f.IsConditionless);
				//make sure empty term is not lost on s
				this.JsonEquals(f, mi);
			});

		}

		[Test]
		public void ConditionlessIsStillConditionless()
		{
			var mi = MethodInfo.GetCurrentMethod();
			var f = Filter<ElasticsearchProject>.Term(p => p.Name, "");
			Assert.True(f.IsConditionless);
			
		}

	}
}

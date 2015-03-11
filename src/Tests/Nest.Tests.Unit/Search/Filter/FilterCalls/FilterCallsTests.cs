using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.FilterCalls
{
	[TestFixture]
	public class FilterCallsTests : BaseJsonTests
	{
		[Test]
		public void AndFilterCombines()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			  .From(0)
			  .Take(10)
			  .PostFilter(ff =>
				ff.And(af =>
				  af.Term(f => f.Name, "foo")
				  || af.Term(f => f.Name, "bar")
				));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void AndFilterMultipleCombines()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			  .From(0)
			  .Take(10)
			  .PostFilter(ff =>
				ff.And(af =>
				  af.Term(f => f.Name, "foo")
				  || af.Term(f => f.Name, "bar")
				  , af =>
					 af.Term(f => f.Name, "foo2")
				  || af.Term(f => f.Name, "bar2")
				));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void OrFilterCombines()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			  .From(0)
			  .Take(10)
			  .PostFilter(ff =>
				ff.Or(of =>
				  of.Term(f => f.Name, "foo")
				  && of.Term(f => f.Name, "bar")
				));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void OrFilterMultipleCombines()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			  .From(0)
			  .Take(10)
			  .PostFilter(ff =>
				ff.Or(of =>
				  of.Term(f => f.Name, "foo")
				  && of.Term(f => f.Name, "bar")
				  , of =>
					 of.Term(f => f.Name, "foo2")
				  && of.Term(f => f.Name, "bar2")
				));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void NotFilterCombines()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			  .From(0)
			  .Take(10)
			  .PostFilter(ff =>
				ff.Not(of =>
				  of.Term(f => f.Name, "foo")
				  && of.Term(f => f.Name, "bar")
				));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void CacheSettingsDoNotSurviceBoolOp()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.PostFilter(ff => 
					ff.Name("first_cache_name").Cache(true).CacheKey("first_cache_key").Term(f => f.Name, "foo")
					&& ff.Term(f => f.Name, "bar")
				);
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void CacheSettingsDoNotSurviceBoolOpNew()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.PostFilter(ff =>
					ff.Name("first_cache_name").Cache(true).CacheKey("first_cache_key").Exists(f => f.Name)
					&& ff.Exists(f => f.Content)
				);
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

	}
}

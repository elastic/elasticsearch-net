using System.Reflection;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.BoolFilterMerges
{
	[TestFixture]
	public class BoolFilterMergesTests : BaseJsonTests
	{
		[Test]
		public void ShouldNotJoinIntoStrictShouldFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Filter(q =>
				  q.Strict().Bool(
						bf=>bf.Should(
							qq=>qq.Term(f => f.Name, "foo2"), 
							qq => qq.Term(f => f.Name, "bar2")
						)
					) 
					|| q.Term(f => f.Name, "blah2")
			  );
			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}
		

		[Test]
		public void ShouldJoinIntoNonStrictShouldFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Filter(q =>
				  q.Bool(
						bf => bf.Should(
							qq => qq.Term(f => f.Name, "foo2"),
							qq => qq.Term(f => f.Name, "bar2")
						)
				  )
				|| q.Term(f => f.Name, "blah2")
			  );
			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}


		[Test]
		public void ShouldJoinIntoMustFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Filter(q =>
				  q.Bool(
						bf => bf.Must(
							qq => qq.Term(f => f.Name, "foo2"),
							qq => qq.Term(f => f.Name, "bar2")
						)
					)
					&& q.Term(f => f.Name, "blah2")
			  );
			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}
		[Test]
		public void ShouldJoinMustNotIntoMustFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Filter(q =>
				  q.Bool(
						bf => bf.Must(
							qq => qq.Term(f => f.Name, "foo2"),
							qq => qq.Term(f => f.Name, "bar2")
						)
					)
					&& !q.Term(f => f.Name, "blah2")
			  );
			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}

		[Test]
		public void ShouldNotJoinMustNotIntoMustStrictFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Filter(q =>
				  q.Strict().Bool(
						bf => bf.Must(
							qq => qq.Term(f => f.Name, "foo2"),
							qq => qq.Term(f => f.Name, "bar2")
						)
					)
					&& !q.Term(f => f.Name, "blah2")
			  );
			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}

		[Test]
		public void ShouldNotJoinMustNotIntoMustStrictFilterInverse()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Filter(q =>
				  !q.Term(f => f.Name, "blah2")
				  && q.Strict().Bool(
						bf => bf.Must(
							qq => qq.Term(f => f.Name, "foo2"),
							qq => qq.Term(f => f.Name, "bar2")
						)
					)
			  );
			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}

		[Test]
		public void ShouldNotJoinIntoStrictMustFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Filter(q =>
				  q.Strict().Bool(
						bf => bf.Must(
							qq => qq.Term(f => f.Name, "foo2"),
							qq => qq.Term(f => f.Name, "bar2")
						)
					)
					&& q.Term(f => f.Name, "blah2")
			  );
			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}

	}
}

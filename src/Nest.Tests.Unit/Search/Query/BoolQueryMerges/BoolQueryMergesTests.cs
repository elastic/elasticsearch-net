using System.Reflection;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.BoolQueryMerges
{
	[TestFixture]
	public class BoolQueryMergesTests : BaseJsonTests
	{
		[Test]
		public void ShouldNotJoinIntoStrictShouldQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Query(q =>
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
		public void ShouldNotJoinIntoShouldQueryWithMinimumMatch()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Query(q =>
				  q.Bool(
						bf => bf.Should(
							qq => qq.Term(f => f.Name, "foo2"),
							qq => qq.Term(f => f.Name, "bar2")
						)
						.MinimumNumberShouldMatch(2)
					)
					|| q.Term(f => f.Name, "blah2")
			  );
			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}
		[Test]
		public void ShouldNotJoinIntoShouldQueryWithMinimumMatchPercentage()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Query(q =>
				  q.Bool(
						bf => bf.Should(
							qq => qq.Term(f => f.Name, "foo2"),
							qq => qq.Term(f => f.Name, "bar2")
						)
						.MinimumNumberShouldMatch("50%")
					)
					|| q.Term(f => f.Name, "blah2")
			  );
			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}

		[Test]
		public void ShouldJoinIntoNonStrictShouldQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Query(q =>
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
		public void ShouldJoinIntoMustQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Query(q =>
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
		public void ShouldJoinMustNotIntoMustQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Query(q =>
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
		public void ShouldNotJoinMustNotIntoMustStrictQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Query(q =>
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
		public void ShouldNotJoinMustNotIntoMustStrictQueryInverse()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Query(q =>
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
		public void ShouldNotJoinIntoStrictMustQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .Query(q =>
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

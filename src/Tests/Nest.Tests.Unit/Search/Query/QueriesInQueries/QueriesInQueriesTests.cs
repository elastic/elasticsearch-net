using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.QueriesInQueries
{
	[TestFixture]
	public class QueriesInQueriesTests : BaseJsonTests
	{
		[Test]
		public void NestedQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.Nested(tcq => tcq
					.Query(qq =>
					qq.Term(f => f.Name, "foo") || qq.Term(f => f.Name, "bar")
					)
				)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void IndicesAlternateChildQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.Indices(tcq => tcq
					.Query<Person>(qq =>
					qq.Term(f => f.FirstName, "foo") || qq.Term(f => f.FirstName, "bar")
					)
				)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void IndicesQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.Indices(tcq => tcq
					.Query(qq =>
					qq.Term(f => f.Name, "foo") || qq.Term(f => f.Name, "bar")
					)
				)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void IndicesNoMatchQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.Indices(tcq => tcq
					.NoMatchQuery(qq =>
					qq.Term(f => f.Name, "foo") || qq.Term(f => f.Name, "bar")
					)
				)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void IndicesNoMatchAlternateQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.Indices(tcq => tcq
					.NoMatchQuery<Person>(qq =>
					qq.Term(f => f.FirstName, "foo") || qq.Term(f => f.FirstName, "bar")
					)
				)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void HasChildQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.HasChild<Person>(tcq => tcq
					.Query(qq =>
					qq.Term(f => f.FirstName, "foo") || qq.Term(f => f.FirstName, "bar")
					)
				)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void FilteredQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.Filtered(tcq => tcq
					.Query(qq =>
					qq.Term(f => f.Name, "foo") || qq.Term(f => f.Name, "bar")
					)
				)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void FilteredQueryConditionless()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.Filtered(tcq => tcq
					.Query(qq => qq.Term(f => f.Name, null))
					.Filter(ff=>ff.Term(f=>f.Name, null))
				)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void DismaxQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.Dismax(tcq => tcq
					.Queries(
					qq => qq.Term(f => f.Name, "foo") || qq.Term(f => f.Name, "bar"),
					qq => qq.Term(f => f.Name, "foo2") || qq.Term(f => f.Name, "bar2")
					)
				)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void ConstantScore()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.ConstantScore(tcq => tcq
					.Query(qq =>
					qq.Term(f => f.Name, "foo") || qq.Term(f => f.Name, "bar")
					)
				)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void BoostingQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.Boosting(tcq => tcq
					.Positive(qq => qq.Term(f => f.Name, "foo") || qq.Term(f => f.Name, "bar"))
					.Negative(qq => qq.Term(f => f.Name, "foo2") || qq.Term(f => f.Name, "bar2"))
				)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}



	}
}

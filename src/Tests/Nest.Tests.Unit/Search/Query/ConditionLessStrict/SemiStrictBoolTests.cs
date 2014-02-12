using System;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.ConditionLessStrict
{
	[TestFixture]
	public class SemiStrictBoolTests : BaseJsonTests
	{
		public class Criteria
		{
			public string Name1 { get; set; }
			public string Name2 { get; set; }
			public int? Int1 { get; set; }
			public DateTime? Date { get; set; }
		}
		private readonly Criteria _c = new Criteria();

		private void DoSemiStrictBoolQuery(Func<QueryDescriptor<ElasticsearchProject>, BaseQuery> query)
		{
			Assert.Throws<DslException>(() =>
			{
				var s = new SearchDescriptor<ElasticsearchProject>()
					.From(0)
					.Take(10)
					.Query(query);

				this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod(), "MatchAll");
			});
		}
		private void DoValidSemiStrictBoolQuery(Func<QueryDescriptor<ElasticsearchProject>, BaseQuery> query)
		{
			Assert.DoesNotThrow(() =>
			{
				var s = new SearchDescriptor<ElasticsearchProject>()
					.From(0)
					.Take(10)
					.Query(query);

				this.JsonNotEquals(s, System.Reflection.MethodInfo.GetCurrentMethod(), "MatchAll");
			});
		}
		private void DoValidSemiStrictBoolQueryStatic(BaseQuery query)
		{
			Assert.DoesNotThrow(() =>
			{
				var s = new SearchDescriptor<ElasticsearchProject>()
					.From(0)
					.Take(10)
					.Query(query);

				this.JsonNotEquals(s, System.Reflection.MethodInfo.GetCurrentMethod(), "MatchAll");
			});
		}

		[Test]
		public void OneStrictOneNot()
		{
			this.DoSemiStrictBoolQuery(q => q.Strict().Terms(p => p.Name, new string[] { this._c.Name1 }) && q.Term(p => p.Name, this._c.Name1));
			this.DoSemiStrictBoolQuery(q => q.Terms(p => p.Name, new string[] { this._c.Name1 }) || q.Strict().Term(p => p.Name, this._c.Name1));
		}
		[Test]
		public void BothStrict()
		{
			this.DoSemiStrictBoolQuery(q => q.Strict().Terms(p => p.Name, new string[] { this._c.Name1 }) && q.Strict().Term(p => p.Name, this._c.Name1));
			this.DoSemiStrictBoolQuery(q => q.Strict().Terms(p => p.Name, new string[] { this._c.Name1 }) || q.Strict().Term(p => p.Name, this._c.Name1));
		}
		[Test]
		public void OneStrictOneNotValid()
		{
			this.DoValidSemiStrictBoolQuery(q => q.Strict().Terms(p => p.Name, new string[] { "term" }) && q.Term(p => p.Name, this._c.Name1));
			this.DoValidSemiStrictBoolQuery(q => q.Terms(p => p.Name, new string[] { this._c.Name1 }) || q.Strict().Term(p => p.Name, "test"));
		}
		[Test]
		public void BothStrictValid()
		{
			this.DoValidSemiStrictBoolQuery(q => q.Strict().Terms(p => p.Name, new string[] { "term1" }) && q.Strict().Term(p => p.Name, "term2"));
			this.DoValidSemiStrictBoolQuery(q => q.Strict().Terms(p => p.Name, new string[] { "term1" }) || q.Strict().Term(p => p.Name, "term2"));
		}

		[Test]
		public void BothStrictValidStatic()
		{
			this.DoValidSemiStrictBoolQueryStatic(Query<ElasticsearchProject>.Strict().Terms(p => p.Name, new string[] { "term1" }) && Query<ElasticsearchProject>.Strict().Term(p => p.Name, "term2"));
			this.DoValidSemiStrictBoolQueryStatic(Query<ElasticsearchProject>.Strict().Terms(p => p.Name, new string[] { "term1" }) || Query<ElasticsearchProject>.Strict().Term(p => p.Name, "term2"));
		}
	}
}

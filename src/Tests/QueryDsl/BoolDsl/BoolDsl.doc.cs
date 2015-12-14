using System;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using Tests.QueryDsl.BoolDsl.Operators;
using static Nest.Static;

namespace Tests.QueryDsl.BoolDsl
{
	public class BoolDslTests : OperatorUsageBase
	{
		protected readonly IElasticClient Client = TestClient.GetFixedReturnClient(new { });
		
		/** Writing boolean queries can grow rather verbose rather quickly using the querydsl e.g */

		public void VerboseWay()
		{
			var searchResults = this.Client.Search<Project>(s => s
				.Query(q => q
					.Bool(b => b
						.Should(
							bs => bs.Term(p => p.Name, "x"),
							bs => bs.Term(p => p.Name, "y")
						)
					)
				)
			);
		}
		/** now this is just a single bool with only two clauses, imagine multiple nested bools this quickly becomes an excersise in 
			hadouken indenting
		*
		*[[indent]]
		*.hadouken indenting example
		*image::http://i.imgur.com/BtjZedW.jpg[dead indent]	
		*

		* For this reason NEST introduces operator overloading so complex bool queries become easier to write, the previous example will become. */

		public void UsingOperator()
		{
			var searchResults = this.Client.Search<Project>(s => s
				.Query(q => q.Term(p => p.Name, "x") || q.Term(p => p.Name, "y"))
			);
			/** Or using the object initializer syntax */
			searchResults = this.Client.Search<Project>(new SearchRequest<Project>
			{
				Query = new TermQuery { Field = "name", Value= "x" } 
					|| new TermQuery { Field = Field<Project>(p=>p.Name), Value = "y" }
			});
		}

		/** A naive implementation of operator overloading would rewrite 

		* `term && term && term` to 

		*     bool
		*         must
		*             term
		*             bool
		*                 must
		*                     term
		*                     term

		* As you can image this becomes unwieldy quite fast the more complex a query becomes NEST can spot these and 
		* join them together to become a single bool query

		*     bool
		*         must 
		*             term
		*             term
		*             term
		
		*/

		[U] public void JoinsMustQueries() =>
			Assert(
				q => q.Query() && q.Query() && q.Query(),
				Query && Query && Query,
				c => c.Bool.Must.Should().HaveCount(3)
			);

		/** The bool DSL offers also a short hand notation to mark a query as a must_not using ! */

		[U] public void MustNotOperator() => Assert(q => !q.Query(), !Query, c => c.Bool.MustNot.Should().HaveCount(1));

		/** And to mark a query as a filter using + */

		[U] public void UnaryAddOperator() => Assert(q => +q.Query(), +Query, c => c.Bool.Filter.Should().HaveCount(1));

		/** Both of these can be combined with ands to a single bool query  */

		[U] public void MustNotOperatorAnd() => Assert(q => !q.Query() && !q.Query(), !Query && !Query, c => c.Bool.MustNot.Should().HaveCount(2));
		[U] public void UnaryAddOperatorAnd() => Assert(q => +q.Query() && +q.Query(), +Query && +Query, c => c.Bool.Filter.Should().HaveCount(2));

		/** When combining multiple queries some or all possibly marked as must_not or filter NEST still combines to a single bool query

		*     bool
		*         must 
		*             term
		*             term
		*             term
		*         must_not
		*             term
		*/

		[U] public void JoinsMustWithMustNot()
		{
			Assert(
				q => q.Query() && q.Query() && q.Query() && !q.Query(),
				Query && Query && Query && !Query,
				c=>
				{
					c.Bool.Must.Should().HaveCount(3);
					c.Bool.MustNot.Should().HaveCount(1);
				});
			
		}

		/** Even more involved `term && term && term && !term && +term && +term` still only results in a single bool query:

		*     bool
		*         must 
		*             term
		*             term
		*             term
		*         must_not
		*             term
		*         filter
		*             term
		*             term
		*/

		[U] public void JoinsMustWithMustNotAndFilter() =>
			Assert(
				q => q.Query() && q.Query() && q.Query() && !q.Query() && +q.Query() && +q.Query(),
				Query && Query && Query && !Query && +Query && +Query,
				c=>
				{
					c.Bool.Must.Should().HaveCount(3);
					c.Bool.MustNot.Should().HaveCount(1);
					c.Bool.Filter.Should().HaveCount(2);
				});

		/** You can still mix and match actual bool queries with the bool dsl e.g

		* `bool(must=term, term, term) && !term`

		* it would still merge into a single bool query. */
		
		[U] public void MixAndMatch() =>
			Assert(
				q => q.Bool(b=>b.Must(mq=>mq.Query(),mq=>mq.Query(), mq=>mq.Query())) && !q.Query(),
				new BoolQuery { Must = new QueryContainer[] { Query, Query, Query } } && !Query,
				c=>
				{
					c.Bool.Must.Should().HaveCount(3);
					c.Bool.MustNot.Should().HaveCount(1);
				});

		/* NEST will also do the same with `should`'s or OR's when it sees that the boolean queries in play **ONLY** consist of `should clauses`. 
		* This is because the boolquery does not quite follow the same boolean logic you expect from a programming language. 

		* To summarize the latter: 

		* `term || term || term`

		* becomes

		*     bool
		*         should
		*             term
		*             term
		*             term

		* but 

		* `term1 && (term2 || term3 || term4)` will NOT become

		*     bool
		*         must 
		*             term1
		*         should
		*             term2
		*             term3
		*             term4
		
		* This is because when a bool query has **only** should clauses atleast 1 of them has to match. When that bool query also has a must clause the should clauses start acting as a boost factor
		* and none of them have to match, drastically altering its meaning.

		* So in the previous you could get back results that ONLY contain `term1` this is clearly not what you want in the strict boolean sense of the input.

		* NEST therefor rewrites this query to 

		*     bool 
		*         must 
		*             term1
		*             bool
		*                 should
		*                     term2
		*                     term3
		*                     term4

		*/

		[U] public void JoinsWithShouldClauses() =>
			Assert(
				q => q.Query() && (q.Query() || q.Query() || q.Query()),
				Query && (Query || Query || Query),
				c=>
				{
					c.Bool.Must.Should().HaveCount(2);
					var lastClause = c.Bool.Must.Last() as IQueryContainer;
					lastClause.Should().NotBeNull();
					lastClause.Bool.Should().NotBeNull();
					lastClause.Bool.Should.Should().HaveCount(3);
				});

		/* Note also that you can parenthesis to force evaluation order */
		
		/* Also note that using shoulds as boosting factors can be really powerful so if you need this always remember that you can mix and match an actual bool query with the bool dsl */

		/* There is another subtle situation where NEST will not blindly merge 2 bool queries with only should clauses. Image the following:

		* `bool(should=term1, term2, term3, term4, minimum_should_match=2) || term5 || term6` 
		
		* if NEST identified both sides of the OR operation as only containing should clauses and it would join them together it would give a different meaning to the `minimum_should_match` parameter of the first boolean query. 
		* Rewriting this to a single bool with 5 should clauses would break because only matching on term5 or term6 should still be a hit.
		*/ 
 
		[U] public void MixAndMatchMinimumShouldMatch() =>
			Assert(
				q => q.Bool(b=>b
						.Should(mq=>mq.Query(),mq=>mq.Query(), mq=>mq.Query(), mq=>mq.Query())
						.MinimumShouldMatch(2)
					) 
					|| !q.Query() || q.Query(),
				new BoolQuery
				{
					Should = new QueryContainer[] { Query, Query, Query, Query },
					MinimumShouldMatch = 2
				} || !Query || Query,
				c=>
				{
					c.Bool.Should.Should().HaveCount(3);
					var nestedBool = c.Bool.Should.First() as IQueryContainer;
					nestedBool.Bool.Should.Should().HaveCount(4);
				});

		/* Nest will also not combine if any metadata is set on the bool e.g boost/name nest will treat these as locked */

		[U] public void DoNotCombineLockedBools() =>
			Assert(
				q => q.Bool(b=>b.Name("firstBool").Should(mq=>mq.Query())) 
					|| q.Bool(b=>b.Name("secondBool").Should(mq=>mq.Query())),
				new BoolQuery { Name = "firstBool", Should = new QueryContainer[] { Query } }
				 || new BoolQuery { Name = "secondBool", Should = new QueryContainer[] { Query } },
				c=>
				{
					c.Bool.Should.Should().HaveCount(2);
					var nestedBool = c.Bool.Should.First() as IQueryContainer;
					nestedBool.Bool.Should.Should().HaveCount(1);
					nestedBool.Bool.Name.Should().Be("firstBool");
				});


		private void Assert(
			Func<QueryContainerDescriptor<Project>, QueryContainer> fluent,
			QueryBase ois,
			Action<IQueryContainer> assert
			)
		{
			assert(fluent.InvokeQuery(new QueryContainerDescriptor<Project>()));
			assert((QueryContainer)ois);
		}
		
		private IQueryContainer Create(Func<QueryContainerDescriptor<Project>, QueryContainer> selector) => selector.InvokeQuery(new QueryContainerDescriptor<Project>());
	}
}

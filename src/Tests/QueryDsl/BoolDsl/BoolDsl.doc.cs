using System;
using System.Linq;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using Tests.QueryDsl.BoolDsl.Operators;
using static Nest.Infer;
using static Tests.Framework.RoundTripper;

namespace Tests.QueryDsl.BoolDsl
{
    /**
     * [[bool-queries]]
    * === Writing bool queries
	*/
    public class BoolDslTests : OperatorUsageBase
	{
		protected readonly IElasticClient Client =
            TestClient.GetFixedReturnClient(new { }, modifySettings: c => c.DisableDirectStreaming());

		/**
         * Writing `bool` queries can grow verbose rather quickly when using the query DSL. For example,
		* take a single {ref_current}/query-dsl-bool-query.html[bool query] with two `should` clauses
		*/
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
        /**
         * Now, imagine multiple nested `bool` queries; you'll realise that this quickly becomes an exercise
         * in __hadouken indenting__
		*
		*.hadouken indenting
		*image::hadouken-indentation.jpg[hadouken indenting]
		*
		*[float]
		*=== Operator overloading
		*
		* For this reason, NEST introduces **operator overloading** so complex `bool` queries become easier to write.
        * The overloaded operators are
        *
        * - <<binary-or-operator, Binary `||` operator>>
        * - <<binary-and-operator, Binary `&&` operator>>
        * - <<unary-negation-operator, Unary `!` operator>>
        * - <<unary-plus-operator, Unary `+` operator>>
        *
        * We'll demonstrate each with examples.
        *
        * [[binary-or-operator]]
        * ==== Binary || operator
        *
        * Using the overloaded binary `||` operator, a `bool` query with `should` clauses can be more succinctly
        * expressed.
        *
        * The previous example now becomes the following with the Fluent API
		*/
        public void UsingOperator()
		{
            //hide
		    var client = this.Client;

			var firstSearchResponse = client.Search<Project>(s => s
				.Query(q => q
                    .Term(p => p.Name, "x") || q
                    .Term(p => p.Name, "y")
                )
			);

            /** and, with the the Object Initializer syntax */
            var secondSearchResponse = client.Search<Project>(new SearchRequest<Project>
			{
				Query = new TermQuery { Field = Field<Project>(p => p.Name), Value = "x" } ||
                        new TermQuery { Field = Field<Project>(p => p.Name), Value = "y" }
			});

            /**
             * Both result in the following JSON query DSL
             */
             //json
		    var expected = new
		    {
                query = new
                {
                    @bool = new
                    {
                        should = new object[]
                        {
                            new
                            {
                                term = new
                                {
                                    name = new
                                    {
                                        value = "x"
                                    }
                                }
                            },
                            new
                            {
                                term = new
                                {
                                    name = new
                                    {
                                        value = "y"
                                    }
                                }
                            }
                        }
                    }
                }
		    };

            //hide
		    Expect(expected)
		        .NoRoundTrip()
		        .WhenSerializing(Encoding.UTF8.GetString(firstSearchResponse.ApiCall.RequestBodyInBytes));
            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(secondSearchResponse.ApiCall.RequestBodyInBytes));
        }

        /**[[binary-and-operator]]
         * ==== Binary && operator
         *
         * The overloaded binary `&&` operator can be used to combine queries together. When the queries to be combined
         * don't have any unary operators applied to them, the resulting query is a `bool` query with `must` clauses
         */
        [U]
        public void MustQueries()
        {
            //hide
            var client = this.Client;

            var firstSearchResponse = client.Search<Project>(s => s
                .Query(q => q
                    .Term(p => p.Name, "x") && q
                    .Term(p => p.Name, "y")
                )
            );

            /** and, with the the Object Initializer syntax */
            var secondSearchResponse = client.Search<Project>(new SearchRequest<Project>
            {
                Query = new TermQuery { Field = Field<Project>(p => p.Name), Value = "x" } &&
                        new TermQuery { Field = Field<Project>(p => p.Name), Value = "y" }
            });

            /**
             * Both result in the following JSON query DSL
             */
            //json
            var expected = new
            {
                query = new
                {
                    @bool = new
                    {
                        must = new object[]
                        {
                            new
                            {
                                term = new
                                {
                                    name = new
                                    {
                                        value = "x"
                                    }
                                }
                            },
                            new
                            {
                                term = new
                                {
                                    name = new
                                    {
                                        value = "y"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(firstSearchResponse.ApiCall.RequestBodyInBytes));
            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(secondSearchResponse.ApiCall.RequestBodyInBytes));
        }

        /** A naive implementation of operator overloading would rewrite
		*
        * [source,sh]
        * ----
		* term && term && term
		* ----
        *
        * to
        *
		*....
		*bool
		*|___must
		*    |___term
		*    |___bool
		*        |___must
		*            |___term
		*            |___term
		*....
		*
		* As you can imagine this becomes unwieldy quite fast, the more complex a query becomes. NEST is smart enough
        * to join the `&&` queries together to form a single `bool` query
		*
		*....
		*bool
		*|___must
		*    |___term
		*    |___term
		*    |___term
		*....
        *
        * as demonstrated with the following
		*/

        [U] public void JoinsMustQueries()
		{
			Assert(
				q => q.Query() && q.Query() && q.Query(), // <1> three queries `&&` together using the Fluent API
				Query && Query && Query, // <2> three queries `&&` together using Object Initialzer syntax
				c => c.Bool.Must.Should().HaveCount(3) // <3> assert the resulting `bool` query in each case has 3 `must` clauses
			);
		}


        /**[[unary-negation-operator]]
         * ==== Unary ! operator
         *
         * NEST also offers a shorthand notation for creating a `bool` query with a `must_not` clause
         * using the unary `!` operator
         */
        [U]
        public void MustNotQuery()
        {
            //hide
            var client = this.Client;

            var firstSearchResponse = client.Search<Project>(s => s
                .Query(q => !q
                    .Term(p => p.Name, "x")
                )
            );

            /** and, with the Object Initializer syntax */
            var secondSearchResponse = client.Search<Project>(new SearchRequest<Project>
            {
                Query = !new TermQuery { Field = Field<Project>(p => p.Name), Value = "x" }
            });

            /**
             * Both result in the following JSON query DSL
             */
            //json
            var expected = new
            {
                query = new
                {
                    @bool = new
                    {
                        must_not = new object[]
                        {
                            new
                            {
                                term = new
                                {
                                    name = new
                                    {
                                        value = "x"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(firstSearchResponse.ApiCall.RequestBodyInBytes));
            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(secondSearchResponse.ApiCall.RequestBodyInBytes));
        }

        // hide
        [U] public void MustNotOperator()
		{
			Assert(q => !q.Query(), !Query, c => c.Bool.MustNot.Should().HaveCount(1));
		}

        /**Two queries marked with the unary `!`  operator can be combined with the `&&` operator to form
         * a single `bool` query with two `must_not` clauses
         */
        [U]
        public void MustNotOperatorAnd()
        {
            Assert(
                q => !q.Query() && !q.Query(), // <1> two queries with `!` operator applied, `&&` together using the Fluent API
                !Query && !Query, // <2> two queries with `!` operator applied, `&&` together using the Object Initializer syntax
                c => c.Bool.MustNot.Should().HaveCount(2)); // <3> assert the resulting `bool` query in each case has two `must_not` clauses
        }

        /**[[unary-plus-operator]]
         * ==== Unary + operator
         *
         * A query can be transformed into a `bool` query with a `filter` clause using the unary `+` operator
         */
        [U]
        public void FilterQuery()
        {
            //hide
            var client = this.Client;

            var firstSearchResponse = client.Search<Project>(s => s
                .Query(q => +q
                    .Term(p => p.Name, "x")
                )
            );

            /** and, with the Object Initializer syntax */
            var secondSearchResponse = client.Search<Project>(new SearchRequest<Project>
            {
                Query = +new TermQuery { Field = Field<Project>(p => p.Name), Value = "x" }
            });

            /**
             * Both result in the following JSON query DSL
             */
            //json
            var expected = new
            {
                query = new
                {
                    @bool = new
                    {
                        filter = new object[]
                        {
                            new
                            {
                                term = new
                                {
                                    name = new
                                    {
                                        value = "x"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(firstSearchResponse.ApiCall.RequestBodyInBytes));
            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(secondSearchResponse.ApiCall.RequestBodyInBytes));
        }

        //hide
        [U] public void UnaryAddOperator()
		{
			Assert(q => +q.Query(), +Query, c => c.Bool.Filter.Should().HaveCount(1));
		}

        /**
         * This runs the {ref_current}/query-filter-context.html[query in a filter context],
         * which can be useful in improving performance where the relevancy score for the query
         * is not required to affect the order of results.
         *
         * Similarly to the unary `!` operator, queries marked with the unary `+`  operator can be
         * combined with the `&&` operator to form a single `bool` query with two `filter` clauses
        */
        [U] public void UnaryAddOperatorAnd()
		{
			Assert(
                q => +q.Query() && +q.Query(),
                +Query && +Query,
                c => c.Bool.Filter.Should().HaveCount(2));
		}

		/**[float]
		* === Combining bool queries
		*
		* When combining multiple queries with the binary `&&` operator
        * where some or all queries have unary operators applied,
        * NEST is still able to combine them to form a single `bool` query.
        *
        * Take for example the following `bool` query
		*
		*....
		*bool
		*|___must
		*|   |___term
		*|   |___term
		*|   |___term
		*|
		*|___must_not
		*    |___term
		*....
        *
        * This can be constructed with NEST using
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

		/** An even more complex example
         *
         * [source, sh]
         * ----
         * term && term && term && !term && +term && +term
         * ----
         *
         * still only results in a single `bool` query with the following structure
         *
		 *....
		 *bool
		 *|___must
		 *|   |___term
		 *|   |___term
		 *|   |___term
		 *|
		 *|___must_not
		 *|   |___term
		 *|
		 *|___filter
		 *    |___term
		 *    |___term
		 *....
		 */
		[U] public void JoinsMustWithMustNotAndFilter()
		{
			Assert(
				q => q.Query() && q.Query() && q.Query() && !q.Query() && +q.Query() && +q.Query(),
				Query && Query && Query && !Query && +Query && +Query,
				c =>
				{
					c.Bool.Must.Should().HaveCount(3);
					c.Bool.MustNot.Should().HaveCount(1);
					c.Bool.Filter.Should().HaveCount(2);
				});
		}

		/** You can still mix and match actual `bool` queries with operator overloaded queries e.g
		*
        * [source,sh]
        * ----
        * bool(must=term, term, term) && !term
        * ----
        *
        * This will still merge into a single `bool` query.
		*/
		[U] public void MixAndMatch()
		{
			Assert(
				q => q.Bool(b => b.Must(mq => mq.Query(), mq => mq.Query(), mq => mq.Query())) && !q.Query(),
				new BoolQuery { Must = new QueryContainer[] { Query, Query, Query } } && !Query,
				c =>
				{
					c.Bool.Must.Should().HaveCount(3);
					c.Bool.MustNot.Should().HaveCount(1);
				});
		}

        /**==== Combining queries with || or should clauses
         *
         * As per the previous example, NEST will combine multiple `should` or `||` into a single `bool` query
         * with `should` clauses, when it sees that the `bool` queries in play **only** consist of `should` clauses;
		*
		* To summarize, this
		*
        * [source, sh]
        * ----
		* term || term || term
		* ----
        *
		* becomes
		*....
		*bool
		*|___should
		*    |___term
		*    |___term
		*    |___term
		*....
		*
        * However, the `bool` query does not quite follow the same boolean logic you expect from a
        * programming language. That is
        *
        * [source,sh]
        * ----
		* term1 && (term2 || term3 || term4)
        * ----
        *
        * does **not** become
		*
		* ....
		*bool
		*|___must
		*|   |___term1
		*|
		*|___should
		*    |___term2
		*    |___term3
		*    |___term4
		*....
		*
		* Why is this? Well, when a `bool` query has **only** `should` clauses, **__at least one__** of them must match.
        * However, when that `bool` query also has a `must` clause, the `should` clauses instead now act as a
        * _boost_ factor, meaning none of them have to match but if they do, the relevancy score for that document
        * will be boosted and thus appear higher in the results. The semantics for how `should` clauses behave then
        * changes based on the presence of the `must` clause.
		*
		* So, relating this back to the previous example, you could get back results that **only** contain `term1`.
        * This is clearly not what was intended when using operator overloading.
		*
		* To aid with this, NEST rewrites the previous query as
		*....
		*bool
		*|___must
		*    |___term1
		*    |___bool
		*        |___should
		*            |___term2
		*            |___term3
		*            |___term4
		*....
		*/
        [U] public void JoinsWithShouldClauses()
		{
			Assert(
				q => q.Query() && (q.Query() || q.Query() || q.Query()),
				Query && (Query || Query || Query),
				c =>
				{
					c.Bool.Must.Should().HaveCount(2);
					var lastMustClause = (IQueryContainer)c.Bool.Must.Last();
					lastMustClause.Should().NotBeNull();
					lastMustClause.Bool.Should().NotBeNull();
					lastMustClause.Bool.Should.Should().HaveCount(3);
				});
		}

		/** TIP: *Add parentheses to force evaluation order*
		*
		* Using `should` clauses as boost factors can be a really powerful construct when building
        * search queries, and remember, you can mix and match an actual `bool` query with NEST's operator overloading.
		*
		* There is another subtle situation where NEST will not blindly merge two `bool` queries with only
        * `should` clauses. Consider the following
		*
		* [source,sh]
		* ----
		* bool(should=term1, term2, term3, term4, minimum_should_match=2) || term5 || term6
		* ----
		*
		* if NEST identified both sides of a binary `||` operation as only containing `should` clauses and
		* joined them together, it would give a different meaning to the `minimum_should_match` parameter of
        * the first `bool` query; rewriting this to a single `bool` with 5 `should` clauses would break the semantics
        * of the original query because only matching on `term5` or `term6` should still be a hit.
		**/
		[U]
		public void MixAndMatchMinimumShouldMatch()
		{
			Assert(
				q => q.Bool(b => b
					.Should(mq => mq.Query(), mq => mq.Query(), mq => mq.Query(), mq => mq.Query())
					.MinimumShouldMatch(2)
					)
				     || !q.Query() || q.Query(),
				new BoolQuery
				{
					Should = new QueryContainer[] { Query, Query, Query, Query },
					MinimumShouldMatch = 2
				} || !Query || Query,
				c =>
				{
					c.Bool.Should.Should().HaveCount(3);
					var nestedBool = c.Bool.Should.First() as IQueryContainer;
					nestedBool.Bool.Should.Should().HaveCount(4);
				});
		}

		/**[float]
		* === Locked bool queries
		*
		* NEST will not combine `bool` queries if any of the query metadata is set e.g if metadata such as `boost` or `name` are set,
		* NEST will treat these as locked.
		*
		* Here we demonstrate that two locked `bool` queries are not combined
		*/
		[U] public void DoNotCombineLockedBools()
		{
			Assert(
				q => q.Bool(b => b.Name("leftBool").Should(mq => mq.Query()))
				     || q.Bool(b => b.Name("rightBool").Should(mq => mq.Query())),
				new BoolQuery { Name = "leftBool", Should = new QueryContainer[] { Query } }
				|| new BoolQuery { Name = "rightBool", Should = new QueryContainer[] { Query } },
				c => AssertDoesNotJoinOntoLockedBool(c, "leftBool"));
		}

		/** neither are two `bool` queries where either right query is locked */
		[U] public void DoNotCombineRightLockedBool()
		{
			Assert(
				q => q.Bool(b => b.Should(mq => mq.Query()))
				     || q.Bool(b => b.Name("rightBool").Should(mq => mq.Query())),
				new BoolQuery { Should = new QueryContainer[] { Query } }
				|| new BoolQuery { Name = "rightBool", Should = new QueryContainer[] { Query } },
				c => AssertDoesNotJoinOntoLockedBool(c, "rightBool"));
		}

		/** or the left query is locked */
		[U] public void DoNotCombineLeftLockedBool()
		{
			Assert(
				q => q.Bool(b => b.Name("leftBool").Should(mq => mq.Query()))
				     || q.Bool(b => b.Should(mq => mq.Query())),
				new BoolQuery { Name = "leftBool", Should = new QueryContainer[] { Query } }
				|| new BoolQuery { Should = new QueryContainer[] { Query } },
				c => AssertDoesNotJoinOntoLockedBool(c, "leftBool"));
		}

		//hide
		private static void AssertDoesNotJoinOntoLockedBool(IQueryContainer c, string firstName)
		{
			//hide
			c.Bool.Should.Should().HaveCount(2);
			var nestedBool = c.Bool.Should.Cast<IQueryContainer>().First(b=>!string.IsNullOrEmpty(b.Bool?.Name));
			nestedBool.Bool.Should.Should().HaveCount(1);
			nestedBool.Bool.Name.Should().Be(firstName);
		}

		//hide
		[U] public void AndShouldNotBeViralAnyWayYouComposeIt()
		{
			QueryContainer andAssign = new TermQuery { Field = "a", Value = "a" };
			andAssign &= new TermQuery { Field = "b", Value = "b" };

			AssertAndIsNotViral(andAssign, "&=");

			QueryContainer andOperator =
				new TermQuery { Field = "a", Value = "a" } && new TermQuery { Field = "b", Value = "b" };

			AssertAndIsNotViral(andOperator, "&&");
		}

		//hide
		private static void AssertAndIsNotViral(QueryContainer query, string origin)
		{
			IQueryContainer original = query;
			original.Bool.Must.Should().HaveCount(2, $"query composed using {origin} should have 2 must clauses before");
			IQueryContainer result = query && new TermQuery { Field = "c", Value = "c" };
			result.Bool.Must.Should().HaveCount(3, $"query composed using {origin} combined with && should have 3 must clauses");
			original.Bool.Must.Should().HaveCount(2, $"query composed using {origin} should still have 2 must clauses after composition");
		}

		/**[float]
		* === Perfomance considerations
		*
		* If you have a requirement of combining many many queries using the bool dsl please take the following into account.
		*
		* You *can* use bitwise assignments in a loop to combine many queries into a bigger bool.
		*
		* In this example we are creating a single bool query with a 1000 must clauses using the `&=` assign operator.
		*/
		private static void SlowCombine()
		{
			var c = new QueryContainer();
			var q = new TermQuery { Field = "x", Value = "x" };

			for (var i = 0; i < 1000; i++)
			{
				c &= q;
			}
		}
		/**
		 *....
		 * |     Median|     StdDev|       Gen 0|  Gen 1|  Gen 2|  Bytes Allocated/Op
		 * |  1.8507 ms|  0.1878 ms|    1,793.00|  21.00|      -|        1.872.672,28
		 *....
		 *
		 * As you can see while still fast its causes a lot of allocations to happen because with each iteration
		 * we need to re evaluate the mergability of our bool query.
		 *
		 * Since we already know the shape of our bool query in advance its much much faster to do this instead:
		 *
		 */
		private static void FastCombine()
		{

			QueryContainer q = new TermQuery { Field = "x", Value = "x" };
			var x = Enumerable.Range(0, 1000).Select(f => q).ToArray();
			var boolQuery = new BoolQuery
			{
				Must = x
			};
		}
		/**
		 *....
	 	 * |      Median|     StdDev|   Gen 0|  Gen 1|  Gen 2|  Bytes Allocated/Op
		 * |  31.4610 us|  0.9495 us|  439.00|      -|      -|            7.912,95
		 *....
		 *
		 * The drop both in performance and allocations is tremendous!
		 *
		 * [NOTE]
		 * ====
		 * If you assigning many `bool` queries prior to NEST 2.4.6 into a bigger `bool` query using an assignment loop,
		 * the client did not do a good job of flattening the result in the most optimal way and could
		 * cause a stackoverflow when doing ~2000 iterations. This only applied to bitwise assigning many `bool` queries,
		 * other queries were not affected.
		 *
		 * Since NEST 2.4.6 you can combine as many bool queries as you'd like this way too.
		 * See https://github.com/elastic/elasticsearch-net/pull/2235[PR #2335 on github for more information]
		 * ====
		 */
		private static void Dummy() { }

		//hide
		private void Assert(
			Func<QueryContainerDescriptor<Project>, QueryContainer> fluent,
			QueryBase ois,
			Action<IQueryContainer> assert
			)
		{
			assert(fluent.Invoke(new QueryContainerDescriptor<Project>()));
			assert((QueryContainer)ois);
		}

		//hide
		private IQueryContainer Create(Func<QueryContainerDescriptor<Project>, QueryContainer> selector) => selector.Invoke(new QueryContainerDescriptor<Project>());
	}
}

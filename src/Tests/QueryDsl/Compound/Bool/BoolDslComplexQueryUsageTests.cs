using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Tests.QueryDsl.BoolDsl;

namespace Tests.QueryDsl.Compound.Bool
{
	public class BoolDslComplexQueryUsageTests : BoolQueryUsageTests
	{
		protected static readonly TermQuery Query = new TermQuery { Field = "x", Value = "y" };
		protected static readonly TermQuery NullQuery = null;

		public BoolDslComplexQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object QueryJson => new
		{
			@bool = new
			{
				should = new object[] {
					//first bool
					new {
						@bool = new {
							must = new object[] {
								new { term = new { x = new { value = "y" } } },
								new { term = new { x = new { value = "y" } } }
							}
						}
					},
					new {
						@bool = new {
							must = new object[] {
								new {
									@bool = new {
										must = new object[] {
											new {
												@bool = new {
													//complex nested bool
													should = new object[] {
														new {
															@bool = new {
																filter = new object[] { new { term = new { x = new { value = "y" } } } }
															}
														},
														new {
															@bool = new {
																filter = new object[] { new { term = new { x = new { value = "y" } } } }
															}
														},
														new {
															@bool = new {
																must_not = new object[] {
																	new { term = new { x = new { value = "y" } } },
																	new { term = new { x = new { value = "y" } } }
																}
															}
														}
													}
												}
											},
											//simple nested or
											new {
												@bool = new {
													should = new object[] {
														new { term = new { x = new { value = "y" } } },
														new { term = new { x = new { value = "y" } } },
														new { term = new { x = new { value = "y" } } }
													}
												}
											}
										}
									}
								},
								//actual (locked) locked query
								base.QueryJson,
							}
						}
					}
				}
			}
		};

		protected override QueryContainer QueryInitializer =>
			//first bool
			Query && Query
			//second bool
			|| (
				//complex nested bool
				(+Query || +Query || !Query && (!Query && !ConditionlessQuery))
				// simple nested or
				&& (Query || Query || Query)
				//all conditionless bool
				&& (NullQuery || +ConditionlessQuery || !ConditionlessQuery)
				// actual bool query
				&& (base.QueryInitializer));

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) =>
			//first bool
			q.Query() && q.Query()
			//second bool
			|| (
				//complex nested bool
				(+q.Query() || +q.Query() || !q.Query() && (!q.Query() && !q.ConditionlessQuery()))
				// simple nested or
				&& (q.Query() || q.Query() || q.Query())
				//all conditionless bool
				&& (q.NullQuery() || +q.ConditionlessQuery() || !q.ConditionlessQuery())
				// actual bool query
				&& (base.QueryFluent(q)));

		//hide
		[U]
		protected void AsssertShape()
		{
			this.AssertShape(this.QueryInitializer);
			//this.AssertShape(this.QueryFluent(new QueryContainerDescriptor<Project>()));
		}

		//hide
		private void AssertShape(IQueryContainer container)
		{
			//top level bool
			container.Bool.Should().NotBeNull();
			container.Bool.Should.Should().HaveCount(2);
			container.Bool.MustNot.Should().BeNull();
			container.Bool.Filter.Should().BeNull();
			container.Bool.Must.Should().BeNull();

			//first bool
			var firstBool = (container.Bool.Should.First() as IQueryContainer)?.Bool;
			firstBool.Should().NotBeNull();
			firstBool.Must.Should().HaveCount(2);
			firstBool.MustNot.Should().BeNull();
			firstBool.Filter.Should().BeNull();
			firstBool.Should.Should().BeNull();

			//second bool
			var secondBool = (container.Bool.Should.Last() as IQueryContainer)?.Bool;
			secondBool.Should().NotBeNull();
			secondBool.Must.Should().HaveCount(2); //the last bool query was all conditionless
			secondBool.MustNot.Should().BeNull();
			secondBool.Filter.Should().BeNull();
			secondBool.Should.Should().BeNull();

			//complex nested bool
			var complexBool = (secondBool.Must.First() as IQueryContainer)?.Bool;
			complexBool.Should().NotBeNull();
			//complex bool is 3 ors and the next simple nested or bool query also has 3 should clauses
			complexBool.Must.Should().HaveCount(2);

			var complexNestedBool = (complexBool.Must.First() as IQueryContainer)?.Bool;
			complexNestedBool.Should().NotBeNull();
			complexNestedBool.Should.Should().HaveCount(3);

			//inner must nots
			var mustNotsBool = (complexNestedBool.Should.Cast<IQueryContainer>().FirstOrDefault(q => q.Bool != null && q.Bool.MustNot != null))?.Bool;
			mustNotsBool.Should().NotBeNull();
			mustNotsBool.MustNot.Should().HaveCount(2); //one of the three must nots was conditionless
		}

	}
}

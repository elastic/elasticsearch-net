using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.QueryDsl.FullText.Intervals
{
	/**
	 * An intervals query allows fine-grained control over the order and proximity of matching terms.
	 * Matching rules are constructed from a small set of definitions, and the rules are then applied to terms from a particular field.
	 *
	 * The definitions produce sequences of minimal intervals that span terms in a body of text. These intervals can be further combined and filtered by parent sources.
	 *
	 * NOTE: Only available in Elasticsearch 6.7.0+
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/query-dsl-intervals-query.html[Intervals query]
	 */
	public class IntervalsUsageTests : QueryDslUsageTestsBase
	{
		public IntervalsUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IIntervalsQuery>(a => a.Intervals)
		{
			q => q.Field = null,
			q => q.AnyOf = null
		};

		protected override QueryContainer QueryInitializer => new IntervalsQuery
		{
			Field = Field<Project>(p => p.Description),
			Name = "named_query",
			Boost = 1.1,
			AnyOf = new IntervalsAnyOf
			{
				Intervals = new IntervalsContainer[]
				{
					new IntervalsMatch
					{
						Query = "my favourite food",
						MaxGaps = 5,
						Ordered = true,
						Filter = new IntervalsFilter
						{
							Containing = new IntervalsMatch
							{
								Query = "kimchy"
							}
						}
					},
					new IntervalsAllOf
					{
						Intervals = new IntervalsContainer[]
						{
							new IntervalsMatch
							{
								Query = "hot water",
							},
							new IntervalsMatch
							{
								Query = "cold porridge",
							},
						},
						Filter = new IntervalsFilter
						{
							Script = new InlineScript("interval.start > 0 && interval.end < 200")
						}
					}
				}
			}
		};

		protected override object QueryJson => new
		{
			intervals = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					any_of = new
					{
						intervals = new object[]
						{
							new
							{
								match = new
								{
									query = "my favourite food",
									max_gaps = 5,
									ordered = true,
									filter = new
									{
										containing = new
										{
											match = new
											{
												query = "kimchy"
											}
										}
									}
								}
							},
							new
							{
								all_of = new
								{
									intervals = new object[]
									{
										new
										{
											match = new
											{
												query = "hot water"
											}
										},
										new
										{
											match = new
											{
												query = "cold porridge"
											}
										},
									},
									filter = new
									{
										script = new
										{
											source = "interval.start > 0 && interval.end < 200"
										}
									}
								}
							}
						}
					}

				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Intervals(c => c
				.Field(p => p.Description)
				.Name("named_query")
				.Boost(1.1)
				.AnyOf(any => any
					.Intervals(i => i
						.Match(m => m
							.Query("my favourite food")
							.MaxGaps(5)
							.Ordered()
							.Filter(f => f
								.Containing(co => co
									.Match(mm => mm
										.Query("kimchy")
									)
								)
							)
						)
						.AllOf(all => all
							.Intervals(ii => ii
								.Match(m => m
									.Query("hot water")
								)
								.Match(m => m
									.Query("cold porridge")
								)
							)
							.Filter(f => f
								.Script(s => s
									.Source("interval.start > 0 && interval.end < 200")
								)
							)
						)
					)
				)
			);
	}
}

using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.QueryDsl.Verbatim
{
	/**[[verbatim-and-strict-query-usage]]
	 * == Verbatim and Strict Query Usage
	 * NEST has the concept of conditionless queries; if the input to a query is determined to be __empty__, for example,
	 * `null` or `""` for a string input, then the query will not be serialized and sent to Elasticsearch. If a conditionless
	 * query is part of a compound query then the query will not be part of the json query dsl sent to Elasticsearch.
	 *
	 * Conditionless behavior can be controlled on individual queries by using Strict and Verbatim queries
	 *
	 * Strict:: Individual queries can be marked as strict meaning that if they are conditionless, an exception is thrown.
	 * This is useful for when a query must have an input value.
	 *
	 * Verbatim:: Individual queries can be marked as verbatim meaning that the query should be sent to Elasticsearch **as is**,
	 * even if it is conditionless.
	 *
	 * [float]
	 * == Verbatim Usage
	 *
	 * `IsVerbatim` should be set on individual queries to take effect
	 */
	public class CompoundVerbatimQueryUsageTests : QueryDslUsageTestsBase
	{
		protected override bool SupportsDeserialization => false;

		public CompoundVerbatimQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object QueryJson => new
		{
			@bool = new
			{
				must = new object[]
				{
					new
					{
						term = new
						{
							description = new
							{
								value = ""
							}
						}
					},
					new
					{
						term = new
						{
							name = new
							{
								value = "foo"
							}
						}
					}
				}
			}
		};

		protected override QueryContainer QueryInitializer =>
			new TermQuery
			{
				IsVerbatim = true,
				Field = "description",
				Value = ""
			}
			&& new TermQuery
			{
				Field = "name",
				Value = "foo"
			};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Bool(b => b
				.Must(qt => qt
					.Term(t => t
						.Verbatim()
						.Field(p => p.Description)
						.Value("")
					), qt => qt
					.Term(t => t
						.Field(p => p.Name)
						.Value("foo")
					)
				)
			);
	}

	/**[float]
	 * == Non-Cascading Strict Outer Queries
	 * Setting `IsStrict` on the outer query container does not cascade
	 */
	public class QueryContainerStrictQueryUsageTests : QueryDslUsageTestsBase
	{
		protected override bool SupportsDeserialization => false;

		public QueryContainerStrictQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object QueryJson => new
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
								value = "foo"
							}
						}
					}
				}
			}
		};

		protected override QueryContainer QueryInitializer
		{
			get
			{
				IQueryContainer query = new QueryContainer(new BoolQuery
				{
					Must = new List<QueryContainer>
					{
						new TermQuery
						{
							Field = "description",
							Value = ""
						},
						new TermQuery
						{
							Field = "name",
							Value = "foo"
						}
					}
				});

				query.IsStrict = true;
				return (QueryContainer)query;
			}
		}

#pragma warning disable 618
		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Strict()
			.Bool(b => b
				.Must(qt => qt
					.Term(t => t
						.Field(p => p.Description)
						.Value("")
					), qt => qt
					.Term(t => t
						.Field(p => p.Name)
						.Value("foo")
					)
				)
			);
#pragma warning restore 618
	}

	/**[float]
	 * == Non-Cascading Verbatim Outer Queries
	 * Setting `IsVerbatim` on the outer query container does not cascade
	 */
	public class QueryContainerVerbatimQueryUsageTests : QueryDslUsageTestsBase
	{
		protected override bool SupportsDeserialization => false;

		public QueryContainerVerbatimQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object QueryJson => new
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
								value = "foo"
							}
						}
					}
				}
			}
		};

		protected override QueryContainer QueryInitializer
		{
			get
			{
				IQueryContainer query = new QueryContainer(new BoolQuery
				{
					Must = new List<QueryContainer>
					{
						new TermQuery
						{
							Field = "description",
							Value = ""
						},
						new TermQuery
						{
							Field = "name",
							Value = "foo"
						}
					}
				});

				query.IsVerbatim = true;
				return (QueryContainer)query;
			}
		}

#pragma warning disable 618
		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Verbatim()
			.Bool(b => b
				.Must(qt => qt
					.Term(t => t
						.Field(p => p.Description)
						.Value("")
					), qt => qt
					.Term(t => t
						.Field(p => p.Name)
						.Value("foo")
					)
				)
			);
#pragma warning restore 618
	}

	/**[float]
	 * == Verbatim Single Queries
	 * Setting `IsVerbatim` on a single query is still supported though
	 */
	public class QueryContainerVerbatimSupportedUsageTests : QueryDslUsageTestsBase
	{
		protected override bool SupportsDeserialization => false;

		public QueryContainerVerbatimSupportedUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object QueryJson => new
		{
			@bool = new
			{
			}
		};

		protected override QueryContainer QueryInitializer => new BoolQuery
		{
			IsVerbatim = true,
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Bool(b => b
				.Verbatim()
			);
	}

	public class SingleVerbatimQueryUsageTests : QueryDslUsageTestsBase
	{
		public SingleVerbatimQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool SupportsDeserialization => false;

		protected override object QueryJson => new
		{
			term = new
			{
				description = new
				{
					value = ""
				}
			}

		};

		protected override QueryContainer QueryInitializer => new TermQuery
		{
			IsVerbatim = true,
			Field = "description",
			Value = ""
		};


		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Term(t => t
				.Verbatim()
				.Field(p => p.Description)
				.Value("")
			);
	}

	/**[float]
	 * == Verbatim Compound Queries
	 * Similarly to verbatim single queries, setting `IsVerbatim` on a single query that is part
	 * of a compound query is also supported
	 */
	public class CompoundVerbatimInnerQueryUsageTests : QueryDslUsageTestsBase
	{
		public CompoundVerbatimInnerQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool SupportsDeserialization => false;

		protected override object QueryJson => new
		{
			@bool = new
			{
				filter = new []
				{
					new
					{
						@bool = new
						{
							must = new []
							{
								new
								{
									exists = new
									{
										field = "numberOfCommits"
									}
								}
							},
							must_not = new []
							{
								new
								{
									term = new
									{
										name = new
										{
											value = ""
										}
									}
								}
							}
						}
					}
				}
			}
		};



		protected override QueryContainer QueryInitializer => new BoolQuery
		{
			Filter = new QueryContainer[] {
				!new TermQuery
				{
					IsVerbatim = true,
					Field = "name",
					Value = ""
				} &&
				new ExistsQuery
				{
					Field = "numberOfCommits"
				}
			}
		};


		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Bool(b => b
				.Filter(f => !f
					.Term(t => t
						.Verbatim()
						.Field(p => p.Name)
						.Value("")
					) && f
					.Exists(e => e
						.Field(p => p.NumberOfCommits)
					)
				)
			);
	}

	public class StrictQueryUsageTests
	{
		[U]
		public void FluentThrows()
		{
			//hide
			var e = Assert.Throws<ArgumentException>(() =>
				new SearchDescriptor<Project>()
					.Query(q => q
						.Term(t => t
							.Strict()
							.Field("myfield")
							.Value("")
						)
					)
			);
			//hide
			e.Message.Should().Be("Query is conditionless but strict is turned on");
		}

		[U]
		public void InitializerThrows()
		{
			//hide
			var e = Assert.Throws<ArgumentException>(() =>
				new SearchRequest<Project>
				{
					Query = new TermQuery
					{
						IsStrict = true,
						Field = "myfield",
						Value = ""
					}
				}
			);
			//hide
			e.Message.Should().Be("Query is conditionless but strict is turned on");
		}
	}
}

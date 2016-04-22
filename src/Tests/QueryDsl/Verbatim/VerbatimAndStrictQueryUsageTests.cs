using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.QueryDsl.Verbatim
{
	/** `IsVerbatim` should be set on individual queries to take effect */
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

	/** Setting `IsStrict` on the outer query container does not cascade */
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

	/** Setting `IsVerbatim` on the outer query container does not cascade */
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

	/** Setting `IsVerbatim` on a compound query is still supported though */
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

	public class StrictQueryUsageTests
	{
		[U]
		public void FluentThrows()
		{
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
			e.Message.Should().Be("Query is conditionless but strict is turned on");
		}

		[U]
		public void InitializerThrows()
		{
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
			e.Message.Should().Be("Query is conditionless but strict is turned on");
		}
	}
}

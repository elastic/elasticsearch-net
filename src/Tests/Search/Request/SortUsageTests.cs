using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Search.Request
{
	/**
	 * Allows to add one or more sort on specific fields. Each sort can be reversed as well.
	 * The sort is defined on a per field level, with special field name for `_score` to sort by score.
	 */
	public class SortUsageTests : SearchUsageTestBase
	{
		public SortUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override object ExpectJson =>
			new
			{
				sort = new object[] {
					new { startedOn = new { order = "asc" } },
					new { name = new { order = "desc" } },
					new { _score = new { order = "desc" } },
					new { _doc = new { order = "asc" } },
					new Dictionary<string, object>
					{
						{
							"tags.added", new
							{
								missing = "_last",
								order = "desc",
								mode = "avg",
								nested_path = "tags",
								nested_filter = new
								{
									match_all = new { }
								},
								unmapped_type = "date"
							}
						}
					},
					new
					{
						numberOfCommits = new
						{
							missing = -1,
							order = "desc"
						}
					},
					new
					{
						_geo_distance = new
						{
							location = new[]
							{
								new
								{
									lat = 70.0,
									lon = -70.0
								},
								new
								{
									lat = -12.0,
									lon = 12.0
								}
							},
							order = "asc",
							mode = "min",
							distance_type = "arc",
							unit = "cm"
						}
					},
					new
					{
						_script = new
						{
							order = "asc",
							type = "number",
							script = new
							{
								@params = new
								{
									factor = 1.1
								},
								source = "doc['numberOfCommits'].value * params.factor",
							}
						}
					}
				}
			};

#pragma warning disable 618 // uses NestedPath and NestedFilter
		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Sort(ss => ss
				.Ascending(p => p.StartedOn)
				.Descending(p => p.Name)
				.Descending(SortSpecialField.Score)
				.Ascending(SortSpecialField.DocumentIndexOrder)

				.Field(f => f
					.Field(p => p.Tags.First().Added)
					.Order(SortOrder.Descending)
					.MissingLast()
					.UnmappedType(FieldType.Date)
					.Mode(SortMode.Average)
					.NestedPath(p => p.Tags)
					.NestedFilter(q => q.MatchAll())
				)
				.Field(f => f
					.Field(p => p.NumberOfCommits)
					.Order(SortOrder.Descending)
					.Missing(-1)
				)
				.GeoDistance(g => g
					.Field(p => p.Location)
					.DistanceType(GeoDistanceType.Arc)
					.Order(SortOrder.Ascending)
					.Unit(DistanceUnit.Centimeters)
					.Mode(SortMode.Min)
					.Points(new GeoLocation(70, -70), new GeoLocation(-12, 12))
				)
				.Script(sc => sc
					.Type("number")
					.Ascending()
					.Script(script => script
						.Source("doc['numberOfCommits'].value * params.factor")
						.Params(p => p.Add("factor", 1.1))
					)
				)
			);
#pragma warning restore 618

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Sort = new List<ISort>
				{
					new SortField { Field = "startedOn", Order = SortOrder.Ascending },
					new SortField { Field = "name", Order = SortOrder.Descending },
					new SortField { Field = "_score", Order = SortOrder.Descending },
					new SortField { Field = "_doc", Order = SortOrder.Ascending },
					new SortField
					{
						Field = Field<Project>(p=>p.Tags.First().Added),
						Order = SortOrder.Descending,
						Missing = "_last",
						UnmappedType = FieldType.Date,
						Mode = SortMode.Average,
#pragma warning disable 618
						NestedPath = Field<Project>(p=>p.Tags),
						NestedFilter = new MatchAllQuery(),
#pragma warning restore 618
					},
					new SortField
					{
						Field = Field<Project>(p => p.NumberOfCommits),
						Order = SortOrder.Descending,
						Missing = -1
					},
					new GeoDistanceSort
					{
						Field = "location",
						Order = SortOrder.Ascending,
						DistanceType = GeoDistanceType.Arc,
						GeoUnit = DistanceUnit.Centimeters,
						Mode = SortMode.Min,
						Points = new[] {new GeoLocation(70, -70), new GeoLocation(-12, 12)}
					},
					new ScriptSort
					{
						Type = "number",
						Order = SortOrder.Ascending,
						Script = new InlineScript("doc['numberOfCommits'].value * params.factor")
						{
							Params = new Dictionary<string, object>
							{
								{"factor", 1.1}
							}
						}
					}
				}
			};
	}

	/**
	 * [float]
	 * === Nested sort usage
	 *
	 * In Elasticsearch 6.1.0+, using `nested_path` and `nested_filter` for sorting on fields mapped as
	 * `nested` types is deprecated. Instead, you should use the `nested` sort instead.
	 */
	[SkipVersion("<6.1.0", "Only available in Elasticsearch 6.1.0+")]
	public class NestedSortUsageTests : SearchUsageTestBase
	{
		public NestedSortUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override object ExpectJson =>
			new
			{
				sort = new object[]
				{
					new Dictionary<string, object>
					{
						{
							"tags.added", new
							{
								missing = "_last",
								order = "desc",
								mode = "avg",
								nested = new
								{
									path = "tags",
									filter = new
									{
										match_all = new { }
									}
								},
								unmapped_type = "date"
							}
						}
					}
				}
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Sort(ss => ss
				.Field(f => f
					.Field(p => p.Tags.First().Added)
					.Order(SortOrder.Descending)
					.MissingLast()
					.UnmappedType(FieldType.Date)
					.Mode(SortMode.Average)
					.Nested(n => n
						.Path(p => p.Tags)
						.Filter(ff => ff
							.MatchAll()
						)
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Sort = new List<ISort>
				{
					new SortField
					{
						Field = Field<Project>(p => p.Tags.First().Added),
						Order = SortOrder.Descending,
						Missing = "_last",
						UnmappedType = FieldType.Date,
						Mode = SortMode.Average,
						Nested = new NestedSort
						{
							Path = Field<Project>(p => p.Tags),
							Filter = new MatchAllQuery()
						}
					}
				}
			};
	}
}

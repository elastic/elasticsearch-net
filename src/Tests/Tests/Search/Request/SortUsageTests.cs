using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Search.Request
{
	/**
	 * Allows to add one or more sort on specific fields. Each sort can be reversed as well.
	 * The sort is defined on a per field level, with special field name for `_score` to sort by score.
	 */
	public class SortUsageTests : SearchUsageTestBase
	{
		public SortUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new
			{
				sort = new object[]
				{
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
							locationPoint = new[]
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
						_geo_distance = new
						{
							locationPoint = new[]
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
							}
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
					.Nested(n => n
						.Path(p => p.Tags)
						.Filter(q => q.MatchAll())
					)
				)
				.Field(f => f
					.Field(p => p.NumberOfCommits)
					.Order(SortOrder.Descending)
					.Missing(-1)
				)
				.GeoDistance(g => g
					.Field(p => p.LocationPoint)
					.DistanceType(GeoDistanceType.Arc)
					.Order(SortOrder.Ascending)
					.Unit(DistanceUnit.Centimeters)
					.Mode(SortMode.Min)
					.Points(new GeoLocation(70, -70), new GeoLocation(-12, 12))
				)
				.GeoDistance(g => g
					.Field(p => p.LocationPoint)
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
					new FieldSort { Field = "startedOn", Order = SortOrder.Ascending },
					new FieldSort { Field = "name", Order = SortOrder.Descending },
					new FieldSort { Field = "_score", Order = SortOrder.Descending },
					new FieldSort { Field = "_doc", Order = SortOrder.Ascending },
					new FieldSort
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
					},
					new FieldSort
					{
						Field = Field<Project>(p => p.NumberOfCommits),
						Order = SortOrder.Descending,
						Missing = -1
					},
					new GeoDistanceSort
					{
						Field = "locationPoint",
						Order = SortOrder.Ascending,
						DistanceType = GeoDistanceType.Arc,
						Unit = DistanceUnit.Centimeters,
						Mode = SortMode.Min,
						Points = new[] { new GeoLocation(70, -70), new GeoLocation(-12, 12) }
					},
					new GeoDistanceSort
					{
						Field = "locationPoint",
						Points = new[] { new GeoLocation(70, -70), new GeoLocation(-12, 12) }
					},
					new ScriptSort
					{
						Type = "number",
						Order = SortOrder.Ascending,
						Script = new InlineScript("doc['numberOfCommits'].value * params.factor")
						{
							Params = new Dictionary<string, object>
							{
								{ "factor", 1.1 }
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
		public NestedSortUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
					new FieldSort
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

	//hide
	[SkipVersion("<6.4.0", "IgnoreUnmapped introduced in 6.4.0 on geo distance sort")]
	public class GeoDistanceIgnoreUnmappedUsageTests : SearchUsageTestBase
	{
		public GeoDistanceIgnoreUnmappedUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new
			{
				sort = new object[]
				{
					new
					{
						_geo_distance = new
						{
							locationPoint = new[]
							{
								new { lat = 70.0, lon = -70.0 },
								new { lat = -12.0, lon = 12.0 }
							},
							order = "asc",
							mode = "min",
							distance_type = "arc",
							ignore_unmapped = true,
							unit = "cm"
						}
					}
				}
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Sort(ss => ss
				.GeoDistance(g => g
					.Field(p => p.LocationPoint)
					.IgnoreUnmapped()
					.DistanceType(GeoDistanceType.Arc)
					.Order(SortOrder.Ascending)
					.Unit(DistanceUnit.Centimeters)
					.Mode(SortMode.Min)
					.Points(new GeoLocation(70, -70), new GeoLocation(-12, 12))
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Sort = new List<ISort>
				{
					new GeoDistanceSort
					{
						Field = "locationPoint",
						IgnoreUnmapped = true,
						Order = SortOrder.Ascending,
						DistanceType = GeoDistanceType.Arc,
						Unit = DistanceUnit.Centimeters,
						Mode = SortMode.Min,
						Points = new[] { new GeoLocation(70, -70), new GeoLocation(-12, 12) }
					},
				}
			};
	}

	//hide
	[SkipVersion("<7.2.0", "numeric_type added in 7.2.0")]
	public class NumericTypeUsageTests : SearchUsageTestBase
	{
		public NumericTypeUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new
			{
				sort = new object[]
				{
					new { startedOn = new { numeric_type = "date", order = "asc" } }
				}
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Sort(ss => ss
				.Field(g => g
					.Field(p => p.StartedOn)
					.NumericType(NumericType.Date)
					.Ascending()
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Sort = new List<ISort>
				{
					new FieldSort { Field = "startedOn", NumericType = NumericType.Date, Order = SortOrder.Ascending },
				}
			};
	}
}

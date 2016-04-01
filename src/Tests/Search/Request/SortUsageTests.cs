using System;
using System.Collections.Generic;
using Nest;
using Tests.Framework.Integration;
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
		public SortUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new
			{
				sort = new object[] {
					new { startedOn = new { order = "asc" } },
					new { name = new { order = "desc" } },
					new { _score = new { order = "desc" } },
					new { _doc = new { order = "asc" } },
					new {
						lastActivity = new {
							missing = "_last",
							order = "desc",
							mode = "avg",
							nested_filter = new {
							  match_all = new {}
							},
							nested_path = "tags",
							unmapped_type = "date"
						}
					},
					new {
						_geo_distance = new {
							location = new [] {
								new {
									lat = 70.0,
									lon = -70.0
								},
								new {
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
					new {
						_script = new {
							order = "asc",
							type = "number",
							script = new {
								@params = new {
									factor = 1.1
								},
								inline = "doc['numberOfCommits'].value * factor"
							}
						}
					}
				}
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Sort(ss => ss
				.Ascending(p => p.StartedOn)
				.Descending(p => p.Name)
				.Descending(SortSpecialField.Score)
				.Ascending(SortSpecialField.DocumentIndexOrder)
				.Field(f => f
					.Field(p => p.LastActivity)
					.Order(SortOrder.Descending)
					.MissingLast()
					.UnmappedType(FieldType.Date)
					.Mode(SortMode.Average)
					.NestedPath(p => p.Tags)
					.NestedFilter(q => q.MatchAll())
				)
				.GeoDistance(g => g
					.Field(p => p.Location)
					.DistanceType(GeoDistanceType.Arc)
					.Order(SortOrder.Ascending)
					.Unit(DistanceUnit.Centimeters)
					.Mode(SortMode.Min)
					.PinTo(new GeoLocation(70, -70), new GeoLocation(-12, 12))
				)
				.Script(sc => sc
					.Type("number")
					.Ascending()
					.Script(script => script
						.Inline("doc['numberOfCommits'].value * factor")
						.Params(p => p.Add("factor", 1.1))
					)
				)
			);

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
						Field = Field<Project>(p=>p.LastActivity),
						Order = SortOrder.Descending,
						Missing = "_last",
						UnmappedType = FieldType.Date,
						Mode = SortMode.Average,
						NestedPath = Field<Project>(p=>p.Tags),
						NestedFilter = new MatchAllQuery(),
					},
					new GeoDistanceSort
					{
						Field = "location",
						Order = SortOrder.Ascending,
						DistanceType = GeoDistanceType.Arc,
						GeoUnit = DistanceUnit.Centimeters,
						Mode = SortMode.Min,
						Points = new [] {new GeoLocation(70, -70), new GeoLocation(-12, 12) }
					},
					new ScriptSort
					{
						Type = "number",
						Order = SortOrder.Ascending,
						Script =  new InlineScript("doc['numberOfCommits'].value * factor")
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
}

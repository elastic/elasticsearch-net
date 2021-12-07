// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Clients.Elasticsearch.Infer;

namespace Tests.Search.Search;

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
								unmapped_type = "date",
								ignore_unmapped = true
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
				//new
				//{
				//	_geo_distance = new
				//	{
				//		locationPoint = new[]
				//		{
				//			new
				//			{
				//				lat = 70.0,
				//				lon = -70.0
				//			},
				//			new
				//			{
				//				lat = -12.0,
				//				lon = 12.0
				//			}
				//		},
				//		order = "asc",
				//		mode = "min",
				//		distance_type = "arc",
				//		unit = "cm"
				//	}
				//},
				//new
				//{
				//	_geo_distance = new
				//	{
				//		locationPoint = new[]
				//		{
				//			new
				//			{
				//				lat = 70.0,
				//				lon = -70.0
				//			},
				//			new
				//			{
				//				lat = -12.0,
				//				lon = 12.0
				//			}
				//		}
				//	}
				//},
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

	protected override Action<SearchRequestDescriptor<Project>> Fluent => s => s
		.Sort(ss => ss
			.Ascending(p => p.StartedOn)
			.Descending(p => p.Name)
			.Descending(SortSpecialField.Score)
			.Ascending(SortSpecialField.DocumentIndexOrder)
			.Field(f => f
				.Field(p => p.Tags.First().Added)
				.Order(SortOrder.Desc)
				.MissingLast()
				.UnmappedType(FieldType.Date)
				.Mode(SortMode.Avg)
				.IgnoreUnmappedFields()
				.Nested(n => n
					.Path(p => p.Tags)
					.Filter(q => q.MatchAll())
				)
			)
			.Field(f => f
				.Field(p => p.NumberOfCommits)
				.Order(SortOrder.Desc)
				.Missing(-1)
			)
			//.GeoDistance(g => g
			//	.Field(p => p.LocationPoint)
			//	.DistanceType(GeoDistanceType.Arc)
			//	.Order(SortOrder.Ascending)
			//	.Unit(DistanceUnit.Centimeters)
			//	.Mode(SortMode.Min)
			//	.Points(new GeoLocation(70, -70), new GeoLocation(-12, 12))
			//)
			//.GeoDistance(g => g
			//	.Field(p => p.LocationPoint)
			//	.Points(new GeoLocation(70, -70), new GeoLocation(-12, 12))
			//)
			.Script(sc => sc
				.Type(ScriptSortType.Number)
				.Ascending()
				.Script(script => script
					.Source("doc['numberOfCommits'].value * params.factor")
					.Params(p => p.Add("factor", 1.1))
				)
			)
		);

	protected override SearchRequest<Project> Initializer =>
		new()
		{
			Sort = new SortCollection
			{
				new FieldSort("startedOn") { Order = SortOrder.Asc },
				new FieldSort { Field = "name", Order = SortOrder.Desc },
				new FieldSort { Field = "_score", Order = SortOrder.Desc },
				new FieldSort { Field = "_doc", Order = SortOrder.Asc },
				new FieldSort
				{
					Field = Field<Project>(p => p.Tags.First().Added),
					Order = SortOrder.Desc,
					Missing = "_last",
					UnmappedType = FieldType.Date,
					Mode = SortMode.Avg,
					Nested = new NestedSort
					{
						Path = Field<Project>(p => p.Tags),
						Filter = new MatchAllQuery()
					},
					IgnoreUnmapped = true
				},
				new FieldSort
				{
					Field = Field<Project>(p => p.NumberOfCommits),
					Order = SortOrder.Desc,
					Missing = -1
				},
				//new GeoDistanceSort
				//{
				//	Field = "locationPoint",
				//	Order = SortOrder.Asc,
				//	DistanceType = GeoDistanceType.Arc,
				//	Unit = DistanceUnit.Centimeters,
				//	Mode = SortMode.Min,
				//	Points = new[] { new GeoLocation(70, -70), new GeoLocation(-12, 12) }
				//},
				//new GeoDistanceSort
				//{
				//	Field = "locationPoint",
				//	Points = new[] { new GeoLocation(70, -70), new GeoLocation(-12, 12) }
				//},
				new ScriptSort
				{
					Type = ScriptSortType.Number,
					Order = SortOrder.Asc,
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

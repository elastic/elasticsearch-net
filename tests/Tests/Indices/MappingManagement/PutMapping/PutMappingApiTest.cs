// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.MappingManagement.PutMapping
{
	[SkipVersion("<7.4.0", "Shape queries introduced in 7.4.0+")]
	public class PutMappingApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<WritableCluster, PutMappingResponse, IPutMappingRequest, PutMappingDescriptor<Project>, PutMappingRequest<Project>>
	{
		public PutMappingApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			properties = new
			{
				branches = new
				{
					fields = new
					{
						keyword = new
						{
							type = "keyword",
							ignore_above = 256
						}
					},
					type = "text"
				},
				curatedTags = new
				{
					properties = new
					{
						added = new { type = "date" },
						name = new { type = "text" }
					},
					type = "object"
				},
				type = new { type = "text" },
				dateString = new { type = "text" },
				description = new { type = "text" },
				join = new
				{
					relations = new { project = "commits" },
					type = "join"
				},
				lastActivity = new { type = "date" },
				leadDeveloper = new
				{
					properties = new
					{
						firstName = new { type = "text" },
						gender = new { type = "keyword" },
						id = new { type = "long" },
						ipAddress = new { type = "text" },
						jobTitle = new { type = "text" },
						lastName = new { type = "text" },
						location = new { type = "geo_point" },
						nickname = new { type = "text" },
						geoIp = new { type = "object" }
					},
					type = "object"
				},
				locationPoint = new
				{
					properties = new
					{
						lat = new { type = "double" },
						lon = new { type = "double" }
					},
					type = "object"
				},
				locationShape = new
				{
					type = "geo_shape"
				},
				arbitraryShape = new
				{
					type = "shape"
				},
				metadata = new { type = "object" },
				name = new
				{
					index = false,
					type = "text"
				},
				numberOfCommits = new { type = "integer" },
				numberOfContributors = new { type = "integer" },
				sourceOnly = new { properties = new { }, type = "object" },
				startedOn = new { type = "date" },
				state = new { type = "keyword" },
				visibility = new { type = "keyword" },
				suggest = new { type = "completion" },
				ranges = new
				{
					properties = new
					{
						dates = new { type = "date_range" },
						doubles = new { type = "double_range" },
						floats = new { type = "float_range" },
						integers = new { type = "integer_range" },
						longs = new { type = "long_range" },
						ips = new { type = "ip_range" }
					},
					type = "object"
				},
				requiredBranches = new
				{
					type = "integer"
				},
				tags = new
				{
					properties = new
					{
						added = new { type = "date" },
						name = new { type = "text" }
					},
					type = "object"
				},
				rank = new
				{
					type = "rank_feature"
				},
				labels = new
				{
					type = "flattened",
					depth_limit = 4
				},
				versionControl = new
				{
					type = "keyword"
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<PutMappingDescriptor<Project>, IPutMappingRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.AutoMap()
			.Properties(prop => prop
				.Join(join => join
					.Name(p => p.Join)
					.Relations(relations => relations
						.Join<Project, CommitActivity>()
					)
				)
				.Object<Tag>(o => o
					.Name(p => p.CuratedTags)
					.AutoMap()
					.Properties(ps => ps
						.Text(t => t
							.Name(tag => tag.Name)
						)
					)
				)
				.Text(t => t.Name(p => p.Description))
				.Text(t => t.Name(p => p.DateString))
				.Text(t => t.Name(p => p.Type))
				.Text(s => s
					.Name(p => p.Name)
					.Index(false)
				)
				.Shape(shape => shape.Name(p => p.ArbitraryShape))
				.Object<Developer>(o => o
					.Name(p => p.LeadDeveloper)
					.AutoMap()
					.Properties(ps => ps
						.Text(t => t.Name(dv => dv.FirstName))
						.Text(t => t.Name(dv => dv.IpAddress))
						.Text(t => t.Name(dv => dv.JobTitle))
						.Text(t => t.Name(dv => dv.LastName))
						.Text(t => t.Name(dv => dv.OnlineHandle))
						.Object<GeoIp>(t => t.Name(dv => dv.GeoIp))
					)
				)
				.Object<object>(o => o
					.Name(p => p.Metadata)
				)
				.Object<Tag>(o => o
					.AutoMap()
					.Name(p => p.Tags)
					.Properties(ps => ps
						.Text(t => t
							.Name(tag => tag.Name)
						)
					)
				)
				.RankFeature(rf => rf
					.Name(p => p.Rank)
				)
				.Flattened(f => f
					.Name(p => p.Labels)
					.DepthLimit(4)
				)
				.Keyword(k => k
					.Name(n => n.VersionControl)
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutMappingRequest<Project> Initializer => new PutMappingRequest<Project>(CallIsolatedValue)
		{
			Properties = new Properties<Project>
			{
				{
					p => p.Join, new JoinProperty
					{
						Relations = new Relations
						{
							{ typeof(Project), typeof(CommitActivity) }
						}
					}
				},
				{
					p => p.Branches, new TextProperty
					{
						Fields = new Properties
						{
							{
								"keyword", new KeywordProperty
								{
									IgnoreAbove = 256
								}
							}
						}
					}
				},
				{
					p => p.CuratedTags, new ObjectProperty
					{
						Properties = new Properties<Tag>
						{
							{ p => p.Added, new DateProperty() },
							{ p => p.Name, new TextProperty() },
						}
					}
				},
				{ p => p.Description, new TextProperty() },
				{ p => p.DateString, new TextProperty() },
				{ p => p.Type, new TextProperty() },
				{ p => p.LastActivity, new DateProperty() },
				{
					p => p.LeadDeveloper, new ObjectProperty
					{
						Properties = new Properties<Developer>
						{
							{ p => p.FirstName, new TextProperty() },
							{ p => p.Gender, new KeywordProperty() },
							{ p => p.Id, new NumberProperty(NumberType.Long) },
							{ p => p.IpAddress, new TextProperty() },
							{ p => p.JobTitle, new TextProperty() },
							{ p => p.LastName, new TextProperty() },
							{ p => p.Location, new GeoPointProperty() },
							{ p => p.OnlineHandle, new TextProperty() },
							{ p => p.GeoIp, new ObjectProperty() },
						}
					}
				},
				{
					p => p.LocationPoint, new ObjectProperty
					{
						Properties = new Properties<SimpleGeoPoint>
						{
							{ p => p.Lat, new NumberProperty(NumberType.Double) },
							{ p => p.Lon, new NumberProperty(NumberType.Double) },
						}
					}
				},
				{ p => p.LocationShape, new GeoShapeProperty() },
				{ p => p.ArbitraryShape, new ShapeProperty() },
				{ p => p.Metadata, new ObjectProperty() },
				{ p => p.Name, new TextProperty { Index = false } },
				{ p => p.NumberOfCommits, new NumberProperty(NumberType.Integer) },
				{ p => p.NumberOfContributors, new NumberProperty(NumberType.Integer) },
				{
					p => p.SourceOnly, new ObjectProperty()
					{
						Properties = new Properties()
					}
				},
				{ p => p.StartedOn, new DateProperty() },
				{ p => p.State, new KeywordProperty() },
				{ p => p.Visibility, new KeywordProperty() },
				{ p => p.Suggest, new CompletionProperty() },
				{
					p => p.Ranges, new ObjectProperty
					{
						Properties = new Properties<Ranges>
						{
							{ p => p.Dates, new DateRangeProperty() },
							{ p => p.Doubles, new DoubleRangeProperty() },
							{ p => p.Floats, new FloatRangeProperty() },
							{ p => p.Integers, new IntegerRangeProperty() },
							{ p => p.Longs, new LongRangeProperty() },
							{ p => p.Ips, new IpRangeProperty() },
						}
					}
				},
				{ p => p.RequiredBranches, new NumberProperty(NumberType.Integer) },
				{
					p => p.Tags, new ObjectProperty
					{
						Properties = new Properties<Tag>
						{
							{ p => p.Added, new DateProperty() },
							{ p => p.Name, new TextProperty() },
						}
					}
				},
				{ p => p.Rank, new RankFeatureProperty() },
				{ p => p.Labels, new FlattenedProperty
				{
					DepthLimit = 4
				} },
				{ p => p.VersionControl, new KeywordProperty() }
			}
		};

		protected override string UrlPath => $"/{CallIsolatedValue}/_mapping";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Map(f),
			(client, f) => client.MapAsync(f),
			(client, r) => client.Map(r),
			(client, r) => client.MapAsync(r)
		);
	}
}

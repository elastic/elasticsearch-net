using System;
using Elasticsearch.Net;
using Nest;
using Tests.ClientConcepts.HighLevel.Inference;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.MappingManagement.PutMapping
{
	[SkipVersion("<5.2.0", "This uses the range types introduced in 5.2.0")]
	public class PutMappingApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<WritableCluster, IPutMappingResponse, IPutMappingRequest, PutMappingDescriptor<Project>, PutMappingRequest<Project>>
	{
		public PutMappingApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Map(f),
			fluentAsync: (client, f) => client.MapAsync(f),
			request: (client, r) => client.Map(r),
			requestAsync: (client, r) => client.MapAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/{CallIsolatedValue}/doc/_mapping";

		protected override object ExpectJson { get; } = new
		{
			include_in_all = true,
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
						gender = new { type = "integer" },
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
				location = new
				{
					properties = new
					{
						lat = new { type = "double" },
						lon = new { type = "double" }
					},
					type = "object"
				},
				metadata = new { type = "object" },
				name = new
				{
					index = false,
					type = "text"
				},
				numberOfCommits = new { type = "integer" },
				numberOfContributors = new { type = "integer" },
				sourceOnly = new {properties = new { }, type = "object"},
				startedOn = new {type = "date"},
				state = new { type = "integer" },
				suggest = new { type = "completion" },
				ranges = new
				{
					properties = new
					{
						dates = new { type = "date_range" },
						doubles = new { type = "double_range" },
						floats = new { type = "float_range" },
						integers = new { type = "integer_range" },
						longs = new { type = "long_range" }
					},
					type = "object"
				},
				tags = new
				{
					properties = new
					{
						added = new { type = "date" },
						name = new { type = "text" }
					},
					type = "object"
				}
			}
		};


		protected override Func<PutMappingDescriptor<Project>, IPutMappingRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.IncludeInAll()
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
				.Text(s => s
					.Name(p => p.Name)
					.Index(false)
				)
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
			);

		protected override PutMappingRequest<Project> Initializer => new PutMappingRequest<Project>(CallIsolatedValue, Type<Project>())
		{
			IncludeInAll = true,
			Properties = new Properties<Project>
			{
				{
					p => p.Join, new JoinProperty
					{
						Relations = new Relations
						{
							{typeof(Project), typeof(CommitActivity)}
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
							{p => p.Added, new DateProperty()},
							{p => p.Name, new TextProperty()},
						}
					}
				},
				{p => p.Description, new TextProperty()},
				{p => p.DateString, new TextProperty { }},
				{p => p.LastActivity, new DateProperty()},
				{
					p => p.LeadDeveloper, new ObjectProperty
					{
						Properties = new Properties<Developer>
						{
							{p => p.FirstName, new TextProperty()},
							{p => p.Gender, new NumberProperty(NumberType.Integer)},
							{p => p.Id, new NumberProperty(NumberType.Long)},
							{p => p.IpAddress, new TextProperty()},
							{p => p.JobTitle, new TextProperty()},
							{p => p.LastName, new TextProperty()},
							{p => p.Location, new GeoPointProperty()},
							{p => p.OnlineHandle, new TextProperty()},
							{p => p.GeoIp, new ObjectProperty()},
						}
					}
				},
				{
					p => p.Location, new ObjectProperty
					{
						Properties = new Properties<SimpleGeoPoint>
						{
							{p => p.Lat, new NumberProperty(NumberType.Double)},
							{p => p.Lon, new NumberProperty(NumberType.Double)},
						}
					}
				},
				{p => p.Metadata, new ObjectProperty()},
				{p => p.Name, new TextProperty {Index = false}},
				{p => p.NumberOfCommits, new NumberProperty(NumberType.Integer)},
				{p => p.NumberOfContributors, new NumberProperty(NumberType.Integer)},
				{
					p => p.SourceOnly, new ObjectProperty()
					{
						Properties = new Properties()
					}
				},
				{p => p.StartedOn, new DateProperty()},
				{p => p.State, new NumberProperty(NumberType.Integer)},
				{p => p.Suggest, new CompletionProperty()},
				{
					p => p.Ranges, new ObjectProperty
					{
						Properties = new Properties<Ranges>
						{
							{p => p.Dates, new DateRangeProperty()},
							{p => p.Doubles, new DoubleRangeProperty()},
							{p => p.Floats, new FloatRangeProperty()},
							{p => p.Integers, new IntegerRangeProperty()},
							{p => p.Longs, new LongRangeProperty()},
						}
					}
				},
				{
					p => p.Tags, new ObjectProperty
					{
						Properties = new Properties<Tag>
						{
							{p => p.Added, new DateProperty()},
							{p => p.Name, new TextProperty()},
						}
					}
				},
			}
		};
	}
}

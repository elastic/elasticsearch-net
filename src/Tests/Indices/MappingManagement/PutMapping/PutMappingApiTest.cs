using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.MappingManagement.PutMapping
{
	[Collection(IntegrationContext.Indexing)]
	public class PutMappingApiTests : ApiIntegrationTestBase<IPutMappingResponse, IPutMappingRequest, PutMappingDescriptor<Project>, PutMappingRequest<Project>>
	{
		public PutMappingApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values) client.CreateIndex(index);
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
		protected override string UrlPath => $"/{CallIsolatedValue}/project/_mapping";

		protected override object ExpectJson { get; } = new
		{
			properties = new
			{
				curatedTags = new
				{
					properties = new
					{
						added = new
						{
							type = "date"
						},
						name = new
						{
							type = "string"
						}
					},
					type = "object"
				},
				description = new
				{
					type = "string"
				},
				lastActivity = new
				{
					type = "date"
				},
				leadDeveloper = new
				{
					properties = new
					{
						firstName = new
						{
							type = "string"
						},
						gender = new
						{
							type = "integer"
						},
						id = new
						{
							type = "long"
						},
						iPAddress = new
						{
							type = "string"
						},
						jobTitle = new
						{
							type = "string"
						},
						lastName = new
						{
							type = "string"
						},
						location = new
						{
							type = "geo_point"
						},
						nickname = new
						{
							type = "string"
						}
					},
					type = "object"
				},
				location = new
				{
					properties = new
					{
						lat = new
						{
							type = "double"
						},
						lon = new
						{
							type = "double"
						}
					},
					type = "object"
				},
				metadata = new
				{
					type = "object"
				},
				name = new
				{
					index = "not_analyzed",
					type = "string"
				},
				numberOfCommits = new
				{
					type = "integer",
					index = "not_analyzed"
				},
				startedOn = new
				{
					type = "date",
					index = "no"
				},
				state = new
				{
					type = "integer"
				},
				suggest = new
				{
					type = "completion"
				},
				tags = new
				{
					properties = new
					{
						added = new
						{
							type = "date"
						},
						name = new
						{
							type = "string"
						}
					},
					type = "object"
				}
			}
		};


		protected override Func<PutMappingDescriptor<Project>, IPutMappingRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.AutoMap()
			.Properties(prop => prop
				.Number(n => n
					.Name(p => p.NumberOfCommits)
					.Type(NumberType.Integer)
					.Index()
				)
				.Date(dt => dt
					.Name(p => p.StartedOn)
					.Index(NonStringIndexOption.No)
				)
				.String(s => s
					.Name(p => p.Name)
					.NotAnalyzed()
				)
				.Object<object>(o => o
					.Name(p => p.Metadata)
				)
			);

		protected override PutMappingRequest<Project> Initializer => new PutMappingRequest<Project>(CallIsolatedValue, Type<Project>())
		{
			Properties = new Properties<Project>
			{

				{ p => p.CuratedTags, new ObjectProperty
						{
							Properties = new Properties<Tag>
							{
								{ p => p.Added, new DateProperty() },
								{ p => p.Name, new StringProperty() },
							}
						}
				},
				{ p => p.Description, new StringProperty() },
				{ p => p.LastActivity, new DateProperty() },
				{ p => p.LeadDeveloper, new ObjectProperty
						{
							Properties = new Properties<Developer>
							{
								{ p => p.FirstName, new StringProperty() },
								{ p => p.Gender, new NumberProperty(NumberType.Integer) },
								{ p => p.Id, new NumberProperty(NumberType.Long) },
								{ p => p.IPAddress, new StringProperty() },
								{ p => p.JobTitle, new StringProperty() },
								{ p => p.LastName, new StringProperty() },
								{ p => p.Location, new GeoPointProperty() },
								{ p => p.OnlineHandle, new StringProperty() },
							}
						}
				},
				{ p => p.Location, new ObjectProperty
						{
							Properties = new Properties<SimpleGeoPoint>
							{
								{ p => p.Lat, new NumberProperty(NumberType.Double) },
								{ p => p.Lon, new NumberProperty(NumberType.Double) },
							}
						}
				},
				{ p => p.Metadata, new ObjectProperty() },
				{ p => p.Name, new StringProperty { Index = FieldIndexOption.NotAnalyzed }  },
				{ p => p.NumberOfCommits, new NumberProperty(NumberType.Integer) { Index = NonStringIndexOption.NotAnalyzed } },
				{ p => p.StartedOn, new DateProperty { Index = NonStringIndexOption.No } },
				{ p => p.State, new NumberProperty(NumberType.Integer) },
				{ p => p.Suggest, new CompletionProperty() },
				{ p => p.Tags, new ObjectProperty
						{
							Properties = new Properties<Tag>
							{
								{ p => p.Added, new DateProperty() },
								{ p => p.Name, new StringProperty() },
							}
						}
				},
			}
		};
	}
}

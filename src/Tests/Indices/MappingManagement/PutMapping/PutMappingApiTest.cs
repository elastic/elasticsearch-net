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

		protected override void BeforeAllCalls(IElasticClient client, IDictionary<ClientMethod, string> values)
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
							type = "text"
						}
					},
					type = "object"
				},
				description = new
				{
					type = "text"
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
							type = "text"
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
							type = "text"
						},
						jobTitle = new
						{
							type = "text"
						},
						lastName = new
						{
							type = "text"
						},
						location = new
						{
							type = "geo_point"
						},
						nickname = new
						{
							type = "text"
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
					index = false,
					type = "text"
				},
				numberOfCommits = new
				{
					type = "integer"
				},
				startedOn = new
				{
					type = "date"
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
							type = "text"
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
				.Text(s => s
					.Name(p => p.Name)
					.Index(false)
				)
				.Object<Developer>(o => o
					.Name(p => p.LeadDeveloper)
					.AutoMap()
					.Properties(ps => ps
						.Text(t => t.Name(dv => dv.FirstName))
						.Text(t => t.Name(dv => dv.IPAddress))
						.Text(t => t.Name(dv => dv.JobTitle))
						.Text(t => t.Name(dv => dv.LastName))
						.Text(t => t.Name(dv => dv.OnlineHandle))
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
			Properties = new Properties<Project>
			{

				{ p => p.CuratedTags, new ObjectProperty
						{
							Properties = new Properties<Tag>
							{
								{ p => p.Added, new DateProperty() },
								{ p => p.Name, new TextProperty() },
							}
						}
				},
				{ p => p.Description, new TextProperty() },
				{ p => p.LastActivity, new DateProperty() },
				{ p => p.LeadDeveloper, new ObjectProperty
						{
							Properties = new Properties<Developer>
							{
								{ p => p.FirstName, new TextProperty() },
								{ p => p.Gender, new NumberProperty(NumberType.Integer) },
								{ p => p.Id, new NumberProperty(NumberType.Long) },
								{ p => p.IPAddress, new TextProperty() },
								{ p => p.JobTitle, new TextProperty() },
								{ p => p.LastName, new TextProperty() },
								{ p => p.Location, new GeoPointProperty() },
								{ p => p.OnlineHandle, new TextProperty() },
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
				{ p => p.Name, new TextProperty { Index = false }  },
				{ p => p.NumberOfCommits, new NumberProperty(NumberType.Integer) },
				{ p => p.StartedOn, new DateProperty() },
				{ p => p.State, new NumberProperty(NumberType.Integer) },
				{ p => p.Suggest, new CompletionProperty() },
				{ p => p.Tags, new ObjectProperty
						{
							Properties = new Properties<Tag>
							{
								{ p => p.Added, new DateProperty() },
								{ p => p.Name, new TextProperty() },
							}
						}
				},
			}
		};
	}
}

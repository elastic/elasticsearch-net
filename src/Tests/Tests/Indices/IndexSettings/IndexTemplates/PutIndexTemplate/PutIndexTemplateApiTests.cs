using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.IndexSettings.IndexTemplates.PutIndexTemplate
{
	public class PutIndexTemplateApiTests
		: ApiIntegrationTestBase<WritableCluster, IPutIndexTemplateResponse, IPutIndexTemplateRequest, PutIndexTemplateDescriptor,
			PutIndexTemplateRequest>
	{
		public PutIndexTemplateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			order = 1,
			version = 2,
			template = "nestx-*",
			settings = new Dictionary<string, object> { { "index.number_of_shards", 1 } },
			mappings = new
			{
				_default_ = new
				{
					dynamic_templates = new object[]
					{
						new
						{
							@base = new
							{
								match = "*",
								match_mapping_type = "*",
								mapping = new
								{
									index = false
								}
							}
						}
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

#pragma warning disable 618
		protected override Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> Fluent => d => d
			.Order(1)
			.Version(2)
			.Template("nestx-*")
			.Create(false)
			.Settings(p => p.NumberOfShards(1))
			.Mappings(m => m
				.Map("_default_", tm => tm
					.DynamicTemplates(t => t
						.DynamicTemplate("base", dt => dt
							.Match("*")
							.MatchMappingType("*")
							.Mapping(mm => mm
								.Generic(g => g
									.Index(false)
								)
							)
						)
					)
				)
			);
#pragma warning restore 618

		protected override HttpMethod HttpMethod => HttpMethod.PUT;


		protected override PutIndexTemplateRequest Initializer => new PutIndexTemplateRequest(CallIsolatedValue)
		{
			Order = 1,
			Version = 2,
#pragma warning disable 618
			Template = "nestx-*",
#pragma warning restore 618
			Create = false,
			Settings = new Nest.IndexSettings
			{
				NumberOfShards = 1
			},
			Mappings = new Mappings
			{
				{
					"_default_", new TypeMapping
					{
						DynamicTemplates = new DynamicTemplateContainer
						{
							{
								"base", new DynamicTemplate
								{
									Match = "*",
									MatchMappingType = "*",
									Mapping = new GenericProperty
									{
										Indexed = false
									}
								}
							}
						}
					}
				}
			}
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_template/{CallIsolatedValue}?create=false";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.PutIndexTemplate(CallIsolatedValue, f),
			(client, f) => client.PutIndexTemplateAsync(CallIsolatedValue, f),
			(client, r) => client.PutIndexTemplate(r),
			(client, r) => client.PutIndexTemplateAsync(r)
		);

		protected override PutIndexTemplateDescriptor NewDescriptor() => new PutIndexTemplateDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(IPutIndexTemplateResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
		}
	}
}

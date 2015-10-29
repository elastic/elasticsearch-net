using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Indices.MappingManagement.PutMapping
{
	[Collection(IntegrationContext.Indexing)]
	public class PutMappingApiTests 
		: ApiIntegrationTestBase<IIndicesResponse, IPutMappingRequest, PutMappingDescriptor<Project>, PutMappingRequest<Project>>
	{
		public PutMappingApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Map(f),
			fluentAsync: (client, f) => client.MapAsync(f),
			request: (client, r) => client.Map(r),
			requestAsync: (client, r) => client.MapAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => "/project/project/_mapping";

		protected override object ExpectJson { get; } = new
		{
			properties = new
			{
				name = new
				{
					type = "string",
					index = "not_analyzed"
				}
			}

		};


		protected override Func<PutMappingDescriptor<Project>, IPutMappingRequest> Fluent => d => d
			.Properties(prop=>prop
				.String(s=>s.Name(p=>p.Name).NotAnalyzed())
			);

		protected override PutMappingRequest<Project> Initializer => new PutMappingRequest<Project>
		{
			Properties = new Properties<Project>
			{
				{ p=>p.Name, new StringProperty { Index = FieldIndexOption.NotAnalyzed }  }
			}
		};
	}
}

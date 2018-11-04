using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Mapping.Types
{
	public abstract class PropertyTestsBase
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, IPutMappingResponse, IPutMappingRequest, PutMappingDescriptor<Project>,
			PutMappingRequest<Project>>
	{
		protected PropertyTestsBase(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<PutMappingDescriptor<Project>, IPutMappingRequest> Fluent => f => f
			.Index(CallIsolatedValue)
			.Properties(FluentProperties);

		protected abstract Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties { get; }
		protected override HttpMethod HttpMethod => HttpMethod.PUT;


		protected override PutMappingRequest<Project> Initializer => new PutMappingRequest<Project>(CallIsolatedValue, typeof(Project))
		{
			Properties = InitializerProperties
		};

		protected abstract IProperties InitializerProperties { get; }
		protected override string UrlPath => $"/{CallIsolatedValue}/project/_mapping";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Map(f),
			(client, f) => client.MapAsync(f),
			(client, r) => client.Map(r),
			(client, r) => client.MapAsync(r)
		);
	}
}

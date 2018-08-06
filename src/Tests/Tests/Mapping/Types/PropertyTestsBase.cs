using System;
using Nest;
using Elasticsearch.Net;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.Mapping.Types
{
	public abstract class PropertyTestsBase
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, IPutMappingResponse, IPutMappingRequest, PutMappingDescriptor<Project>, PutMappingRequest<Project>>
	{
		protected PropertyTestsBase(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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

		protected abstract Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties { get; }

		protected abstract IProperties InitializerProperties { get; }

		protected override Func<PutMappingDescriptor<Project>, IPutMappingRequest> Fluent => f => f
			.Index(CallIsolatedValue)
			.Properties(this.FluentProperties);


		protected override PutMappingRequest<Project> Initializer => new PutMappingRequest<Project>(CallIsolatedValue, typeof(Project))
		{
			Properties = this.InitializerProperties
		};
	}
}

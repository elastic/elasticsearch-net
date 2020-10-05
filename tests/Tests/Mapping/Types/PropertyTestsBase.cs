// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types
{
	public abstract class PropertyTestsBase
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, PutMappingResponse, IPutMappingRequest, PutMappingDescriptor<Project>,
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


		protected override PutMappingRequest<Project> Initializer => new PutMappingRequest<Project>(CallIsolatedValue)
		{
			Properties = InitializerProperties
		};

		protected abstract IProperties InitializerProperties { get; }
		protected override string UrlPath => $"/{CallIsolatedValue}/_mapping";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Map(f),
			(client, f) => client.MapAsync(f),
			(client, r) => client.Map(r),
			(client, r) => client.MapAsync(r)
		);

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] protected override Task HitsTheCorrectUrl() => base.HitsTheCorrectUrl();

		[U] protected override Task UsesCorrectHttpMethod() => base.UsesCorrectHttpMethod();

		[U] protected override void SerializesInitializer() => base.SerializesInitializer();

		[U] protected override void SerializesFluent() => base.SerializesFluent();

		[I] public override Task ReturnsExpectedStatusCode() => base.ReturnsExpectedResponse();

		[I] public override Task ReturnsExpectedIsValid() => base.ReturnsExpectedIsValid();

		[I] public override Task ReturnsExpectedResponse() => base.ReturnsExpectedResponse();
	}
}

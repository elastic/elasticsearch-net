using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Static;

namespace Tests.Document.Multiple.MultiGet
{
	[Collection(IntegrationContext.ReadOnly)]
	public class MultiGetSimplifiedApiTests : ApiTestBase<IMultiGetResponse, IMultiGetRequest, MultiGetDescriptor, MultiGetRequest>
	{
		public MultiGetSimplifiedApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.MultiGet(f),
			fluentAsync: (client, f) => client.MultiGetAsync(f),
			request: (client, r) => client.MultiGet(r),
			requestAsync: (client, r) => client.MultiGetAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/project/_mget";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new 
		{
			docs = Project.Projects.Select(p =>  p.Name).Take(10)
		};

		protected override Func<MultiGetDescriptor, IMultiGetRequest> Fluent => d => d
			.Index<Project>()
			.Type<Project>()
			.GetMany<Project>(Project.Projects.Select(p => p.Name).Take(10));
			

		protected override MultiGetRequest Initializer => new MultiGetRequest(Index<Project>(), Type<Project>())
		{
			GetOperations = Project.Projects.Select(p => p.Name).Take(10)
				.Select(n=>new MultiGetOperation<Project>(n))
				
		};
	}

	[Collection(IntegrationContext.ReadOnly)]
	public class MultiGetApiTests : ApiTestBase<IMultiGetResponse, IMultiGetRequest, MultiGetDescriptor, MultiGetRequest>
	{
		public MultiGetApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.MultiGet(f),
			fluentAsync: (client, f) => client.MultiGetAsync(f),
			request: (client, r) => client.MultiGet(r),
			requestAsync: (client, r) => client.MultiGetAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/_mget";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new 
		{
			docs = Project.Projects.Select(p => new { _type = "project", _id = p.Name, _routing = p.Name, _source = false }).Take(10)
		};

		protected override Func<MultiGetDescriptor, IMultiGetRequest> Fluent => d => d
			.Index<Project>()
			.GetMany<Project>(Project.Projects.Select(p => p.Name).Take(10), (g, i) => g.Routing(i).Source(false));
			

		protected override MultiGetRequest Initializer => new MultiGetRequest(Index<Project>())
		{
			GetOperations = Project.Projects.Select(p => p.Name).Take(10)
				.Select(n=>new MultiGetOperation<Project>(n) { Routing = n, Source = false })
				
		};
	}
}

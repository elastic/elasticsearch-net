// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.Scripting.GetScript
{
	public class GetScriptApiTests
		: ApiTestBase<ReadOnlyCluster, GetScriptResponse, IGetScriptRequest, GetScriptDescriptor, GetScriptRequest>
	{
		private static readonly string _name = "scrpt1";

		public GetScriptApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<GetScriptDescriptor, IGetScriptRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetScriptRequest Initializer => new GetScriptRequest(_name);
		protected override string UrlPath => $"/_scripts/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetScript(_name, f),
			(client, f) => client.GetScriptAsync(_name, f),
			(client, r) => client.GetScript(r),
			(client, r) => client.GetScriptAsync(r)
		);

		protected override GetScriptDescriptor NewDescriptor() => new GetScriptDescriptor(_name);
	}
}

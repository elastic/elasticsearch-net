// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.Scripting.DeleteScript
{
	public class DeleteScriptApiTests
		: ApiTestBase<ReadOnlyCluster, DeleteScriptResponse, IDeleteScriptRequest, DeleteScriptDescriptor, DeleteScriptRequest>
	{
		private static readonly string _name = "scrpt1";

		public DeleteScriptApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<DeleteScriptDescriptor, IDeleteScriptRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteScriptRequest Initializer => new DeleteScriptRequest(_name);
		protected override string UrlPath => $"/_scripts/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteScript(_name, f),
			(client, f) => client.DeleteScriptAsync(_name, f),
			(client, r) => client.DeleteScript(r),
			(client, r) => client.DeleteScriptAsync(r)
		);

		protected override DeleteScriptDescriptor NewDescriptor() => new DeleteScriptDescriptor(_name);
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.Scripting
{
	public class ScriptingCrudTests
		: CrudTestBase<IntrusiveOperationCluster, PutScriptResponse, GetScriptResponse, PutScriptResponse, DeleteScriptResponse>
	{
		private readonly string _updatedScript = "2+2";

		public ScriptingCrudTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses Create() => Calls<PutScriptDescriptor, PutScriptRequest, IPutScriptRequest, PutScriptResponse>(
			CreateInitializer,
			CreateFluent,
			(s, c, f) => c.PutScript(s, f),
			(s, c, f) => c.PutScriptAsync(s, f),
			(s, c, r) => c.PutScript(r),
			(s, c, r) => c.PutScriptAsync(r)
		);

		private PutScriptRequest CreateInitializer(string id) => new PutScriptRequest(id) { Script = new PainlessScript("1+1") };

		private IPutScriptRequest CreateFluent(string id, PutScriptDescriptor d) => d.Painless("1+1");

		protected override LazyResponses Read() => Calls<GetScriptDescriptor, GetScriptRequest, IGetScriptRequest, GetScriptResponse>(
			id => new GetScriptRequest(id),
			(id, d) => d,
			(s, c, f) => c.GetScript(s, f),
			(s, c, f) => c.GetScriptAsync(s, f),
			(s, c, r) => c.GetScript(r),
			(s, c, r) => c.GetScriptAsync(r)
		);

		protected override LazyResponses Update() => Calls<PutScriptDescriptor, PutScriptRequest, IPutScriptRequest, PutScriptResponse>(
			UpdateInitializer,
			UpdateFluent,
			(s, c, f) => c.PutScript(s, f),
			(s, c, f) => c.PutScriptAsync(s, f),
			(s, c, r) => c.PutScript(r),
			(s, c, r) => c.PutScriptAsync(r)
		);

		private PutScriptRequest UpdateInitializer(string id) => new PutScriptRequest(id) { Script = new PainlessScript(_updatedScript) };

		private IPutScriptRequest UpdateFluent(string id, PutScriptDescriptor d) => d.Painless(_updatedScript);

		protected override LazyResponses Delete() => Calls<DeleteScriptDescriptor, DeleteScriptRequest, IDeleteScriptRequest, DeleteScriptResponse>(
			id => new DeleteScriptRequest(id),
			(id, d) => d,
			(s, c, f) => c.DeleteScript(s, f),
			(s, c, f) => c.DeleteScriptAsync(s, f),
			(s, c, r) => c.DeleteScript(r),
			(s, c, r) => c.DeleteScriptAsync(r)
		);

		protected override void ExpectAfterUpdate(GetScriptResponse response) => response.Script.Source.Should().Be(_updatedScript);

		protected override void ExpectDeleteNotFoundResponse(DeleteScriptResponse response)
		{
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(404);
			response.ServerError.Error.Reason.Should().Contain("not exist");
		}
	}
}

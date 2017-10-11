using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Modules.Scripting
{
	public class ScriptingCrudTests
		: CrudTestBase<IntrusiveOperationCluster, IPutScriptResponse, IGetScriptResponse, IPutScriptResponse, IDeleteScriptResponse>
	{
		public ScriptingCrudTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses Create() => Calls<PutScriptDescriptor, PutScriptRequest, IPutScriptRequest, IPutScriptResponse>(
			CreateInitializer,
			CreateFluent,
			fluent: (s, c, f) => c.PutScript(s, f),
			fluentAsync: (s, c, f) => c.PutScriptAsync(s, f),
			request: (s, c, r) => c.PutScript(r),
			requestAsync: (s, c, r) => c.PutScriptAsync(r)
		);

		protected PutScriptRequest CreateInitializer(string id) => new PutScriptRequest(id) { Script = new PainlessScript("1+1") };
		protected IPutScriptRequest CreateFluent(string id, PutScriptDescriptor d) => d.Painless("1+1");

		protected override LazyResponses Read() => Calls<GetScriptDescriptor, GetScriptRequest, IGetScriptRequest, IGetScriptResponse>(
			ReadInitializer,
			ReadFluent,
			fluent: (s, c, f) => c.GetScript(s, f),
			fluentAsync: (s, c, f) => c.GetScriptAsync(s, f),
			request: (s, c, r) => c.GetScript(r),
			requestAsync: (s, c, r) => c.GetScriptAsync(r)
		);

		protected GetScriptRequest ReadInitializer(string id) => new GetScriptRequest(id);
		protected IGetScriptRequest ReadFluent(string id, GetScriptDescriptor d) => d;

		protected override LazyResponses Update() => Calls<PutScriptDescriptor, PutScriptRequest, IPutScriptRequest, IPutScriptResponse>(
			UpdateInitializer,
			UpdateFluent,
			fluent: (s, c, f) => c.PutScript(s, f),
			fluentAsync: (s, c, f) => c.PutScriptAsync(s, f),
			request: (s, c, r) => c.PutScript(r),
			requestAsync: (s, c, r) => c.PutScriptAsync(r)
		);

		private string _updatedScript = "2+2";

		protected PutScriptRequest UpdateInitializer(string id) => new PutScriptRequest(id) { Script = new PainlessScript(_updatedScript) };
		protected IPutScriptRequest UpdateFluent(string id, PutScriptDescriptor d) => d.Painless(_updatedScript);

		protected override LazyResponses Delete() => Calls<DeleteScriptDescriptor, DeleteScriptRequest, IDeleteScriptRequest, IDeleteScriptResponse>(
			DeleteInitializer,
			DeleteFluent,
			fluent: (s, c, f) => c.DeleteScript(s, f),
			fluentAsync: (s, c, f) => c.DeleteScriptAsync(s, f),
			request: (s, c, r) => c.DeleteScript(r),
			requestAsync: (s, c, r) => c.DeleteScriptAsync(r)
		);

		protected DeleteScriptRequest DeleteInitializer(string id) => new DeleteScriptRequest(id);
		protected IDeleteScriptRequest DeleteFluent(string id, DeleteScriptDescriptor d) => d;

		protected override void ExpectAfterUpdate(IGetScriptResponse response) => response.Script.Source.Should().Be(_updatedScript);
	}
}

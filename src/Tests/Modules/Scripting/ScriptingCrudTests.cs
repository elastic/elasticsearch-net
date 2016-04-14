using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Modules.Scripting
{
	[Collection(IntegrationContext.Indexing)]
	public class ScriptingCrudTests
		: CrudTestBase<IPutScriptResponse, IGetScriptResponse, IPutScriptResponse, IDeleteScriptResponse>
	{
		public ScriptingCrudTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses Create() => Calls<PutScriptDescriptor, PutScriptRequest, IPutScriptRequest, IPutScriptResponse>(
			CreateInitializer,
			CreateFluent,
			fluent: (s, c, f) => c.PutScript(_lang, s, f),
			fluentAsync: (s, c, f) => c.PutScriptAsync(_lang, s, f),
			request: (s, c, r) => c.PutScript(r),
			requestAsync: (s, c, r) => c.PutScriptAsync(r)
		);

		private string _lang = "groovy";

		protected PutScriptRequest CreateInitializer(string id) => new PutScriptRequest(_lang, id) { Script = "1+1" };
		protected IPutScriptRequest CreateFluent(string id, PutScriptDescriptor d) => d.Script("1+1");

		protected override LazyResponses Read() => Calls<GetScriptDescriptor, GetScriptRequest, IGetScriptRequest, IGetScriptResponse>(
			ReadInitializer,
			ReadFluent,
			fluent: (s, c, f) => c.GetScript(_lang, s, f),
			fluentAsync: (s, c, f) => c.GetScriptAsync(_lang, s, f),
			request: (s, c, r) => c.GetScript(r),
			requestAsync: (s, c, r) => c.GetScriptAsync(r)
		);

		protected GetScriptRequest ReadInitializer(string id) => new GetScriptRequest(_lang, id);
		protected IGetScriptRequest ReadFluent(string id, GetScriptDescriptor d) => d;

		protected override LazyResponses Update() => Calls<PutScriptDescriptor, PutScriptRequest, IPutScriptRequest, IPutScriptResponse>(
			UpdateInitializer,
			UpdateFluent,
			fluent: (s, c, f) => c.PutScript(_lang, s, f),
			fluentAsync: (s, c, f) => c.PutScriptAsync(_lang, s, f),
			request: (s, c, r) => c.PutScript(r),
			requestAsync: (s, c, r) => c.PutScriptAsync(r)
		);

		private string _updatedScript = "2+2";

		protected PutScriptRequest UpdateInitializer(string id) => new PutScriptRequest(_lang, id) { Script = _updatedScript };
		protected IPutScriptRequest UpdateFluent(string id, PutScriptDescriptor d) => d.Script(_updatedScript);

		protected override LazyResponses Delete() => Calls<DeleteScriptDescriptor, DeleteScriptRequest, IDeleteScriptRequest, IDeleteScriptResponse>(
			DeleteInitializer,
			DeleteFluent,
			fluent: (s, c, f) => c.DeleteScript(_lang, s, f),
			fluentAsync: (s, c, f) => c.DeleteScriptAsync(_lang, s, f),
			request: (s, c, r) => c.DeleteScript(r),
			requestAsync: (s, c, r) => c.DeleteScriptAsync(r)
		);

		protected DeleteScriptRequest DeleteInitializer(string id) => new DeleteScriptRequest(_lang, id);
		protected IDeleteScriptRequest DeleteFluent(string id, DeleteScriptDescriptor d) => d;

		protected override void ExpectAfterUpdate(IGetScriptResponse response) => response.Script.Should().Be(_updatedScript);
	}
}

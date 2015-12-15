using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single
{
	[Collection(IntegrationContext.Indexing)]
	public class DocumentCrudTests : CrudTestBase<IIndexResponse, IGetScriptResponse, IAcknowledgedResponse, IAcknowledgedResponse>
	{
		public DocumentCrudTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override bool SupportsDeletes => true;

		protected override LazyResponses Create() => Calls<IndexDescriptor<Project>, IndexRequest<Project>, IIndexRequest, IIndexResponse>(
			CreateInitializer,
			CreateFluent,
			fluent: (s, c, f) => c.Index(Project.Instance, f),
			fluentAsync: (s, c, f) => c.IndexAsync(Project.Instance, f),
			request: (s, c, r) => c.Index(r),
			requestAsync: (s, c, r) => c.IndexAsync(r)
		);

		private string _lang = "groovy";

		protected IndexRequest<Project> CreateInitializer(string id) =>
			new IndexRequest<Project>(Project.Instance, id: id) {};

		protected IIndexRequest<Project> CreateFluent(string id, IndexDescriptor<Project> d) => d.Id(id);

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

		protected override LazyResponses Update() => Calls<PutScriptDescriptor, PutScriptRequest, IPutScriptRequest, IAcknowledgedResponse>(
			UpdateInitializer,
			UpdateFluent,
			fluent: (s, c, f) => c.PutScript(_lang, s, f),
			fluentAsync: (s, c, f) => c.PutScriptAsync(_lang, s, f),
			request: (s, c, r) => c.PutScript(r),
			requestAsync: (s, c, r) => c.PutScriptAsync(r)
		);

		private string _updatedScript = "2+2";

		protected PutScriptRequest UpdateInitializer(string id) =>
			new PutScriptRequest(_lang, id) { Script = _updatedScript};

		protected IPutScriptRequest UpdateFluent(string id, PutScriptDescriptor d) => d.Script(_updatedScript);

		protected override LazyResponses Delete() => Calls<DeleteScriptDescriptor, DeleteScriptRequest, IDeleteScriptRequest, IAcknowledgedResponse>(
			DeleteInitializer,
			DeleteFluent,
			fluent: (s, c, f) => c.DeleteScript(_lang, s, f),
			fluentAsync: (s, c, f) => c.DeleteScriptAsync(_lang, s, f),
			request: (s, c, r) => c.DeleteScript(r),
			requestAsync: (s, c, r) => c.DeleteScriptAsync(r)
		);

		protected DeleteScriptRequest DeleteInitializer(string id) => new DeleteScriptRequest(_lang, id);

		protected IDeleteScriptRequest DeleteFluent(string id, DeleteScriptDescriptor d) => d;

		[I] protected async Task ScriptIsUpdated() => await this.AssertOnGetAfterUpdate(r =>
			r.Script.Should().Be(_updatedScript)
		);

		[I] protected async Task ScriptIsDeleted() => await this.AssertOnGetAfterDelete(r =>
			r.IsValid.Should().BeFalse()
		);
	}
}

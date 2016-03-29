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
	public class DocumentCrudTests : CrudTestBase<IIndexResponse, IGetResponse<Project>, IUpdateResponse<Project>, IDeleteResponse>
	{
		public DocumentCrudTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool SupportsDeletes => true;

		protected override LazyResponses Create() => Calls<IndexDescriptor<Project>, IndexRequest<Project>, IIndexRequest, IIndexResponse>(
			CreateInitializer,
			CreateFluent,
			fluent: (s, c, f) => c.Index(Project.Instance, f),
			fluentAsync: (s, c, f) => c.IndexAsync(Project.Instance, f),
			request: (s, c, r) => c.Index(r),
			requestAsync: (s, c, r) => c.IndexAsync(r)
		);

		protected IndexRequest<Project> CreateInitializer(string id) => new IndexRequest<Project>(Project.Instance, id: id) { };

		protected IIndexRequest<Project> CreateFluent(string id, IndexDescriptor<Project> d) => d.Id(id);

		protected override LazyResponses Read() => Calls<GetDescriptor<Project>, GetRequest<Project>, IGetRequest, IGetResponse<Project>>(
			ReadInitializer,
			ReadFluent,
			fluent: (s, c, f) => c.Get<Project>(s, f),
			fluentAsync: (s, c, f) => c.GetAsync<Project>(s, f),
			request: (s, c, r) => c.Get<Project>(r),
			requestAsync: (s, c, r) => c.GetAsync<Project>(r)
		);

		protected GetRequest<Project> ReadInitializer(string id) => new GetRequest<Project>(id);
		protected IGetRequest ReadFluent(string id, GetDescriptor<Project> d) => d;

		protected override LazyResponses Update() => Calls<
			UpdateDescriptor<Project, Project>, 
			UpdateRequest<Project, Project>, 
			IUpdateRequest<Project, Project>, 
			IUpdateResponse<Project>
			>(
				UpdateInitializer,
				UpdateFluent,
				fluent: (s, c, f) => c.Update<Project, Project>(s, f),
				fluentAsync: (s, c, f) => c.UpdateAsync<Project, Project>(s, f),
				request: (s, c, r) => c.Update<Project, Project>(r),
				requestAsync: (s, c, r) => c.UpdateAsync<Project, Project>(r)
			);

		protected UpdateRequest<Project, Project> UpdateInitializer(string id) =>
			new UpdateRequest<Project, Project>(id) { Doc = new Project { Description = id + " updated" } };

		protected IUpdateRequest<Project, Project> UpdateFluent(string id, UpdateDescriptor<Project, Project> d) => d
			.Doc(new Project { Description = id + " updated"} );

		protected override LazyResponses Delete() => Calls<DeleteDescriptor<Project>, DeleteRequest<Project>, IDeleteRequest, IDeleteResponse>(
			DeleteInitializer,
			DeleteFluent,
			fluent: (s, c, f) => c.Delete<Project>(s, f),
			fluentAsync: (s, c, f) => c.DeleteAsync<Project>(s, f),
			request: (s, c, r) => c.Delete(r),
			requestAsync: (s, c, r) => c.DeleteAsync(r)
		);

		protected DeleteRequest<Project> DeleteInitializer(string id) => new DeleteRequest<Project>(id);

		protected IDeleteRequest DeleteFluent(string id, DeleteDescriptor<Project> d) => d;

		[I] protected async Task DocumentIsUpdated() => await this.AssertOnGetAfterUpdate(r =>
			r.Source.Description.Should().EndWith("updated")
		);

		[I] protected async Task DocumentIsDeleted() => await this.AssertOnGetAfterDelete(r => 
			r.Found.Should().BeFalse()
		);
	}
}

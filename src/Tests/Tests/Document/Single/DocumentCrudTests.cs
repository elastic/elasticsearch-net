using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Document.Single
{
	public class DocumentCrudTests : CrudTestBase<IIndexResponse, IGetResponse<Project>, IUpdateResponse<Project>, IDeleteResponse>
	{
		public DocumentCrudTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool SupportsDeletes => true;

		protected override LazyResponses Create() => Calls<IndexDescriptor<Project>, IndexRequest<Project>, IIndexRequest, IIndexResponse>(
			CreateInitializer,
			CreateFluent,
			(s, c, f) => c.Index(Project.Instance, f),
			(s, c, f) => c.IndexAsync(Project.Instance, f),
			(s, c, r) => c.Index(r),
			(s, c, r) => c.IndexAsync(r)
		);

		protected IndexRequest<Project> CreateInitializer(string id) => new IndexRequest<Project>(Project.Instance, id: id) { };

		protected IIndexRequest<Project> CreateFluent(string id, IndexDescriptor<Project> d) => d.Id(id);

		protected override LazyResponses Read() => Calls<GetDescriptor<Project>, GetRequest<Project>, IGetRequest, IGetResponse<Project>>(
			ReadInitializer,
			ReadFluent,
			(s, c, f) => c.Get<Project>(s, f),
			(s, c, f) => c.GetAsync<Project>(s, f),
			(s, c, r) => c.Get<Project>(r),
			(s, c, r) => c.GetAsync<Project>(r)
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
			(s, c, f) => c.Update<Project, Project>(s, f),
			(s, c, f) => c.UpdateAsync<Project, Project>(s, f),
			(s, c, r) => c.Update<Project, Project>(r),
			(s, c, r) => c.UpdateAsync<Project, Project>(r)
		);

		protected UpdateRequest<Project, Project> UpdateInitializer(string id) =>
			new UpdateRequest<Project, Project>(id) { Doc = new Project { Description = id + " updated" } };

		protected IUpdateRequest<Project, Project> UpdateFluent(string id, UpdateDescriptor<Project, Project> d) => d
			.Doc(new Project { Description = id + " updated" });

		protected override LazyResponses Delete() => Calls<DeleteDescriptor<Project>, DeleteRequest<Project>, IDeleteRequest, IDeleteResponse>(
			DeleteInitializer,
			DeleteFluent,
			(s, c, f) => c.Delete<Project>(s, f),
			(s, c, f) => c.DeleteAsync<Project>(s, f),
			(s, c, r) => c.Delete(r),
			(s, c, r) => c.DeleteAsync(r)
		);

		protected DeleteRequest<Project> DeleteInitializer(string id) => new DeleteRequest<Project>(id);

		protected IDeleteRequest DeleteFluent(string id, DeleteDescriptor<Project> d) => d;

		[I] protected async Task DocumentIsUpdated() => await AssertOnGetAfterUpdate(r =>
		{
			r.Source.Should().NotBeNull();
			r.Source.Description.Should().EndWith("updated");
		});

		[I] protected async Task DocumentIsDeleted() => await AssertOnGetAfterDelete(r =>
			r.Found.Should().BeFalse()
		);

		[I] protected override async Task GetAfterDeleteIsValid() => await AssertOnGetAfterDelete(r =>
			r.ShouldBeValid()
		);
	}
}

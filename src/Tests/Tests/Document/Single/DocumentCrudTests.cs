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
	public class DocumentCrudTests
		: CrudTestBase<WritableCluster, IIndexResponse, IGetResponse<Project>, IUpdateResponse<Project>, IDeleteResponse, IExistsResponse>
	{
		public DocumentCrudTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool SupportsDeletes => true;

		protected override LazyResponses Exists() =>
			Calls<DocumentExistsDescriptor<Project>, DocumentExistsRequest<Project>, IDocumentExistsRequest, IExistsResponse>(
				id => new DocumentExistsRequest<Project>(id),
				(id, d) => d.Routing(Project.Instance.Name),
				(s, c, f) => c.DocumentExists<Project>(s, f),
				(s, c, f) => c.DocumentExistsAsync<Project>(s, f),
				(s, c, r) => c.DocumentExists(r),
				(s, c, r) => c.DocumentExistsAsync(r)
			);

		protected override LazyResponses Create() => Calls<IndexDescriptor<Project>, IndexRequest<Project>, IIndexRequest<Project>, IIndexResponse>(
			id => new IndexRequest<Project>(Project.Instance, id: id),
			(id, d) => d.Id(id),
			(s, c, f) => c.Index(Project.Instance, f),
			(s, c, f) => c.IndexAsync(Project.Instance, f),
			(s, c, r) => c.Index(r),
			(s, c, r) => c.IndexAsync(r)
		);

		protected override LazyResponses Read() => Calls<GetDescriptor<Project>, GetRequest<Project>, IGetRequest, IGetResponse<Project>>(
			id => new GetRequest<Project>(id) { Routing = Project.Instance.Name },
			(id, d) => d.Routing(Project.Instance.Name),
			(s, c, f) => c.Get<Project>(s, f),
			(s, c, f) => c.GetAsync<Project>(s, f),
			(s, c, r) => c.Get<Project>(r),
			(s, c, r) => c.GetAsync<Project>(r)
		);

		protected override LazyResponses Update() => Calls<
			UpdateDescriptor<Project, Project>,
			UpdateRequest<Project, Project>,
			IUpdateRequest<Project, Project>,
			IUpdateResponse<Project>
		>(
			id => new UpdateRequest<Project, Project>(id)
			{
				Routing = Project.Instance.Name,
				Doc = new Project { Description = id + " updated" }
			},
			(id, d) => d
				.Routing(Project.Instance.Name)
				.Doc(new Project { Description = id + " updated" }),
			(s, c, f) => c.Update<Project, Project>(s, f),
			(s, c, f) => c.UpdateAsync<Project, Project>(s, f),
			(s, c, r) => c.Update<Project, Project>(r),
			(s, c, r) => c.UpdateAsync<Project, Project>(r)
		);

		protected override LazyResponses Delete() => Calls<DeleteDescriptor<Project>, DeleteRequest<Project>, IDeleteRequest, IDeleteResponse>(
			id => new DeleteRequest<Project>(id) { Routing = Project.Instance.Name },
			(id, d) => d.Routing(Project.Instance.Name),
			(s, c, f) => c.Delete<Project>(s, f),
			(s, c, f) => c.DeleteAsync<Project>(s, f),
			(s, c, r) => c.Delete(r),
			(s, c, r) => c.DeleteAsync(r)
		);

		[I] protected async Task DocumentIsUpdated() => await AssertOnGetAfterUpdate(r =>
		{
			r.Source.Should().NotBeNull();
			r.Version.Should().BeGreaterThan(1);
			r.Source.Description.Should().EndWith("updated");
		});

		[I] protected async Task DocumentIsDeleted() => await AssertOnGetAfterDelete(r =>
			r.Found.Should().BeFalse()
		);

		[I]
		protected override async Task GetAfterDeleteIsValid() => await AssertOnGetAfterDelete(r =>
		{
			r.ShouldNotBeValid();
			r.Index.Should().NotBeNullOrEmpty();
			r.Type.Should().NotBeNullOrEmpty();
			r.Id.Should().NotBeNullOrEmpty();
		});

		protected override void ExpectDeleteNotFoundResponse(IDeleteResponse response)
		{
			response.Index.Should().NotBeNullOrEmpty();
			response.Type.Should().NotBeNullOrEmpty();
			response.Id.Should().NotBeNullOrEmpty();
			response.Version.Should().BeGreaterThan(0);
			response.SequenceNumber.Should().BeGreaterThan(0);
			response.Result.Should().Be(Result.NotFound);
		}
	}
}

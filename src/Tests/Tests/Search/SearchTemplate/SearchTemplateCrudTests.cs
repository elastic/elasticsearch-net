using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

#pragma warning disable 618

namespace Tests.Search.SearchTemplate
{
	public class SearchTemplateCrudTests
		: CrudTestBase<IntrusiveOperationCluster, IPutSearchTemplateResponse, IGetSearchTemplateResponse, IPutSearchTemplateResponse,
			IDeleteSearchTemplateResponse>
	{
		private readonly string _updatedTemplate = "\"template\" : { \"query\": { \"match_all : {} } }\"";

		//These calls have low priority and often cause `process_cluster_event_timeout_exception`'s
		public SearchTemplateCrudTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool SupportsDeletes => true;

		protected override LazyResponses Create() =>
			Calls<PutSearchTemplateDescriptor, PutSearchTemplateRequest, IPutSearchTemplateRequest, IPutSearchTemplateResponse>(
				CreateInitializer,
				CreateFlunt,
				(s, c, f) => c.PutSearchTemplate(s, f),
				(s, c, f) => c.PutSearchTemplateAsync(s, f),
				(s, c, r) => c.PutSearchTemplate(r),
				(s, c, r) => c.PutSearchTemplateAsync(r)
			);

		protected PutSearchTemplateRequest CreateInitializer(string id) =>
			new PutSearchTemplateRequest(id) { Template = "{}" };

		protected IPutSearchTemplateRequest CreateFlunt(string id, PutSearchTemplateDescriptor d) => d.Template("{}");

		protected override LazyResponses Read() =>
			Calls<GetSearchTemplateDescriptor, GetSearchTemplateRequest, IGetSearchTemplateRequest, IGetSearchTemplateResponse>(
				ReadInitializer,
				ReadFluent,
				(s, c, f) => c.GetSearchTemplate(s),
				(s, c, f) => c.GetSearchTemplateAsync(s),
				(s, c, r) => c.GetSearchTemplate(r),
				(s, c, r) => c.GetSearchTemplateAsync(r)
			);

		protected GetSearchTemplateRequest ReadInitializer(string id) => new GetSearchTemplateRequest(id);

		protected IGetSearchTemplateRequest ReadFluent(string id, GetSearchTemplateDescriptor d) => d;

		protected override LazyResponses Update() =>
			Calls<PutSearchTemplateDescriptor, PutSearchTemplateRequest, IPutSearchTemplateRequest, IPutSearchTemplateResponse>(
				UpdateInitializer,
				UpdateFluent,
				(s, c, f) => c.PutSearchTemplate(s, f),
				(s, c, f) => c.PutSearchTemplateAsync(s, f),
				(s, c, r) => c.PutSearchTemplate(r),
				(s, c, r) => c.PutSearchTemplateAsync(r)
			);

		protected PutSearchTemplateRequest UpdateInitializer(string id) => new PutSearchTemplateRequest(id)
		{
			Template = _updatedTemplate
		};

		protected IPutSearchTemplateRequest UpdateFluent(string id, PutSearchTemplateDescriptor d) => d
			.Template(_updatedTemplate);

		protected override LazyResponses Delete() =>
			Calls<DeleteSearchTemplateDescriptor, DeleteSearchTemplateRequest, IDeleteSearchTemplateRequest, IDeleteSearchTemplateResponse>(
				DeleteInitializer,
				DeleteFluent,
				(s, c, f) => c.DeleteSearchTemplate(s, f),
				(s, c, f) => c.DeleteSearchTemplateAsync(s, f),
				(s, c, r) => c.DeleteSearchTemplate(r),
				(s, c, r) => c.DeleteSearchTemplateAsync(r)
			);

		protected DeleteSearchTemplateRequest DeleteInitializer(string id) => new DeleteSearchTemplateRequest(id);

		protected IDeleteSearchTemplateRequest DeleteFluent(string id, DeleteSearchTemplateDescriptor d) => d;

		protected override void ExpectAfterUpdate(IGetSearchTemplateResponse response) =>
			response.Template.Should().Be(_updatedTemplate);
	}
}

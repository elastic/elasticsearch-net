using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Search.SearchTemplate
{
	[Collection(IntegrationContext.Indexing)]
	public class SearchTemplateCrudTests
		: CrudTestBase<IPutSearchTemplateResponse, IGetSearchTemplateResponse, IPutSearchTemplateResponse, IDeleteSearchTemplateResponse>
	{
		public SearchTemplateCrudTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool SupportsDeletes => true;

		protected override LazyResponses Create() => Calls<PutSearchTemplateDescriptor, PutSearchTemplateRequest, IPutSearchTemplateRequest, IPutSearchTemplateResponse>(
			CreateInitializer,
			CreateFlunt,
			fluent: (s, c, f) => c.PutSearchTemplate(s, f),
			fluentAsync: (s, c, f) => c.PutSearchTemplateAsync(s, f),
			request : (s, c, r) => c.PutSearchTemplate(r),
			requestAsync: (s, c, r) => c.PutSearchTemplateAsync(r)
		);

		protected PutSearchTemplateRequest CreateInitializer(string id) => 
			new PutSearchTemplateRequest(id) { Template = "{}" };

		protected IPutSearchTemplateRequest CreateFlunt(string id, PutSearchTemplateDescriptor d) => d.Template("{}");

		protected override LazyResponses Read() => Calls<GetSearchTemplateDescriptor, GetSearchTemplateRequest, IGetSearchTemplateRequest, IGetSearchTemplateResponse>(
			ReadInitializer,
			ReadFluent,
			fluent: (s, c, f) => c.GetSearchTemplate(s),
			fluentAsync: (s, c, f) => c.GetSearchTemplateAsync(s),
			request: (s, c, r) => c.GetSearchTemplate(r),
			requestAsync: (s, c, r) => c.GetSearchTemplateAsync(r)
		);

		protected GetSearchTemplateRequest ReadInitializer(string id) => new GetSearchTemplateRequest(id);
		protected IGetSearchTemplateRequest ReadFluent(string id, GetSearchTemplateDescriptor d) => d;

		protected override LazyResponses Update() => Calls<PutSearchTemplateDescriptor, PutSearchTemplateRequest, IPutSearchTemplateRequest, IPutSearchTemplateResponse>(
			UpdateInitializer,
			UpdateFluent,
			fluent: (s, c, f) => c.PutSearchTemplate(s, f),
			fluentAsync: (s, c, f) => c.PutSearchTemplateAsync(s, f),
			request: (s, c, r) => c.PutSearchTemplate(r),
			requestAsync: (s, c, r) => c.PutSearchTemplateAsync(r)
		);

		private string _updatedTemplate = "\"template\" : { \"query\": { \"match_all : {} } }\"";

		protected PutSearchTemplateRequest UpdateInitializer(string id) => new PutSearchTemplateRequest(id)
		{
			Template = _updatedTemplate
		};

		protected IPutSearchTemplateRequest UpdateFluent(string id, PutSearchTemplateDescriptor d) => d
			.Template(_updatedTemplate);

		protected override LazyResponses Delete() => Calls<DeleteSearchTemplateDescriptor, DeleteSearchTemplateRequest, IDeleteSearchTemplateRequest, IDeleteSearchTemplateResponse>(
			DeleteInitializer,
			DeleteFluent,
			fluent: (s, c, f) => c.DeleteSearchTemplate(s, f),
			fluentAsync: (s, c, f) => c.DeleteSearchTemplateAsync(s, f),
			request: (s, c, r) => c.DeleteSearchTemplate(r),
			requestAsync: (s, c, r) => c.DeleteSearchTemplateAsync(r)	
		);

		protected DeleteSearchTemplateRequest DeleteInitializer(string id) => new DeleteSearchTemplateRequest(id);
		protected IDeleteSearchTemplateRequest DeleteFluent(string id, DeleteSearchTemplateDescriptor d) => d;

		protected override void ExpectAfterUpdate(IGetSearchTemplateResponse response) => 
			response.Template.Should().Be(_updatedTemplate);
	}
}

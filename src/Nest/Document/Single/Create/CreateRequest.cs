using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(CreateJsonConverter))]
	public interface ICreateRequest : IRequest<CreateRequestParameters>, IUntypedDocumentRequest {}

	public partial interface ICreateRequest<TDocument> : ICreateRequest where TDocument : class
	{
		TDocument Document { get; set; }
	}

	public partial class CreateRequest<TDocument> where TDocument : class
	{
		partial void DocumentFromPath(TDocument document) => this.Document = document;

		object IUntypedDocumentRequest.UntypedDocument => this.Document;

		public TDocument Document { get; set; }
	}

	[DescriptorFor("Create")]
	public partial class CreateDescriptor<TDocument> where TDocument : class
	{
		partial void DocumentFromPath(TDocument document) => Assign(a => a.Document = document);

		object IUntypedDocumentRequest.UntypedDocument => Self.Document;

		TDocument ICreateRequest<TDocument>.Document { get; set; }

		/// <summary>
		/// Sets the id for the document. Overrides the id that may be inferred from the document.
		/// </summary>
		public CreateDescriptor<TDocument> Id(Id id) => Assign(a => a.RouteValues.Required("id", id));
	}
}

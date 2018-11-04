using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(CreateJsonConverter))]
	public interface ICreateRequest : IRequest<CreateRequestParameters>, IUntypedDocumentRequest { }

	public partial interface ICreateRequest<TDocument> : ICreateRequest where TDocument : class
	{
		TDocument Document { get; set; }
	}

	public partial class CreateRequest<TDocument> where TDocument : class
	{
		public TDocument Document { get; set; }

		object IUntypedDocumentRequest.UntypedDocument => Document;

		partial void DocumentFromPath(TDocument document) => Document = document;
	}

	[DescriptorFor("Create")]
	public partial class CreateDescriptor<TDocument> where TDocument : class
	{
		TDocument ICreateRequest<TDocument>.Document { get; set; }

		object IUntypedDocumentRequest.UntypedDocument => Self.Document;

		partial void DocumentFromPath(TDocument document) => Assign(a => a.Document = document);

		/// <summary>
		/// Sets the id for the document. Overrides the id that may be inferred from the document.
		/// </summary>
		public CreateDescriptor<TDocument> Id(Id id) => Assign(a => a.RouteValues.Required("id", id));
	}
}

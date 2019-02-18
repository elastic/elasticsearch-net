using System.IO;
using Elasticsearch.Net;

namespace Nest
{
	[MapsApi("create.json")]
	[ContractJsonConverter(typeof(CreateJsonConverter))]
	public partial interface ICreateRequest<TDocument> : IProxyRequest where TDocument : class
	{
		TDocument Document { get; set; }
	}

	public partial class CreateRequest<TDocument> where TDocument : class
	{
		public TDocument Document { get; set; }

		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Document, stream, formatting);

		partial void DocumentFromPath(TDocument document) => Document = document;

		private TDocument AutoRouteDocument() => Document;
	}

	public partial class CreateDescriptor<TDocument> where TDocument : class
	{
		TDocument ICreateRequest<TDocument>.Document { get; set; }

		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Self.Document, stream, formatting);

		partial void DocumentFromPath(TDocument document) => Assign(a => a.Document = document);

		private TDocument AutoRouteDocument() => Self.Document;

		/// <summary>
		/// Sets the id for the document. Overrides the id that may be inferred from the document.
		/// </summary>
		public CreateDescriptor<TDocument> Id(Id id) => Assign(a => a.RouteValues.Required("id", id));
	}
}

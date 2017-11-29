using System.IO;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(CreateJsonConverter))]
	public partial interface ICreateRequest<TDocument> : IProxyRequest where TDocument : class
	{
		TDocument Document { get; set; }
	}

	public partial class CreateRequest<TDocument> where TDocument : class
	{
		public TDocument Document { get; set; }

		partial void DocumentFromPath(TDocument document) => this.Document = document;

		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(this.Document, stream, formatting);

	}

	[DescriptorFor("Create")]
	public partial class CreateDescriptor<TDocument> where TDocument : class
	{
		TDocument ICreateRequest<TDocument>.Document { get; set; }

		partial void DocumentFromPath(TDocument document) => Assign(a => a.Document = document);

		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Self.Document, stream, formatting);

		/// <summary>
		/// Sets the id for the document. Overrides the id that may be inferred from the document.
		/// </summary>
		public CreateDescriptor<TDocument> Id(Id id) => Assign(a => a.RouteValues.Required("id", id));
	}
}

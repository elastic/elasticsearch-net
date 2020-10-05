// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Transport;
using Nest.Utf8Json;

namespace Nest
{
	[MapsApi("create.json")]
	[JsonFormatter(typeof(CreateRequestFormatter<>))]
	public partial interface ICreateRequest<TDocument> : IProxyRequest, IDocumentRequest where TDocument : class
	{
		TDocument Document { get; set; }
	}

	public partial class CreateRequest<TDocument> where TDocument : class
	{
		public TDocument Document { get; set; }

		void IProxyRequest.WriteJson(ITransportSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Document, stream, formatting);

		partial void DocumentFromPath(TDocument document) => Document = document;
	}

	public partial class CreateDescriptor<TDocument> where TDocument : class
	{
		TDocument ICreateRequest<TDocument>.Document { get; set; }

		void IProxyRequest.WriteJson(ITransportSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Self.Document, stream, formatting);

		partial void DocumentFromPath(TDocument document) => Assign(document, (a, v) => a.Document = v);

		/// <summary>
		/// Sets the id for the document. Overrides the id that may be inferred from the document.
		/// </summary>
		public CreateDescriptor<TDocument> Id(Id id) => Assign(id,(a,v) => a.RouteValues.Required("id", v));
	}
}

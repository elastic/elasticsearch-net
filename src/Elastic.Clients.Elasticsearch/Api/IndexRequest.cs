// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public partial class IndexRequest<TDocument> : ICustomJsonWriter
	{
		public IndexRequest() : this(typeof(TDocument)) { }

		//public IndexRequest(TDocument document) : this(typeof(TDocument)) => Document = document;

		public IndexRequest(TDocument document, Id id) : this(typeof(TDocument), id) => Document = document;

		protected override HttpMethod? DynamicHttpMethod => GetHttpMethod(this);

		public IndexRequest(TDocument document, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(document)) => Document = document;

		internal IRequest<IndexRequestParameters> Self => this;

		[JsonIgnore] private Id? Id => Self.RouteValues.Get<Id>("id");

		void ICustomJsonWriter.WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer) => SourceSerialisation.Serialize(Document, writer, sourceSerializer);

		internal static HttpMethod GetHttpMethod(IndexRequest<TDocument> request) =>
			request.Id?.StringOrLongValue != null || request.Self.RouteValues.ContainsId ? HttpMethod.PUT : HttpMethod.POST;
	}

	public sealed partial class IndexRequestDescriptor<TDocument> : ICustomJsonWriter
	{
		// TODO: Codegen
		public IndexRequestDescriptor(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Elasticsearch.Id.From(documentWithId)) => DocumentFromPath(documentWithId);

		// ??
		private void DocumentFromPath(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);

		// TODO: Codegen
		// Perhaps not required as this should always be set via ctors
		public IndexRequestDescriptor<TDocument> Index(IndexName index) => Assign(index, (a, v) => a.RouteValues.Required("index", v));

		// TODO: Codegen
		public IndexRequestDescriptor<TDocument> Document(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);

		internal Id _id;

		public void WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer) => SourceSerialisation.Serialize(DocumentValue, writer, sourceSerializer);

		// TODO: Codegen for optional params
		public IndexRequestDescriptor<TDocument> Id(Id id) => Assign(id, (a, v) => a.RouteValues.Optional("id", v));
	
		protected override HttpMethod? DynamicHttpMethod => _id is not null || RouteValues.ContainsId ? HttpMethod.PUT : HttpMethod.POST;
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Ingest.Simulate;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public partial class IndexRequest<TDocument> : ICustomJsonWriter
	{
		public IndexRequest() : this(typeof(TDocument)) { }

		public IndexRequest(TDocument document) : this(typeof(TDocument)) => Document = document;

		public IndexRequest(TDocument document, Id id) : this(typeof(TDocument), id) => Document = document;

		protected override HttpMethod? DynamicHttpMethod => GetHttpMethod(this);

		internal IRequest<IndexRequestParameters> Self => this;

		[JsonIgnore] public TDocument Document { get; set; } // TODO: This can be code-generated

		[JsonIgnore] private Id? Id => Self.RouteValues.Get<Id>("id");

		void ICustomJsonWriter.WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer) => SourceSerialisation.Serialize(Document, writer, sourceSerializer);
		//{
			//if (sourceSerializer is DefaultHighLevelSerializer defaultSerializer)
			//{
			//	JsonSerializer.Serialize(writer, Document, defaultSerializer.Options);
			//}
			//else
			//{
			//	// If we're in a custom serializer then we can't be quite so efficient

			//	// TODO: Review perf
			//	using var ms = new MemoryStream();
			//	sourceSerializer.Serialize(Document, ms); // TODO - This needs to camelCase
			//	ms.Position = 0;
			//	using var
			//		document = JsonDocument
			//			.Parse(ms); // This is not super efficient but a variant on the suggestion at https://github.com/dotnet/runtime/issues/1784#issuecomment-608331125
			//	document.RootElement.WriteTo(writer);

			//	// Perhaps can be improved in .NET 6/ updated system.text.json - https://github.com/dotnet/runtime/pull/53212
			//}
		//}

		internal static HttpMethod GetHttpMethod(IndexRequest<TDocument> request) =>
			request.Id is not null || request.Self.RouteValues.ContainsId ? HttpMethod.PUT : HttpMethod.POST;
		//request.Id?.StringOrLongValue != null || request.RouteValues.ContainsId ? HttpMethod.PUT : HttpMethod.POST;
	}

	internal static class SourceSerialisation
	{
		public static void Serialize<T>(T toSerialize, Utf8JsonWriter writer, Serializer sourceSerializer)
		{
			if (sourceSerializer is DefaultHighLevelSerializer defaultSerializer)
			{
				JsonSerializer.Serialize(writer, toSerialize, defaultSerializer.Options);
			}
			else
			{
				// We cannot use STJ since the implementation may use another serialiser.
				// This path is a little less optimal
				using var ms = new MemoryStream();
				sourceSerializer.Serialize(toSerialize, ms);
				ms.Position = 0;
#if NET6_0_OR_GREATER
				writer.WriteRawValue(ms.GetBuffer());
#else
				// If we're in a custom serializer then we can't be quite so efficient
				using var document = JsonDocument.Parse(ms); // This is not super efficient but a variant on the suggestion at https://github.com/dotnet/runtime/issues/1784#issuecomment-608331125
				document.RootElement.WriteTo(writer);
#endif
			}
		}
	}

	public sealed partial class IndexRequestDescriptor<TDocument> : ICustomJsonWriter
	{
		internal TDocument _document;
		internal Id _id;

		public void WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer) => SourceSerialisation.Serialize(_document, writer, sourceSerializer);

		internal IndexRequestDescriptor<TDocument> Document(TDocument document) => Assign(document, (a, v) => a._document = v);

		// TODO: We should be able to generate these for optional params
		public IndexRequestDescriptor<TDocument> Id(Id id)
		{
			RouteValues.Optional("id", id);
			return this;
		}

		protected override HttpMethod? DynamicHttpMethod =>	_id is not null || RouteValues.ContainsId ? HttpMethod.PUT : HttpMethod.POST;
	}
}

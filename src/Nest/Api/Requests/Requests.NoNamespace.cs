using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Nest
{
	public partial interface IIndexRequest<TDocument> : IProxyRequest
	{
		TDocument Document { get; set; } // TODO - This should be generated based on the spec
		Id? Id { get; }
	}

	public partial class IndexRequest<TDocument> : IProxyRequest
	{
		protected override HttpMethod? DynamicHttpMethod => GetHttpMethod(this);

		[JsonIgnore] public TDocument Document { get; set; }

		[JsonIgnore] Id IIndexRequest<TDocument>.Id => Self.RouteValues.Get<Id>("id");

		void IProxyRequest.WriteJson(Utf8JsonWriter writer, ITransportSerializer sourceSerializer)
		{
			// TODO: Review perf
			using var ms = new MemoryStream();
			sourceSerializer.Serialize(Document, ms); // TODO - This needs to camelCase
			ms.Position = 0;
			using var
				document = JsonDocument
					.Parse(ms); // This is not super efficient but a variant on the suggestion at https://github.com/dotnet/runtime/issues/1784#issuecomment-608331125
			document.RootElement.WriteTo(writer);

			// Perhaps can be improved in .NET 6/ updated system.text.json - https://github.com/dotnet/runtime/pull/53212
		}

		internal static HttpMethod GetHttpMethod(IIndexRequest<TDocument> request) =>
			request.Id?.StringOrLongValue != null || request.RouteValues.ContainsId ? HttpMethod.PUT : HttpMethod.POST;
	}

	public partial class IndexDescriptor<TDocument>
	{
		public void WriteJson(Utf8JsonWriter writer, ITransportSerializer sourceSerializer) =>
			throw new NotImplementedException();

		Id? IIndexRequest<TDocument>.Id => Self.RouteValues.Get<Id>("id");

		TDocument IIndexRequest<TDocument>.Document { get; set; }
	}
}

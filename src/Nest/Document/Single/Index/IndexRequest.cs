using System.IO;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(IndexJsonConverter))]
	public partial interface IIndexRequest<TDocument> : IProxyRequest where TDocument : class
	{
		TDocument Document { get; set; }
	}

	public partial class IndexRequest<TDocument>
		where TDocument : class
	{
		public TDocument Document { get; set; }

		protected void DefaultRouting() => this.Self.RequestParameters.SetQueryString("routing", new Routing(() => Document));

		protected override HttpMethod HttpMethod => GetHttpMethod(this);

		internal static HttpMethod GetHttpMethod(IIndexRequest<TDocument> request) => request.Id.IsConditionless() ? HttpMethod.POST : HttpMethod.PUT;

		partial void DocumentFromPath(TDocument document) => this.Document = document;

		private TDocument AutoRouteDocument() => Self.Document;

		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(this.Document, stream, formatting);

	}

	public partial class IndexDescriptor<TDocument>  where TDocument : class
	{
		TDocument IIndexRequest<TDocument>.Document { get; set; }

		protected override HttpMethod HttpMethod => IndexRequest<TDocument>.GetHttpMethod(this);

		partial void DocumentFromPath(TDocument document) => Assign(a => a.Document = document);
		private TDocument AutoRouteDocument() => Self.Document;

		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Self.Document, stream, formatting);

	}

}

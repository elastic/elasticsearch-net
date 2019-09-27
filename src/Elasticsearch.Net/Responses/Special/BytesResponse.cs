using Elasticsearch.Net.Extensions;

namespace Elasticsearch.Net
{
	public class BytesResponse : ElasticsearchResponse<byte[]>
	{
		public BytesResponse() { }

		public BytesResponse(byte[] body) => Body = body;

		public override bool TryGetServerError(out ServerError serverError)
		{
			serverError = null;
			if (Body == null || Body.Length == 0 || ResponseMimeType != RequestData.MimeType)
				return false;

			using(var stream = ConnectionConfiguration.MemoryStreamFactory.Create(Body))
				return ServerError.TryCreate(stream, out serverError);
		}
	}
}

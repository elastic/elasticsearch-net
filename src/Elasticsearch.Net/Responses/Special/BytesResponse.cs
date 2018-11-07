using System.IO;

namespace Elasticsearch.Net
{
	public class BytesResponse : ElasticsearchResponse<byte[]>
	{
		public BytesResponse() { }

		public BytesResponse(byte[] body) => Body = body;

		public bool TryGetServerError(out ServerError serverError)
		{
			serverError = null;
			if (Body == null || Body.Length == 0 || ResponseMimeType != RequestData.MimeType)
				return false;

			using (var stream = new MemoryStream(Body))
				return ServerError.TryCreate(stream, out serverError);
		}

		protected override bool TryGetServerErrorReason(out string reason)
		{
			reason = null;
			if (!TryGetServerError(out var serverError)) return false;

			reason = serverError?.Error?.ToString();
			return !reason.IsNullOrEmpty();
		}
	}
}

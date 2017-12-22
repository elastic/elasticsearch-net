using System.IO;
using System.Text;

namespace Elasticsearch.Net
{
	public class BytesResponse : ElasticsearchResponse<byte[]>
	{
		public BytesResponse() { }
		public BytesResponse(byte[] body) => this.Body = body;

		public bool TryGetServerError(out ServerError serverError)
		{
			serverError = null;
			if (this.Body == null || this.Body.Length == 0) return false;
			using(var stream = new MemoryStream(this.Body))
				serverError = ServerError.Create(stream);
			return true;
		}

		protected override bool TryGetServerErrorReason(out string reason)
		{
			reason = null;
			if (!this.TryGetServerError(out var serverError)) return false;
			reason = serverError?.Error?.ToString();
			return !reason.IsNullOrEmpty();
		}
	}
}

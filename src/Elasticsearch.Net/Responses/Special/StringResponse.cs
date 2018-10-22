using System.IO;
using System.Text;

namespace Elasticsearch.Net
{
	public class StringResponse : ElasticsearchResponse<string>
	{
		public StringResponse() { }
		public StringResponse(string body) => this.Body = body;

		public bool TryGetServerError(out ServerError serverError)
		{
			serverError = null;
			if (string.IsNullOrEmpty(this.Body)) return false;
			using(var stream = RecyclableMemoryStreamFactory.Default.Create(Encoding.UTF8.GetBytes(this.Body)))
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

using System.Text;
using Elasticsearch.Net.Extensions;

namespace Elasticsearch.Net
{
	public class StringResponse : ElasticsearchResponse<string>
	{
		public StringResponse() { }

		public StringResponse(string body) => Body = body;

		public override bool TryGetServerError(out ServerError serverError)
		{
			serverError = null;
			if (string.IsNullOrEmpty(Body) || ResponseMimeType != RequestData.MimeType)
				return false;

			using(var stream = ConnectionConfiguration.MemoryStreamFactory.Create(Encoding.UTF8.GetBytes(Body)))
				return ServerError.TryCreate(stream, out serverError);
		}
	}
}

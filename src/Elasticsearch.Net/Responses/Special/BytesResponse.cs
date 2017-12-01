namespace Elasticsearch.Net
{
	public class BytesResponse : ElasticsearchResponse<byte[]>
	{
		public BytesResponse(byte[] body) => this.Body = body;
	}
}

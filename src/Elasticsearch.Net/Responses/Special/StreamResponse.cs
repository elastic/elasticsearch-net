using System;
using System.IO;

namespace Elasticsearch.Net
{
	public class StreamResponse : ElasticsearchResponse<Stream>
	{
		public StreamResponse(Stream body) => this.Body = body;
	}
}

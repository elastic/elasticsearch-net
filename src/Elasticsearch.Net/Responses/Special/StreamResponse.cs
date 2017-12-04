using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Elasticsearch.Net
{
	public class StreamResponse : ElasticsearchResponse<Stream>
	{
		public StreamResponse() => this.Body = Stream.Null;
		public StreamResponse(Stream body) => this.Body = body;
	}
}

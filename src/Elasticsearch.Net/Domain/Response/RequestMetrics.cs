using System;

namespace Elasticsearch.Net
{
	public class RequestMetrics
	{
		public RequestType RequestType { get; set; }
		public Uri Node { get; set; }
		public DateTime StartedOn { get; set; }
		public long EllapsedMilliseconds { get; set; }
		public string Path { get; set;  }
		public int? HttpStatusCode { get; set; }
		public bool Success { get; set; }
	}
}
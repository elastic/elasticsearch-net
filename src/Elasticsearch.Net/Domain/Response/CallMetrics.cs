using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	//TODO decide on this vs audit trail, combine?
	public class CallMetrics
	{
		public string Path { get; set; }
		public long SerializationTime { get; set; }
		public long DeserializationTime { get; set; }
		public DateTime StartedOn { get; set; }
		public DateTime CompletedOn { get; set; }
		public List<RequestMetrics> Requests { get; set; }
	}
}
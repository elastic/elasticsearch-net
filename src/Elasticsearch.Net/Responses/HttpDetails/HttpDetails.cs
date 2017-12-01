using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	public class HttpDetails : IApiCallDetails
	{
		public bool Success { get; set; }
		public Exception OriginalException { get; set; }
		public ServerError ServerError { get; set; }
		public HttpMethod HttpMethod { get; set; }
		public Uri Uri { get; set; }
		public int? HttpStatusCode { get; set; }
		public bool SuccessOrKnownError { get; set; }
		public byte[] ResponseBodyInBytes { get; set; }
		public byte[] RequestBodyInBytes { get; set; }
		public List<Audit> AuditTrail { get; set; }
		public string DebugInformation { get; set; }
		public IEnumerable<string> DeprecationWarnings { get; set; }
	}
}

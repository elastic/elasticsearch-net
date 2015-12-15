using System;
using System.Collections.Generic;
using System.Threading;

namespace Elasticsearch.Net
{
	public class RequestConfiguration : IRequestConfiguration
	{
		public TimeSpan? RequestTimeout { get; set; }
		public TimeSpan? ConnectTimeout { get; set; }
		public string ContentType { get; set; }
		public int? MaxRetries { get; set; }
		public Uri ForceNode { get; set; }
		public bool? DisableSniff { get; set; }
		public bool? DisablePing { get; set; }
		public IEnumerable<int> AllowedStatusCodes { get; set; }
		public BasicAuthenticationCredentials BasicAuthenticationCredentials { get; set; }
		public bool EnableHttpPipelining { get; set; }
		public CancellationToken CancellationToken { get; set; }
	}
}
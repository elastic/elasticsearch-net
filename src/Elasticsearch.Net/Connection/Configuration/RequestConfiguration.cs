using Elasticsearch.Net.Connection.Security;
using System;
using System.Collections.Generic;
using System.Net;

namespace Elasticsearch.Net.Connection.Configuration
{
	public class RequestConfiguration : IRequestConfiguration
	{
		public int? RequestTimeout { get; set; }
		public int? ConnectTimeout { get; set; }
		public string ContentType { get; set; }
		public int? MaxRetries { get; set; }
		public Uri ForceNode { get; set; }
		public bool? DisableSniff { get; set; }
		public bool? DisablePing { get; set; }
		public IEnumerable<int> AllowedStatusCodes { get; set; }
		public BasicAuthorizationCredentials BasicAuthorizationCredentials { get; set; }
		public bool EnableHttpPipelining { get; set; }
		public Action<HttpWebRequest, byte[]> RequestSigner { get; set; }
	}
}
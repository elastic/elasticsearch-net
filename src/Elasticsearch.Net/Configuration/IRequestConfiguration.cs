using System;
using System.Collections.Generic;
using System.Threading;

namespace Elasticsearch.Net
{
	/// TODO some of these settings are not used
	/// this indicates they are not properly tested
	public interface IRequestConfiguration 
	{
		/// <summary>
		/// The timeout for this specific request, takes precedence over the global timeout settings
		/// </summary>
		TimeSpan? RequestTimeout { get; set; }

		/// <summary>
		/// The connect timeout for this specific request
		/// </summary>
		TimeSpan? ConnectTimeout { get; set;  }

		/// <summary>
		/// Force a difference content type header on the request
		/// </summary>
		string ContentType { get; set; }
		
		/// <summary>
		/// This will override whatever is set on the connection configuration or whatever default the connectionpool has.
		/// </summary>
		int? MaxRetries { get; set; }

		/// <summary>
		/// This will force the operation on the specified node, this will bypass any configured connection pool and will no retry.
		/// </summary>
		Uri ForceNode { get; set; }

		/// <summary>
		/// Forces no sniffing to occur on the request no matter what configuration is in place 
		/// globally
		/// </summary>
		bool? DisableSniff { get; set; }

		/// <summary>
		/// Under no circumstance do a ping before the actual call. If a node was previously dead a small ping with 
		/// low connect timeout will be tried first in normal circumstances
		/// </summary>
		bool? DisablePing { get; set; }

		/// <summary>
		/// Treat the following statuses (on top of the 200 range) NOT as error.
		/// </summary>
		IEnumerable<int> AllowedStatusCodes { get; set; }

		/// <summary>
		/// Basic access authorization credentials to specify with this request.
		/// Overrides any credentials that are set at the global IConnectionSettings level.
		/// </summary>
		BasicAuthenticationCredentials BasicAuthenticationCredentials { get; set; }

		/// <summary>
		/// Whether or not this request should be pipelined. http://en.wikipedia.org/wiki/HTTP_pipelining
		/// <para>Note: HTTP pipelining must also be enabled in Elasticsearch for this to work properly.</para>
		/// </summary>
		bool EnableHttpPipelining { get; set; }

		/// <summary>
		/// The cancellation token to use to internally to cancel async operations
		/// </summary>
		CancellationToken CancellationToken { get; set; }
	}
}

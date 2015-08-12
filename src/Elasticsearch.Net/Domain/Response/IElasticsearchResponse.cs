using System;
using System.Collections.Generic;
using System.Diagnostics;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net
{
	public interface IElasticsearchResponse
	{
		/// <summary>
		/// The response status code is in the 200 range or is in the allowed list of status codes set on the request.
		/// </summary>
		bool Success { get; }
		
		/// <summary>
		/// The request settings used by the request responsible for this response
		/// </summary>
		IConnectionConfigurationValues Settings { get; }
		
		/// <summary>
		/// If Success is false this will hold the original exception.
		/// Can be a CLR exception or a mapped server side exception (ElasticsearchServerException)
		/// </summary>
		Exception OriginalException { get; }
		
		/// <summary>
		/// The HTTP method used by the request
		/// </summary>
		HttpMethod RequestMethod { get; }
		
		/// <summary>
		/// The url as requested 
		/// </summary>
		Uri RequestUri { get; }
		
		/// <summary>
		/// The status code as returned by Elasticsearch 
		/// </summary>
		int? HttpStatusCode { get; }

		/// <summary>
		/// The number of times to request had to be retried before succeeding on a live node
		/// </summary>
		int NumberOfRetries { get; }

		/// <summary>
		/// Returns timing and stats metric about the current API method invocation. 
		/// </summary>
		CallMetrics Metrics { get; }

		/// <summary>
		/// The raw byte response, only set when IncludeRawResponse() is set on Connection configuration
		/// </summary>
		[DebuggerDisplay("{ResponseRaw != null ? System.Text.Encoding.UTF8.GetString(ResponseRaw) : null,nq}")]
		byte[] ResponseRaw { get; }

		[DebuggerDisplay("{Request != null ? System.Text.Encoding.UTF8.GetString(Request) : null,nq}")]
		byte[] Request { get; }

		List<Audit> AuditTrail { get; }

	}


	public interface IResponseWithRequestInformation
	{
		IElasticsearchResponse RequestInformation { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Elasticsearch.Net
{
	public interface IApiCallDetails
	{
		/// <summary>
		/// The response status code is in the 200 range or is in the allowed list of status codes set on the request.
		/// </summary>
		bool Success { get; }
		
		/// <summary>
		/// If Success is false this will hold the original exception.
		/// This will be the orginating CLR exception in most cases.
		/// </summary>
		Exception OriginalException { get; }

		/// <summary>
		/// The error returned by Elasticsearch
		/// </summary>
		ServerError ServerError { get; }

		/// <summary>
		/// The HTTP method used by the request
		/// </summary>
		HttpMethod HttpMethod { get; }
		
		/// <summary>
		/// The url as requested 
		/// </summary>
		Uri Uri { get; }
		
		/// <summary>
		/// The HTTP status code as returned by Elasticsearch 
		/// </summary>
		int? HttpStatusCode { get; }

		/// <summary>
		/// The raw byte response, only set when IncludeRawResponse() is set on Connection configuration
		/// </summary>
		[DebuggerDisplay("{ResponseBodyInBytes != null ? System.Text.Encoding.UTF8.GetString(ResponseBodyInBytes) : null,nq}")]
		byte[] ResponseBodyInBytes { get; }

		[DebuggerDisplay("{RequestBodyInBytes != null ? System.Text.Encoding.UTF8.GetString(RequestBodyInBytes) : null,nq}")]
		byte[] RequestBodyInBytes { get; }

		List<Audit> AuditTrail { get; }

		string DebugInformation { get; }
	}
}
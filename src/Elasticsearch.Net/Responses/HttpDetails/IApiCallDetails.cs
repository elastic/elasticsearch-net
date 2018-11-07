using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Details about the API call
	/// </summary>
	public interface IApiCallDetails
	{

		//TODO: Get rid of setter
        /// <summary>
        /// An audit trail of requests made to nodes within the cluster
        /// </summary>
        List<Audit> AuditTrail { get; set; }

		/// <summary>
		/// A lazy human readable string representation of what happened during this request for both successful and
		/// failed requests.
		/// </summary>
		string DebugInformation { get; }

		/// <summary>
		/// A collection of deprecation warnings returned from Elasticsearch.
		/// <para>Used to signal that the request uses an API feature that is marked as deprecated</para>
		/// </summary>
		IEnumerable<string> DeprecationWarnings { get; }

		/// <summary>
		/// The HTTP method used by the request
		/// </summary>
		HttpMethod HttpMethod { get; }

		/// <summary>
		/// The HTTP status code as returned by Elasticsearch
		/// </summary>
		int? HttpStatusCode { get; }

		/// <summary>
		/// If <see cref="Success"/> is <c>false</c>, this will hold the original exception.
		/// This will be the originating CLR exception in most cases.
		/// </summary>
		Exception OriginalException { get; }

		/// <summary>
		/// The request body bytes.
		/// <para>NOTE: Only set when disable direct streaming is set for the request</para>
		/// </summary>
		[DebuggerDisplay("{RequestBodyInBytes != null ? System.Text.Encoding.UTF8.GetString(RequestBodyInBytes) : null,nq}")]
		byte[] RequestBodyInBytes { get; }

		/// <summary>
		/// The response body bytes.
		/// <para>NOTE: Only set when disable direct streaming is set for the request</para>
		/// </summary>
		[DebuggerDisplay("{ResponseBodyInBytes != null ? System.Text.Encoding.UTF8.GetString(ResponseBodyInBytes) : null,nq}")]
		byte[] ResponseBodyInBytes { get; }

		/// <summary>The response MIME type </summary>
		string ResponseMimeType { get; }

		/// <summary>
		/// The response status code is in the 200 range or is in the allowed list of status codes set on the request.
		/// </summary>
		bool Success { get; }

		/// <summary>
		/// The response is successful or has a response code between 400-599, the call should not be retried.
		/// Only on 502,503 and 504 will this return false;
		/// </summary>
		bool SuccessOrKnownError { get; }

		/// <summary>
		/// The url as requested
		/// </summary>
		Uri Uri { get; }
	}
}

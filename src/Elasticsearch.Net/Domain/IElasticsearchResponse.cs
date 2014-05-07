using System;
using System.Diagnostics;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net
{
	public interface IElasticsearchResponse
	{
		bool Success { get; }
		bool SuccessOrKnownError { get; }
		IConnectionConfigurationValues Settings { get; }
		Exception OriginalException { get; }
		string RequestMethod { get; }
		string RequestUrl { get; }
		[DebuggerDisplay("{Request != null ? System.Text.Encoding.UTF8.GetString(Request) : null,nq}")]
		byte[] Request { get; }
		int? HttpStatusCode { get; }
		int NumberOfRetries { get; }

		/// <summary>
		/// The raw byte response, only set when IncludeRawResponse() is set on Connection configuration
		/// </summary>
		[DebuggerDisplay("{ResponseRaw != null ? System.Text.Encoding.UTF8.GetString(ResponseRaw) : null,nq}")]
		byte[] ResponseRaw { get; }
	}

	public interface IResponseWithRequestInformation
	{
		IElasticsearchResponse RequestInformation { get; set; }
	}
}
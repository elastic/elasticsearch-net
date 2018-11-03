using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A response from Elasticsearch including details about the request/response life cycle
	/// </summary>
	public abstract class ElasticsearchResponseBase : IApiCallDetails, IElasticsearchResponse
	{
		public IApiCallDetails ApiCall { get; set; }
		public List<Audit> AuditTrail => ApiCall.AuditTrail;

		public string DebugInformation => ApiCall.DebugInformation;
		public IEnumerable<string> DeprecationWarnings => ApiCall.DeprecationWarnings;
		public HttpMethod HttpMethod => ApiCall.HttpMethod;
		public int? HttpStatusCode => ApiCall.HttpStatusCode;
		public Exception OriginalException => ApiCall.OriginalException;

		/// <summary>The raw byte request message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] RequestBodyInBytes => ApiCall.RequestBodyInBytes;

		/// <summary>The raw byte response message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] ResponseBodyInBytes => ApiCall.ResponseBodyInBytes;

		public string ResponseMimeType => ApiCall.ResponseMimeType;

		public bool Success => ApiCall.Success;
		public bool SuccessOrKnownError => ApiCall.SuccessOrKnownError;
		public Uri Uri => ApiCall.Uri;

		//ignored
		List<Audit> IApiCallDetails.AuditTrail { get; set; }

		bool IElasticsearchResponse.TryGetServerErrorReason(out string reason) => TryGetServerErrorReason(out reason);

		protected virtual bool TryGetServerErrorReason(out string reason)
		{
			reason = null;
			return false;
		}

		public override string ToString() => ApiCall.ToString();
	}

	/// <summary>
	/// A response from Elasticsearch including details about the request/response life cycle. Base class for the built in low level response
	/// types: <see cref="StringResponse" /> <see cref="BytesResponse" /> and <see cref="DynamicResponse" />
	/// </summary>
	public abstract class ElasticsearchResponse<T> : ElasticsearchResponseBase
	{
		public T Body { get; protected internal set; }
	}
}

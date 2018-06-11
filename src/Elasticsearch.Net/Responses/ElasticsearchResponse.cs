using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A response from Elasticsearch including details about the request/response life cycle
	/// </summary>
	public abstract class ElasticsearchResponseBase : IApiCallDetails, IElasticsearchResponse
	{
		public IApiCallDetails ApiCall { get; set; }

		bool IElasticsearchResponse.TryGetServerErrorReason(out string reason) => this.TryGetServerErrorReason(out reason);

		protected virtual bool TryGetServerErrorReason(out string reason)
		{
			reason = null;
			return false;
		}

		//ignored
		List<Audit> IApiCallDetails.AuditTrail { get; set; }

		public bool Success => this.ApiCall.Success;
		public string ResponseMimeType => this.ApiCall.ResponseMimeType;
		public HttpMethod HttpMethod => this.ApiCall.HttpMethod;
		public Uri Uri => this.ApiCall.Uri;
		public int? HttpStatusCode => this.ApiCall.HttpStatusCode;
		public List<Audit> AuditTrail => this.ApiCall.AuditTrail;
		public IEnumerable<string> DeprecationWarnings => this.ApiCall.DeprecationWarnings;
		public bool SuccessOrKnownError => this.ApiCall.SuccessOrKnownError;
		public Exception OriginalException => this.ApiCall.OriginalException;

		/// <summary>The raw byte request message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] RequestBodyInBytes => this.ApiCall.RequestBodyInBytes;

		/// <summary>The raw byte response message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] ResponseBodyInBytes => this.ApiCall.ResponseBodyInBytes;

		public string DebugInformation => this.ApiCall.DebugInformation;

		public override string ToString() => this.ApiCall.ToString();


	}

	/// <summary>
	/// A response from Elasticsearch including details about the request/response life cycle. Base class for the built in low level response
	/// types: <see cref="StringResponse"/> <see cref="BytesResponse"/> and <see cref="DynamicResponse"/>
	/// </summary>
	public abstract class ElasticsearchResponse<T> : ElasticsearchResponseBase
	{
		public T Body { get; protected internal set; }
	}
}

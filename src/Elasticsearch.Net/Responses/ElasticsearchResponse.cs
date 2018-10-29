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
		/// <inheritdoc />
		public IApiCallDetails ApiCall { get; set; }

		bool IElasticsearchResponse.TryGetServerErrorReason(out string reason) => this.TryGetServerErrorReason(out reason);

		protected virtual bool TryGetServerErrorReason(out string reason)
		{
			reason = null;
			return false;
		}

		// TODO: Remove?
		//ignored
		List<Audit> IApiCallDetails.AuditTrail { get; set; }

		/// <inheritdoc cref="IApiCallDetails.Success"/>
		public bool Success => this.ApiCall.Success;

		/// <inheritdoc cref="IApiCallDetails.ResponseMimeType"/>
		public string ResponseMimeType => this.ApiCall.ResponseMimeType;

		/// <inheritdoc cref="IApiCallDetails.HttpMethod"/>
		public HttpMethod HttpMethod => this.ApiCall.HttpMethod;

		/// <inheritdoc cref="IApiCallDetails.Uri"/>
		public Uri Uri => this.ApiCall.Uri;

		/// <inheritdoc cref="IApiCallDetails.HttpStatusCode"/>
		public int? HttpStatusCode => this.ApiCall.HttpStatusCode;

		/// <inheritdoc cref="IApiCallDetails.AuditTrail"/>
		public List<Audit> AuditTrail => this.ApiCall.AuditTrail;

		/// <inheritdoc cref="IApiCallDetails.DeprecationWarnings"/>
		public IEnumerable<string> DeprecationWarnings => this.ApiCall.DeprecationWarnings;

		/// <inheritdoc cref="IApiCallDetails.SuccessOrKnownError"/>
		public bool SuccessOrKnownError => this.ApiCall.SuccessOrKnownError;

		/// <inheritdoc cref="IApiCallDetails.OriginalException"/>
		public Exception OriginalException => this.ApiCall.OriginalException;

		/// <inheritdoc cref="IApiCallDetails.RequestBodyInBytes"/>
		public byte[] RequestBodyInBytes => this.ApiCall.RequestBodyInBytes;

		/// <inheritdoc cref="IApiCallDetails.ResponseBodyInBytes"/>
		public byte[] ResponseBodyInBytes => this.ApiCall.ResponseBodyInBytes;

		/// <inheritdoc cref="IApiCallDetails.DebugInformation"/>
		public string DebugInformation => this.ApiCall.DebugInformation;

		public override string ToString() => this.ApiCall.ToString();
	}

	/// <summary>
	/// A response from Elasticsearch including details about the request/response life cycle. Base class for the built in low level response
	/// types, <see cref="StringResponse"/>, <see cref="BytesResponse"/>, <see cref="DynamicResponse"/> and <see cref="VoidResponse"/>
	/// </summary>
	public abstract class ElasticsearchResponse<T> : ElasticsearchResponseBase
	{
		public T Body { get; protected internal set; }
	}
}

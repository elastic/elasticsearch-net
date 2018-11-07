using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A response from Elasticsearch including details about the request/response life cycle
	/// </summary>
	public abstract class ElasticsearchResponseBase : IApiCallDetails, IElasticsearchResponse
	{
		/// <inheritdoc />
		public IApiCallDetails ApiCall { get; set; }

		/// <inheritdoc cref="IApiCallDetails.AuditTrail"/>
		public List<Audit> AuditTrail => ApiCall.AuditTrail;

		/// <inheritdoc cref="IApiCallDetails.DebugInformation"/>
		public string DebugInformation => ApiCall.DebugInformation;

		/// <inheritdoc cref="IApiCallDetails.DeprecationWarnings"/>
		public IEnumerable<string> DeprecationWarnings => ApiCall.DeprecationWarnings;

		/// <inheritdoc cref="IApiCallDetails.HttpMethod"/>
		public HttpMethod HttpMethod => ApiCall.HttpMethod;

		/// <inheritdoc cref="IApiCallDetails.HttpStatusCode"/>
		public int? HttpStatusCode => ApiCall.HttpStatusCode;

		/// <inheritdoc cref="IApiCallDetails.OriginalException"/>
		public Exception OriginalException => ApiCall.OriginalException;

		/// <inheritdoc cref="IApiCallDetails.RequestBodyInBytes"/>
		public byte[] RequestBodyInBytes => ApiCall.RequestBodyInBytes;

		/// <inheritdoc cref="IApiCallDetails.ResponseBodyInBytes"/>
		public byte[] ResponseBodyInBytes => ApiCall.ResponseBodyInBytes;

		/// <inheritdoc cref="IApiCallDetails.ResponseMimeType"/>
		public string ResponseMimeType => ApiCall.ResponseMimeType;

		/// <inheritdoc cref="IApiCallDetails.Success"/>
		public bool Success => ApiCall.Success;

		/// <inheritdoc cref="IApiCallDetails.SuccessOrKnownError"/>
		public bool SuccessOrKnownError => ApiCall.SuccessOrKnownError;

		/// <inheritdoc cref="IApiCallDetails.Uri"/>
		public Uri Uri => ApiCall.Uri;

		// TODO: Remove?
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
	/// types, <see cref="StringResponse"/>, <see cref="BytesResponse"/>, <see cref="DynamicResponse"/> and <see cref="VoidResponse"/>
	/// </summary>
	public abstract class ElasticsearchResponse<T> : ElasticsearchResponseBase
	{
		public T Body { get; protected internal set; }
	}
}

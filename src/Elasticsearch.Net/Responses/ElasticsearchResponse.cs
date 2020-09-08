// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using Elasticsearch.Net.Diagnostics;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A response from Elasticsearch including details about the request/response life cycle
	/// </summary>
	public abstract class ElasticsearchResponseBase : IApiCallDetails, IElasticsearchResponse
	{
		/// <inheritdoc />
		public IApiCallDetails ApiCall { get; set; }

		/// <inheritdoc cref="IApiCallDetails.TcpStats"/>
		public ReadOnlyDictionary<TcpState, int> TcpStats
		{
			get => ApiCall.TcpStats;
			set => ApiCall.TcpStats = value;
		}

		/// <inheritdoc cref="IApiCallDetails.DebugInformation"/>
		public string DebugInformation => ApiCall.DebugInformation;
		/// <inheritdoc cref="IApiCallDetails.HttpMethod"/>
		public HttpMethod HttpMethod => ApiCall.HttpMethod;
		/// <inheritdoc cref="IApiCallDetails.AuditTrail"/>
		public List<Audit> AuditTrail
		{
			get => ApiCall.AuditTrail;
			set => ApiCall.AuditTrail = value;
		}

		/// <inheritdoc cref="IApiCallDetails.ThreadPoolStats"/>
		public ReadOnlyDictionary<string, ThreadPoolStatistics> ThreadPoolStats
		{
			get => ApiCall.ThreadPoolStats;
			set => ApiCall.ThreadPoolStats = value;
		}

		/// <inheritdoc cref="IApiCallDetails.DeprecationWarnings"/>
		public IEnumerable<string> DeprecationWarnings => ApiCall.DeprecationWarnings;
		/// <inheritdoc cref="IApiCallDetails.SuccessOrKnownError"/>
		public bool SuccessOrKnownError => ApiCall.SuccessOrKnownError;
		/// <inheritdoc cref="IApiCallDetails.HttpStatusCode"/>
		public int? HttpStatusCode => ApiCall.HttpStatusCode;

		/// <inheritdoc cref="IApiCallDetails.Success"/>
		public bool Success => ApiCall.Success;
		/// <inheritdoc cref="IApiCallDetails.OriginalException"/>
		public Exception OriginalException => ApiCall.OriginalException;
		/// <inheritdoc cref="IApiCallDetails.ResponseMimeType"/>
		public string ResponseMimeType => ApiCall.ResponseMimeType;
		/// <inheritdoc cref="IApiCallDetails.Uri"/>
		public Uri Uri => ApiCall.Uri;

		/// <inheritdoc cref="IApiCallDetails.ConnectionConfiguration"/>
		public IConnectionConfigurationValues ConnectionConfiguration => ApiCall.ConnectionConfiguration;

		/// <inheritdoc cref="IApiCallDetails.ResponseBodyInBytes"/>
		public byte[] ResponseBodyInBytes => ApiCall.ResponseBodyInBytes;

		/// <inheritdoc cref="IApiCallDetails.RequestBodyInBytes"/>
		public byte[] RequestBodyInBytes => ApiCall.RequestBodyInBytes;

		bool IElasticsearchResponse.TryGetServerErrorReason(out string reason) => TryGetServerErrorReason(out reason);

		public virtual bool TryGetServerError(out ServerError serverError)
		{
			serverError = null;
			var bytes = ApiCall.ResponseBodyInBytes;
			if (bytes == null || ResponseMimeType != RequestData.MimeType)
				return false;

			using(var stream = ConnectionConfiguration.MemoryStreamFactory.Create(bytes))
				return ServerError.TryCreate(stream, out serverError);
		}

		protected bool TryGetServerErrorReason(out string reason)
		{
			reason = null;
			if (!TryGetServerError(out var serverError)) return false;

			reason = serverError?.Error?.ToString();
			return !string.IsNullOrEmpty(reason);
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

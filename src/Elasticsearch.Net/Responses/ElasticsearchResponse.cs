// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using Elasticsearch.Net.Diagnostics;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A response from Elasticsearch including details about the request/response life cycle
	/// </summary>
	public abstract class ElasticsearchResponseBase : IApiCallDetails, IElasticsearchResponse
	{

		/// <inheritdoc />
		[JsonIgnore]
		public IApiCallDetails ApiCall { get; set; }


		/// <inheritdoc cref="IApiCallDetails.TcpStats"/>
		[JsonIgnore]
		public ReadOnlyDictionary<TcpState, int> TcpStats
		{
			get => ApiCall.TcpStats;
			set => ApiCall.TcpStats = value;
		}


		/// <inheritdoc cref="IApiCallDetails.DebugInformation"/>
		[JsonIgnore]
		public string DebugInformation => ApiCall.DebugInformation;
		/// <inheritdoc cref="IApiCallDetails.HttpMethod"/>
		[JsonIgnore]
		public HttpMethod HttpMethod => ApiCall.HttpMethod;
		/// <inheritdoc cref="IApiCallDetails.AuditTrail"/>
		[JsonIgnore]
		public List<Audit> AuditTrail
		{
			get => ApiCall.AuditTrail;
			set => ApiCall.AuditTrail = value;
		}

		/// <inheritdoc cref="IApiCallDetails.ThreadPoolStats"/>
		[JsonIgnore]
		public ReadOnlyDictionary<string, ThreadPoolStatistics> ThreadPoolStats
		{
			get => ApiCall.ThreadPoolStats;
			set => ApiCall.ThreadPoolStats = value;
		}

		/// <inheritdoc cref="IApiCallDetails.DeprecationWarnings"/>
		[JsonIgnore]
		public IEnumerable<string> DeprecationWarnings => ApiCall.DeprecationWarnings;
		/// <inheritdoc cref="IApiCallDetails.SuccessOrKnownError"/>
		[JsonIgnore]
		public bool SuccessOrKnownError => ApiCall.SuccessOrKnownError;
		/// <inheritdoc cref="IApiCallDetails.HttpStatusCode"/>
		[JsonIgnore]
		public int? HttpStatusCode => ApiCall.HttpStatusCode;

		/// <inheritdoc cref="IApiCallDetails.Success"/>
		[JsonIgnore]
		public bool Success => ApiCall.Success;
		/// <inheritdoc cref="IApiCallDetails.OriginalException"/>
		[JsonIgnore]
		public Exception OriginalException => ApiCall.OriginalException;
		/// <inheritdoc cref="IApiCallDetails.ResponseMimeType"/>
		[JsonIgnore]
		public string ResponseMimeType => ApiCall.ResponseMimeType;
		/// <inheritdoc cref="IApiCallDetails.Uri"/>
		[JsonIgnore]
		public Uri Uri => ApiCall.Uri;

		/// <inheritdoc cref="IApiCallDetails.ConnectionConfiguration"/>
		[JsonIgnore]
		public IConnectionConfigurationValues ConnectionConfiguration => ApiCall.ConnectionConfiguration;

		/// <inheritdoc cref="IApiCallDetails.ResponseBodyInBytes"/>
		[JsonIgnore]
		public byte[] ResponseBodyInBytes => ApiCall.ResponseBodyInBytes;

		/// <inheritdoc cref="IApiCallDetails.RequestBodyInBytes"/>
		[JsonIgnore]
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

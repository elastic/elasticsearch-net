// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using System.Text.Json.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;

namespace Nest
{
	public abstract class ResponseBase : IResponse
	{
		private Error? _error;
		private IApiCallDetails? _originalApiCall;
		private ServerError? _serverError;
		private int? _statusCode;

		/// <summary> Returns useful information about the request(s) that were part of this API call. </summary>
		public virtual IApiCallDetails? ApiCall => _originalApiCall;

		/// <inheritdoc />
		public string DebugInformation
		{
			get
			{
				var sb = new StringBuilder();
				sb.Append($"{(!IsValid ? "Inv" : "V")}alid NEST response built from a ");
				sb.AppendLine(ApiCall?.ToString().ToCamelCase() ?? "null ApiCall which is highly exceptional, please open a bug if you see this");
				if (!IsValid) DebugIsValid(sb);
				if (ApiCall != null) ResponseStatics.DebugInformationBuilder(ApiCall, sb);
				return sb.ToString();
			}
		}

		/// <inheritdoc />
		public virtual bool IsValid
		{
			get
			{
				var statusCode = ApiCall?.HttpStatusCode;
				if (statusCode == 404) return false;
				return (ApiCall?.Success ?? false) && ServerError is null;
			}
		}
		
		/// <inheritdoc />
		public Exception? OriginalException => ApiCall?.OriginalException;

		/// <inheritdoc />
		public ServerError? ServerError
		{
			get
			{
				if (_serverError is not null) return _serverError;
				if (_error is null) return null;

				_serverError = new ServerError(_error, _statusCode);
				return _serverError;
			}
		}
		
		[JsonPropertyName("error")]
		internal Error? Error
		{
			get => _error;
			set
			{
				_error = value;
				_serverError = null;
			}
		}
		
		[JsonPropertyName("status")]
		internal int? StatusCode
		{
			get => _statusCode;
			set
			{
				_statusCode = value;
				_serverError = null;
			}
		}

		[JsonIgnore]
		IApiCallDetails? ITransportResponse.ApiCall
		{
			get => _originalApiCall;
			set => _originalApiCall = value;
		}

		/// <summary>Subclasses can override this to provide more information on why a call is not valid.</summary>
		protected virtual void DebugIsValid(StringBuilder sb) { }

		/// <inheritdoc />
		public override string ToString() => $"{(!IsValid ? "Inv" : "V")}alid NEST response built from a {ApiCall?.ToString().ToCamelCase()}";
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;

namespace Nest
{
	/// <summary>
	/// A response from Elasticsearch
	/// </summary>
	public interface IResponse : ITransportResponse
	{
		/// <summary>
		/// A lazily computed, human readable string representation of what happened during a request for both successful and
		/// failed requests. Useful whilst developing or to log when <see cref="IsValid" /> is false on responses.
		/// </summary>
		[IgnoreDataMember]
		string DebugInformation { get; }

		/// <summary>
		/// Checks if a response is functionally valid or not.
		/// This is a NEST abstraction to have a single property to check whether there was something wrong with a request.
		/// <para>
		/// For instance, an Elasticsearch bulk response always returns 200 and individual bulk items may fail,
		/// <see cref="IsValid" /> will be false in that case.
		/// </para>
		/// <para>
		/// You can also configure the client to always throw an <see cref="TransportException" /> using
		/// <see cref="ITransportConfigurationValues.ThrowExceptions" /> if the response is not valid
		/// </para>
		/// </summary>
		[IgnoreDataMember]
		bool IsValid { get; }

		/// <summary>
		/// If the request resulted in an exception on the client side this will hold the exception that was thrown.
		/// <para>
		/// This property is a shortcut to <see cref="ITransportResponse.ApiCall" />'s
		/// <see cref="IApiCallDetails.OriginalException" /> and
		/// is possibly set when <see cref="IsValid" /> is false depending on the cause of the error
		/// </para>
		/// <para>
		/// You can also configure the client to always throw an <see cref="TransportException" /> using
		/// <see cref="ITransportConfigurationValues.ThrowExceptions" /> if the response is not valid
		/// </para>
		/// </summary>
		[IgnoreDataMember]
		Exception OriginalException { get; }

		/// <summary>
		/// If the response results in an error on Elasticsearch's side an <pre>error</pre> element will be returned, this is
		/// mapped to
		/// <see cref="ServerError" /> in NEST.
		/// <para>Possibly set when <see cref="IsValid" /> is false, depending on the cause of the error</para>
		/// <para>
		/// You can also configure the client to always throw an <see cref="TransportException" /> using
		/// <see cref="ITransportConfigurationValues.ThrowExceptions" /> if the response is not valid
		/// </para>
		/// </summary>
		[IgnoreDataMember]
		ServerError ServerError { get; }
	}

	public abstract class ResponseBase : IResponse
	{
		private Error _error;
		private IApiCallDetails _originalApiCall;
		private ServerError _serverError;
		private int? _statusCode;

		/// <summary> Returns useful information about the request(s) that were part of this API call. </summary>
		public virtual IApiCallDetails ApiCall => _originalApiCall;

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

				return (ApiCall?.Success ?? false) && ServerError == null;
			}
		}


		/// <inheritdoc />
		public Exception OriginalException => ApiCall?.OriginalException;

		/// <inheritdoc />
		public ServerError ServerError
		{
			get
			{
				if (_serverError != null) return _serverError;
				if (_error == null) return null;

				_serverError = new ServerError(_error, _statusCode);
				return _serverError;
			}
		}

		[DataMember(Name = "error")]
		[JsonPropertyName("error")]
		internal Error Error
		{
			get => _error;
			set
			{
				_error = value;
				_serverError = null;
			}
		}

		[DataMember(Name = "status")]
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

		[IgnoreDataMember]
		IApiCallDetails ITransportResponse.ApiCall
		{
			get => _originalApiCall;
			set => _originalApiCall = value;
		}

		/// <summary>Subclasses can override this to provide more information on why a call is not valid.</summary>
		protected virtual void DebugIsValid(StringBuilder sb) { }

		public override string ToString() => $"{(!IsValid ? "Inv" : "V")}alid NEST response built from a {ApiCall?.ToString().ToCamelCase()}";
	}
}

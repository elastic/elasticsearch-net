using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Shared interface by all elasticsearch responses
	/// </summary>
	public interface IResponse : IElasticsearchResponse
	{
		/// <summary>
		/// This property can be used to check if a response is functionally valid or not.
		/// This is a NEST abstraction to have a single point to check whether something wrong happened with the request.
		/// <para>For instance an elasticsearch bulk response always returns 200 and individual bulk items may fail, <see cref="IsValid"/> will be false in that case</para>
		/// <para>You can also configure the client to always throw an <see cref="ElasticsearchClientException"/> using <see cref="IConnectionConfigurationValues.ThrowExceptions"/> if the response is not valid</para>
		/// </summary>
		[JsonIgnore]
		bool IsValid { get; }

		/// <summary>
		/// If the response results in an error on elasticsearch's side an <pre>error</pre> element will be returned, this is mapped to <see cref="ServerError"/> in NEST.
		/// <para>Possibly set when <see cref="IsValid"/> is false, depending on the cause of the error</para>
		/// <para>You can also configure the client to always throw an <see cref="ElasticsearchClientException"/> using <see cref="IConnectionConfigurationValues.ThrowExceptions"/> if the response is not valid</para>
		/// </summary>
		[JsonIgnore]
		ServerError ServerError { get; }

		/// <summary>
		/// If the request resulted in an exception on the client side this will hold the exception that was thrown.
		/// <para>This property is a shortcut to <see cref="IElasticsearchResponse.ApiCall"/>'s <see cref="IApiCallDetails.OriginalException"/> and is possibly set when <see cref="IsValid"/> is false depending on the cause of the error</para>
		/// <para>You can also configure the client to always throw an <see cref="ElasticsearchClientException"/> using <see cref="IConnectionConfigurationValues.ThrowExceptions"/> if the response is not valid</para>
		/// </summary>
		[JsonIgnore]
		Exception OriginalException { get; }

        /// <summary>
        /// A lazy human readable string representation of what happened during this request for both successful and
        /// failed requests, very useful while developing or to log when <see cref="IsValid"/> is false on responses.
        /// </summary>
        [JsonIgnore]
		string DebugInformation { get; }
	}

	public abstract class ResponseBase : IResponse
	{
		private IApiCallDetails _originalApiCall;
		private ServerError _serverError;
		private Error _error;
		private int? _statusCode;

        [JsonIgnore]
		IApiCallDetails IElasticsearchResponse.ApiCall { get => _originalApiCall; set => _originalApiCall = value; }

		bool IElasticsearchResponse.TryGetServerErrorReason(out string reason)
		{
			reason = this.ServerError?.Error?.ToString();
			return !reason.IsNullOrEmpty();
		}

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

		[JsonProperty("error")]
		internal Error Error
		{
			get => _error;
			set
			{
				_error = value;
				_serverError = null;
			}
		}

		[JsonProperty("status")]
		internal int? StatusCode
		{
			get => _statusCode;
			set
			{
				_statusCode = value;
				_serverError = null;
			}
		}

		/// <inheritdoc/>
		public virtual bool IsValid => (this.ApiCall?.Success ?? false) && (this.ServerError == null);

		/// <summary> Returns useful information about the request(s) that were part of this API call. </summary>
		public virtual IApiCallDetails ApiCall => _originalApiCall;


		/// <inheritdoc/>
		public Exception OriginalException => this.ApiCall?.OriginalException;

		/// <inheritdoc/>
		public string DebugInformation
		{
			get
			{
				var sb = new StringBuilder();
				sb.Append($"{(!this.IsValid ? "Inv" : "V")}alid NEST response built from a ");
				sb.AppendLine(this.ApiCall?.ToString().ToCamelCase() ?? "null ApiCall which is highly exceptional, please open a bug if you see this");
				if (!this.IsValid) this.DebugIsValid(sb);
				if (this.ApiCall != null) ResponseStatics.DebugInformationBuilder(ApiCall, sb);
				return sb.ToString();
			}
		}
		/// <summary>Subclasses can override this to provide more information on why a call is not valid.</summary>
		protected virtual void DebugIsValid(StringBuilder sb) { }

		public override string ToString() =>  $"{(!this.IsValid ? "Inv" : "V")}alid NEST response built from a {this.ApiCall?.ToString().ToCamelCase()}";
	}
}

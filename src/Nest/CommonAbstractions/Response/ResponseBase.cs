using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Elasticsearch.Net_5_2_0;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	/// <summary>
	/// Shared interface by all elasticsearch responses
	/// </summary>
	public interface IResponse : IBodyWithApiCallDetails
	{
		/// <summary>
		/// This property can be used to check if a response is functionally valid or not.
		/// This is a NEST abstraction to have a single point to check whether something wrong happend with the request.
		/// <para>For instance an elasticsearch bulk response always returns 200 and individual bulk items may fail, <see cref="IsValid"/> will be false in that case</para>
		/// <para>You can also configure the client to always throw an <see cref="ElasticsearchClientException"/> using <see cref="IConnectionConfigurationValues.ThrowExceptions"/> if the response is not valid</para>
		/// </summary>
		[JsonIgnore]
		bool IsValid { get; }

		/// <summary>
		/// If the response results in an error on elasticsearch's side an <pre>error</pre> element will be returned, this is mapped to <see cref="ServerError"/> in Nest_5_2_0.
		/// <para>This property is a shortcut to <see cref="IBodyWithApiCallDetails.ApiCall"/>'s <see cref="IApiCallDetails.ServerError"/> and is possibly set when <see cref="IsValid"/> is false depending on the cause of the error</para>
		/// <para>You can also configure the client to always throw an <see cref="ElasticsearchClientException"/> using <see cref="IConnectionConfigurationValues.ThrowExceptions"/> if the response is not valid</para>
		/// </summary>
		[JsonIgnore]
		ServerError ServerError { get; }

		/// <summary>
		/// If the request resulted in an exception on the client side this will hold the exception that was thrown.
		/// <para>This property is a shortcut to <see cref="IBodyWithApiCallDetails.ApiCall"/>'s <see cref="IApiCallDetails.OriginalException"/> and is possibly set when <see cref="IsValid"/> is false depending on the cause of the error</para>
		/// <para>You can also configure the client to always throw an <see cref="ElasticsearchClientException"/> using <see cref="IConnectionConfigurationValues.ThrowExceptions"/> if the response is not valid</para>
		/// </summary>
		[JsonIgnore]
		Exception OriginalException { get; }

		/// <summary>
		/// A lazy human readable string representation of what happened during this request for both succesful and failed requests, very useful while developing or to log when you get <see cref="IsValid"/> = false responses.
		/// </summary>
		[JsonIgnore]
		string DebugInformation { get; }

	}

	public abstract class ResponseBase : IResponse
	{
		/// <inheritdoc/>
		public virtual bool IsValid => (this.ApiCall?.Success ?? false) && (this.ServerError == null);

		IApiCallDetails IBodyWithApiCallDetails.ApiCall { get; set; }

		/// <inheritdoc/>
		protected virtual IApiCallDetails ApiCall => ((IBodyWithApiCallDetails)this).ApiCall;

		/// <inheritdoc/>
		public virtual ServerError ServerError => this.ApiCall?.ServerError;

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
		protected virtual void DebugIsValid(StringBuilder sb) { }

		public override string ToString() =>  $"{(!this.IsValid ? "Inv" : "V")}alid NEST response built from a {this.ApiCall?.ToString().ToCamelCase()}";

	}
}

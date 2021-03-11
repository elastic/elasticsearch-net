using System;
using System.Runtime.Serialization;
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
		/// <see cref="ITransportConfiguration.ThrowExceptions" /> if the response is not valid
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
		/// <see cref="ITransportConfiguration.ThrowExceptions" /> if the response is not valid
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
		/// <see cref="ITransportConfiguration.ThrowExceptions" /> if the response is not valid
		/// </para>
		/// </summary>
		[IgnoreDataMember]
		ServerError ServerError { get; }
	}
}

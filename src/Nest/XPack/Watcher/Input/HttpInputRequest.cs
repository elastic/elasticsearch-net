// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(HttpInputRequest))]
	public interface IHttpInputRequest
	{
		/// <summary>
		/// Authentication related HTTP headers.
		/// </summary>
		[DataMember(Name = "auth")]
		IHttpInputAuthentication Authentication { get; set; }

		/// <summary>
		/// The HTTP request body.
		/// The body can be static text or include mustache templates.
		/// </summary>
		[DataMember(Name = "body")]
		string Body { get; set; }

		/// <summary>
		/// The timeout for setting up the http connection.
		/// If the connection could not be set up within this time,
		/// the input will timeout and fail.
		/// </summary>
		[DataMember(Name = "connection_timeout")]
		Time ConnectionTimeout { get; set; }

		/// <summary>
		/// The HTTP request headers.
		/// The header values can be static text or include mustache templates.
		/// </summary>
		[DataMember(Name = "headers")]
		IDictionary<string, string> Headers { get; set; }

		/// <summary>
		/// The host to connect to. This is required
		/// </summary>
		[DataMember(Name = "host")]
		string Host { get; set; }

		/// <summary>
		/// The HTTP method. Defaults to <see cref="HttpInputMethod.Get" />
		/// </summary>
		[DataMember(Name = "method")]
		HttpInputMethod? Method { get; set; }

		/// <summary>
		/// The url query string parameters.
		/// The parameter values can be static text or contain mustache templates.
		/// </summary>
		[DataMember(Name = "params")]
		IDictionary<string, string> Params { get; set; }

		/// <summary>
		/// The url path. The path can be static text or contain mustache templates.
		/// Url query string parameters must be specified with <see cref="Params" />
		/// </summary>
		[DataMember(Name = "path")]
		string Path { get; set; }

		/// <summary>
		/// The port that the http service is listening on.
		/// This is required
		/// </summary>
		[DataMember(Name = "port")]
		int? Port { get; set; }

		/// <summary>
		/// The proxy to use when connecting to the host.
		/// </summary>
		[DataMember(Name = "proxy")]
		IHttpInputProxy Proxy { get; set; }

		/// <summary>
		/// The timeout for reading data from http connection.
		/// If no response was received within this time,
		/// the input will timeout and fail.
		/// </summary>
		[DataMember(Name = "read_timeout")]
		Time ReadTimeout { get; set; }

		/// <summary>
		/// The url scheme
		/// </summary>
		[DataMember(Name = "scheme")]
		ConnectionScheme? Scheme { get; set; }

		/// <summary>
		/// Sets the scheme, host, port and params all at once by specifying a real URL.
		/// May not be combined with <see cref="Scheme" />, <see cref="Host" />,
		/// <see cref="Port" /> and <see cref="Params" />.
		/// As if parameters are set, specifying them individually might overwrite them.
		/// </summary>
		[DataMember(Name = "url")]
		string Url { get; set; }
	}

	public class HttpInputRequest : IHttpInputRequest
	{
		/// <inheritdoc />
		public IHttpInputAuthentication Authentication { get; set; }

		/// <inheritdoc />
		public string Body { get; set; }

		/// <inheritdoc />
		public Time ConnectionTimeout { get; set; }

		/// <inheritdoc />
		public IDictionary<string, string> Headers { get; set; }

		/// <inheritdoc />
		public string Host { get; set; }

		/// <inheritdoc />
		public HttpInputMethod? Method { get; set; }

		/// <inheritdoc />
		public IDictionary<string, string> Params { get; set; }

		/// <inheritdoc />
		public string Path { get; set; }

		/// <inheritdoc />
		public int? Port { get; set; }

		/// <inheritdoc />
		public IHttpInputProxy Proxy { get; set; }

		/// <inheritdoc />
		public Time ReadTimeout { get; set; }

		/// <inheritdoc />
		public ConnectionScheme? Scheme { get; set; }

		/// <inheritdoc />
		public string Url { get; set; }
	}

	public class HttpInputRequestDescriptor
		: DescriptorBase<HttpInputRequestDescriptor, IHttpInputRequest>, IHttpInputRequest
	{
		IHttpInputAuthentication IHttpInputRequest.Authentication { get; set; }
		string IHttpInputRequest.Body { get; set; }
		Time IHttpInputRequest.ConnectionTimeout { get; set; }
		IDictionary<string, string> IHttpInputRequest.Headers { get; set; }
		string IHttpInputRequest.Host { get; set; }
		HttpInputMethod? IHttpInputRequest.Method { get; set; }
		IDictionary<string, string> IHttpInputRequest.Params { get; set; }
		string IHttpInputRequest.Path { get; set; }
		int? IHttpInputRequest.Port { get; set; }
		IHttpInputProxy IHttpInputRequest.Proxy { get; set; }
		Time IHttpInputRequest.ReadTimeout { get; set; }
		ConnectionScheme? IHttpInputRequest.Scheme { get; set; }
		string IHttpInputRequest.Url { get; set; }

		/// <inheritdoc />
		public HttpInputRequestDescriptor Authentication(Func<HttpInputAuthenticationDescriptor, IHttpInputAuthentication> authSelector) =>
			Assign(authSelector(new HttpInputAuthenticationDescriptor()), (a, v) => a.Authentication = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Body(string body) => Assign(body, (a, v) => a.Body = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor ConnectionTimeout(Time connectionTimeout) => Assign(connectionTimeout, (a, v) => a.ConnectionTimeout = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Headers(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> headersSelector) =>
			Assign(headersSelector(new FluentDictionary<string, string>()), (a, v) => a.Headers = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Headers(Dictionary<string, string> headersDictionary) =>
			Assign(headersDictionary, (a, v) => a.Headers = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Host(string host) => Assign(host, (a, v) => a.Host = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Method(HttpInputMethod? method) => Assign(method, (a, v) => a.Method = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Path(string path) => Assign(path, (a, v) => a.Path = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Params(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> paramsSelector) =>
			Assign(paramsSelector.Invoke(new FluentDictionary<string, string>()), (a, v) => a.Params = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Params(Dictionary<string, string> paramsDictionary) =>
			Assign(paramsDictionary, (a, v) => a.Params = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Port(int? port) => Assign(port, (a, v) => a.Port = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Proxy(Func<HttpInputProxyDescriptor, IHttpInputProxy> proxySelector) =>
			Assign(proxySelector.Invoke(new HttpInputProxyDescriptor()), (a, v) => a.Proxy = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor ReadTimeout(Time readTimeout) => Assign(readTimeout, (a, v) => a.ReadTimeout = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Scheme(ConnectionScheme? scheme) => Assign(scheme, (a, v) => a.Scheme = v);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Url(string url) => Assign(url, (a, v) => a.Url = v);
	}
}

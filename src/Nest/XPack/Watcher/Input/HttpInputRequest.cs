using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<HttpInputRequest>))]
	public interface IHttpInputRequest
	{
		/// <summary>
		/// The url scheme
		/// </summary>
		[JsonProperty("scheme")]
		[JsonConverter(typeof(StringEnumConverter))]
		ConnectionScheme? Scheme { get; set; }

		/// <summary>
		/// The port that the http service is listening on.
		/// This is required
		/// </summary>
		[JsonProperty("port")]
		int? Port { get; set; }

		/// <summary>
		/// The host to connect to. This is required
		/// </summary>
		[JsonProperty("host")]
		string Host { get; set; }

		/// <summary>
		/// The url path. The path can be static text or contain mustache templates.
		/// Url query string parameters must be specified with <see cref="Params"/>
		/// </summary>
		[JsonProperty("path")]
		string Path { get; set; }

		/// <summary>
		/// The HTTP method. Defaults to <see cref="HttpInputMethod.Get"/>
		/// </summary>
		[JsonProperty("method")]
		HttpInputMethod? Method { get; set; }

		/// <summary>
		/// The HTTP request headers.
		/// The header values can be static text or include mustache templates.
		/// </summary>
		[JsonProperty("headers")]
		IDictionary<string, string> Headers { get; set; }

		/// <summary>
		/// The url query string parameters.
		/// The parameter values can be static text or contain mustache templates.
	    /// </summary>
		[JsonProperty("params")]
		IDictionary<string, string> Params { get; set; }

		/// <summary>
		/// Sets the scheme, host, port and params all at once by specifying a real URL.
		/// May not be combined with <see cref="Scheme"/>, <see cref="Host"/>,
		/// <see cref="Port"/> and <see cref="Params"/>.
		/// As if parameters are set, specifying them individually might overwrite them.
		/// </summary>
		[JsonProperty("url")]
		string Url { get; set; }

		/// <summary>
		/// Authentication related HTTP headers.
		/// </summary>
		[JsonProperty("auth")]
		IHttpInputAuthentication Authentication { get; set; }

		/// <summary>
		/// The proxy to use when connecting to the host.
		/// </summary>
		[JsonProperty("proxy")]
		IHttpInputProxy Proxy { get; set; }

		/// <summary>
		/// The timeout for setting up the http connection.
		/// If the connection could not be set up within this time,
		/// the input will timeout and fail.
		/// </summary>
		[JsonProperty("connection_timeout")]
		Time ConnectionTimeout { get; set; }

		/// <summary>
		/// The timeout for reading data from http connection.
		/// If no response was received within this time,
		/// the input will timeout and fail.
		/// </summary>
		[JsonProperty("read_timeout")]
		Time ReadTimeout { get; set; }

		/// <summary>
		/// The HTTP request body.
		/// The body can be static text or include mustache templates.
		/// </summary>
		[JsonProperty("body")]
		string Body { get; set; }
	}

	public class HttpInputRequest : IHttpInputRequest
	{
		/// <inheritdoc />
		public ConnectionScheme? Scheme { get; set; }

		/// <inheritdoc />
		public int? Port { get; set; }

		/// <inheritdoc />
		public string Host { get; set; }

		/// <inheritdoc />
		public string Path { get; set; }

		/// <inheritdoc />
		public HttpInputMethod? Method { get; set; }

		/// <inheritdoc />
		public IDictionary<string, string> Headers { get; set; }

		/// <inheritdoc />
		public IDictionary<string, string> Params { get; set; }

		/// <inheritdoc />
		public string Url { get; set; }

		/// <inheritdoc />
		public IHttpInputAuthentication Authentication { get; set; }

		/// <inheritdoc />
		public IHttpInputProxy Proxy { get; set; }

		/// <inheritdoc />
		public Time ConnectionTimeout { get; set; }

		/// <inheritdoc />
		public Time ReadTimeout { get; set; }

		/// <inheritdoc />
		public string Body { get; set; }
	}

	public class HttpInputRequestDescriptor
		: DescriptorBase<HttpInputRequestDescriptor, IHttpInputRequest>, IHttpInputRequest
	{
		ConnectionScheme? IHttpInputRequest.Scheme { get; set; }
		int? IHttpInputRequest.Port { get; set; }
		string IHttpInputRequest.Host { get; set; }
		string IHttpInputRequest.Path { get; set; }
		HttpInputMethod? IHttpInputRequest.Method { get; set; }
		IDictionary<string, string> IHttpInputRequest.Headers { get; set; }
		IDictionary<string, string> IHttpInputRequest.Params { get; set; }
		IHttpInputAuthentication IHttpInputRequest.Authentication { get; set; }
		string IHttpInputRequest.Body { get; set; }
		string IHttpInputRequest.Url { get; set; }
		Time IHttpInputRequest.ReadTimeout { get; set; }
		Time IHttpInputRequest.ConnectionTimeout { get; set; }
		IHttpInputProxy IHttpInputRequest.Proxy { get; set; }

		/// <inheritdoc />
		public HttpInputRequestDescriptor Authentication(Func<HttpInputAuthenticationDescriptor, IHttpInputAuthentication> authSelector) =>
			Assign(a => a.Authentication = authSelector(new HttpInputAuthenticationDescriptor()));

		/// <inheritdoc />
		public HttpInputRequestDescriptor Body(string body) => Assign(a => a.Body = body);

		/// <inheritdoc />
		public HttpInputRequestDescriptor ConnectionTimeout(Time connectionTimeout) => Assign(a => a.ConnectionTimeout = connectionTimeout);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Headers(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> headersSelector) =>
			Assign(a => a.Headers = headersSelector(new FluentDictionary<string, string>()));

		/// <inheritdoc />
		public HttpInputRequestDescriptor Headers(Dictionary<string, string> headersDictionary) =>
			Assign(a => a.Headers = headersDictionary);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Host(string host) => Assign(a => a.Host = host);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Method(HttpInputMethod? method) => Assign(a => a.Method = method);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Path(string path) => Assign(a => a.Path = path);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Params(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> paramsSelector) =>
			Assign(a => a.Params = paramsSelector.Invoke(new FluentDictionary<string, string>()));

		/// <inheritdoc />
		public HttpInputRequestDescriptor Params(Dictionary<string, string> paramsDictionary) =>
			Assign(a => a.Params = paramsDictionary);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Port(int? port) => Assign(a => a.Port = port);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Proxy(Func<HttpInputProxyDescriptor, IHttpInputProxy> proxySelector) =>
			Assign(a => a.Proxy = proxySelector.Invoke(new HttpInputProxyDescriptor()));

		/// <inheritdoc />
		public HttpInputRequestDescriptor ReadTimeout(Time readTimeout) => Assign(a => a.ReadTimeout = readTimeout);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Scheme(ConnectionScheme? scheme) => Assign(a => a.Scheme = scheme);

		/// <inheritdoc />
		public HttpInputRequestDescriptor Url(string url) => Assign(a => a.Url = url);
	}
}

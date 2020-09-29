// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(WebhookAction))]
	public interface IWebhookAction : IAction, IHttpInputRequest { }

	public class WebhookAction : ActionBase, IWebhookAction
	{
		public WebhookAction(string name) : base(name) { }

		public override ActionType ActionType => ActionType.Webhook;

		public IHttpInputAuthentication Authentication { get; set; }

		public string Body { get; set; }

		public Time ConnectionTimeout { get; set; }

		public IDictionary<string, string> Headers { get; set; }

		public string Host { get; set; }

		public HttpInputMethod? Method { get; set; }

		public IDictionary<string, string> Params { get; set; }

		public string Path { get; set; }

		public int? Port { get; set; }

		public IHttpInputProxy Proxy { get; set; }

		public Time ReadTimeout { get; set; }

		public ConnectionScheme? Scheme { get; set; }

		public string Url { get; set; }
	}

	public class WebhookActionDescriptor : ActionsDescriptorBase<WebhookActionDescriptor, IWebhookAction>, IWebhookAction
	{
		public WebhookActionDescriptor(string name) : base(name) { }

		protected override ActionType ActionType => ActionType.Webhook;
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
		public WebhookActionDescriptor Authentication(Func<HttpInputAuthenticationDescriptor, IHttpInputAuthentication> selector) =>
			Assign(selector, (a, v) => a.Authentication = v?.Invoke(new HttpInputAuthenticationDescriptor()));

		/// <inheritdoc />
		public WebhookActionDescriptor Body(string body) => Assign(body, (a, v) => a.Body = v);

		/// <inheritdoc />
		public WebhookActionDescriptor ConnectionTimeout(Time connectionTimeout) => Assign(connectionTimeout, (a, v) => a.ConnectionTimeout = v);

		/// <inheritdoc />
		public WebhookActionDescriptor Headers(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> headersSelector) =>
			Assign(headersSelector(new FluentDictionary<string, string>()), (a, v) => a.Headers = v);

		/// <inheritdoc />
		public WebhookActionDescriptor Headers(Dictionary<string, string> headersDictionary) =>
			Assign(headersDictionary, (a, v) => a.Headers = v);

		/// <inheritdoc />
		public WebhookActionDescriptor Host(string host) => Assign(host, (a, v) => a.Host = v);

		/// <inheritdoc />
		public WebhookActionDescriptor Method(HttpInputMethod? method) => Assign(method, (a, v) => a.Method = v);

		/// <inheritdoc />
		public WebhookActionDescriptor Path(string path) => Assign(path, (a, v) => a.Path = v);

		/// <inheritdoc />
		public WebhookActionDescriptor Params(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> paramsSelector) =>
			Assign(paramsSelector, (a, v) => a.Params = v?.Invoke(new FluentDictionary<string, string>()));

		/// <inheritdoc />
		public WebhookActionDescriptor Params(Dictionary<string, string> paramsDictionary) =>
			Assign(paramsDictionary, (a, v) => a.Params = v);

		/// <inheritdoc />
		public WebhookActionDescriptor Port(int? port) => Assign(port, (a, v) => a.Port = v);

		/// <inheritdoc />
		public WebhookActionDescriptor Proxy(Func<HttpInputProxyDescriptor, IHttpInputProxy> proxySelector) =>
			Assign(proxySelector.Invoke(new HttpInputProxyDescriptor()), (a, v) => a.Proxy = v);

		/// <inheritdoc />
		public WebhookActionDescriptor ReadTimeout(Time readTimeout) => Assign(readTimeout, (a, v) => a.ReadTimeout = v);

		/// <inheritdoc />
		public WebhookActionDescriptor Scheme(ConnectionScheme? scheme) => Assign(scheme, (a, v) => a.Scheme = v);

		/// <inheritdoc />
		public WebhookActionDescriptor Url(string url) => Assign(url, (a, v) => a.Url = v);
	}
}

using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	[JsonObject]
	[ExactContractJsonConverter(typeof(ReadAsTypeJsonConverter<WebhookAction>))]
	public interface IWebhookAction : IAction, IHttpInputRequest
	{
	}

	public class WebhookAction : ActionBase, IWebhookAction
	{
		public override ActionType ActionType => ActionType.Webhook;

		public ConnectionScheme? Scheme { get; set; }

		public int? Port { get; set; }

		public string Host { get; set; }

		public string Path { get; set; }

		public HttpInputMethod? Method { get; set; }

		public IDictionary<string, string> Headers { get; set; }

		public IDictionary<string, string> Params { get; set; }

		public string Url { get; set; }

		public IHttpInputAuthentication Authentication { get; set; }

		public IHttpInputProxy Proxy { get; set; }

		public Time ConnectionTimeout { get; set; }

		public Time ReadTimeout { get; set; }

		public string Body { get; set; }

		public WebhookAction(string name) : base(name) {}
	}

	public class WebhookActionDescriptor : ActionsDescriptorBase<WebhookActionDescriptor, IWebhookAction>, IWebhookAction
	{
		protected override ActionType ActionType => ActionType.Webhook;

		ConnectionScheme? IHttpInputRequest.Scheme { get; set; }
		int? IHttpInputRequest.Port { get; set; }
		string IHttpInputRequest.Host { get; set; }
		string IHttpInputRequest.Path { get; set; }
		HttpInputMethod? IHttpInputRequest.Method { get; set; }
		IDictionary<string, string> IHttpInputRequest.Headers { get; set; }
		IDictionary<string, string> IHttpInputRequest.Params { get; set; }
		string IHttpInputRequest.Url { get; set; }
		IHttpInputAuthentication IHttpInputRequest.Authentication { get; set; }
		IHttpInputProxy IHttpInputRequest.Proxy { get; set; }
		Time IHttpInputRequest.ConnectionTimeout { get; set; }
		Time IHttpInputRequest.ReadTimeout { get; set; }
		string IHttpInputRequest.Body { get; set; }

		public WebhookActionDescriptor(string name) : base(name) {}

		/// <inheritdoc />
		public WebhookActionDescriptor Authentication(Func<HttpInputAuthenticationDescriptor, IHttpInputAuthentication> selector) =>
			Assign(a => a.Authentication = selector?.Invoke(new HttpInputAuthenticationDescriptor()));

		/// <inheritdoc />
		public WebhookActionDescriptor Body(string body) => Assign(a => a.Body = body);

		/// <inheritdoc />
		public WebhookActionDescriptor ConnectionTimeout(Time connectionTimeout) => Assign(a => a.ConnectionTimeout = connectionTimeout);

		/// <inheritdoc />
		public WebhookActionDescriptor Headers(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> headersSelector) =>
			Assign(a => a.Headers = headersSelector(new FluentDictionary<string, string>()));

		/// <inheritdoc />
		public WebhookActionDescriptor Headers(Dictionary<string, string> headersDictionary) =>
			Assign(a => a.Headers = headersDictionary);

		/// <inheritdoc />
		public WebhookActionDescriptor Host(string host) => Assign(a => a.Host = host);

		/// <inheritdoc />
		public WebhookActionDescriptor Method(HttpInputMethod? method) => Assign(a => a.Method = method);

		/// <inheritdoc />
		public WebhookActionDescriptor Path(string path) => Assign(a => a.Path = path);

		/// <inheritdoc />
		public WebhookActionDescriptor Params(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> paramsSelector) =>
			Assign(a => a.Params = paramsSelector?.Invoke(new FluentDictionary<string, string>()));

		/// <inheritdoc />
		public WebhookActionDescriptor Params(Dictionary<string, string> paramsDictionary) =>
			Assign(a => a.Params = paramsDictionary);

		/// <inheritdoc />
		public WebhookActionDescriptor Port(int? port) => Assign(a => a.Port = port);

		/// <inheritdoc />
		public WebhookActionDescriptor Proxy(Func<HttpInputProxyDescriptor, IHttpInputProxy> proxySelector) =>
			Assign(a => a.Proxy = proxySelector.Invoke(new HttpInputProxyDescriptor()));

		/// <inheritdoc />
		public WebhookActionDescriptor ReadTimeout(Time readTimeout) => Assign(a => a.ReadTimeout = readTimeout);

		/// <inheritdoc />
		public WebhookActionDescriptor Scheme(ConnectionScheme? scheme) => Assign(a => a.Scheme = scheme);

		/// <inheritdoc />
		public WebhookActionDescriptor Url(string url) => Assign(a => a.Url = url);
	}
}

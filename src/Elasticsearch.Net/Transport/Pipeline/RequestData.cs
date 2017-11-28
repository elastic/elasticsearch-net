using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Purify;

namespace Elasticsearch.Net
{
	public class RequestData
	{
		public const string MimeType = "application/json";
		public const string RunAsSecurityHeader = "es-security-runas-user";

		public Uri Uri => this.Node != null ? new Uri(this.Node.Uri, this.Path).Purify() : null;

		public HttpMethod Method { get; private set; }
		public string Path { get; }
		public PostData PostData { get; }
		public bool MadeItToResponse { get; set;}
		public AuditEvent OnFailureAuditEvent => this.MadeItToResponse ? AuditEvent.BadResponse : AuditEvent.BadRequest;
		public PipelineFailure OnFailurePipelineFailure => this.MadeItToResponse ? PipelineFailure.BadResponse : PipelineFailure.BadRequest;

		public Node Node { get; set; }
		public TimeSpan RequestTimeout { get; }
		public TimeSpan PingTimeout { get; }
		public int KeepAliveTime { get; }
		public int KeepAliveInterval { get; }

		public bool Pipelined { get; }
		public bool HttpCompression { get; }
		public string ContentType { get; }
		public string Accept { get; }
		public string RunAs { get; }

		public NameValueCollection Headers { get; }
		public string ProxyAddress { get; }
		public string ProxyUsername { get; }
		public string ProxyPassword { get; }
		public bool DisableAutomaticProxyDetection { get; }

		public BasicAuthenticationCredentials BasicAuthorizationCredentials { get; }
		public IEnumerable<int> AllowedStatusCodes { get; }
		public Func<IApiCallDetails, Stream, object> CustomConverter { get; private set; }
		public IConnectionConfigurationValues ConnectionSettings { get; }
		public IMemoryStreamFactory MemoryStreamFactory { get; }

		public X509CertificateCollection ClientCertificates { get; set; }

		public RequestData(HttpMethod method, string path, PostData data, IConnectionConfigurationValues global, IRequestParameters local, IMemoryStreamFactory memoryStreamFactory)
			: this(method, path, data, global, local?.RequestConfiguration, memoryStreamFactory)
		{
			this.CustomConverter = local?.DeserializationOverride;
			this.Path = this.CreatePathWithQueryStrings(path, this.ConnectionSettings, local);
		}

		private RequestData(
			HttpMethod method,
			string path,
			PostData data,
			IConnectionConfigurationValues global,
			IRequestConfiguration local,
			IMemoryStreamFactory memoryStreamFactory)
		{
			this.ConnectionSettings = global;
			this.MemoryStreamFactory = memoryStreamFactory;
			this.Method = method;
			this.PostData = data;

			if (data != null)
				data.DisableDirectStreaming = local?.DisableDirectStreaming ?? global.DisableDirectStreaming;

			this.Path = this.CreatePathWithQueryStrings(path, this.ConnectionSettings, null);

			this.Pipelined = local?.EnableHttpPipelining ?? global.HttpPipeliningEnabled;
			this.HttpCompression = global.EnableHttpCompression;
			this.ContentType = local?.ContentType ?? MimeType;
			this.Accept = local?.Accept ?? MimeType;
			this.Headers = global.Headers != null ? new NameValueCollection(global.Headers) : new NameValueCollection();
			this.RunAs = local?.RunAs;

			this.RequestTimeout = local?.RequestTimeout ?? global.RequestTimeout;
			this.PingTimeout =
				local?.PingTimeout
				?? global?.PingTimeout
				?? (global.ConnectionPool.UsingSsl ? ConnectionConfiguration.DefaultPingTimeoutOnSSL : ConnectionConfiguration.DefaultPingTimeout);

			this.KeepAliveInterval = (int)(global.KeepAliveInterval?.TotalMilliseconds ?? 2000);
			this.KeepAliveTime = (int)(global.KeepAliveTime?.TotalMilliseconds ?? 2000);

			this.ProxyAddress = global.ProxyAddress;
			this.ProxyUsername = global.ProxyUsername;
			this.ProxyPassword = global.ProxyPassword;
			this.DisableAutomaticProxyDetection = global.DisableAutomaticProxyDetection;
			this.BasicAuthorizationCredentials = local?.BasicAuthenticationCredentials ?? global.BasicAuthenticationCredentials;
			this.AllowedStatusCodes = local?.AllowedStatusCodes ?? Enumerable.Empty<int>();
			this.ClientCertificates = local?.ClientCertificates ?? global.ClientCertificates;
		}

		private string CreatePathWithQueryStrings(string path, IConnectionConfigurationValues global, IRequestParameters request = null)
		{

			//Make sure we append global query string as well the request specific query string parameters
			var copy = new NameValueCollection(global.QueryStringParameters);
			var formatter = new UrlFormatProvider(this.ConnectionSettings);
			if (request != null)
				copy.Add(request.QueryString.ToNameValueCollection(formatter));
			if (!copy.HasKeys()) return path;

			var queryString = copy.ToQueryString();
			var tempUri = new Uri("http://localhost:9200/" + path).Purify();
			if (tempUri.Query.IsNullOrEmpty())
				path += queryString;
			else
				path += "&" + queryString.Substring(1, queryString.Length - 1);
			return path;
		}
	}
}

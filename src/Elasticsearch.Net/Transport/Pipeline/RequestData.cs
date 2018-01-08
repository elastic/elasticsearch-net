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

		public Uri Uri => this.Node != null ? new Uri(this.Node.Uri, this.PathAndQuery).Purify() : null;

		public HttpMethod Method { get; private set; }
		public string PathAndQuery { get; }
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
		public string RequestMimeType { get; }
		public string Accept { get; }
		public string RunAs { get; }
		public IReadOnlyCollection<int> SkipDeserializationForStatusCodes { get; }

		public NameValueCollection Headers { get; }
		public string ProxyAddress { get; }
		public string ProxyUsername { get; }
		public string ProxyPassword { get; }
		public bool DisableAutomaticProxyDetection { get; }
		public bool ThrowExceptions { get; }

		public BasicAuthenticationCredentials BasicAuthorizationCredentials { get; }
		public IEnumerable<int> AllowedStatusCodes { get; }
		public Func<IApiCallDetails, Stream, object> CustomConverter { get; }
		public IConnectionConfigurationValues ConnectionSettings { get; }
		public IMemoryStreamFactory MemoryStreamFactory { get; }

		public X509CertificateCollection ClientCertificates { get; }

		public RequestData(HttpMethod method, string path, PostData data, IConnectionConfigurationValues global, IRequestParameters local, IMemoryStreamFactory memoryStreamFactory)
			: this(method, data, global, local?.RequestConfiguration, memoryStreamFactory)
		{
			this.CustomConverter = local?.DeserializationOverride;
			this.PathAndQuery = this.CreatePathWithQueryStrings(path, this.ConnectionSettings, local);
		}

		private RequestData(
			HttpMethod method,
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

			this.Pipelined = local?.EnableHttpPipelining ?? global.HttpPipeliningEnabled;
			this.HttpCompression = global.EnableHttpCompression;
			this.RequestMimeType = local?.ContentType ?? MimeType;
			this.Accept = local?.Accept ?? MimeType;
			this.Headers = global.Headers != null ? new NameValueCollection(global.Headers) : new NameValueCollection();
			this.RunAs = local?.RunAs;
			this.SkipDeserializationForStatusCodes = global?.SkipDeserializationForStatusCodes;
			this.ThrowExceptions = local?.ThrowExceptions ?? global.ThrowExceptions;

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

		private string CreatePathWithQueryStrings(string path, IConnectionConfigurationValues global, IRequestParameters request)
		{
			path = path ?? string.Empty;
			if (global.QueryStringParameters.Count == 0 && request.QueryString.Count == 0) return path;

			//Make sure we append global query string as well the request specific query string parameters
			var copy = new NameValueCollection(global.QueryStringParameters);
			var formatter = this.ConnectionSettings.UrlFormatter;
			copy.SetLocalQueryString(request.QueryString, formatter);
			if (!copy.HasKeys()) return path;
			var queryString = copy.ToQueryString(formatter);

			var tempUri = new Uri("http://localhost:9200/" + path);
			if (tempUri.Query.IsNullOrEmpty())
				path += queryString;
			else
				path += "&" + queryString.Substring(1, queryString.Length - 1);
			return path;
		}
	}
}

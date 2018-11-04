using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Elasticsearch.Net
{
	public class RequestData
	{
		public const string MimeType = "application/json";
		public const string RunAsSecurityHeader = "es-security-runas-user";

		public RequestData(HttpMethod method, string path, PostData<object> data, IConnectionConfigurationValues global, IRequestParameters local,
			IMemoryStreamFactory memoryStreamFactory
		)
			: this(method, path, data, global, local?.RequestConfiguration, memoryStreamFactory)
		{
			CustomConverter = local?.DeserializationOverride;
			Path = CreatePathWithQueryStrings(path, ConnectionSettings, local);
		}

		private RequestData(
			HttpMethod method,
			string path,
			PostData<object> data,
			IConnectionConfigurationValues global,
			IRequestConfiguration local,
			IMemoryStreamFactory memoryStreamFactory
		)
		{
			ConnectionSettings = global;
			MemoryStreamFactory = memoryStreamFactory;
			Method = method;
			PostData = data;

			if (data != null)
				data.DisableDirectStreaming = local?.DisableDirectStreaming ?? global.DisableDirectStreaming;

			Path = CreatePathWithQueryStrings(path, ConnectionSettings, null);

			Pipelined = local?.EnableHttpPipelining ?? global.HttpPipeliningEnabled;
			HttpCompression = global.EnableHttpCompression;
			ContentType = local?.ContentType ?? MimeType;
			Accept = local?.Accept ?? MimeType;
			Headers = global.Headers != null ? new NameValueCollection(global.Headers) : new NameValueCollection();
			RunAs = local?.RunAs;

			RequestTimeout = local?.RequestTimeout ?? global.RequestTimeout;
			PingTimeout =
				local?.PingTimeout
				?? global?.PingTimeout
				?? (global.ConnectionPool.UsingSsl ? ConnectionConfiguration.DefaultPingTimeoutOnSSL : ConnectionConfiguration.DefaultPingTimeout);

			KeepAliveInterval = (int)(global.KeepAliveInterval?.TotalMilliseconds ?? 2000);
			KeepAliveTime = (int)(global.KeepAliveTime?.TotalMilliseconds ?? 2000);

			ProxyAddress = global.ProxyAddress;
			ProxyUsername = global.ProxyUsername;
			ProxyPassword = global.ProxyPassword;
			DisableAutomaticProxyDetection = global.DisableAutomaticProxyDetection;
			BasicAuthorizationCredentials = local?.BasicAuthenticationCredentials ?? global.BasicAuthenticationCredentials;
			AllowedStatusCodes = local?.AllowedStatusCodes ?? Enumerable.Empty<int>();
			ClientCertificates = local?.ClientCertificates ?? global.ClientCertificates;
		}

		public string Accept { get; }
		public IEnumerable<int> AllowedStatusCodes { get; }

		public BasicAuthenticationCredentials BasicAuthorizationCredentials { get; }

		public X509CertificateCollection ClientCertificates { get; set; }
		public IConnectionConfigurationValues ConnectionSettings { get; }
		public string ContentType { get; }
		public Func<IApiCallDetails, Stream, object> CustomConverter { get; private set; }
		public bool DisableAutomaticProxyDetection { get; }

		public NameValueCollection Headers { get; }
		public bool HttpCompression { get; }
		public int KeepAliveInterval { get; }
		public int KeepAliveTime { get; }
		public bool MadeItToResponse { get; set; }
		public IMemoryStreamFactory MemoryStreamFactory { get; }

		public HttpMethod Method { get; private set; }

		public Node Node { get; set; }
		public AuditEvent OnFailureAuditEvent => MadeItToResponse ? AuditEvent.BadResponse : AuditEvent.BadRequest;
		public PipelineFailure OnFailurePipelineFailure => MadeItToResponse ? PipelineFailure.BadResponse : PipelineFailure.BadRequest;
		public string Path { get; }
		public TimeSpan PingTimeout { get; }

		public bool Pipelined { get; }
		public PostData<object> PostData { get; }
		public string ProxyAddress { get; }
		public string ProxyPassword { get; }
		public string ProxyUsername { get; }
		public TimeSpan RequestTimeout { get; }
		public string RunAs { get; }

		public Uri Uri => Node != null ? new Uri(Node.Uri, Path) : null;

		private string CreatePathWithQueryStrings(string path, IConnectionConfigurationValues global, IRequestParameters request = null)
		{
			//Make sure we append global query string as well the request specific query string parameters
			var copy = new NameValueCollection(global.QueryStringParameters);
			var formatter = new UrlFormatProvider(ConnectionSettings);
			if (request != null)
				copy.Add(request.QueryString.ToNameValueCollection(formatter));
			if (!copy.HasKeys()) return path;

			var queryString = copy.ToQueryString();
			var tempUri = new Uri("http://localhost:9200/" + path);
			if (tempUri.Query.IsNullOrEmpty())
				path += queryString;
			else
				path += "&" + queryString.Substring(1, queryString.Length - 1);
			return path;
		}
	}
}

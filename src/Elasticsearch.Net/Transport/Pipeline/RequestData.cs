// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Elasticsearch.Net.Extensions;

namespace Elasticsearch.Net
{
	public class RequestData
	{
		public const string MimeType = "application/json";
		public const string MimeTypeTextPlain = "text/plain";
		public const string OpaqueIdHeader = "X-Opaque-Id";
		public const string RunAsSecurityHeader = "es-security-runas-user";

		public RequestData(HttpMethod method, string path, PostData data, IConnectionConfigurationValues global, IRequestParameters local,
			IMemoryStreamFactory memoryStreamFactory
		)
			: this(method, data, global, local?.RequestConfiguration, memoryStreamFactory)
		{
			_path = path;
			CustomResponseBuilder = local?.CustomResponseBuilder;
			PathAndQuery = CreatePathWithQueryStrings(path, ConnectionSettings, local);
		}

		private RequestData(
			HttpMethod method,
			PostData data,
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

			Pipelined = local?.EnableHttpPipelining ?? global.HttpPipeliningEnabled;
			HttpCompression = global.EnableHttpCompression;
			RequestMimeType = local?.ContentType ?? MimeType;
			Accept = local?.Accept ?? MimeType;

			if (global.Headers != null)
				Headers = new NameValueCollection(global.Headers);

			if (local?.Headers != null)
			{
				Headers ??= new NameValueCollection();
				foreach (var key in local.Headers.AllKeys)
					Headers[key] = local.Headers[key];
			}

			if (!string.IsNullOrEmpty(local?.OpaqueId))
			{
				Headers ??= new NameValueCollection();
				Headers.Add(OpaqueIdHeader, local.OpaqueId);
			}

			RunAs = local?.RunAs;
			SkipDeserializationForStatusCodes = global?.SkipDeserializationForStatusCodes;
			ThrowExceptions = local?.ThrowExceptions ?? global.ThrowExceptions;

			RequestTimeout = local?.RequestTimeout ?? global.RequestTimeout;
			PingTimeout =
				local?.PingTimeout
				?? global.PingTimeout
				?? (global.ConnectionPool.UsingSsl ? ConnectionConfiguration.DefaultPingTimeoutOnSSL : ConnectionConfiguration.DefaultPingTimeout);

			KeepAliveInterval = (int)(global.KeepAliveInterval?.TotalMilliseconds ?? 2000);
			KeepAliveTime = (int)(global.KeepAliveTime?.TotalMilliseconds ?? 2000);
			DnsRefreshTimeout = global.DnsRefreshTimeout;

			ProxyAddress = global.ProxyAddress;
			ProxyUsername = global.ProxyUsername;
			ProxyPassword = global.ProxyPassword;
			DisableAutomaticProxyDetection = global.DisableAutomaticProxyDetection;
			BasicAuthorizationCredentials = local?.BasicAuthenticationCredentials ?? global.BasicAuthenticationCredentials;
			ApiKeyAuthenticationCredentials = local?.ApiKeyAuthenticationCredentials ?? global.ApiKeyAuthenticationCredentials;
			AllowedStatusCodes = local?.AllowedStatusCodes ?? EmptyReadOnly<int>.Collection;
			ClientCertificates = local?.ClientCertificates ?? global.ClientCertificates;
			UserAgent = global.UserAgent;
			TransferEncodingChunked = local?.TransferEncodingChunked ?? global.TransferEncodingChunked;
			TcpStats = local?.EnableTcpStats ?? global.EnableTcpStats;
			ThreadPoolStats = local?.EnableThreadPoolStats ?? global.EnableThreadPoolStats;
		}

		private readonly string _path;

		public string Accept { get; }
		public IReadOnlyCollection<int> AllowedStatusCodes { get; }

		public ApiKeyAuthenticationCredentials ApiKeyAuthenticationCredentials { get; }

		public BasicAuthenticationCredentials BasicAuthorizationCredentials { get; }

		public X509CertificateCollection ClientCertificates { get; }
		public IConnectionConfigurationValues ConnectionSettings { get; }
		public CustomResponseBuilderBase CustomResponseBuilder { get; }
		public bool DisableAutomaticProxyDetection { get; }

		public NameValueCollection Headers { get; }
		public bool HttpCompression { get; }
		public int KeepAliveInterval { get; }
		public int KeepAliveTime { get; }
		public bool MadeItToResponse { get; set; }
		public IMemoryStreamFactory MemoryStreamFactory { get; }

		public HttpMethod Method { get; }

		public Node Node { get; set; }
		public AuditEvent OnFailureAuditEvent => MadeItToResponse ? AuditEvent.BadResponse : AuditEvent.BadRequest;
		public PipelineFailure OnFailurePipelineFailure => MadeItToResponse ? PipelineFailure.BadResponse : PipelineFailure.BadRequest;
		public string PathAndQuery { get; }
		public TimeSpan PingTimeout { get; }

		public bool Pipelined { get; }
		public PostData PostData { get; }
		public string ProxyAddress { get; }
		public SecureString ProxyPassword { get; }
		public string ProxyUsername { get; }
		// TODO: rename to ContentType in 8.0.0
		public string RequestMimeType { get; }
		public TimeSpan RequestTimeout { get; }
		public string RunAs { get; }
		public IReadOnlyCollection<int> SkipDeserializationForStatusCodes { get; }
		public bool ThrowExceptions { get; }
		public string UserAgent { get; }
		public bool TransferEncodingChunked { get; }
		public bool TcpStats { get; }
		public bool ThreadPoolStats { get; }

		public Uri Uri => Node != null ? new Uri(Node.Uri, PathAndQuery) : null;
		public TimeSpan DnsRefreshTimeout { get; }

		public override string ToString() => $"{Method.GetStringValue()} {_path}";

		// TODO This feels like its in the wrong place
		private string CreatePathWithQueryStrings(string path, IConnectionConfigurationValues global, IRequestParameters request)
		{
			path ??= string.Empty;
			if (path.Contains("?"))
				throw new ArgumentException($"{nameof(path)} can not contain querystring parameters and needs to be already escaped");

			var g = global.QueryStringParameters;
			var l = request?.QueryString;

			if ((g == null || g.Count == 0) && (l == null || l.Count == 0)) return path;

			//create a copy of the global query string collection if needed.
			var nv = g == null ? new NameValueCollection() : new NameValueCollection(g);

			//set all querystring pairs from local `l` on the querystring collection
			var formatter = ConnectionSettings.UrlFormatter;
			nv.UpdateFromDictionary(l, formatter);

			//if nv has no keys simply return path as provided
			if (!nv.HasKeys()) return path;

			//create string for query string collection where key and value are escaped properly.
			var queryString = nv.ToQueryString();
			path += queryString;
			return path;
		}
	}
}

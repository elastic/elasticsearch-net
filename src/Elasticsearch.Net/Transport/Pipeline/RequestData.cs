// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Elasticsearch.Net.Extensions;

namespace Elasticsearch.Net
{
	public class RequestData
	{
		private const string MimeTypeOld = "application/json";

		public const string MimeTypeTextPlain = "text/plain";

		public const string OpaqueIdHeader = "X-Opaque-Id";
		public const string RunAsSecurityHeader = "es-security-runas-user";

		private static readonly string MimeType = "application/vnd.elasticsearch+json; compatible-with="
			+ ClientVersionInfo.LowLevelClientVersionInfo.Version.Major;

		private static readonly string TrimmedMimeType = "application/vnd.elasticsearch+json;compatible-with="
			+ ClientVersionInfo.LowLevelClientVersionInfo.Version.Major;

		public static readonly string DefaultJsonMimeType =
			ClientVersionInfo.LowLevelClientVersionInfo.Version.Major >= 8 ? MimeType : MimeTypeOld;

		private readonly string _path;
		private Node _node;
		private Uri _requestUri;

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

			JsonContentMimeType = DefaultJsonBasedOnConfigurationSettings(ConnectionSettings);

			if (data != null)
				data.DisableDirectStreaming = local?.DisableDirectStreaming ?? global.DisableDirectStreaming;

			Pipelined = local?.EnableHttpPipelining ?? global.HttpPipeliningEnabled;
			HttpCompression = global.EnableHttpCompression;
			RequestMimeType = local?.ContentType ?? JsonContentMimeType;
			Accept = local?.Accept ?? JsonContentMimeType;

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
			MetaHeaderProvider = global.MetaHeaderProvider;
			RequestMetaData = local?.RequestMetaData?.Items ?? EmptyReadOnly<string, string>.Dictionary;
		}

		public string Accept { get; }
		public IReadOnlyCollection<int> AllowedStatusCodes { get; }

		public ApiKeyAuthenticationCredentials ApiKeyAuthenticationCredentials { get; }

		public BasicAuthenticationCredentials BasicAuthorizationCredentials { get; }

		public X509CertificateCollection ClientCertificates { get; }
		public IConnectionConfigurationValues ConnectionSettings { get; }
		public CustomResponseBuilderBase CustomResponseBuilder { get; }
		public bool DisableAutomaticProxyDetection { get; }

		public TimeSpan DnsRefreshTimeout { get; }

		public NameValueCollection Headers { get; }
		public bool HttpCompression { get; }

		public bool IsAsync { get; internal set; }

		public string JsonContentMimeType { get; }
		public int KeepAliveInterval { get; }
		public int KeepAliveTime { get; }
		public bool MadeItToResponse { get; set; }
		public IMemoryStreamFactory MemoryStreamFactory { get; }

		public MetaHeaderProvider MetaHeaderProvider { get; }

		public HttpMethod Method { get; }

		public Node Node
		{
			get => _node;
			set
			{
				_requestUri = null;
				_node = value;
			}
		}

		public AuditEvent OnFailureAuditEvent => MadeItToResponse ? AuditEvent.BadResponse : AuditEvent.BadRequest;
		public PipelineFailure OnFailurePipelineFailure => MadeItToResponse ? PipelineFailure.BadResponse : PipelineFailure.BadRequest;
		public string PathAndQuery { get; }
		public TimeSpan PingTimeout { get; }

		public bool Pipelined { get; }
		public PostData PostData { get; }
		public string ProxyAddress { get; }
		public SecureString ProxyPassword { get; }
		public string ProxyUsername { get; }

		public IReadOnlyDictionary<string, string> RequestMetaData { get; }

		// TODO: rename to ContentType in 8.0.0
		public string RequestMimeType { get; }
		public TimeSpan RequestTimeout { get; }
		public string RunAs { get; }
		public IReadOnlyCollection<int> SkipDeserializationForStatusCodes { get; }
		public bool TcpStats { get; }
		public bool ThreadPoolStats { get; }
		public bool ThrowExceptions { get; }
		public bool TransferEncodingChunked { get; }

		/// <summary>
		/// The <see cref="Uri" /> for the request.
		/// </summary>
		public Uri Uri
		{
			get
			{
				if (_requestUri is not null) return _requestUri;

				_requestUri = Node is not null ? new Uri(Node.Uri, PathAndQuery) : null;
				return _requestUri;
			}
		}

		public string UserAgent { get; }

		public override string ToString() => $"{Method.GetStringValue()} {_path}";

		public static string DefaultJsonBasedOnConfigurationSettings(IConnectionConfigurationValues settings) =>
			settings.EnableApiVersioningHeader ? MimeType : MimeTypeOld;


		public static bool IsJsonMimeType(string mimeType) =>
			ValidResponseContentType(MimeType, mimeType) || ValidResponseContentType(MimeTypeOld, mimeType);

		public static bool ValidResponseContentType(string acceptMimeType, string responseMimeType)
		{
			if (string.IsNullOrEmpty(acceptMimeType)) return false;
			if (string.IsNullOrEmpty(responseMimeType)) return false;

			// we a startswith check because the response can return charset information
			// e.g: application/json; charset=UTF-8
			if (acceptMimeType == MimeTypeOld)
				return responseMimeType.StartsWith(MimeTypeOld, StringComparison.OrdinalIgnoreCase);

			//vendored check
			if (acceptMimeType == MimeType)
			{
				// we check both vendored and nonvendored since on 7.x the response does not return a
				// vendored Content-Type header on the response.
				return
					responseMimeType.Equals(MimeType, StringComparison.Ordinal)
					|| responseMimeType.Equals(TrimmedMimeType, StringComparison.Ordinal) // Required for .NET FX as the whitespace in the response Content-Type header is stripped
					|| responseMimeType.Equals(MimeTypeOld, StringComparison.Ordinal)
					|| responseMimeType.StartsWith(MimeTypeOld, StringComparison.OrdinalIgnoreCase)
					|| responseMimeType.StartsWith(MimeType, StringComparison.OrdinalIgnoreCase);
			}

			return responseMimeType.StartsWith(acceptMimeType, StringComparison.OrdinalIgnoreCase);
		}


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

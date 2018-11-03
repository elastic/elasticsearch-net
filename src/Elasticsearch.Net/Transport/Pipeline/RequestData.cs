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
		public const string OpaqueIdHeader = "X-Opaque-Id";
		public const string RunAsSecurityHeader = "es-security-runas-user";

		public RequestData(HttpMethod method, string path, PostData data, IConnectionConfigurationValues global, IRequestParameters local,
			IMemoryStreamFactory memoryStreamFactory
		)
			: this(method, data, global, local?.RequestConfiguration, memoryStreamFactory)
		{
			CustomConverter = local?.DeserializationOverride;
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
			Headers = global.Headers != null ? new NameValueCollection(global.Headers) : new NameValueCollection();

			if (!string.IsNullOrEmpty(local?.OpaqueId))
				Headers.Add(OpaqueIdHeader, local.OpaqueId);

			RunAs = local?.RunAs;
			SkipDeserializationForStatusCodes = global?.SkipDeserializationForStatusCodes;
			ThrowExceptions = local?.ThrowExceptions ?? global.ThrowExceptions;

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

		public X509CertificateCollection ClientCertificates { get; }
		public IConnectionConfigurationValues ConnectionSettings { get; }
		public Func<IApiCallDetails, Stream, object> CustomConverter { get; }
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
		public string PathAndQuery { get; }
		public TimeSpan PingTimeout { get; }

		public bool Pipelined { get; }
		public PostData PostData { get; }
		public string ProxyAddress { get; }
		public string ProxyPassword { get; }
		public string ProxyUsername { get; }
		public string RequestMimeType { get; }
		public TimeSpan RequestTimeout { get; }
		public string RunAs { get; }
		public IReadOnlyCollection<int> SkipDeserializationForStatusCodes { get; }
		public bool ThrowExceptions { get; }

		public Uri Uri => Node != null ? new Uri(Node.Uri, PathAndQuery) : null;

		private string CreatePathWithQueryStrings(string path, IConnectionConfigurationValues global, IRequestParameters request)
		{
			path = path ?? string.Empty;
			if (path.Contains("?"))
				throw new ArgumentException($"{nameof(path)} can not contain querystring parmeters and needs to be already escaped");

			var g = global.QueryStringParameters;
			var l = request?.QueryString;
			if (g?.Count == 0 && l?.Count == 0) return path;

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

	internal static class NameValueCollectionExtensions
	{
		internal static string ToQueryString(this NameValueCollection nv)
		{
			if (nv == null) return string.Empty;
			if (nv.AllKeys.Length == 0) return string.Empty;

			string E(string v)
			{
				return Uri.EscapeDataString(v);
			}

			return "?" + string.Join("&", nv.AllKeys.Select(key => $"{E(key)}={E(nv[key])}"));
		}

		internal static void UpdateFromDictionary(this NameValueCollection queryString, Dictionary<string, object> queryStringUpdates,
			ElasticsearchUrlFormatter provider
		)
		{
			if (queryString == null || queryString.Count < 0) return;
			if (queryStringUpdates == null || queryStringUpdates.Count < 0) return;

			foreach (var kv in queryStringUpdates.Where(kv => !kv.Key.IsNullOrEmpty()))
			{
				if (kv.Value == null)
				{
					queryString.Remove(kv.Key);
					continue;
				}
				var resolved = provider.CreateString(kv.Value);
				if (!resolved.IsNullOrEmpty())
					queryString[kv.Key] = resolved;
				else
					queryString.Remove(kv.Key);
			}
		}
	}
}

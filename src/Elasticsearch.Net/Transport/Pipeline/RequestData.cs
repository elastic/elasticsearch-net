using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Elasticsearch.Net
{
	public class RequestData
	{
		public const string MimeType = "application/json";
		public const string RunAsSecurityHeader = "es-security-runas-user";
		public const string OpaqueIdHeader = "X-Opaque-Id";

		public Uri Uri => this.Node != null ? new Uri(this.Node.Uri, this.PathAndQuery) : null;

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

			if (!string.IsNullOrEmpty(local?.OpaqueId))
				this.Headers.Add(OpaqueIdHeader, local.OpaqueId);

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
			if (path.Contains("?"))
				throw new ArgumentException($"{nameof(path)} can not contain querystring parmeters and needs to be already escaped");

			var g = global.QueryStringParameters;
			var l = request?.QueryString;
			if (g?.Count == 0 && l?.Count == 0) return path;

			//create a copy of the global query string collection if needed.
			var nv = g == null ? new NameValueCollection() : new NameValueCollection(g);

			//set all querystring pairs from local `l` on the querystring collection
			var formatter = this.ConnectionSettings.UrlFormatter;
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
			string E(string v) => Uri.EscapeDataString(v);
			return "?" + string.Join("&", nv.AllKeys.Select(key => $"{E(key)}={E(nv[key])}"));
		}

		internal static void UpdateFromDictionary(this NameValueCollection queryString, Dictionary<string, object> queryStringUpdates, ElasticsearchUrlFormatter provider)
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

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Purify;

namespace Elasticsearch.Net
{
	public class RequestData
	{
		public const string MimeType = "application/json";

		public Uri Uri => new Uri(this.Node.Uri, this.Path).Purify();

		public HttpMethod Method { get; private set; }
		public string Path { get; }
		public PostData<object> PostData { get; }

		public Node Node { get; internal set; }
		public TimeSpan RequestTimeout { get; }
		public TimeSpan PingTimeout { get; }
		public int KeepAliveTime { get; }
		public int KeepAliveInterval { get; }

		public bool Pipelined { get; }
		public bool HttpCompression { get; }
		public string ContentType { get; }
		public string RunAs { get; }

		public NameValueCollection Headers { get; }
		public string ProxyAddress { get; }
		public string ProxyUsername { get; }
		public string ProxyPassword { get; }
		public bool DisableAutomaticProxyDetection { get; }
		public BasicAuthenticationCredentials BasicAuthorizationCredentials { get; }
		public CancellationToken CancellationToken { get; }
		public IEnumerable<int> AllowedStatusCodes { get; }
		public Func<IApiCallDetails, Stream, object> CustomConverter { get; private set; }
		public IConnectionConfigurationValues ConnectionSettings { get; }
		public IMemoryStreamFactory MemoryStreamFactory { get; }

		[Obsolete("this constructor is scheduled to be removed in the next major version")]
		public RequestData(HttpMethod method, string path, PostData<object> data, IConnectionConfigurationValues global, IMemoryStreamFactory memoryStreamFactory)
#pragma warning disable CS0618 // Type or member is obsolete
			: this(method, path, data, global, (IRequestConfiguration)null, memoryStreamFactory)
#pragma warning restore CS0618 // Type or member is obsolete
		{ }

		public RequestData(HttpMethod method, string path, PostData<object> data, IConnectionConfigurationValues global, IRequestParameters local, IMemoryStreamFactory memoryStreamFactory)
#pragma warning disable CS0618 // Type or member is obsolete
			: this(method, path, data, global, (IRequestConfiguration)local?.RequestConfiguration, memoryStreamFactory)
#pragma warning restore CS0618 // Type or member is obsolete
		{
			this.CustomConverter = local?.DeserializationOverride;
			this.Path = this.CreatePathWithQueryStrings(path, this.ConnectionSettings, local);
		}

		[Obsolete("this constructor is scheduled to become private in the next major version")]
		public RequestData(HttpMethod method, string path, PostData<object> data, IConnectionConfigurationValues global, IRequestConfiguration local, IMemoryStreamFactory memoryStreamFactory)
		{
			this.ConnectionSettings = global;
			this.MemoryStreamFactory = memoryStreamFactory;
			this.Method = method;
			this.PostData = data;
			this.Path = this.CreatePathWithQueryStrings(path, this.ConnectionSettings, null);

			this.Pipelined = global.HttpPipeliningEnabled || (local?.EnableHttpPipelining).GetValueOrDefault(false);
			this.HttpCompression = global.EnableHttpCompression;
			this.ContentType = local?.ContentType ?? MimeType;
			this.Headers = global.Headers;
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
			this.CancellationToken = local?.CancellationToken ?? CancellationToken.None;
			this.AllowedStatusCodes = local?.AllowedStatusCodes ?? Enumerable.Empty<int>();
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

		protected bool Equals(RequestData other) =>
			RequestTimeout.Equals(other.RequestTimeout)
			&& PingTimeout.Equals(other.PingTimeout)
			&& KeepAliveTime == other.KeepAliveTime
			&& KeepAliveInterval == other.KeepAliveInterval
			&& Pipelined == other.Pipelined
			&& HttpCompression == other.HttpCompression
			&& Equals(Headers, other.Headers)
			&& string.Equals(RunAs, other.RunAs)
			&& string.Equals(ProxyAddress, other.ProxyAddress)
			&& string.Equals(ProxyUsername, other.ProxyUsername)
			&& string.Equals(ProxyPassword, other.ProxyPassword)
			&& DisableAutomaticProxyDetection == other.DisableAutomaticProxyDetection
			&& Equals(BasicAuthorizationCredentials, other.BasicAuthorizationCredentials)
			&& Equals(ConnectionSettings, other.ConnectionSettings)
			&& Equals(MemoryStreamFactory, other.MemoryStreamFactory);

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((RequestData) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = RequestTimeout.GetHashCode();
				hashCode = (hashCode*397) ^ PingTimeout.GetHashCode();
				hashCode = (hashCode*397) ^ KeepAliveTime;
				hashCode = (hashCode*397) ^ KeepAliveInterval;
				hashCode = (hashCode*397) ^ (RunAs?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ Pipelined.GetHashCode();
				hashCode = (hashCode*397) ^ HttpCompression.GetHashCode();
				hashCode = (hashCode*397) ^ (Headers?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (ProxyAddress?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (ProxyUsername?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (ProxyPassword?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ DisableAutomaticProxyDetection.GetHashCode();
				hashCode = (hashCode*397) ^ (BasicAuthorizationCredentials?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (ConnectionSettings?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (MemoryStreamFactory?.GetHashCode() ?? 0);
				return hashCode;
			}
		}

	}
}

using System;
using System.Collections.Generic;
using System.Threading;

namespace Elasticsearch.Net
{
	public interface IRequestConfiguration
	{
		/// <summary>
		/// The timeout for this specific request, takes precedence over the global timeout settings
		/// </summary>
		TimeSpan? RequestTimeout { get; set; }

		/// <summary>
		/// The ping timeout for this specific request
		/// </summary>
		TimeSpan? PingTimeout { get; set;  }

		/// <summary>
		/// Force a difference content type header on the request
		/// </summary>
		string ContentType { get; set; }

		/// <summary>
		/// This will override whatever is set on the connection configuration or whatever default the connectionpool has.
		/// </summary>
		int? MaxRetries { get; set; }

		/// <summary>
		/// This will force the operation on the specified node, this will bypass any configured connection pool and will no retry.
		/// </summary>
		Uri ForceNode { get; set; }

		/// <summary>
		/// Forces no sniffing to occur on the request no matter what configuration is in place
		/// globally
		/// </summary>
		bool? DisableSniff { get; set; }

		/// <summary>
		/// Under no circumstance do a ping before the actual call. If a node was previously dead a small ping with
		/// low connect timeout will be tried first in normal circumstances
		/// </summary>
		bool? DisablePing { get; set; }

		/// <summary>
		/// Treat the following statuses (on top of the 200 range) NOT as error.
		/// </summary>
		IEnumerable<int> AllowedStatusCodes { get; set; }

		/// <summary>
		/// Basic access authorization credentials to specify with this request.
		/// Overrides any credentials that are set at the global IConnectionSettings level.
		/// </summary>
		BasicAuthenticationCredentials BasicAuthenticationCredentials { get; set; }

		/// <summary>
		/// Whether or not this request should be pipelined. http://en.wikipedia.org/wiki/HTTP_pipelining defaults to true
		/// </summary>
		bool EnableHttpPipelining { get; set; }

		/// <summary>
		/// The cancellation token to use to internally to cancel async operations
		/// </summary>
		CancellationToken CancellationToken { get; set; }

		/// <summary>
		/// Submit the request on behalf in the context of a different shield user
		/// <pre/>https://www.elastic.co/guide/en/shield/current/submitting-requests-for-other-users.html
		/// </summary>
		string RunAs { get; set; }
	}

	public class RequestConfiguration : IRequestConfiguration
	{
		public TimeSpan? RequestTimeout { get; set; }
		public TimeSpan? PingTimeout { get; set; }
		public string ContentType { get; set; }
		public int? MaxRetries { get; set; }
		public Uri ForceNode { get; set; }
		public bool? DisableSniff { get; set; }
		public bool? DisablePing { get; set; }
		public IEnumerable<int> AllowedStatusCodes { get; set; }
		public BasicAuthenticationCredentials BasicAuthenticationCredentials { get; set; }
		public bool EnableHttpPipelining { get; set; } = true;
		public CancellationToken CancellationToken { get; set; }
		/// <summary>
		/// Submit the request on behalf in the context of a different user
		/// https://www.elastic.co/guide/en/shield/current/submitting-requests-for-other-users.html
		/// </summary>
		public string RunAs { get; set; }
	}

	public class RequestConfigurationDescriptor : IRequestConfiguration
	{

		private IRequestConfiguration Self => this;
		TimeSpan? IRequestConfiguration.RequestTimeout { get; set; }
		TimeSpan? IRequestConfiguration.PingTimeout { get; set; }
		string IRequestConfiguration.ContentType { get; set; }
		int? IRequestConfiguration.MaxRetries { get; set; }
		Uri IRequestConfiguration.ForceNode { get; set; }
		bool? IRequestConfiguration.DisableSniff { get; set; }
		bool? IRequestConfiguration.DisablePing { get; set; }
		IEnumerable<int> IRequestConfiguration.AllowedStatusCodes { get; set; }
		BasicAuthenticationCredentials IRequestConfiguration.BasicAuthenticationCredentials { get; set; }
		bool IRequestConfiguration.EnableHttpPipelining { get; set; } = true;
		CancellationToken IRequestConfiguration.CancellationToken { get; set; }
		string IRequestConfiguration.RunAs { get; set; }

		public RequestConfigurationDescriptor(IRequestConfiguration config)
		{
			Self.RequestTimeout = config?.RequestTimeout;
			Self.PingTimeout = config?.PingTimeout;
			Self.ContentType = config?.ContentType;
			Self.MaxRetries = config?.MaxRetries;
			Self.ForceNode = config?.ForceNode;
			Self.DisableSniff = config?.DisableSniff;
			Self.DisablePing = config?.DisablePing;
			Self.AllowedStatusCodes = config?.AllowedStatusCodes;
			Self.BasicAuthenticationCredentials = config?.BasicAuthenticationCredentials;
			Self.EnableHttpPipelining = config?.EnableHttpPipelining ?? true;
			Self.CancellationToken = config?.CancellationToken ?? default(CancellationToken);
			Self.RunAs = config?.RunAs;
		}

		/// <summary>
		/// Submit the request on behalf in the context of a different shield user
		/// <pre/>https://www.elastic.co/guide/en/shield/current/submitting-requests-for-other-users.html
		/// </summary>
		public RequestConfigurationDescriptor RunAs(string username)
		{
			Self.RunAs = username;
			return this;
		}

		public RequestConfigurationDescriptor RequestTimeout(TimeSpan requestTimeout)
		{
			Self.RequestTimeout = requestTimeout;
			return this;
		}

		public RequestConfigurationDescriptor PingTimeout(TimeSpan pingTimeout)
		{
			Self.PingTimeout = pingTimeout;
			return this;
		}

		public RequestConfigurationDescriptor AcceptContentType(string acceptContentTypeHeader)
		{
			Self.ContentType = acceptContentTypeHeader;
			return this;
		}

		public RequestConfigurationDescriptor AllowedStatusCodes(IEnumerable<int> codes)
		{
			Self.AllowedStatusCodes = codes;
			return this;
		}

		public RequestConfigurationDescriptor AllowedStatusCodes(params int[] codes)
		{
			Self.AllowedStatusCodes = codes;
			return this;
		}

		public RequestConfigurationDescriptor DisableSniffing(bool? disable = true)
		{
			Self.DisableSniff = disable;
			return this;
		}

		public RequestConfigurationDescriptor DisablePing(bool? disable = true)
		{
			Self.DisablePing = disable;
			return this;
		}

		public RequestConfigurationDescriptor ForceNode(Uri uri)
		{
			Self.ForceNode = uri;
			return this;
		}
		public RequestConfigurationDescriptor MaxRetries(int retry)
		{
			Self.MaxRetries = retry;
			return this;
		}
		public RequestConfigurationDescriptor CancellationToken(CancellationToken token)
		{
			Self.CancellationToken = token;
			return this;
		}

		public RequestConfigurationDescriptor BasicAuthentication(string userName, string password)
		{
			if (Self.BasicAuthenticationCredentials == null)
				Self.BasicAuthenticationCredentials = new BasicAuthenticationCredentials();
			Self.BasicAuthenticationCredentials.Username = userName;
			Self.BasicAuthenticationCredentials.Password = password;
			return this;
		}

		public RequestConfigurationDescriptor EnableHttpPipelining(bool enable = true)
		{
			Self.EnableHttpPipelining = enable;
			return this;
		}
	}
}

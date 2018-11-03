using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Elasticsearch.Net
{
	public interface IRequestConfiguration
	{
		/// <summary>
		/// Force a different Accept header on the request
		/// </summary>
		string Accept { get; set; }

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
		/// Use the following client certificates to authenticate this single request
		/// </summary>
		X509CertificateCollection ClientCertificates { get; set; }

		/// <summary>
		/// Force a different Content-Type header on the request
		/// </summary>
		string ContentType { get; set; }

		/// <summary>
		/// Whether to buffer the request and response bytes for the call
		/// </summary>
		bool? DisableDirectStreaming { get; set; }

		/// <summary>
		/// Under no circumstance do a ping before the actual call. If a node was previously dead a small ping with
		/// low connect timeout will be tried first in normal circumstances
		/// </summary>
		bool? DisablePing { get; set; }

		/// <summary>
		/// Forces no sniffing to occur on the request no matter what configuration is in place
		/// globally
		/// </summary>
		bool? DisableSniff { get; set; }

		/// <summary>
		/// Whether or not this request should be pipelined. http://en.wikipedia.org/wiki/HTTP_pipelining defaults to true
		/// </summary>
		bool EnableHttpPipelining { get; set; }

		/// <summary>
		/// This will force the operation on the specified node, this will bypass any configured connection pool and will no retry.
		/// </summary>
		Uri ForceNode { get; set; }

		/// <summary>
		/// This will override whatever is set on the connection configuration or whatever default the connectionpool has.
		/// </summary>
		int? MaxRetries { get; set; }

		/// <summary>
		/// Associate an Id with this user-initiated task, such that it can be located in the cluster task list.
		/// Valid only for Elasticsearch 6.2.0+
		/// </summary>
		string OpaqueId { get; set; }

		/// <summary>
		/// The ping timeout for this specific request
		/// </summary>
		TimeSpan? PingTimeout { get; set; }

		/// <summary>
		/// The timeout for this specific request, takes precedence over the global timeout settings
		/// </summary>
		TimeSpan? RequestTimeout { get; set; }

		/// <summary>
		/// Submit the request on behalf in the context of a different shield user
		/// <pre />https://www.elastic.co/guide/en/shield/current/submitting-requests-for-other-users.html
		/// </summary>
		string RunAs { get; set; }

		/// <summary>
		/// Instead of following a c/go like error checking on response.IsValid always throw an exception
		/// on the client when a call resulted in an exception on either the client or the Elasticsearch server.
		/// <para>Reasons for such exceptions could be search parser errors, index missing exceptions, etc...</para>
		/// </summary>
		bool ThrowExceptions { get; set; }
	}

	public class RequestConfiguration : IRequestConfiguration
	{
		public string Accept { get; set; }
		public IEnumerable<int> AllowedStatusCodes { get; set; }
		public BasicAuthenticationCredentials BasicAuthenticationCredentials { get; set; }
		public CancellationToken CancellationToken { get; set; }

		public X509CertificateCollection ClientCertificates { get; set; }
		public string ContentType { get; set; }
		public bool? DisableDirectStreaming { get; set; }
		public bool? DisablePing { get; set; }
		public bool? DisableSniff { get; set; }
		public bool EnableHttpPipelining { get; set; } = true;
		public Uri ForceNode { get; set; }
		public int? MaxRetries { get; set; }
		public string OpaqueId { get; set; }
		public TimeSpan? PingTimeout { get; set; }
		public TimeSpan? RequestTimeout { get; set; }

		/// <summary>
		/// Submit the request on behalf in the context of a different user
		/// https://www.elastic.co/guide/en/shield/current/submitting-requests-for-other-users.html
		/// </summary>
		public string RunAs { get; set; }

		public bool ThrowExceptions { get; set; }
	}

	public class RequestConfigurationDescriptor : IRequestConfiguration
	{
		public RequestConfigurationDescriptor(IRequestConfiguration config)
		{
			Self.RequestTimeout = config?.RequestTimeout;
			Self.PingTimeout = config?.PingTimeout;
			Self.ContentType = config?.ContentType;
			Self.Accept = config?.Accept;
			Self.MaxRetries = config?.MaxRetries;
			Self.ForceNode = config?.ForceNode;
			Self.DisableSniff = config?.DisableSniff;
			Self.DisablePing = config?.DisablePing;
			Self.DisableDirectStreaming = config?.DisableDirectStreaming;
			Self.AllowedStatusCodes = config?.AllowedStatusCodes;
			Self.BasicAuthenticationCredentials = config?.BasicAuthenticationCredentials;
			Self.EnableHttpPipelining = config?.EnableHttpPipelining ?? true;
			Self.RunAs = config?.RunAs;
			Self.ClientCertificates = config?.ClientCertificates;
			Self.OpaqueId = config?.OpaqueId;
		}

		string IRequestConfiguration.Accept { get; set; }
		IEnumerable<int> IRequestConfiguration.AllowedStatusCodes { get; set; }
		BasicAuthenticationCredentials IRequestConfiguration.BasicAuthenticationCredentials { get; set; }
		X509CertificateCollection IRequestConfiguration.ClientCertificates { get; set; }
		string IRequestConfiguration.ContentType { get; set; }
		bool? IRequestConfiguration.DisableDirectStreaming { get; set; }
		bool? IRequestConfiguration.DisablePing { get; set; }
		bool? IRequestConfiguration.DisableSniff { get; set; }
		bool IRequestConfiguration.EnableHttpPipelining { get; set; } = true;
		Uri IRequestConfiguration.ForceNode { get; set; }

		int? IRequestConfiguration.MaxRetries { get; set; }
		string IRequestConfiguration.OpaqueId { get; set; }
		TimeSpan? IRequestConfiguration.PingTimeout { get; set; }
		TimeSpan? IRequestConfiguration.RequestTimeout { get; set; }
		string IRequestConfiguration.RunAs { get; set; }
		private IRequestConfiguration Self => this;
		bool IRequestConfiguration.ThrowExceptions { get; set; }

		/// <summary>
		/// Submit the request on behalf in the context of a different shield user
		/// <pre />https://www.elastic.co/guide/en/shield/current/submitting-requests-for-other-users.html
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

		/// <summary>
		/// Associate an Id with this user-initiated task, such that it can be located in the cluster task list.
		/// Valid only for Elasticsearch 6.2.0+
		/// </summary>
		public RequestConfigurationDescriptor OpaqueId(string opaqueId)
		{
			Self.OpaqueId = opaqueId;
			return this;
		}

		public RequestConfigurationDescriptor PingTimeout(TimeSpan pingTimeout)
		{
			Self.PingTimeout = pingTimeout;
			return this;
		}

		public RequestConfigurationDescriptor ContentType(string contentTypeHeader)
		{
			Self.ContentType = contentTypeHeader;
			return this;
		}

		public RequestConfigurationDescriptor Accept(string acceptHeader)
		{
			Self.Accept = acceptHeader;
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

		public RequestConfigurationDescriptor ThrowExceptions(bool throwExceptions = true)
		{
			Self.ThrowExceptions = throwExceptions;
			return this;
		}

		public RequestConfigurationDescriptor DisableDirectStreaming(bool? disable = true)
		{
			Self.DisableDirectStreaming = disable;
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

		/// <summary> Use the following client certificates to authenticate this request to Elasticsearch </summary>
		public RequestConfigurationDescriptor ClientCertificates(X509CertificateCollection certificates)
		{
			Self.ClientCertificates = certificates;
			return this;
		}

		/// <summary> Use the following client certificate to authenticate this request to Elasticsearch </summary>
		public RequestConfigurationDescriptor ClientCertificate(X509Certificate certificate) =>
			ClientCertificates(new X509Certificate2Collection { certificate });

		/// <summary> Use the following client certificate to authenticate this request to Elasticsearch </summary>
		public RequestConfigurationDescriptor ClientCertificate(string certificatePath) =>
			ClientCertificates(new X509Certificate2Collection { new X509Certificate(certificatePath) });
	}
}

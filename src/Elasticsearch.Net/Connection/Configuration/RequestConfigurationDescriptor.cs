using Elasticsearch.Net.Connection.Security;
using System;
using System.Collections.Generic;

namespace Elasticsearch.Net.Connection.Configuration
{
	public class RequestConfigurationDescriptor : IRequestConfiguration
	{
		private IRequestConfiguration Self { get { return this; } }

		int? IRequestConfiguration.RequestTimeout { get; set; }

		int? IRequestConfiguration.ConnectTimeout { get; set; }
	
		string IRequestConfiguration.ContentType { get; set; }
		
		int? IRequestConfiguration.MaxRetries { get; set; }
		
		Uri IRequestConfiguration.ForceNode { get; set; }
		
		bool? IRequestConfiguration.DisableSniff { get; set; }
		
		bool? IRequestConfiguration.DisablePing { get; set; }
		
		IEnumerable<int> IRequestConfiguration.AllowedStatusCodes { get; set; }

		BasicAuthorizationCredentials IRequestConfiguration.BasicAuthorizationCredentials { get; set; }

		bool IRequestConfiguration.EnableHttpPipelining { get; set; }

		public RequestConfigurationDescriptor RequestTimeout(int requestTimeoutInMilliseconds)
		{
			Self.RequestTimeout = requestTimeoutInMilliseconds;
			return this;
		}

		public RequestConfigurationDescriptor ConnectTimeout(int connectTimeoutInMilliseconds)
		{
			Self.ConnectTimeout = connectTimeoutInMilliseconds;
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

		public RequestConfigurationDescriptor BasicAuthentication(string userName, string password)
		{
			if (Self.BasicAuthorizationCredentials == null)
				Self.BasicAuthorizationCredentials = new BasicAuthorizationCredentials();
			Self.BasicAuthorizationCredentials.UserName = userName;
			Self.BasicAuthorizationCredentials.Password = password;
			return this;
		}

		public RequestConfigurationDescriptor EnableHttpPipelining(bool enable = true)
		{
			Self.EnableHttpPipelining = enable;
			return this;
		}
	}
}
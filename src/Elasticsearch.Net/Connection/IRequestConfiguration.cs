using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Elasticsearch.Net.Connection
{
	public interface IRequestConfiguration : IRequestConnectionConfiguration
	{
		/// <summary>
		/// This will override whatever is set on the connection configuration or whatever default the connectionpool has.
		/// </summary>
		int? MaxRetries { get; }

		/// <summary>
		/// This will force the operation on the specified node, this will bypass any configured connection pool and will no retry.
		/// </summary>
		Uri ForcedNode { get; }

		/// <summary>
		/// Forces no sniffing to occur on the request no matter what configuration is in place 
		/// globally
		/// </summary>
		bool? SniffingDisabled { get; }

		bool? PingDisabled { get; }

		IEnumerable<int> AllowedStatusCodes { get; }


	}

	public class RequestConfiguration : RequestConfiguration<RequestConfiguration>, IRequestConfiguration
	{
		
	}

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RequestConfiguration<T> : RequestConnectionConfiguration<T>, IRequestConfiguration
		where T : RequestConfiguration<T>, IRequestConfiguration
	{
		private int? _maxRetries;
		private Uri _forceNode;
		private bool? _sniffingDisabled;
		private bool? _pingDisabled;
		private IEnumerable<int> _allowedStatusCodes;

		int? IRequestConfiguration.MaxRetries
		{
			get { return _maxRetries; }
		}

		Uri IRequestConfiguration.ForcedNode
		{
			get { return _forceNode; }
		}

		bool? IRequestConfiguration.SniffingDisabled
		{
			get { return _sniffingDisabled; }
		}

		bool? IRequestConfiguration.PingDisabled
		{
			get { return _pingDisabled; }
		}

		IEnumerable<int> IRequestConfiguration.AllowedStatusCodes
		{
			get { return _allowedStatusCodes ?? Enumerable.Empty<int>();  }
		}
		
		public T AllowStatusCodes(IEnumerable<int> codes)
		{
			this._allowedStatusCodes = codes;
			return (T)this;
		}

		public T AllowStatusCodes(params int[] codes)
		{
			this._allowedStatusCodes = codes;
			return (T)this;
		}

		public T DisableSniffing(bool? disable = true)
		{
			this._sniffingDisabled = disable;
			return (T)this;
		}

		public T DisablePing(bool? disable = true)
		{
			this._pingDisabled = disable;
			return (T)this;
		}

		public T ForceNode(Uri uri)
		{
			this._forceNode = uri;
			return (T)this;
		}
		public T Retry(int retry)
		{
			this._maxRetries = retry;
			return (T)this;
		}
		
		
	}
}
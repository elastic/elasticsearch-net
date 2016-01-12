using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Elasticsearch.Net.Providers;

namespace Elasticsearch.Net.ConnectionPool
{
	public class SniffingConnectionPool : StaticConnectionPool, IDisposable
	{
		private readonly ReaderWriterLockSlim _readerWriter = new ReaderWriterLockSlim();
	    private bool _isDisposed;

		public override bool AcceptsUpdates { get { return true; } }

		public SniffingConnectionPool(
			IEnumerable<Uri> uris, 
			bool randomizeOnStartup = true, 
			IDateTimeProvider dateTimeProvider = null)
			: base(uris, randomizeOnStartup, dateTimeProvider)
		{
		}

		public override void UpdateNodeList(IList<Uri> newClusterState, Uri sniffNode = null)
		{
		    CheckDisposed();

			try
			{
				this._readerWriter.EnterWriteLock();
				this.NodeUris = newClusterState;
				this.UriLookup = newClusterState.ToDictionary(k => k, v => new EndpointState()
				{
					Attemps = v.Equals(sniffNode) ? 1 : 0
				});
			}
			finally
			{
				this._readerWriter.ExitWriteLock();
			}
		}

		public override Uri GetNext(int? initialSeed, out int seed, out bool shouldPingHint)
		{
            CheckDisposed();

            try
			{
				this._readerWriter.EnterReadLock();
				return base.GetNext(initialSeed, out seed, out shouldPingHint);
			}
			finally
			{
				this._readerWriter.ExitReadLock();
			}
		}

		public override void MarkAlive(Uri uri)
		{
            CheckDisposed();

            try
			{
				this._readerWriter.EnterReadLock();
				base.MarkAlive(uri);
			}
			finally
			{
				this._readerWriter.ExitReadLock();
				
			}
		}

		public override void MarkDead(Uri uri, int? deadTimeout, int? maxDeadTimeout)
		{
            CheckDisposed();

            try
			{
				this._readerWriter.EnterReadLock();
				base.MarkDead(uri, deadTimeout, maxDeadTimeout);
			}
			finally
			{
				this._readerWriter.ExitReadLock();
				
			}
		}

	    public void Dispose()
	    {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

	    protected virtual void Dispose(bool disposing)
	    {
	        if (!_isDisposed)
	        {
	            if (disposing)
	            {
	                _readerWriter.Dispose();
	                _isDisposed = true;
	            }
	            _isDisposed = true;
	        }
	    }

	    private void CheckDisposed()
	    {
            if(_isDisposed)
                throw new ObjectDisposedException(nameof(SniffingConnectionPool));
	    }
    }
}
using System;
using System.Diagnostics;

namespace Elasticsearch.Net
{
	internal class Auditable : IDisposable
	{
		public static Auditable Noop { get; } = new Auditable();

		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly Func<long?> _getElapsed;
		private readonly Audit _audit;
		private readonly long? _currentElapsedTicks;

		private bool _isDisposed;
		private Stopwatch _sw;

		public Node Node { set => this._audit.Node = value; }
		public AuditEvent Event { set => this._audit.Event = value; }
		public Exception Exception { set => this._audit.Exception = value; }

#pragma warning disable 618 //obselete member scheduled for removal
		public string Path { set => this._audit.Path = value; }
#pragma warning restore 618

		private Auditable()
		{
			this._isDisposed = true;
			this._audit = Audit.Noop;
		}

		public Auditable(Audit audit, IDateTimeProvider dateTimeProvider, Func<long?> getElapsed)
		{
			this._audit = audit;
			this._dateTimeProvider = dateTimeProvider;

			this._getElapsed = getElapsed;
			this._currentElapsedTicks = getElapsed();
		}

		public void Dispose()
		{
			if (this._isDisposed) return;
			this._isDisposed = true;

			this._audit.ElapsedTicks = this._getElapsed() - _currentElapsedTicks.GetValueOrDefault(0);
			this._audit.Ended = this._dateTimeProvider.Now();
		}
	}
}

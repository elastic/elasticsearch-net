using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Elasticsearch.Net
{
	public class ClientProfileMeasurement : IDisposable
	{
		public static IReadOnlyCollection<ClientProfileMeasurement> EmptyCollection { get; } =
			new ReadOnlyCollection<ClientProfileMeasurement>(new List<ClientProfileMeasurement>());

		private readonly Func<long> _getTicks;
		private readonly long _startTicks;
		public string Section { get; }
		public long ElapsedTicks { get; private set; }

		public TimeSpan Elapsed => TimeSpan.FromTicks((long)(((double) this.ElapsedTicks / Stopwatch.Frequency) * 10_000_000));

		public ClientProfileMeasurement(string section, Func<long> getTicks)
		{
			this._getTicks = getTicks;
			this._startTicks = this._getTicks();
			this.Section = section;
		}
		void IDisposable.Dispose()
		{
			this.ElapsedTicks = _getTicks() - this._startTicks;
		}

		public override string ToString() => $"[{Section}] Elapsed: {Elapsed}";
	}
}

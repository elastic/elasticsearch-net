using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace Elasticsearch.Net
{
	/// <summary>
	/// An audit of a request made
	/// </summary>
	public class Audit
	{
		/// <summary>
		/// The type of audit event
		/// </summary>
		public AuditEvent Event { get; internal set; }

		/// <summary>
		/// The start date and time of the audit
		/// </summary>
		public DateTime Started { get; }

		/// <summary>
		/// The end date and time of the audit
		/// </summary>
		public DateTime Ended { get; internal set; }

		/// <summary>
		///
		/// </summary>
		internal long? ElapsedTicks { get; set; }

		public TimeSpan Elapsed => !ElapsedTicks.HasValue || ElapsedTicks.Value < 0
			? Ended - Started
			: TimeSpan.FromTicks((long)(((double) this.ElapsedTicks / Stopwatch.Frequency) * 10_000_000));

		/// <summary>
		/// The node on which the request was made
		/// </summary>
		public Node Node { get; internal set; }

		/// <summary>
		/// The path of the request
		/// </summary>
		[Obsolete("Scheduled for removal in 6.0")]
		public string Path { get; internal set; }

		/// <summary>
		/// The exception for the audit, if there was one.
		/// </summary>
		public Exception Exception { get; internal set; }

		public IReadOnlyCollection<ClientProfileMeasurement> ProfileMeasurements { get; internal set; }

		public static Audit Noop { get; } = new Audit();
		private readonly bool _isNoop;
		internal Audit()
		{
			_isNoop = true;
		}

		public Audit(AuditEvent type, DateTime started) : this(type, started, null) { }
		public Audit(AuditEvent type, DateTime started, IReadOnlyCollection<ClientProfileMeasurement> profiles)
		{
			this.Event = type;
			this.Started = started;
			this.ProfileMeasurements = profiles ?? ClientProfileMeasurement.EmptyCollection;
		}

		public override string ToString()
		{
			if (this._isNoop) return "Noop audit instance";
			var sb = new StringBuilder();
			sb.Append($"{this.Event.GetStringValue()}:");
			if (this.Node?.Uri != null) sb.Append($" Node: {this.Node.Uri}");
			if (this.Exception != null) sb.Append($" Exception: {this.Exception.GetType().Name}");
			sb.Append($" Elapsed: {this.Elapsed}");
			return sb.ToString();
		}
	}
}

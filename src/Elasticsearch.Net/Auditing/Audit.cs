using System;

namespace Elasticsearch.Net
{
	public class DiagnosticAudit
	{
		/// <summary>
		/// The exception for the audit, if there was one.
		/// </summary>
		public Exception Exception { get; internal set; }

		/// <summary>
		/// The node on which the request was made
		/// </summary>
		public Node Node { get; internal set; }

		/// <summary>
		/// The path of the request
		/// </summary>
		public string Path { get; internal set; }
		
		public override string ToString() => $"Node: {Node?.Uri}, NodeAlive: {Node?.IsAlive} ";
	}
	
	
	/// <summary>
	/// An audit of the request made
	/// </summary>
	public class Audit : DiagnosticAudit
	{
		public Audit(AuditEvent type, DateTime started)
		{
			Event = type;
			Started = started;
		}

		/// <summary>
		/// The end date and time of the audit
		/// </summary>
		public DateTime Ended { get; internal set; }

		/// <summary>
		/// The type of audit event
		/// </summary>
		public AuditEvent Event { get; internal set; }

		/// <summary>
		/// The start date and time of the audit
		/// </summary>
		public DateTime Started { get; }

		public override string ToString()
		{
			var took = Ended - Started;
			if (took < TimeSpan.Zero) took = TimeSpan.Zero;
			return $"Node: {Node?.Uri}, Event: {Event.GetStringValue()} NodeAlive: {Node?.IsAlive}, Took: {took}";
		}
	}
}

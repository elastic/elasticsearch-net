using System;

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
        /// The node on which the request was made
        /// </summary>
        public Node Node { get; internal set; }
        /// <summary>
        /// The path of the request
        /// </summary>
        public string Path { get; internal set; }
        /// <summary>
        /// The exception for the audit, if there was one.
        /// </summary>
        public Exception Exception { get; internal set; }

		public Audit(AuditEvent type, DateTime started)
		{
			this.Event = type;
			this.Started = started;
		}

		public override string ToString()
		{
			var took = Ended - Started;
			return $"Node: {Node?.Uri}, Event: {Event.GetStringValue()} NodeAlive: {Node?.IsAlive}, Took: {took}";
		}
	}
}

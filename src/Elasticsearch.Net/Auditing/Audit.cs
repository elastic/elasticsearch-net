using System;

namespace Elasticsearch.Net_5_2_0
{
	public class Audit
	{
		public AuditEvent Event { get; internal set; }
		public DateTime Started { get; }
		public DateTime Ended { get; internal set; }
		public Node Node { get; internal set; }
		public string Path { get; internal set; }
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

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Elasticsearch.Net
{
	/// <summary>
	/// An audit of the request made
	/// </summary>
	public class Audit 
	{
		public Audit(AuditEvent type, DateTime started)
		{
			Event = type;
			Started = started;
		}
		
		/// <summary>
		/// The type of audit event
		/// </summary>
		public AuditEvent Event { get; internal set; }

		
		/// <summary>
		/// The node on which the request was made
		/// </summary>
		public Node Node { get; internal set; }

		/// <summary>
		/// The path of the request
		/// </summary>
		public string Path { get; internal set; }
		

		/// <summary>
		/// The end date and time of the audit
		/// </summary>
		public DateTime Ended { get; internal set; }

		/// <summary>
		/// The start date and time of the audit
		/// </summary>
		public DateTime Started { get; }
		
		/// <summary>
		/// The exception for the audit, if there was one.
		/// </summary>
		public Exception Exception { get; internal set; }

		public override string ToString()
		{
			var took = Ended - Started;
			var tookString = string.Empty;
			if (took >= TimeSpan.Zero) tookString = $" Took: {took}";
			
			return Node == null ? $"Event: {Event.GetStringValue()}{tookString}" : $"Event: {Event.GetStringValue()} Node: {Node?.Uri} NodeAlive: {Node?.IsAlive}Took: {tookString}";
		}
	}
}

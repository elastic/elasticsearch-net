/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

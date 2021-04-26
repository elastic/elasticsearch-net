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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(PagerDutyEvent))]
	public interface IPagerDutyEvent
	{
		[DataMember(Name = "account")]
		string Account { get; set; }

		[DataMember(Name = "attach_payload")]
		bool? AttachPayload { get; set; }

		[DataMember(Name = "client")]
		string Client { get; set; }

		[DataMember(Name = "client_url")]
		string ClientUrl { get; set; }

		[DataMember(Name = "context")]
		IEnumerable<IPagerDutyContext> Context { get; set; }

		[DataMember(Name = "description")]
		string Description { get; set; }

		[DataMember(Name = "event_type")]
		PagerDutyEventType? EventType { get; set; }

		[DataMember(Name = "incident_key")]
		string IncidentKey { get; set; }
	}

	public class PagerDutyEvent : IPagerDutyEvent
	{
		public string Account { get; set; }

		public bool? AttachPayload { get; set; }

		public string Client { get; set; }

		public string ClientUrl { get; set; }

		public IEnumerable<IPagerDutyContext> Context { get; set; }

		public string Description { get; set; }

		public PagerDutyEventType? EventType { get; set; }

		public string IncidentKey { get; set; }
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
